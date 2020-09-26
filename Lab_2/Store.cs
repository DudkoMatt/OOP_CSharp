using System;
using System.Collections.Generic;

namespace Lab_2
{
    public class Store
    {
        private static ulong _nextStoreId = 1000;
        public readonly ulong Id;
        public readonly string Name;
        public readonly string Address;

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
            _storage.AddProduct(new ProductRecord(id, amount), price);
        }  // noexcept
        
        public void AddProduct(ProductRecord record)
        {
            _storage.AddProduct(record);
        }  // noexcept
        
        public void AddProduct(ProductRecord record, ulong price)
        {
            _storage.AddProduct(record, price);
        }  // noexcept

        public void SetProductPrice(ulong id, ulong price)
        {
            _storage.SetProductPrice(id, price);
        }  // noexcept

        public ulong GetProductPrice(ulong id)
        {
            return _storage.GetProductPrice(id);
        }  // ProductPriceNotSetException
        
        public ulong BuyProduct(ProductRecord record)
        {
            _storage.BuyProduct(record, out var price);
            return price;
        }  // ProductAmountNotSetException, ProductAmountNotEnoughException
        
        public void CheckAvailability(List<ProductRecord> listOfProducts)
        {
            foreach (var record in listOfProducts)
            {
                _storage.GetProductPrice(record.ProductId);  // ProductPriceNotSetException
                if (_storage.GetProductAmount(record.ProductId) < record.Amount)  // ProductAmountNotSetException
                {
                    throw new ProductAmountNotEnoughException();
                }
            }
        }  // ProductPriceNotSetException, ProductAmountNotSetException, ProductAmountNotEnoughException
        
        public void BuyProductList(List<ProductRecord> listOfProducts, out ulong price)
        {
            price = 0;
            CheckAvailability(listOfProducts);
            
            foreach (var record in listOfProducts)
            {
                price += BuyProduct(record);
            }
        }  // ProductPriceNotSetException, ProductAmountNotSetException, ProductAmountNotEnoughException

        public ulong CalculateTotalCostOfProducts(List<ProductRecord> listOfProducts)
        {
            var price = 0ul;
            CheckAvailability(listOfProducts);

            foreach (var record in listOfProducts)
            {
                price += _storage.GetProductPrice(record.ProductId) * record.Amount;
            }
            
            return price;
        }  // ProductPriceNotSetException, ProductAmountNotSetException, ProductAmountNotEnoughException

        public List<ProductRecord> ListOfProductsOnMaxPrice(ulong maxPrice, out ulong moneyRemaining)
        {
            var list = new List<ProductRecord>();
            foreach (var productId in _storage.GetProductsList())
            {
                ulong price, amount;

                try
                {
                    price = _storage.GetProductPrice(productId);
                    amount = _storage.GetProductAmount(productId);
                }
                catch (ProductException)
                {
                    continue;
                }

                var k = Math.Min(maxPrice / price, amount);
                if (k == 0) continue;

                maxPrice -= price * k;
                list.Add(new ProductRecord(productId, k));
            }

            moneyRemaining = maxPrice;
            return list;
        }  // noexcept
    }
}