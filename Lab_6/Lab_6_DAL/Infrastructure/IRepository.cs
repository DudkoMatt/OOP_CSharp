using System;
using System.Collections.Generic;

namespace Lab_6_DAL.Infrastructure
{
    public interface IRepository<T> where T: IEntity
    {
        T Get(int id);
        bool TryGet(int id);
        IEnumerable<T> GetAll();

        int GetMaxId();
        
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}