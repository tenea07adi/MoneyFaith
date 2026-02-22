using Abstractions.DTOs;

namespace ConsoleApp.Interfaces
{
    public interface ICalculationInteractionsStrategy
    {
        public CalculationConfigDTO ReadCalculationConfigDTO();
        public void WriteCalculationResult(string calculationName, CalculationConfigDTO config, CalculationResultDTO result);
    }
}
