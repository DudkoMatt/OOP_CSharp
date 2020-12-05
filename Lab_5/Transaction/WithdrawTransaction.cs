using System;

namespace Lab_5
{
    public class WithdrawTransaction : Transaction
    {
        public WithdrawTransaction(DateTime dateTime, Account account, double money) : base(dateTime, account, money)
        {
            Account.Withdraw(Money + Account.Bank.WithdrawCommission);
        }

        protected override void _cancel()
        {
            Account.Put(Money);
        }
    }
}