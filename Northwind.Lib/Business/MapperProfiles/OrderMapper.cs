using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Northwind.Lib.Business.DTOs;
using Northwind.Lib.Repository.Models;

namespace Northwind.Lib.Business.MapperProfiles
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Customer!.CompanyName))
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
        }
    }
}