using System;

namespace Lab_5
{
    public class WithdrawTransaction : Transaction
    {
        public WithdrawTransaction(DateTime dateTime, Account account, double money) : base(dateTime, account, money)
        {
            Account.Withdraw(Money);
        }

        public override void Cancel()
        {
            Account.Put(Money);
        }
    }
}