namespace Lab_5
{
    public class DebitAccount : Account
    {
        public DebitAccount(Client client, Bank bank, double interestPercent) : base(client, bank, interestPercent)
        { }
    }
}