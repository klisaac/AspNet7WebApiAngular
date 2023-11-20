using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using AspNet7.Application.Commands;
using AspNet7.Integration.Tests.Infrastructure;


namespace AspNet7.Integration.Tests.Controllers
{
     public class UserControllerIntegrationTests : BaseControllerTest
    {
        public UserControllerIntegrationTests(ApplicationFactoryTest<StartupTest> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test_CreateUser_WithValidUserDetails()
        {
            var client = Factory.CreateClient();
            var httpResponse = await client.PostAsync("/api/user/create", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand() { UserName = "adrian", Password = "Welcome@123", ConfirmPassword = "Welcome@123" }), Encoding.UTF8, "application/json"));
            var tt = httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.True(Convert.ToBoolean(stringResponse));
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Test_CreateUser_WithPasswordAndConfirmPasswordNotMatching()
        {
            var client = Factory.CreateClient();
            var httpResponse = await client.PostAsync("/api/user/create", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand() { UserName = "brian", Password = "Welcome@123", ConfirmPassword = "Welcome" }), Encoding.UTF8, "application/json"));
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var badRequest = JsonConvert.DeserializeObject<ValidationProblemDetails>(stringResponse);
            Assert.True(badRequest.Errors.Count > 0);
            Assert.NotEqual(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Test_Login_WithValidCredentials()
        {
            var client = Factory.CreateClient();
            var httpResponse = await client.PostAsync("/api/user/authenticate", new StringContent(JsonConvert.SerializeObject(new LoginUserCommand() { UserName = "isaac", Password = "Welcome@123" }), Encoding.UTF8, "application/json"));
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            dynamic result = JObject.Parse(stringResponse);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Test_UpdateUser_WithValidUserDetails()
        {
            var provider = ClaimsProviderTest.WithAdminClaims();
            var client = Factory.CreateClientWithTokenAuth(provider);
            var httpResponse = await client.PutAsync("/api/user/update", new StringContent(JsonConvert.SerializeObject(new UpdateUserCommand() { UserId = 1, UserName = "adrian2", Password = "Welcome123", ConfirmPassword = "Welcome123" }), Encoding.UTF8, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.True(Convert.ToBoolean(stringResponse));
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

    }
}

 

