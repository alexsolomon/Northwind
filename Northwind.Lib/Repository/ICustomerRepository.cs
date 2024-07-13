using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Lib.Repository.Models;

namespace Northwind.Lib.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomer(string id);
    }
}