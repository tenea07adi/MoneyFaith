using Abstractions.Calculator;
using Abstractions.Console;
using ConsoleApp.CalculationInteractionsStrategy;
using ConsoleApp.Constants;
using CoreCalculator.Calculations;

namespace ConsoleApp
{
    public class LifecycleConsole : ILifecycleConsole
    {
        private IPrettyConsole _prettyConsole;

        private Dictionary<string, ICalculation> _calculations= 
            new Dictionary<string, ICalculation>();

        private Dictionary<Type, ICalculationConsoleStrategy> _calculationsStrategy = 
            new Dictionary<Type, ICalculationConsoleStrategy>();

        public LifecycleConsole(IPrettyConsole prettyConsole)
        {
            _prettyConsole = prettyConsole;

            InitCalculations();
        }

        public void Run()
        {
            WriteInitMessage();

            while (true)
            {
                _prettyConsole.NextLine();

                ICalculation currentCalculation = AskForCalculation();
                ICalculationConsoleStrategy currentStrategy = GetCalculationStrategy(currentCalculation);

                var calcConfig = currentStrategy.ReadCalculationConfig();

                var result = currentCalculation.Calculate(calcConfig);

                _prettyConsole.NextLine();

                currentStrategy.DisplayCalculationResult(GetCalculationName(currentCalculation), calcConfig, result);

                var doNewCalc = _prettyConsole.AskData<string>("Do you want one more calculation?", ["Yes", "No"]);

                if (doNewCalc == "No")
                {
                    break;
                }

                var doCleanUp = _prettyConsole.AskData<string>("Do you want to cleanup the screen?", ["Yes", "No"]);

                if (doCleanUp == "Yes")
                {
                    _prettyConsole.Clean(); 
                    WriteInitMessage();
                }
            }
        }

        private void InitCalculations()
        {
            _calculations.Add("Simple stock calculation", new SimpleStockCalculation());
            _calculations.Add("Simple bond calculation", new SimpleBondCalculation());
            _calculations.Add("Stock calculation", new StockCalculation());

            _calculationsStrategy.Add(typeof(SimpleStockCalculation), new SimpleCalculationInteractionsStrategy(_prettyConsole));
            _calculationsStrategy.Add(typeof(SimpleBondCalculation), new SimpleCalculationInteractionsStrategy(_prettyConsole));
            _calculationsStrategy.Add(typeof(StockCalculation), new StockCalculationInteractionsStrategy(_prettyConsole));
        }

        private void WriteInitMessage()
        {
            _prettyConsole.WriteTitle(ConsoleMessageConstants.Title);
            _prettyConsole.Write(ConsoleMessageConstants.Description, true, PrettyColorsEnum.ImportantText);

            _prettyConsole.NextLine();
        }

        private ICalculation AskForCalculation()
        {
            var calculations = _calculations.Keys.ToArray();

            var selectedCalculation = _prettyConsole.SelectData("Select calculation:", calculations, PrettyColorsEnum.SimpleText, PrettyColorsEnum.Title);

            return _calculations[selectedCalculation];
        }

        private ICalculationConsoleStrategy GetCalculationStrategy(ICalculation calculation)
        {
            return _calculationsStrategy[calculation.GetType()];
        }

        private string GetCalculationName(ICalculation calculation)
        {
            return _calculations.FirstOrDefault(x => x.Value == calculation).Key;
        }
    }
}
