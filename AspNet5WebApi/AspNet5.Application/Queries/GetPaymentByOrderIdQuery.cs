using System.Collections.Generic;
using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
{
    public class GetPaymentByOrderIdQuery: IRequest<IEnumerable<PaymentResponse>>
    {
        public int OrderId { get; set; }

        public GetPaymentByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
