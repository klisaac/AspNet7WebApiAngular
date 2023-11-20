using System.ComponentModel.DataAnnotations;

namespace AspNet7.Application.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}