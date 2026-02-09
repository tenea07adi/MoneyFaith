namespace Abstractions.DTOs
{
    public class CalculationConfigDTO
    {
        /// <summary>
        /// Duration of calculation in months
        /// </summary>
        public int DurationInMonths {  get; set; }

        /// <summary>
        /// The annual interest
        /// </summary>
        public decimal AnnualInterest { get; set; }

        /// <summary>
        /// Tthe amount of money taken into account at the beginning of the calculation period
        /// </summary>
        public decimal InitialValue { get; set; }
    }
}
