using System;

namespace Lab_4
{
    // Заглушка для тестирования очистки по времени
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