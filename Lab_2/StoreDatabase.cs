using System.Collections.Generic;

namespace Lab_2
{
    public class StoreDatabase
    {
        private Dictionary<ulong, Store> _stores;

        public StoreDatabase()
        {
            _stores = new Dictionary<ulong, Store>();
        }

        public Store GetStore(ulong id) => _stores[id];  // KeyNotFoundException
        
        public ulong CreateStore(string name, string address)
        {
            var tmp = new Store(name, address);
            _stores.Add(tmp.Id, tmp);
            return tmp.Id;
        }
        
        public IEnumerable<Store> GetStoresList() => _stores.Values;
    }
}