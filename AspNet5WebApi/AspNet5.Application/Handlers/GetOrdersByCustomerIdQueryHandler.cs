using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNet5.Core.Repository;
using AspNet5.Application.Queries;
using AspNet5.Application.Responses;
using System.Collections.Generic;
using AspNet5.Core.Specifications;

namespace AspNet5.Application.Handlers
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
