using Abstractions.DTOs;
using Abstractions.DTOs.SpecificCalculation;

namespace CoreCalculator.Calculations
{
    public class StockCalculation : BaseCalculation<StockCalculationConfigDTO, CalculationResultDTO>
    {
        protected override CalculationResultDTO CalculateSpecific(StockCalculationConfigDTO config)
        {
            decimal total = config.InitialValue;
            decimal totalInvested = config.InitialValue;
            decimal profit = 0;
            var monthsValues = new List<CalculationMonthlyResultDTO>();

            decimal interestDecimal = config.AnnualInterest / 100m;

            for (int i = 1; i <= config.DurationInMonths; i++)
            {
                total += total * interestDecimal / 12;

                if (config.RecursiveInvestmentFrequencyInMonths != 0 && i % config.RecursiveInvestmentFrequencyInMonths == 0)
                {
                    total += config.RecursiveInvestment;
                    totalInvested += config.RecursiveInvestment;
                }

                if (i % 12 == 0 || i == config.DurationInMonths)
                {
                    monthsValues.Add(new CalculationMonthlyResultDTO()
                    {
                        Month = i,
                        Value = Math.Round(total, 2)
                    });
                }
            }

            profit = Math.Round(total - totalInvested, 2);

            return new CalculationResultDTO
            {
                FinalValue = Math.Round(total, 2),
                FinalProfit = profit,
                TotalInvestedValue = Math.Round(totalInvested, 2),
                ProcentualFinalProfit = Math.Round(profit / totalInvested * 100, 2),
                MonthlyResults = monthsValues
            };
        }
    }
}
