using Abstractions.Calculator;
using Abstractions.DTOs;

namespace CoreCalculator.Calculations
{
    public abstract class BaseCalculation<TConfig, TResult> : ICalculation
        where TConfig : CalculationConfigDTO
        where TResult : CalculationResultDTO
    {
        public CalculationResultDTO Calculate(CalculationConfigDTO config)
        {
            return CalculateSpecific((TConfig)config);
        }

        protected abstract TResult CalculateSpecific(TConfig config);
    }
}
