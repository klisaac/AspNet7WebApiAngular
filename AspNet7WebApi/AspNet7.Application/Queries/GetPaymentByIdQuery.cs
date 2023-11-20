using MediatR;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Queries
{
    public class GetPaymentByIdQuery: IRequest<PaymentResponse>
    {
        public int PaymentId { get; set; }

        public GetPaymentByIdQuery(int paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
