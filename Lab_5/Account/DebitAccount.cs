namespace Lab_5
{
    public class DebitAccount : Account
    {
        public DebitAccount(Client client, Bank bank) : base(client, bank)
        { }

        public override void Withdraw(double money)
        {
            if (money > Money) throw new NotEnoughMoneyException();
            Money -= money;
        }
    }
}