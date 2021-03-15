using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNet5.Core.Repository;
using AspNet5.Application.Queries;
using AspNet5.Application.Responses;
using AspNet5.Core.Specifications;

namespace AspNet5.Application.Handlers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CustomerResponse>(await _customerRepository.GetSingleAsync(new CustomerSpecification(request.CustomerId)));
        }
    }
}
