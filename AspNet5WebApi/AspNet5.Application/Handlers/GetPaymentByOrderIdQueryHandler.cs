using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;
using AspNet5.Core.Specifications;
using AspNet5.Core.Repository;
using AspNet5.Application.Queries;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class GetPaymentByOrderIdQueryHandler : IRequestHandler<GetPaymentByOrderIdQuery, IEnumerable<PaymentResponse>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public GetPaymentByOrderIdQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PaymentResponse>> Handle(GetPaymentByOrderIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<PaymentResponse>>(await _paymentRepository.GetAsync(new PaymentSpecification(p => p.OrderId == request.OrderId)));
        }
    }
}
