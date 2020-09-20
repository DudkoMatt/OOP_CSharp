using System;
using System.Collections.Generic;

namespace Lab_2
{
    class Program
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
            var storeHandler = new StoreHandler();
            
            // Register new stores in storeHandler
            // Note: registering store returns id of that store
            storeHandler.CreateStore("Name_1", "Some address 1");
            storeHandler.CreateStore("Name_2", "Some address 2");
            storeHandler.CreateStore("Name_3", "Some address 3");
            
            // Add products to stores
            // Note: you need to specify price of product while adding
            // Also note: if price was set before it will be overriden
            storeHandler.GetStore(1000).AddProduct(1000, 29, 52);
            storeHandler.GetStore(1000).AddProduct(1001, 85, 63);
            storeHandler.GetStore(1000).AddProduct(1002, 98, 40);
            storeHandler.GetStore(1000).AddProduct(1003, 89, 52);
            storeHandler.GetStore(1000).AddProduct(1004, 34, 43);
            storeHandler.GetStore(1000).AddProduct(1005, 38, 21);
            storeHandler.GetStore(1000).AddProduct(1006, 17, 43);
            storeHandler.GetStore(1000).AddProduct(1007, 100, 11);
            storeHandler.GetStore(1000).AddProduct(1008, 26, 87);
            storeHandler.GetStore(1000).AddProduct(1009, 70, 15);

            storeHandler.GetStore(1001).AddProduct(1000, 5, 22);
            storeHandler.GetStore(1001).AddProduct(1001, 94, 49);
            storeHandler.GetStore(1001).AddProduct(1002, 32, 42);
            storeHandler.GetStore(1001).AddProduct(1003, 53, 52);
            storeHandler.GetStore(1001).AddProduct(1004, 11, 77);
            storeHandler.GetStore(1001).AddProduct(1005, 75, 2);
            storeHandler.GetStore(1001).AddProduct(1006, 17, 60);
            storeHandler.GetStore(1001).AddProduct(1007, 50, 54);
            storeHandler.GetStore(1001).AddProduct(1008, 79, 90);
            storeHandler.GetStore(1001).AddProduct(1009, 77, 85);
            
            storeHandler.GetStore(1002).AddProduct(1000, 92, 28);
            storeHandler.GetStore(1002).AddProduct(1001, 79, 53);
            storeHandler.GetStore(1002).AddProduct(1002, 13, 52);
            storeHandler.GetStore(1002).AddProduct(1003, 34, 34);
            storeHandler.GetStore(1002).AddProduct(1004, 97, 15);
            storeHandler.GetStore(1002).AddProduct(1005, 14, 30);
            storeHandler.GetStore(1002).AddProduct(1006, 95, 5);
            storeHandler.GetStore(1002).AddProduct(1007, 88, 94);
            storeHandler.GetStore(1002).AddProduct(1008, 53, 26);
            storeHandler.GetStore(1002).AddProduct(1009, 22, 93);
            
            // Update price of product in store
            storeHandler.GetStore(1000).SetProductPrice(1000, 1);
            
            // Find store with minimum product price
            Console.WriteLine("TryFindMinPriceOfProduct: id = 1000");
            Console.WriteLine(storeHandler.TryFindMinPriceOfProduct(1000, out var minPrice, out var foundStoreId)
                ? $"Found: price: {minPrice}, storeId: {foundStoreId}"
                : "Not found");
            
            Console.WriteLine();
            
            Console.WriteLine("TryFindMinPriceOfProduct: id = 0");
            Console.WriteLine(storeHandler.TryFindMinPriceOfProduct(0, out minPrice, out foundStoreId)
                ? $"Found: price: {minPrice}, storeId: {foundStoreId}"
                : "Not found");
            
            Console.WriteLine();
            
            // Get list of possible products
            Console.WriteLine("Found list of products in storeId = 1000, maxCost = 100:");
            ulong moneyRemaining;
            foreach(var (productId, amount) in storeHandler.GetStore(1000).ListOfProductsOnMaxPrice(100, out moneyRemaining))
            {
                Console.WriteLine($"ProductId: {productId}, " +
                                  $"amount: {amount}, " +
                                  $"cost: {storeHandler.GetStore(1000).GetProductPrice(productId) * amount}");
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Money remaining: {moneyRemaining}\n");
            
            // Buy products in store
            // Create list of products
            var listOfProducts = new List<KeyValuePair<ulong, ulong>>();
            listOfProducts.Add(new KeyValuePair<ulong, ulong>(1000, 6));
            listOfProducts.Add(new KeyValuePair<ulong, ulong>(1001, 10));
            
            // Buy in store
            ulong totalPrice;
            
            // Cannot buy products
            Console.WriteLine("Try to buy productList in 1001 store");
            Console.WriteLine(storeHandler.GetStore(1001).TryBuyProducts(listOfProducts, out totalPrice)
                ? $"Bought all products. Total cost: {totalPrice}"
                : "Cannot buy all products");
            Console.WriteLine();
            
            // Can buy products
            Console.WriteLine("Try to buy productList in 1000 store");
            Console.WriteLine(storeHandler.GetStore(1000).TryBuyProducts(listOfProducts, out totalPrice)
                ? $"Bought all products. Total cost: {totalPrice}"
                : "Cannot buy all products");
            Console.WriteLine();
            
            // Find minimum price of list
            Console.WriteLine("Try to find minimum cost of productList");
            Console.WriteLine(storeHandler.TryFindMinPriceOfProductList(listOfProducts, out foundStoreId, out minPrice)
                ? $"Minimum cost: {minPrice}, found storeId: {foundStoreId}"
                : "Cannot find minimum cost"
                );
        }
    }
}