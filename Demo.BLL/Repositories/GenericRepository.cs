using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {

        //private attribute
        private protected readonly AppDbContext _dbContext;

        //not recommended constructor new opject from AppDbContext
        //because with every call will oben new connecton with database  

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(T entity) 
        {
            _dbContext.Add(entity);
            return _dbContext.SaveChanges(); //return the num of rows affected in database

        }

        public int Delete(T entity)
        {
            _dbContext.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Update(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            /*
            var T = _dbContext.Ts.Local.Where(d => d.Id == id).FirstOrDefault();//search first in your memory using Local Keyword
            if (T is null) //if not in the memory ?
                T = _dbContext.Ts.Where(d => d.Id == id).FirstOrDefault(); // ge from db
            return T;
            */
            //instead of this use Find();
            //return _dbContext.Ts.Find(id);
            //use new Find();
            return _dbContext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>)_dbContext.Employees.Include(E => E.Department).AsNoTracking().ToList();
            else
                return _dbContext.Set<T>().AsNoTracking().ToList();//just show data without edit
        }
        //Set<T>() --> instead of department or employee or ...
    }
}
