using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Northwind.Lib.Repository.Models;

namespace Northwind.Api.Validators
{
    public class CustomerValidator  : AbstractValidator<Customer> 
    {
        public CustomerValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("Company name should be provided");
            RuleFor(x => x.ContactName).NotNull().NotEmpty().WithMessage("Contact name should be provided");
        }
    }
}