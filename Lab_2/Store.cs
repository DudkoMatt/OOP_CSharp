using System;
using System.Collections.Generic;

namespace Lab_2
{
    class Store
    {
        private static ulong _nextStoreId = 1000;
        public readonly ulong Id;
        public string Name;
        public string Address;

        // Products info
        private ProductStorage _storage;
        
        public Store(string name, string address)
        {
            Id = _nextStoreId++;
            Name = name;
            Address = address;
            _storage = new ProductStorage();
        }

        public void AddProduct(ulong id, ulong amount, ulong price)
        {
            _storage.AddProduct(id, amount, price);
        }

        public void SetProductPrice(ulong id, ulong price)
        {
            _storage.SetProductPrice(id, price);
        }

        public bool TryGetProductPrice(ulong id, out ulong price)
        {
            return _storage.TryGetProductPrice(id, out price);
        }

        public ulong GetProductPrice(ulong id)
        {
            return TryGetProductPrice(id, out var price) ? price : 0;
        }

        public bool TryBuyProduct(ulong id, ulong amount, out ulong price)
        {
            return _storage.TryBuyProduct(id, amount, out price);
        }
        
        public ulong BuyProduct(ulong id, ulong amount)
        {
            return _storage.TryBuyProduct(id, amount, out var price) ? price : 0;
        }
        
        public bool TryBuyProducts(List<KeyValuePair<ulong, ulong>> listOfProducts, out ulong price) // id - amount
        {
            price = 0;
            if (!CheckAvailability(listOfProducts)) return false;
            
            foreach (var (id, amount) in listOfProducts)
            {
                price += BuyProduct(id, amount);
            }

            return true;
        }

        public bool TryCalculateTotalCostOfProducts(List<KeyValuePair<ulong, ulong>> listOfProducts, out ulong price)
        {
            price = 0;
            if (!CheckAvailability(listOfProducts)) return false;

            foreach (var (productId, amount) in listOfProducts)
            {
                price += _storage.GetPriceOfProduct(productId) * amount;
            }
            
            return true;
        }

        public bool CheckAvailability(List<KeyValuePair<ulong, ulong>> listOfProducts)
        {
            foreach (var (productId, amount) in listOfProducts)
            {
                if (_storage.GetAmountOfProduct(productId) < amount)
                    return false;
            }
            return true;
        }
        
        public List<KeyValuePair<ulong, ulong>> ListOfProductsOnMaxPrice(ulong maxPrice, out ulong moneyRemaining)
        {
            var list = new List<KeyValuePair<ulong, ulong>>();
            foreach (var productId in _storage.GetListOfProducts())
            {
                if (_storage.GetPriceOfProduct(productId) == 0)
                    continue;
                
                var k = Math.Min(maxPrice / _storage.GetPriceOfProduct(productId), _storage.GetAmountOfProduct(productId));
                if (k == 0) continue;

                maxPrice -= _storage.GetPriceOfProduct(productId) * k;
                list.Add(new KeyValuePair<ulong, ulong>(productId, k));
            }

            moneyRemaining = maxPrice;
            return list;
        }
    }
}