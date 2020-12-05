using System;

namespace Lab_5
{
    public static class DateTimeProvider
    {
        public static bool UseCustomDate = false;
        public static DateTime CustomDate = new DateTime(2020, 1, 1);

        public static DateTime Now
        {
            get
            {
                if (UseCustomDate)
                    return CustomDate;
            
                return DateTime.Now;   
            }
        }
    }
}