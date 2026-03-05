namespace CoreCalculator.Constants
{
    public static class CalculationConstants
    {
        public static int YearMonths { get; } = 12;
        public static int GeneralYearDays { get; } = 365;
        public static int GeneralMonthDays { get; } = 30;
        public static int YearDaysByGeneralMonthDays { get; } = GeneralMonthDays * YearMonths;
    }
}
