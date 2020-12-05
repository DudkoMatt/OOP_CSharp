using System;

namespace Lab_5
{
    public class DepositAccountCreator : AccountCreator
    {
        public DateTime ExpirationDate { get; set; }
        public double InterestPercent { get; set; }
        
        public DepositAccountCreator(BankManager bankManager, DateTime expirationDate, double interestPercent) : base(bankManager)
        {
            ExpirationDate = expirationDate;
            InterestPercent = interestPercent;
        }

        public override Account CreateAccount(ulong bankId, ulong clientId)
        {
            var bank = _bankManager.GetBankById(bankId);
            var client = bank.GetClientById(clientId);
            return new DepositAccount(client, bank, ExpirationDate, InterestPercent);
        }
    }
}