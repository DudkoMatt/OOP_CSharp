namespace Lab_5
{
    public class ConcreteDepositAccountPercentStrategy : IDepositAccountPercentStrategy
    {
        public double Calculate(double money)
        {
            if (money < 50_000) return 3;
            if (money < 100_000) return 3.5;
            return 4;
        }
    }
}