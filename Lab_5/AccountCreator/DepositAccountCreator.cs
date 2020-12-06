using System;

namespace Lab_5
{
    public class DepositAccountCreator : AccountCreator
    {
        public DateTime ExpirationDate { get; set; }

        public DepositAccountCreator(BankManager bankManager, DateTime expirationDate) : base(bankManager)
        {
            ExpirationDate = expirationDate;
        }

        public override Account CreateAccount(ulong bankId, ulong clientId, double initDeposit = 0)
        {
            var bank = BankManager.GetBankById(bankId);
            var client = bank.GetClientById(clientId);
            return new DepositAccount(client, bank, ExpirationDate, BankManager[bankId].CalculateDepositPercent(initDeposit));
        }
    }
}