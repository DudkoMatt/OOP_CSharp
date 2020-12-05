namespace Lab_5
{
    public abstract class AccountCreator
    {
        protected BankManager _bankManager;
        
        public AccountCreator(BankManager bankManager)
        {
            _bankManager = bankManager;
        }

        public abstract Account CreateAccount(ulong bankId, ulong clientId, double initDeposit = 0);
    }
}