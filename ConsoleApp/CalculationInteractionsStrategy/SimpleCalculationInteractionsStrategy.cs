using Abstractions.Console;
using Abstractions.DTOs;

namespace ConsoleApp.CalculationInteractionsStrategy
{
    public class SimpleCalculationInteractionsStrategy : BaseCalculationConsoleStrategy<CalculationConfigDTO, CalculationResultDTO>
    {
        private readonly IPrettyConsole _prettyConsole;

        public SimpleCalculationInteractionsStrategy(IPrettyConsole prettyConsole)
        {
            _prettyConsole = prettyConsole;
        }

        protected override CalculationConfigDTO ReadSpecificCalculationConfig()
        {
            var calcConig = new CalculationConfigDTO();

            calcConig.DurationInMonths = _prettyConsole.ReadData<int>("Duration in months:");
            calcConig.AnnualInterest = _prettyConsole.ReadData<decimal>("Anual interest in procents:");
            calcConig.InitialValue = _prettyConsole.ReadData<decimal>("Initial investment:");

            return calcConig;
        }

        protected override void DisplaySpecificCalculationResult(string calculationName, CalculationConfigDTO config, CalculationResultDTO result)
        {
            _prettyConsole.NextLine();

            _prettyConsole.WriteAsPannel(
                [
                    $"Calculation: {calculationName}",
                    $"Duration in months: {config.DurationInMonths.ToString()}",
                    $"Anual interest in procents: {config.AnnualInterest.ToString()}",
                    $"Initial investment: {config.InitialValue.ToString()}"
                ],
                "Calculation config",
                PrettyColorsEnum.Title);

            _prettyConsole.WriteAsPannel(
                [
                    $"Final amount: {result.FinalValue.ToString()}",
                    $"Total Invested: {result.TotalInvestedValue.ToString()}",
                    $"Final profit: {result.FinalProfit.ToString()}",
                    $"Procentual final profit: {result.ProcentualFinalProfit.ToString()}%",
                ],
                "General results",
                PrettyColorsEnum.Title);

            _prettyConsole.WriteTable(result.MonthlyResults, PrettyColorsEnum.Title);
        }
    }
}
