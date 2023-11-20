using System;
using Microsoft.AspNetCore.Http;
using AspNet7.Application.Common.Identity;

namespace AspNet7.Api.Common.Identity
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public string UserName => _accessor.HttpContext.User.Identity.Name;
    }
}
