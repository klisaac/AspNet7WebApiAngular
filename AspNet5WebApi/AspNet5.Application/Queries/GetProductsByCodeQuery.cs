using System.Collections.Generic;
using MediatR;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Queries
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
