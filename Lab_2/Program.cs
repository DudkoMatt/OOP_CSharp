using System;
using System.Collections.Generic;

namespace Lab_2
{
    public class Program
    {
        public static void Main()
        {
            // Create product database
            var productDatabase = new ProductDatabase();
            
            // Register new products in productDatabase
            // Note: registering product returns id of that product
            productDatabase.CreateProduct("Огурец");
            productDatabase.CreateProduct("Помидор");
            productDatabase.CreateProduct("Шоколад");
            productDatabase.CreateProduct("Масло");
            productDatabase.CreateProduct("Мясо");
            productDatabase.CreateProduct("Сыр");
            productDatabase.CreateProduct("Чай");
            productDatabase.CreateProduct("Перец");
            productDatabase.CreateProduct("Мороженое");
            productDatabase.CreateProduct("Шуруп");

            // Create storageHandler
            var storeDatabase = new StoreDatabase();
            
            // Register new stores in storeHandler
            // Note: registering store returns id of that store
            
            storeDatabase.CreateStore("Name_1", "Some address 1");
            storeDatabase.CreateStore("Name_2", "Some address 2");
            storeDatabase.CreateStore("Name_3", "Some address 3");
            
            // Add products to stores
            // Note: you need to specify price of product while adding
            // Also note: if price was set before it will be overriden
            storeDatabase.GetStore(1000).AddProduct(1000, 29, 52);
            storeDatabase.GetStore(1000).AddProduct(1001, 85, 63);
            storeDatabase.GetStore(1000).AddProduct(1002, 98, 40);
            storeDatabase.GetStore(1000).AddProduct(1003, 89, 52);
            storeDatabase.GetStore(1000).AddProduct(1004, 34, 43);
            storeDatabase.GetStore(1000).AddProduct(1005, 38, 21);
            storeDatabase.GetStore(1000).AddProduct(1006, 17, 43);
            storeDatabase.GetStore(1000).AddProduct(1007, 100, 11);
            storeDatabase.GetStore(1000).AddProduct(1008, 26, 87);
            storeDatabase.GetStore(1000).AddProduct(1009, 70, 15);

            storeDatabase.GetStore(1001).AddProduct(1000, 5, 22);
            storeDatabase.GetStore(1001).AddProduct(1001, 94, 49);
            storeDatabase.GetStore(1001).AddProduct(1002, 32, 42);
            storeDatabase.GetStore(1001).AddProduct(1003, 53, 52);
            storeDatabase.GetStore(1001).AddProduct(1004, 11, 77);
            storeDatabase.GetStore(1001).AddProduct(1005, 75, 2);
            storeDatabase.GetStore(1001).AddProduct(1006, 17, 60);
            storeDatabase.GetStore(1001).AddProduct(1007, 50, 54);
            storeDatabase.GetStore(1001).AddProduct(1008, 79, 90);
            storeDatabase.GetStore(1001).AddProduct(1009, 77, 85);
            
            storeDatabase.GetStore(1002).AddProduct(1000, 92, 28);
            storeDatabase.GetStore(1002).AddProduct(1001, 79, 53);
            storeDatabase.GetStore(1002).AddProduct(1002, 13, 52);
            storeDatabase.GetStore(1002).AddProduct(1003, 34, 34);
            storeDatabase.GetStore(1002).AddProduct(1004, 97, 15);
            storeDatabase.GetStore(1002).AddProduct(1005, 14, 30);
            storeDatabase.GetStore(1002).AddProduct(1006, 95, 5);
            storeDatabase.GetStore(1002).AddProduct(1007, 88, 94);
            storeDatabase.GetStore(1002).AddProduct(1008, 53, 26);
            storeDatabase.GetStore(1002).AddProduct(1009, 22, 93);
            
            // Update price of product in store
            storeDatabase.GetStore(1000).SetProductPrice(1000, 1);
            
            var storeHandler = new StoreHandler(storeDatabase);
            
            // Find store with minimum product price
            // -------------------------------------- 1 --------------------------------------
            
            Console.WriteLine("Try FindMinPriceOfProduct: id = 1000");

            Store foundStore;
            
            try
            {
                foundStore = storeHandler.FindStoreWithMinPriceOfProduct(1000, out var minPrice);
                Console.WriteLine($"Found: price: {minPrice}, storeId: {foundStore.Id}");
            }
            catch (StoreNotFoundException)
            {
                Console.WriteLine("Not found");
            }

            Console.WriteLine();
            
            // -------------------------------------------------------------------------------
            
            // -------------------------------------- 2 --------------------------------------
            
            Console.WriteLine("Try FindMinPriceOfProduct: id = 0");
            
            try
            {
                foundStore = storeHandler.FindStoreWithMinPriceOfProduct(0, out var minPrice);
                Console.WriteLine($"Found: price: {minPrice}, storeId: {foundStore.Id}");
            }
            catch (StoreNotFoundException)
            {
                Console.WriteLine("Not found");
            }

            Console.WriteLine();
            
            // -------------------------------------------------------------------------------
            
            // Get list of possible products
            Console.WriteLine("Found list of products in storeId = 1000, maxCost = 100:");
            ulong moneyRemaining;
            
            foreach(var record in storeDatabase.GetStore(1000).ListOfProductsOnMaxPrice(100, out moneyRemaining))
            {
                Console.WriteLine($"ProductId: {record.ProductId}, " +
                                  $"amount: {record.Amount}, " +
                                  $"cost: {storeDatabase.GetStore(1000).GetProductPrice(record.ProductId) * record.Amount}");
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Money remaining: {moneyRemaining}\n");
            
            // Buy products in store
            // Create list of products
            var listOfProducts = new List<ProductRecord>
            {
                new ProductRecord(1000, 6),
                new ProductRecord(1001, 10)
            };

            
            // -------------------------------------------------------------------------------
            
            // Buy in store
            ulong totalPrice;
            
            // -------------------------------------- 1 --------------------------------------
            // Cannot buy products
            
            Console.WriteLine("Try to buy productList in 1001 store");

            try
            {
                storeHandler.BuyProductListInStore(1001, listOfProducts, out totalPrice);
                Console.WriteLine($"Bought all products. Total cost: {totalPrice}");
            }
            catch (ProductAmountNotSetException e)
            {
                Console.WriteLine($"Cannot buy all products: {e.GetType()}");
            }
            catch (ProductAmountNotEnoughException e)
            {
                Console.WriteLine($"Cannot buy all products: {e.GetType()}");
            }
            catch (ProductPriceNotSetException e)
            {
                Console.WriteLine($"Cannot buy all products: {e.GetType()}");
            }
            
            Console.WriteLine();
            
            // -------------------------------------------------------------------------------
            
            // -------------------------------------- 2 --------------------------------------
            // Can buy products
            
            Console.WriteLine("Try to buy productList in 1000 store");
            
            try
            {
                storeHandler.BuyProductListInStore(1000, listOfProducts, out totalPrice);
                Console.WriteLine($"Bought all products. Total cost: {totalPrice}");
            }
            catch (ProductAmountNotSetException e)
            {
                Console.WriteLine($"Cannot buy all products: {e.GetType()}");
            }
            catch (ProductAmountNotEnoughException e)
            {
                Console.WriteLine($"Cannot buy all products: {e.GetType()}");
            }
            catch (ProductPriceNotSetException e)
            {
                Console.WriteLine($"Cannot buy all products: {e.GetType()}");
            }
            
            Console.WriteLine();
            
            // --------------------------------------------------------------------------------------------
            
            
            // Find minimum price of list
            Console.WriteLine("Try to find minimum cost of productList");

            try
            {
                foundStore = storeHandler.FindStoreWithMinPriceOfProductList(listOfProducts, out var minPrice);
                Console.WriteLine($"Minimum cost: {minPrice}, found storeId: {foundStore.Id}");
            }
            catch (StoreNotFoundException)
            {
                Console.WriteLine("Cannot find minimum cost");
            }
        }
    }
}