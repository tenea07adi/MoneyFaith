using Abstractions.DTOs;
using Abstractions.DTOs.SpecificCalculation;
using CoreCalculator.Constants;

namespace CoreCalculator.Calculations
{
    public class BankDepositCalculation : BaseCalculation<BankDepositCalculationConfigDTO, BankDepositCalculationResultDTO>
    {
        protected override BankDepositCalculationResultDTO CalculateSpecific(BankDepositCalculationConfigDTO config)
        {
            decimal total = config.InitialValue;
            decimal profit = 0;
            decimal totalTaxOnProfitPaid = 0;
            var monthsValues = new List<CalculationMonthlyResultDTO>();

            decimal interestDecimal = config.AnnualInterest / 100m;
            decimal taxOnProfitDecimal = config.TaxOnProfitRate / 100m;

            int totalPeriodInDays = config.DurationInMonths * CalculationConstants.GeneralMonthDays;
            totalPeriodInDays += totalPeriodInDays / CalculationConstants.YearDaysByGeneralMonthDays * 5;

            int monthlyDaysMilestone = totalPeriodInDays / config.DurationInMonths;

            for (int i = 1; i <= totalPeriodInDays; i++)
            {
                var dailyInterest = interestDecimal / CalculationConstants.GeneralYearDays;
                var dailyProfit = total * dailyInterest;

                total += dailyProfit;
                profit += dailyProfit;

                if(i % monthlyDaysMilestone == 0)
                {
                    monthsValues.Add(
                        new CalculationMonthlyResultDTO
                        {
                            Month = i / monthlyDaysMilestone,
                            Value = Math.Round(total, 2)
                        });
                }
            }

            totalTaxOnProfitPaid = profit * taxOnProfitDecimal;
            profit = profit - totalTaxOnProfitPaid;
            total -= totalTaxOnProfitPaid;

            return new BankDepositCalculationResultDTO
            {
                FinalValue = Math.Round(total, 2),
                FinalProfit = Math.Round(profit, 2),
                TotalTaxOnProfitPaid = Math.Round(totalTaxOnProfitPaid, 2),
                TotalInvestedValue = config.InitialValue,
                ProcentualFinalProfit = Math.Round(profit / config.InitialValue * 100, 2),
                MonthlyResults = monthsValues
            };
        }
    }
}
