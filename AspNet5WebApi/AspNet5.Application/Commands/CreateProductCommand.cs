﻿using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
