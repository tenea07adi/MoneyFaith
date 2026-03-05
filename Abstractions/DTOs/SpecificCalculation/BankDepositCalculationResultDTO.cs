namespace Abstractions.DTOs.SpecificCalculation
{
    public class BankDepositCalculationResultDTO : CalculationResultDTO
    {
        /// <summary>
        /// Total tax on profit paid during the whole period of the deposit.
        /// </summary>
        public decimal TotalTaxOnProfitPaid { get; set; }
    }
}
