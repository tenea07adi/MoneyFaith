using Abstractions.Console;
using Abstractions.DTOs.SpecificCalculation;

namespace ConsoleApp.CalculationInteractionsStrategy
{
    public class BankDepositInteractionsStrategy : BaseCalculationConsoleStrategy<BankDepositCalculationConfigDTO, BankDepositCalculationResultDTO>
    {
        private readonly IPrettyConsole _prettyConsole;

        public BankDepositInteractionsStrategy(IPrettyConsole prettyConsole)
        {
            _prettyConsole = prettyConsole;
        }

        protected override BankDepositCalculationConfigDTO ReadSpecificCalculationConfig()
        {
            var calcConfig = new BankDepositCalculationConfigDTO();

            calcConfig.DurationInMonths = _prettyConsole.ReadData<int>("Duration in months:");
            calcConfig.AnnualInterest = _prettyConsole.ReadData<decimal>("Anual interest in procents:");
            calcConfig.InitialValue = _prettyConsole.ReadData<decimal>("Initial investment:");
            calcConfig.TaxOnProfitRate = _prettyConsole.ReadData<decimal>("Tax on profit rate in procents:");

            return calcConfig;
        }

        protected override void DisplaySpecificCalculationResult(string calculationName, BankDepositCalculationConfigDTO config, BankDepositCalculationResultDTO result)
        {
            _prettyConsole.NextLine();

            _prettyConsole.WriteAsPannel(
                [
                    $"Calculation: {calculationName}",
                    $"Duration in months: {config.DurationInMonths.ToString()}",
                    $"Anual interest in procents: {config.AnnualInterest.ToString()}",
                    $"Initial investment: {config.InitialValue.ToString()}",
                    $"Tax on profit rate in procents: {config.TaxOnProfitRate.ToString()}",
                ],
                "Calculation config",
                PrettyColorsEnum.Title);

            _prettyConsole.WriteAsPannel(
                [
                    $"Final amount (after tax): {result.FinalValue.ToString()}",
                    $"Total Invested: {result.TotalInvestedValue.ToString()}",
                    $"Final profit (after tax): {result.FinalProfit.ToString()}",
                    $"Procentual final profit (after tax): {result.ProcentualFinalProfit.ToString()}%",
                    $"Total tax on profit paid: {result.TotalTaxOnProfitPaid.ToString()}"
                ],
                "General results",
                PrettyColorsEnum.Title);

            _prettyConsole.WriteTable(result.MonthlyResults, PrettyColorsEnum.Title);
        }
    }
}
