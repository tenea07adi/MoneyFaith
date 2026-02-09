using Abstractions.DTOs;

namespace Abstractions.Calculator
{
    public interface ICalculation
    {
        public CalculationResultDTO Calculate(CalculationConfigDTO config);
    }
}
