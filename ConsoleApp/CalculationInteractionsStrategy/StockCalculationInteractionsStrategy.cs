using Abstractions.Console;
using Abstractions.DTOs;
using Abstractions.DTOs.SpecificCalculation;

namespace ConsoleApp.CalculationInteractionsStrategy
{
    public class StockCalculationInteractionsStrategy : BaseCalculationConsoleStrategy<StockCalculationConfigDTO, CalculationResultDTO>
    {
        private readonly IPrettyConsole _prettyConsole;

        public StockCalculationInteractionsStrategy(IPrettyConsole prettyConsole)
        {
            _prettyConsole = prettyConsole;
        }

        protected override StockCalculationConfigDTO ReadSpecificCalculationConfig()
        {
            var calcConig = new StockCalculationConfigDTO();

            calcConig.DurationInMonths = _prettyConsole.ReadData<int>("Duration in months:");
            calcConig.AnnualInterest = _prettyConsole.ReadData<decimal>("Anual interest in procents:");
            calcConig.InitialValue = _prettyConsole.ReadData<decimal>("Initial investment:");
            calcConig.RecursiveInvestment = _prettyConsole.ReadData<decimal>("Recursive investment:");
            calcConig.RecursiveInvestmentFrequencyInMonths = _prettyConsole.ReadData<int>("Recursive investment interval in months:");

            return calcConig;
        }

        protected override void DisplaySpecificCalculationResult(string calculationName, StockCalculationConfigDTO config, CalculationResultDTO result)
        {
            _prettyConsole.NextLine();

            _prettyConsole.WriteAsPannel(
                [
                    $"Calculation: {calculationName}",
                        $"Duration in months: {config.DurationInMonths.ToString()}",
                        $"Anual interest in procents: {config.AnnualInterest.ToString()}",
                        $"Initial investment: {config.InitialValue.ToString()}",
                        $"Recursive investment: {config.RecursiveInvestment.ToString()}",
                        $"Recursive investment frequency in months: {config.RecursiveInvestmentFrequencyInMonths.ToString()}",
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
        }
    }
}
