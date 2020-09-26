using System.Collections.Generic;

namespace Lab_2
{
    public class StoreHandler
    {
        private StoreDatabase _database;
        
        public StoreHandler(StoreDatabase database)
        {
            _database = database;
        }
        
        public Store FindStoreWithMinPriceOfProduct(ulong id, out ulong minPrice)
        {
            Store foundStore = null;
            minPrice = ulong.MaxValue;
            foreach (var store in _database.GetStoresList())
            {
                ulong price;
                
                try
                {
                    price = store.GetProductPrice(id);
                }
                catch (ProductPriceNotSetException)
                {
                    continue;
                }

                if (price < minPrice)
                {
                    minPrice = price;
                    foundStore = store;
                }
            }
            
            if (foundStore == null) throw new StoreNotFoundException();
            
            return foundStore;
        }  // StoreNotFoundException
        
        public Store FindStoreWithMinPriceOfProductList(List<ProductRecord> listOfProducts, out ulong minPrice)
        {
            minPrice = ulong.MaxValue;
            Store foundStore = null;

            foreach (var store in _database.GetStoresList())
            {
                ulong price;

                try
                {
                    price = store.CalculateTotalCostOfProducts(listOfProducts);
                }
                catch (ProductException)
                {
                    continue;
                }
                
                if (price < minPrice)
                {
                    minPrice = price;
                    foundStore = store;
                }
            }
            
            if (foundStore == null) throw new StoreNotFoundException();
            
            return foundStore;
        }  // StoreNotFoundException

        public void BuyProductListInStore(ulong id, List<ProductRecord> listOfProducts, out ulong totalPrice)
        {
            _database.GetStore(id).BuyProductList(listOfProducts, out totalPrice);
        }  // KeyNotFoundException, ProductPriceNotSetException, ProductAmountNotSetException, ProductAmountNotEnoughException
    }
}