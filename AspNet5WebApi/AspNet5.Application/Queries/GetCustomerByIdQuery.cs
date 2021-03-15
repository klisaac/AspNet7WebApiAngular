using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerResponse>
    {
        public int CustomerId { get; set; }

        public GetCustomerByIdQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
