using System;
using System.Collections.Generic;
using System.Text;

namespace PIBNAAPI.Command.Interface
{
    public interface IGenericRepository<T> where T: class
    {
        List<T> GetAll();
        T GetById(Object Id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object Id);
        void Save();
    }
}
