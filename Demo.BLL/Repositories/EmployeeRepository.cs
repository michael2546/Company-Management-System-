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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //i inherited it (private protected)
        //private readonly AppDbContext _dbContext;
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext) //Ask CLR for Creating Object From Dbcontext
        {
        }

        #region before GenericRepository

        ////private attribute
        //private readonly AppDbContext _dbContext;

        ////not recommended constructor new opject from AppDbContext
        ////because with every call will oben new connecton with database  

        //public EmployeeRepository(AppDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public int Add(Employee entity)
        //{
        //    _dbContext.Employees.Add(entity);
        //    return _dbContext.SaveChanges(); //return the num of rows affected in database

        //}

        //public int Delete(Employee entity)
        //{
        //    _dbContext.Employees.Remove(entity);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(Employee entity)
        //{
        //    _dbContext.Employees.Update(entity);
        //    return _dbContext.SaveChanges();
        //}

        //public Employee Get(int id)
        //{
        //    /*
        //    var Employee = _dbContext.Employees.Local.Where(d => d.Id == id).FirstOrDefault();//search first in your memory using Local Keyword
        //    if (Employee is null) //if not in the memory ?
        //        Employee = _dbContext.Employees.Where(d => d.Id == id).FirstOrDefault(); // ge from db
        //    return Employee;
        //    */
        //    //instead of this use Find();
        //    //return _dbContext.Employees.Find(id);
        //    //use new Find();
        //    return _dbContext.Find<Employee>(id);
        //}

        //public IEnumerable<Employee> GetAll()
        //    => _dbContext.Employees.AsNoTracking().ToList();//just show data without edit

        #endregion
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return _dbContext.Employees.Where(e => e.Adress.ToLower().Contains( address.ToLower() ));
        }

        public IQueryable<Employee> SearchByName(string name)
            => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));
    }
}
