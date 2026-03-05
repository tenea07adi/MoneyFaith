namespace Abstractions.DTOs.SpecificCalculation
{
    public class BankDepositCalculationConfigDTO : CalculationConfigDTO
    {
        /// <summary>
        /// The percentage of tax on profit.
        /// </summary>
        public decimal TaxOnProfitRate { get; set; } = 0;
    }
}
