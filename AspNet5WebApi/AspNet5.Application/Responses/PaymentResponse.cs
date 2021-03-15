using AspNet5.Application.Models;

namespace AspNet5.Application.Responses
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public int OrderId { get; set; }
    }
}
