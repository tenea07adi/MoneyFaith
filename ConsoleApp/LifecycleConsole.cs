using Abstractions.Calculator;
using Abstractions.Console;
using Abstractions.DTOs;
using ConsoleApp.Constants;
using CoreCalculator.Calculations;

namespace ConsoleApp
{
    public class LifecycleConsole : ILifecycleConsole
    {
        private IPrettyConsole _prettyConsole;

        private Dictionary<string, ICalculation> _calculations= new Dictionary<string, ICalculation>();

        public LifecycleConsole(IPrettyConsole prettyConsole)
        {
            _prettyConsole = prettyConsole;

            InitCalculations();
        }

        public void Restart()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            WriteInitMessage();

            while (true)
            {
                _prettyConsole.NextLine();

                ICalculation currentCalculation = AskForCalculation();

                var calcConig = new CalculationConfigDTO();

                calcConig.DurationInMonths = _prettyConsole.ReadData<int>("Duration in months:");
                calcConig.AnnualInterest = _prettyConsole.ReadData<decimal>("Anual interest in procents:");
                calcConig.InitialValue = _prettyConsole.ReadData<decimal>("Initial investment:");

                var result = currentCalculation.Calculate(calcConig);

                _prettyConsole.NextLine();
                
                _prettyConsole.WriteAsPannel(
                    [
                        $"Calculation: {GetCalculationName(currentCalculation)}",
                        $"Duration in months: {calcConig.DurationInMonths.ToString()}",
                        $"Anual interest in procents: {calcConig.AnnualInterest.ToString()}",
                        $"Initial investment: {calcConig.InitialValue.ToString()}"
                    ],
                    "Calculation config",
                    PrettyColorsEnum.Title);

                _prettyConsole.WriteAsPannel(
                    [
                        $"Final amount: {result.FinalValue.ToString()}",
                        $"Final profit: {result.FinalProfit.ToString()}"
                    ],
                    "General results",
                    PrettyColorsEnum.Title);

                _prettyConsole.WriteTable(result.MonthlyResults, PrettyColorsEnum.Title);

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

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        private void InitCalculations()
        {
            _calculations.Add("Simple stock calculation", new SimpleStockCalculation());
            _calculations.Add("Simple bond calculation", new SimpleBondCalculation());
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

        private string GetCalculationName(ICalculation calculation)
        {
            return _calculations.FirstOrDefault(x => x.Value == calculation).Key;
        }
    }
}
