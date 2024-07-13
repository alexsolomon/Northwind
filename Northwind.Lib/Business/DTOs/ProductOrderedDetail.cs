using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Lib.Business.DTOs
{
    public class ProductOrderedDetail
    {
        public decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
    }
}