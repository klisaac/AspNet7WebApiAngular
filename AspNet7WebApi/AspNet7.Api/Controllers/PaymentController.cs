using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using AspNet7.Application.Commands;
using AspNet7.Application.Queries;
using AspNet7.Application.Responses;
using AspNet7.Core.Configuration;
using AspNet7.Core.Logging;

namespace AspNet7.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Admin)]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAspNet7Logger<PaymentController> _logger;

        public PaymentController(IMediator mediator, IAspNet7Logger<PaymentController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet("getById/{paymentId:int}")]        
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaymentResponse>> GetPaymentById(int paymentId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetPaymentByIdQuery(paymentId)));
            }
            catch (Exception e)
            {
                _logger.Error($"{e.Message} | {e.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getByOrderId/{orderId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetPaymentByOrderId(int orderId)
        {
            return Ok(await _mediator.Send(new GetPaymentByOrderIdQuery(orderId)));
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentResponse>> Create([FromBody]CreatePaymentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
