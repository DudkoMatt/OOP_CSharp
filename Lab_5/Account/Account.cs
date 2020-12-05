namespace Lab_5
{
    public abstract class Account
    {
        public Client Client { get; }
        public Bank Bank { get; }
        public double InterestPercent { get; set; }
        protected double Money;
        
        protected Account(Client client, Bank bank, double interestPercent)
        {
            Client = client;
            Bank = bank;
            InterestPercent = interestPercent;
        }

        protected bool CheckClientData() => Client.CheckProfile();

        public void Put(double money) => Money += money;

        public virtual void Withdraw(double money)
        {
            if (!CheckClientData() && money > Bank.SuspiciousAccountLimit) throw new SuspiciousAccountLimitException();
            if (money > Money) throw new NotEnoughMoneyException();
            Money -= money;
        }

        /// <summary>
        /// Метод, который должен вызываться только для отмены транзакций
        /// </summary>
        /// <param name="money"></param>
        public void Remove(double money) => Money -= money;
    }
}