namespace Lab_5
{
    public static class FastForwardTime
    {
        public static void FastForward(Account account, ulong daysToForward)
        {
            for (var i = 0ul; i < daysToForward; i++)
            {
                account.CalculateDailyAccumulation();
            }
            
            account.AddMonthAccumulation();
        }
    }
}