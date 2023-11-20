using System.Collections.Generic;
using MediatR;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Queries
{
    public class GetAllPaymentsQuery: IRequest<IEnumerable<PaymentResponse>>
    {
    }
}
