using MediatR;
using AspNet5.Application.Models;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Commands
{
    public class CreatePaymentCommand : IRequest<PaymentResponse>
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
