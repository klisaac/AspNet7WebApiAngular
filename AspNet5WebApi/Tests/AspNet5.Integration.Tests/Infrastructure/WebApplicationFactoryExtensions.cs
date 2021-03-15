using System;
using System.Net.Http;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AspNet5.Core.Configuration;

namespace AspNet5.Integration.Tests.Infrastructure
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateClientWithTokenAuth<T>(this WebApplicationFactory<T> factory, ClaimsProviderTest claimsProvider) where T : class
        {
            var configuration = factory.Services.GetRequiredService<IConfiguration>();
            var jwtIssuerOptionsSection = configuration.GetSection(nameof(JwtIssuerOptions));
            var jwtSettings = jwtIssuerOptionsSection.Get<JwtIssuerOptions>();
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey)), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, claimsProvider.Claims, null, DateTime.UtcNow.AddMinutes(30), signingCredentials));

            var client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
            return client;
        }

        public static HttpClient CreateClient<T>(this WebApplicationFactory<T> factory) where T : class
        {
            var client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            return client;
        }
    }
}