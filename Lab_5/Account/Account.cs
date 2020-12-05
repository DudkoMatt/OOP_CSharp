namespace Lab_5
{
    public abstract class Account
    {
        public Client Client { get; }
        public Bank Bank { get; }
        public double InterestPercent { get; set; }  // проценты в годовых
        public double Accumulation { get; protected set; }  // накопления
        public double Money { get; protected set; }
        
        protected Account(Client client, Bank bank, double interestPercent, double initDeposit = 0)
        {
            Client = client;
            Bank = bank;
            InterestPercent = interestPercent;
            Accumulation = 0;
            Money = initDeposit;
        }

        protected bool CheckClientData() => Client.CheckProfile();

        public void Put(double money) => Money += money;

        public virtual void Withdraw(double money)
        {
            if (!CheckClientData() && money > Bank.SuspiciousAccountLimit) throw new SuspiciousAccountLimitException();
            if (money > Money) throw new NotEnoughMoneyException();
            Money -= money;
        }

        public void CalculateDailyAccumulation()
        {
            Accumulation += Money * InterestPercent / 36500;
        }

        public void AddMonthAccumulation()
        {
            Money += Accumulation;
            Accumulation = 0;
        }

        /// <summary>
        /// Метод, который должен вызываться только для отмены транзакций
        /// </summary>
        /// <param name="money"></param>
        public void Remove(double money) => Money -= money;
    }
}