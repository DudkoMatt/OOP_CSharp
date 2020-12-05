namespace Lab_5
{
    public class DebitAccountCreator : AccountCreator
    {
        public DebitAccountCreator(BankManager bankManager) : base(bankManager)
        {
        }

        public override Account CreateAccount(ulong bankId, ulong clientId)
        {
            var bank = _bankManager.GetBankById(bankId);
            var client = bank.GetClientById(clientId);
            return new DebitAccount(client, bank);
        }
    }
}