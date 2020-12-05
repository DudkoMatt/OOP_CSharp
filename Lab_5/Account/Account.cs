namespace Lab_5
{
    public abstract class Account
    {
        public Client Client { get; }
        public Bank Bank { get; }
        protected double Money;
        
        protected Account(Client client, Bank bank)
        {
            Client = client;
            Bank = bank;
        }

        protected bool CheckClientData() => Client.CheckProfile();

        public abstract void Withdraw(double money);

        public void Put(double money) => Money += money;
        
        /// <summary>
        /// Метод, который должен вызываться только для отмены транзакций
        /// </summary>
        /// <param name="money"></param>
        public void Remove(double money) => Money -= money;
    }
}