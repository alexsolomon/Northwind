using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Lib.Business.DTOs;
using Northwind.Lib.Repository.Models;

namespace Northwind.Lib.Repository
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrder(int orderId);
        Task<List<ProductOrderedDetail>?> GetProductOrderDetailsForDate(DateTime startDate, DateTime endDate);
    }
}