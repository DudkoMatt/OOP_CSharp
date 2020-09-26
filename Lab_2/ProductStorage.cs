using System.Collections.Generic;

namespace Lab_2
{
    public class ProductStorage
    {
        private Dictionary<ulong, ulong> _productsAmount;  // id - amount
        private Dictionary<ulong, ulong> _productsPrice;   // id - price

        public ProductStorage()
        {
            _productsAmount = new Dictionary<ulong, ulong>();
            _productsPrice  = new Dictionary<ulong, ulong>();
        }

        public void AddProduct(ProductRecord record, ulong price)
        {
            SetProductPrice(record.ProductId, price);
            AddProduct(record);
        }  // noexcept
        
        public void AddProduct(ProductRecord record)
        {
            if (_productsAmount.ContainsKey(record.ProductId))
                _productsAmount[record.ProductId] += record.Amount;
            else
                _productsAmount[record.ProductId] = record.Amount;
        }  // noexcept

        public void SetProductPrice(ulong id, ulong price) => _productsPrice[id] = price;  // noexcept
        
        public ulong GetProductPrice(ulong id)
        {
            if (!_productsPrice.ContainsKey(id)) throw new ProductPriceNotSetException();
            return _productsPrice[id];
        }  // ProductPriceNotSetException
        
        public ulong GetProductAmount(ulong id)
        {
            if (!_productsAmount.ContainsKey(id)) throw new ProductAmountNotSetException();
            return _productsAmount[id];
        }  // ProductAmountNotSetException

        public IEnumerable<ulong> GetProductsList() => _productsAmount.Keys;  // noexcept
        
        public void BuyProduct(ProductRecord record, out ulong price)
        {
            if (!_productsAmount.ContainsKey(record.ProductId))
                throw new ProductAmountNotSetException();    
            if (record.Amount > _productsAmount[record.ProductId])
                throw new ProductAmountNotEnoughException();
            
            price = _productsPrice[record.ProductId] * record.Amount;
            _productsAmount[record.ProductId] -= record.Amount;
        }  // ProductAmountNotSetException, ProductAmountNotEnoughException
    }
}