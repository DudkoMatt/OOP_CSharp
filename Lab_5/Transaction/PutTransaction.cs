using System;

namespace Lab_5
{
    public class PutTransaction : Transaction
    {
        public PutTransaction(DateTime dateTime, Account account, double money) : base(dateTime, account, money)
        {
            Account.Put(Money);
        }

        protected override void _cancel()
        {
            Account.Remove(Money);
        }
    }
}