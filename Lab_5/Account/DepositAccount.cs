using System;

namespace Lab_5
{
    public class DepositAccount : Account
    {
        private DateTime _expirationDate;
        
        public DepositAccount(Client client, Bank bank, DateTime expirationDate, double interestPercent, double initDeposit = 0) : base(client, bank, interestPercent, initDeposit)
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