using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
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
