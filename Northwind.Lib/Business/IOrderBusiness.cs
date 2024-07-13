using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Lib.Business.DTOs;
using Northwind.Lib.Repository.Models;

namespace Northwind.Lib.Business
{
    public interface IOrderBusiness
    {
        Task<OrderDto?> GetOrder(int orderId);
        Task<List<ProductOrderedDetail>?> GetProductOrderDetailsForDate(DateTime startDate, DateTime endDate);
    }
}