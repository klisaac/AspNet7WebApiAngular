using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNet7.Application.Queries;
using AspNet7.Application.Responses;
using AspNet7.Core.Repository;
using AspNet7.Core.Specifications;

namespace AspNet7.Application.Handlers
{
    public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerByIdQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public GetOrdersByCustomerIdQueryHandler(IOrderRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderResponse> Handle(GetOrdersByCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<OrderResponse>(await _orderItemRepository.GetAsync(new OrderSpecification(request.CustomerId)));
        }
    }
}
