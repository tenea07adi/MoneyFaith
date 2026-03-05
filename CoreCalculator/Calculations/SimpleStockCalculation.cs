using Abstractions.DTOs;
using CoreCalculator.Constants;

namespace CoreCalculator.Calculations
{
    public class SimpleStockCalculation : BaseCalculation<CalculationConfigDTO, CalculationResultDTO>
    {
        protected override CalculationResultDTO CalculateSpecific(CalculationConfigDTO config)
        {
            decimal total = config.InitialValue;
            decimal profit = 0;
            var monthsValues = new List<CalculationMonthlyResultDTO>();

            decimal interestDecimal = config.AnnualInterest / 100m;

            var years = config.DurationInMonths / CalculationConstants.YearMonths;
            var remainingMonths = config.DurationInMonths % CalculationConstants.YearMonths;

            for (int i = 1; i <= years; i++)
            {
                total += total * interestDecimal;

                monthsValues.Add(new CalculationMonthlyResultDTO()
                {
                    Month = i * CalculationConstants.YearMonths,
                    Value = Math.Round(total, 2)
                });
            }

            if (remainingMonths > 0)
            {
                total += total * interestDecimal / CalculationConstants.YearMonths * remainingMonths;

                monthsValues.Add(new CalculationMonthlyResultDTO()
                {
                    Month = years * CalculationConstants.YearMonths + remainingMonths,
                    Value = Math.Round(total, 2)
                });
            }

            profit = Math.Round(total - config.InitialValue, 2);

            return new CalculationResultDTO
            {
                FinalValue = Math.Round(total, 2),
                FinalProfit = profit,
                TotalInvestedValue = Math.Round(config.InitialValue, 2),
                ProcentualFinalProfit = Math.Round(profit / config.InitialValue * 100, 2),
                MonthlyResults = monthsValues
            };
        }
    }
}