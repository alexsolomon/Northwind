using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Web.Authentication
{
    public class UserSession
    {
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}