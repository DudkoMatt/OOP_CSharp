using System;

namespace Lab_5
{
    public abstract class Transaction
    {
        private static ulong _nextTransactionId = 0;
        public readonly ulong TransactionId;
        public readonly double Money;
        public DateTime DateTime { get; private set; }
        public Account Account { get; private set; }

        protected Transaction(DateTime dateTime, Account account, double money)
        {
            TransactionId = _nextTransactionId++;
            DateTime = dateTime;
            Account = account;
            Money = money;
        }
        
        public abstract void Cancel();
    }
}