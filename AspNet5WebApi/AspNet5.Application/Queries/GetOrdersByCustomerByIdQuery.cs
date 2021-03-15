using System.Collections.Generic;
using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
{
    public class GetOrdersByCustomerByIdQuery : IRequest<OrderResponse>
    {
        public int CustomerId { get; set; }

        public GetOrdersByCustomerByIdQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
