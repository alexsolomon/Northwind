using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Lib.Business.DTOs
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? CustomerID { get; set; }
        public List<OrderDetailDto>? OrderDetails { get; set; }
    }
}