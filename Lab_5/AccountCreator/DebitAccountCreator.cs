namespace Lab_5
{
    public class DebitAccountCreator : AccountCreator
    {
        public double InterestPercent { get; set; }
        
        public DebitAccountCreator(BankManager bankManager, double interestPercent) : base(bankManager)
        {
            InterestPercent = interestPercent;
        }

        public override Account CreateAccount(ulong bankId, ulong clientId)
        {
            var bank = _bankManager.GetBankById(bankId);
            var client = bank.GetClientById(clientId);
            return new DebitAccount(client, bank, InterestPercent);
        }
    }
}