using System;

namespace Lab_5
{
    public class DepositAccount : Account
    {
        private DateTime _expirationDate;
        
        public DepositAccount(Client client, Bank bank, DateTime expirationDate, double interestPercent) : base(client, bank, interestPercent)
        {
            _expirationDate = expirationDate;
        }

        public override void Withdraw(double money)
        {
            if (DateTimeProvider.Now < _expirationDate)
                throw new DepositAccountNotExpiredYetException();
            base.Withdraw(money);
        }
    }
}