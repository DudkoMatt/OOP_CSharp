namespace Lab_5
{
    public class CreditAccount : Account
    {
        public double OverdraftLimit;
        
        public CreditAccount(Client client, Bank bank, double overdraftLimit, double initDeposit = 0) : base(client, bank, 0, initDeposit)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(double money)
        {
            if (!CheckClientData() && money > Bank.SuspiciousAccountLimit) throw new SuspiciousAccountLimitException();
            if (money > Money + OverdraftLimit) throw new NotEnoughMoneyException();
            if (money < 0) money += Bank.WithdrawCommission;
            Money -= money;
        }
    }
}