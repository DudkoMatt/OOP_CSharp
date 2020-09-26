using System.Collections.Generic;

namespace Lab_2
{
    // Global. To get info about products
    public class ProductDatabase
    {
        private Dictionary<ulong, Product> _products;  // id - product

        public ProductDatabase()
        {
            _products = new Dictionary<ulong, Product>();
        }

        public ulong CreateProduct(string name)
        {
            var tmp = new Product(name);
            _products.Add(tmp.Id, tmp);
            return tmp.Id;
        }

        public Product GetProduct(ulong id) => _products[id];  // KeyNotFoundException
    }
}