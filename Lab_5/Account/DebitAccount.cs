namespace Lab_5
{
    public class DebitAccount : Account
    {
        public DebitAccount(Client client, Bank bank, double interestPercent, double initDeposit = 0) : base(client, bank, interestPercent, initDeposit)
        { }
    }
}