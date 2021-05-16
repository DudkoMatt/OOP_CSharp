namespace Lab_5
{
    public class CreditAccountCreator : AccountCreator
    {
        public double OverdraftLimit { get; set; }
        
        public CreditAccountCreator(BankManager bankManager, double overdraftLimit) : base(bankManager)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override Account CreateAccount(ulong bankId, ulong clientId, double initDeposit = 0)
        {
            var bank = BankManager.GetBankById(bankId);
            var client = bank.GetClientById(clientId);
            return new CreditAccount(client, bank, OverdraftLimit);
        }
    }
}