using System.Collections.Generic;
using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
{
    public class GetAllUsersQuery: IRequest<IEnumerable<UserResponse>>
    {
    }
}
