using MediatR;
using AspNet7.Application.Models;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Commands
{
    public class CreatePaymentCommand : IRequest<PaymentResponse>
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
