using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Northwind.Lib.Business.DTOs;
using Northwind.Lib.Repository;
using Northwind.Lib.Repository.Models;

namespace Northwind.Lib.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderBusiness(IMapper mapper,
        IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<OrderDto?> GetOrder(int orderId)
        {
            var order = _mapper.Map<OrderDto>(await _orderRepository.GetOrder(orderId));
            return order;
        }

        public Task<List<ProductOrderedDetail>?> GetProductOrderDetailsForDate(DateTime startDate, DateTime endDate)
        {
            return _orderRepository.GetProductOrderDetailsForDate(startDate, endDate);
        }
    }
}