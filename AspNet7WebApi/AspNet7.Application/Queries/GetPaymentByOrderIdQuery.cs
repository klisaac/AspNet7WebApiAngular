using System.Collections.Generic;
using MediatR;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Queries
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
