using System.Collections.Generic;

namespace Lab_2
{
    class ProductStorage
    {
        private Dictionary<ulong, ulong> _productsAmount;  // id - amount
        private Dictionary<ulong, ulong> _productsPrice;   // id - price

        public ProductStorage()
        {
            _productsAmount = new Dictionary<ulong, ulong>();
            _productsPrice  = new Dictionary<ulong, ulong>();
        }

        public void AddProduct(ulong id, ulong amount, ulong price)
        {
            SetProductPrice(id, price);
            if (_productsAmount.ContainsKey(id))
                _productsAmount[id] += amount;
            else
                _productsAmount[id] = amount;
        }

        public void SetProductPrice(ulong id, ulong price) => _productsPrice[id] = price;
        
        public bool TryGetProductPrice(ulong id, out ulong price)
        {
            price = 0;
            if (!_productsPrice.ContainsKey(id)) return false;
            price = _productsPrice[id];
            return true;

        }

        public ulong GetAmountOfProduct(ulong id) => _productsAmount.ContainsKey(id) ? _productsAmount[id] : 0;

        public IEnumerable<ulong> GetListOfProducts() => _productsAmount.Keys;

        public ulong GetPriceOfProduct(ulong id) => _productsPrice.ContainsKey(id) ? _productsPrice[id] : 0;

        public bool TryBuyProduct(ulong id, ulong amount, out ulong price)
        {
            price = 0;
            if (!_productsAmount.ContainsKey(id) || amount > _productsAmount[id]) return false;
            price = _productsPrice[id] * amount;
            _productsAmount[id] -= amount;
            return true;
        }
        
        public void BuyProduct(ulong id, ulong amount, out ulong price)
        {
            price = _productsPrice[id] * amount;
            _productsAmount[id] -= amount;
        }

        // public void RemoveAllProduct(ulong id)
        // {
        //     if (_productsAmount.ContainsKey(id))
        //         _productsAmount.Remove(id);
        // }
    }
}