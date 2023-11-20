using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using AspNet7.Application.Commands;
using AspNet7.Application.Models;
using AspNet7.Application.Responses;
using AspNet7.Core.Entities;
using AspNet7.Core.Pagination;

namespace AspNet7.Application.Common.Mappings
{
    // The best implementation of AutoMapper for class libraries - https://stackoverflow.com/questions/26458731/how-to-configure-auto-mapper-in-class-library-project
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateUserMap();
            CreateCategoryMap();
            CreateProductMap();
            CreateCustomerMap();
            CreateOrderMap();
            CreatePaymentMap();
        }
        
        private void CreateUserMap()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.UserName, map => map.MapFrom(src => src.UserName));

            CreateMap<UpdateUserCommand, User>()
                .ForMember(dest => dest.UserId, map => map.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, map => map.MapFrom(src => src.UserName));

            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.UserId, map => map.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, map => map.MapFrom(src => src.UserName)).ReverseMap();
        }
        private void CreateCategoryMap()
        {
            CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.CategoryId, map => map.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description)).ReverseMap();

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.CategoryId, map => map.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description)).ReverseMap();

            CreateMap<IPagedList<Category>, IPagedList<CategoryResponse>>()
                .ForMember(dest => dest.Items, map => map.MapFrom(src => src.Items))
                .ForMember(dest => dest.PageIndex, map => map.MapFrom(src => src.PageIndex))
                .ForMember(dest => dest.PageSize, map => map.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.TotalCount, map => map.MapFrom(src => src.TotalCount))
                .ForMember(dest => dest.TotalPages, map => map.MapFrom(src => src.TotalPages))
                .ForMember(dest => dest.HasNextPage, map => map.MapFrom(src => src.HasNextPage))
                .ForMember(dest => dest.HasPreviousPage, map => map.MapFrom(src => src.HasPreviousPage));
        }

        private void CreateProductMap()
        {
            CreateMap<CreateProductCommand, Product>()
                .ForMember(dest => dest.Code, map => map.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryId, map => map.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.UnitPrice, map => map.MapFrom(src => src.UnitPrice));

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.ProductId, map => map.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Code, map => map.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
                .ForMember(dest => dest.CategoryId, map => map.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.UnitPrice, map => map.MapFrom(src => src.UnitPrice));

            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.ProductId, map => map.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Code, map => map.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
                .ForMember(dest => dest.Category, map => map.MapFrom(src => src.Category))
                .ForMember(dest => dest.UnitPrice, map => map.MapFrom(src => src.UnitPrice)).ReverseMap();


            CreateMap<IPagedList<Product>, IPagedList<ProductResponse>>()
                .ForMember(dest => dest.Items, map => map.MapFrom(src => src.Items))
                .ForMember(dest => dest.PageIndex, map => map.MapFrom(src => src.PageIndex))
                .ForMember(dest => dest.PageSize, map => map.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.TotalCount, map => map.MapFrom(src => src.TotalCount))
                .ForMember(dest => dest.TotalPages, map => map.MapFrom(src => src.TotalPages))
                .ForMember(dest => dest.HasNextPage, map => map.MapFrom(src => src.HasNextPage))
                .ForMember(dest => dest.HasPreviousPage, map => map.MapFrom(src => src.HasPreviousPage));
        }
        private void CreateCustomerMap()
        {
            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.CustomerId, map => map.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
                .ForMember(dest => dest.SurName, map => map.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Address, map => map.MapFrom(src => src.DefaultAddress.AddressLine))
                .ForMember(dest => dest.City, map => map.MapFrom(src => src.DefaultAddress.City))
                .ForMember(dest => dest.State, map => map.MapFrom(src => src.DefaultAddress.State))
                .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email))
                .ForMember(dest => dest.CitizenId, map => map.MapFrom(src => src.CitizenId)).ReverseMap();
        }

        private void CreateOrderMap()
        {

            CreateMap<OrderItem, OrderItemModel>()
                .ForMember(dest => dest.OrderItemId, map => map.MapFrom(src => src.OrderItemId))
                .ForMember(dest => dest.ProductName, map => map.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, map => map.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, map => map.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.TotalPrice, map => map.MapFrom(src => src.Quantity * src.UnitPrice));

            CreateMap<Order, OrderModel>()
                .ForMember(dest => dest.OrderId, map => map.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.GrandTotal, map => map.MapFrom(src => src.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice)))
                .ForMember(dest => dest.OrderItems, map => map.MapFrom(src => src.OrderItems));

            CreateMap <IEnumerable<Order>, OrderResponse>()
                .ForMember(dest => dest.CustomerId, map => map.MapFrom(src => src.FirstOrDefault().CustomerId))
                .ForMember(dest => dest.CustomerName, map => map.MapFrom(src => src.FirstOrDefault().Customer.Name))
                .ForMember(dest => dest.CustomerSurName, map => map.MapFrom(src => src.FirstOrDefault().Customer.Surname))
                .ForMember(dest => dest.Orders, map => map.MapFrom(src => src));
        }

        private void CreatePaymentMap()
        {
            CreateMap<CreatePaymentCommand, Payment>()
                .ForMember(dest => dest.OrderId, map => map.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Amount, map => map.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Method, map => map.MapFrom(src => Enum.Parse(typeof(PaymentMethod), src.Method,true)));
            
            CreateMap<Payment, PaymentResponse>()
                .ForMember(dest => dest.PaymentId, map => map.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.OrderId, map => map.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Amount, map => map.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Method, map => map.MapFrom(src => Enum.GetName(src.Method)));
        }

    }
}
