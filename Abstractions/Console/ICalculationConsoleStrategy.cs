using Abstractions.DTOs;

namespace Abstractions.Console
{

    public interface ICalculationConsoleStrategy
    {
        public CalculationConfigDTO ReadCalculationConfig();
        public void DisplayCalculationResult(string calculationName, CalculationConfigDTO config, CalculationResultDTO result);
    }
}
