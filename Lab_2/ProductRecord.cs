namespace Lab_2
{
    public class ProductRecord
    {
        public ulong ProductId { get; }
        public ulong Amount { get; }

        public ProductRecord(ulong product, ulong amount)
        {
            ProductId = product;
            Amount = amount;
        }
    }
}