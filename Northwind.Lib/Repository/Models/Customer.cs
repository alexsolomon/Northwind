using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Lib.Repository.Models
{
    public class Customer
    {
        [Key]
        public string? CustomerID { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        // public string? Region { get; set; }
        //    [ForeignKey("CustomerID")]
        // public virtual ICollection<Order>? Orders { get; }

    }
}