using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Northwind.Web.Authentication
{
    public class CustomAuthenticationHandlerOptions : AuthenticationSchemeOptions
    {
        public string Scheme { get; set; } = "";
    }
}