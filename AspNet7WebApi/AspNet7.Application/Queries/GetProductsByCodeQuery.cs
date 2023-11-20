using System.Collections.Generic;
using MediatR;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Queries
{
    public class GetProductsByCodeQuery: IRequest<IEnumerable<ProductResponse>>
    {
        public string ProductCode { get; set; }
        public GetProductsByCodeQuery( string productCode)
        {
            ProductCode = productCode;
        }
    }
}
