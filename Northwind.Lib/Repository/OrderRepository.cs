using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Northwind.Lib.Business.DTOs;
using Northwind.Lib.Repository.Models;

namespace Northwind.Lib.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindDbContext _dbContext;
        public OrderRepository(NorthwindDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order?> GetOrder(int orderId)
        {
            var order = await _dbContext.Orders
            .Include(obj => obj.Customer)
            .Include(obj => obj.OrderDetails)!
            .ThenInclude(obj => obj.Product)
            .FirstOrDefaultAsync(obj => obj.OrderID == orderId);
            return order;
        }
        public async Task<List<ProductOrderedDetail>?> GetProductOrderDetailsForDate(DateTime startDate, DateTime endDate)
        {
            var orderDetails = await _dbContext.Orders
                    .Include(obj => obj.Customer)
                    .Include(obj => obj.OrderDetails)!
                    .ThenInclude(obj => obj.Product)
                    .Where(obj => obj.OrderDate >= startDate && obj.OrderDate <= endDate)
                    .SelectMany<Order, OrderDetail>(o => o.OrderDetails!)
                    .GroupBy(obj => obj.ProductID)
                    .Select(obj => new ProductOrderedDetail
                    {
                        ProductID = obj.Key,
                        ProductName = obj.Min(o => o.Product!.ProductName),
                        Quantity = obj.Sum(o => o.Quantity),
                        TotalPrice = obj.Sum(o => (o.Quantity * o.UnitPrice) - (decimal)o.Discount)
                    }
                    )
                    .ToListAsync();
            return orderDetails.OrderBy(obj => obj.ProductID).ToList();
        }
    }
}