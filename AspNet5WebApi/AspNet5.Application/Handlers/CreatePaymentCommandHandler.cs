using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet5.Application.Commands;
using AspNet5.Core.Entities;
using AspNet5.Core.Logging;
using AspNet5.Core.Repository;
using AspNet5.Application.Common.Identity;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, PaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet5Logger<CreatePaymentCommandHandler> _logger;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, ICurrentUser currentUser, IMapper mapper, IAspNet5Logger<CreatePaymentCommandHandler> logger)
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
            _logger.LogInformation($"Created payment, {JsonSerializer.Serialize(paymentResponse)}.");

            return paymentResponse;
        }
    }
}