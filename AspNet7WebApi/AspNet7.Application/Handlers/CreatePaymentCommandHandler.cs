using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet7.Application.Commands;
using AspNet7.Application.Common.Identity;
using AspNet7.Application.Responses;
using AspNet7.Core.Entities;
using AspNet7.Core.Logging;
using AspNet7.Core.Repository;

namespace AspNet7.Application.Handlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet7Logger<CreatePaymentCommandHandler> _logger;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, ICurrentUser currentUser, IMapper mapper, IAspNet7Logger<CreatePaymentCommandHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaymentResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
                
            var payment = _mapper.Map<Payment>(request);
            payment.IsDeleted = false;
            payment.CreatedBy = _currentUser.UserName;

            var paymentResponse = _mapper.Map<PaymentResponse>(await _paymentRepository.AddByLoadingReferenceAsync(payment, p => p.Order));
            _logger.Information($"Created payment, {JsonSerializer.Serialize(paymentResponse)}.");

            return paymentResponse;
        }
    }
}