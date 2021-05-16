namespace Lab_5
{
    public abstract class AccountCreator
    {
        protected BankManager BankManager;
        
        public AccountCreator(BankManager bankManager)
        {
            BankManager = bankManager;
        }

        public abstract Account CreateAccount(ulong bankId, ulong clientId, double initDeposit = 0);
    }
}