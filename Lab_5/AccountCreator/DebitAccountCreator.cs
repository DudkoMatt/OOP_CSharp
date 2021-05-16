namespace Lab_5
{
    public class DebitAccountCreator : AccountCreator
    {
        public double InterestPercent { get; set; }
        
        public DebitAccountCreator(BankManager bankManager, double interestPercent) : base(bankManager)
        {
            InterestPercent = interestPercent;
        }

        public override Account CreateAccount(ulong bankId, ulong clientId, double initDeposit = 0)
        {
            var bank = BankManager.GetBankById(bankId);
            var client = bank.GetClientById(clientId);
            return new DebitAccount(client, bank, InterestPercent);
        }
    }
}