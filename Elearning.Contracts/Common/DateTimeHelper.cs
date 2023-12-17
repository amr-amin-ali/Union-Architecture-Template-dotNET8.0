﻿namespace Elearning.Contracts.Common
{
    using System.Globalization;

    public sealed class DateTimeHelper
    {
        private static readonly Lazy<DateTimeHelper> _lazy = new Lazy<DateTimeHelper>(() => new DateTimeHelper());
        public static DateTimeHelper HelperInstance => _lazy.Value;

        private DateTimeHelper()
        {

        }

        /// <summary>
        /// kindly refer to this answer https://stackoverflow.com/a/9064954
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        public string FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if ( firstWeek == 1 )
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            var response = result.AddDays(-3);

            return $"{response.Year}-{response.Month}-{response.Day}";
        }
    }
}
