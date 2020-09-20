using System.Collections.Generic;

namespace Lab_2
{
    class StoreHandler
    {
        private Dictionary<ulong, Store> _stores;

        public StoreHandler()
        {
            _stores = new Dictionary<ulong, Store>();
        }

        public Store GetStore(ulong id) => _stores[id];
        
        public ulong CreateStore(string name, string address)
        {
            var tmp = new Store(name, address);
            _stores.Add(tmp.Id, tmp);
            return tmp.Id;
        }

        public bool TryFindMinPriceOfProduct(ulong id, out ulong minPrice, out ulong foundStoreId)
        {
            foundStoreId = 0;
            minPrice = ulong.MaxValue;
            foreach (var (storeId, store) in _stores)
            {
                if (store.TryGetProductPrice(id, out var price) && price < minPrice)
                {
                    minPrice = price;
                    foundStoreId = storeId;
                }
            }

            return foundStoreId != 0;
        }

        public bool TryFindMinPriceOfProductList(List<KeyValuePair<ulong, ulong>> listOfProducts, out ulong foundStoreId, out ulong minPrice)
        {
            minPrice = ulong.MaxValue;
            foundStoreId = 0;

            foreach (var (storeId, store) in _stores)
            {
                if (store.TryCalculateTotalCostOfProducts(listOfProducts, out var price) && price < minPrice)
                {
                    minPrice = price;
                    foundStoreId = storeId;
                }
            }

            return foundStoreId != 0;
        }
    }
}