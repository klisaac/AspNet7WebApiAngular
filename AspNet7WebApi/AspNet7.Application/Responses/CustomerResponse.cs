using AspNet7.Application.Models;
using System.Collections.Generic;

namespace AspNet7.Application.Responses
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Email { get; set; }
        public string CitizenId { get; set; }
    }
}
