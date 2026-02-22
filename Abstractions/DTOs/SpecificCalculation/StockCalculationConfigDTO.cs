namespace Abstractions.DTOs.SpecificCalculation
{
    public class StockCalculationConfigDTO : CalculationConfigDTO
    {
        /// <summary>
        /// The amount of money invested periodically
        /// </summary>
        public decimal RecursiveInvestment { get; set; } = 0;

        /// <summary>
        /// The frequency of the periodic investment in months.
        /// </summary>
        public int RecursiveInvestmentFrequencyInMonths { get; set; } = 0;
    }
}
