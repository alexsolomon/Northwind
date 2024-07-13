using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Lib.Repository.Models;


namespace Northwind.Lib.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NorthwindDbContext _dbContext;
        public CustomerRepository(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Customer?> GetCustomer(string id)
        {
            var order = await _dbContext.Orders.Include(obj => obj.Customer).FirstOrDefaultAsync();
            return _dbContext.Customers.Find(id);
        }
    }
}