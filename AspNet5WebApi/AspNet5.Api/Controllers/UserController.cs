﻿using System;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using MediatR;
using AspNet5.Core.Logging;
using AspNet5.Application.Queries;
using AspNet5.Application.Commands;
using AspNet5.Application.Responses;
using AspNet5.Core.Configuration;

namespace AspNet5.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Admin)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAspNet5Logger<UserController> _logger;
        private readonly JwtIssuerOptions _jwtIssuerOptions;

        public UserController(IMediator mediator, IMapper mapper,
          IAspNet5Logger<UserController> logger,
          IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _jwtIssuerOptions = jwtIssuerOptions.Value;
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody]LoginUserCommand command)
        {
            if (!await _mediator.Send(command))
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtIssuerOptions.SigningKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, command.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, command.UserName),
                    new Claim(ClaimTypes.Name, command.UserName.ToString()),
                    new Claim(ClaimTypes.Role, Role.Admin)
                }),
                Expires = DateTime.Now.AddDays(1),
                NotBefore = DateTime.UtcNow,
                Issuer = _jwtIssuerOptions.Issuer,
                Audience = _jwtIssuerOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            _logger.LogInformation($"User {command.UserName} authenticated.");

            // return basic user info and authentication token
            return Ok(new
            {
                UserName = command.UserName,
                Token = tokenString,
                Expiration = token.ValidTo
            });
        }

        [HttpPost("create")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("delete/{userId:int}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromRoute] int userId)
        {
            return Ok(await _mediator.Send(new DeleteUserCommand() { UserId = userId }));
        }
    }
}
