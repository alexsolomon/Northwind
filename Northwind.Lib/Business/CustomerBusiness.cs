using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Northwind.Lib.Repository;
using Northwind.Lib.Repository.Models;

namespace Northwind.Lib.Business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        public CustomerBusiness(IMapper mapper,
        ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }
        public Task<Customer?> GetCustomer(string id)
        {
            return _customerRepository.GetCustomer(id);
        }
    }
}