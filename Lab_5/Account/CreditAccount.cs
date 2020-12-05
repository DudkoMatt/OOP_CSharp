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
            throw new System.NotImplementedException();
        }
    }
}