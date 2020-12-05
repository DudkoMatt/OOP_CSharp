using System;

namespace Lab_5
{
    public class TransferTransaction : Transaction
    {
        public Account AccountTo { get; private set; }

        public TransferTransaction(DateTime dateTime, Account account, double money, Account accountTo) : base(dateTime,
            account, money)
        {
            AccountTo = accountTo;

            Account.Withdraw(Money);
            AccountTo.Put(Money);
        }

        public override void Cancel()
        {
            AccountTo.Remove(Money);
            Account.Put(Money);
        }
    }
}