using Abstractions.Console;
using Abstractions.DTOs;

namespace ConsoleApp.CalculationInteractionsStrategy
{
    public abstract class BaseCalculationConsoleStrategy <TConfig, TResult> : ICalculationConsoleStrategy
        where TConfig : CalculationConfigDTO
        where TResult : CalculationResultDTO
    {
        public void DisplayCalculationResult(string calculationName, CalculationConfigDTO config, CalculationResultDTO result)
        {
            DisplaySpecificCalculationResult(calculationName, (TConfig)config, (TResult)result);
        }

        public CalculationConfigDTO ReadCalculationConfig()
        {
            return ReadSpecificCalculationConfig();
        }

        protected abstract void DisplaySpecificCalculationResult(string calculationName, TConfig config, TResult result);

        protected abstract TConfig ReadSpecificCalculationConfig();
    }
}
