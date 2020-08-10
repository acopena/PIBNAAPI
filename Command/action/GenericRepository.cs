using Microsoft.EntityFrameworkCore;
using PIBNAAPI.Command.Interface;
using PIBNAAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PIBNAAPI.Command.action
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private PIBNAContext _context;
        private DbSet<T> table;

        public GenericRepository(PIBNAContext context)
        {
            _context = context;
        }

        public void Delete(object Id)
        {
            T exists = table.Find(Id);
            table.Remove(exists);
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object Id)
        {
            return table.Find(Id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }


    }
}
