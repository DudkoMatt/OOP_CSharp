namespace Lab_5
{
    public class CreditAccount : Account
    {
        public double OverdraftLimit;
        
        public CreditAccount(Client client, Bank bank, double overdraftLimit) : base(client, bank)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(double money)
        {
            if (!CheckClientData() && money > Bank.SuspiciousAccountLimit) throw new SuspiciousAccountLimitException();
            if (money > Money + OverdraftLimit) throw new NotEnoughMoneyException();
            Money -= money;
        }
    }
}