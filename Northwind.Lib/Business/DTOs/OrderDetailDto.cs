using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Lib.Business.DTOs
{
    public class OrderDetailDto
    {
        public decimal UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public float Discount { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
    }
}