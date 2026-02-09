namespace Abstractions.DTOs
{
    public class CalculationResultDTO
    {
        public List<CalculationMonthlyResultDTO> MonthlyResults { get; set; } = new List<CalculationMonthlyResultDTO>();
        public decimal FinalValue;
        public decimal FinalProfit;
    }
}
