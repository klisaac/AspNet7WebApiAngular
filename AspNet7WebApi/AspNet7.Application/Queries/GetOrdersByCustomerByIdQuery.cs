using System.Collections.Generic;
using MediatR;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Queries
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
