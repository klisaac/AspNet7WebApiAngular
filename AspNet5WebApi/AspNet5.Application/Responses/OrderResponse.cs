using System.Collections.Generic;
using AspNet5.Application.Models;
namespace AspNet5.Application.Responses
{
    public class OrderResponse
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurName { get; set; }
        public IEnumerable<OrderModel> Orders { get; set; }
    }
}
