using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;
using AspNet7.Application.Commands;
using AspNet7.Application.Queries;
using AspNet7.Application.Responses;
using AspNet7.Core.Pagination;
using AspNet7.Integration.Tests.Infrastructure;


namespace AspNet7.Integration.Tests.Controllers
{
    public class ProductControllerIntegrationTests : BaseControllerTest
    {
        public ProductControllerIntegrationTests(ApplicationFactoryTest<StartupTest> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test_CreateProduct_WithValidProductDetails()
        {
            var createProductRequest = new CreateProductCommand() { Code = "P-REDME001", Name = "Redme Note 8 Pro", CategoryId = 4, UnitPrice = 200 };
            var provider = ClaimsProviderTest.WithAdminClaims();
            var client = Factory.CreateClientWithTokenAuth(provider);
            var httpResponse = await client.PostAsync("/api/product/create", new StringContent(JsonConvert.SerializeObject(createProductRequest), Encoding.UTF8, "application/json"));
        
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductResponse>(stringResponse);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(product.ProductId > 0);
            Assert.True(product.Name == "Redme Note 8 Pro");
            Assert.True(product.Category.CategoryId == 4);
            Assert.True(product.UnitPrice == 200);
        }

        [Fact]
        public async Task Test_CreateProduct_WithInvalidProductDetails()
        {
            var provider = ClaimsProviderTest.WithAdminClaims();
            var client = Factory.CreateClientWithTokenAuth(provider);
            var httpResponse = await client.PostAsync("/api/product/create", new StringContent(JsonConvert.SerializeObject(new CreateProductCommand() { CategoryId = 4, UnitPrice = 200 }), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Test_Search_Products_By_Criteria()
        {
            var filteringOptions = new List<FilteringOption>() { new FilteringOption() { Field = "name", Operator = FilteringOption.FilteringOperator.Contains, Value = "Camera" } };
            var sortingOption = new List<SortingOption>() { new SortingOption() { Field = "name", Direction = SortingOption.SortingDirection.ASC, Priority = 1 } };
            var pageSearchArgs = new Core.Pagination.SearchArgs() { FilteringOptions = filteringOptions, PagingStrategy = PagingStrategy.WithCount, SortingOptions = sortingOption, PageIndex = 1, PageSize = 10 };
            var searchPageRequest = new PageSearchArgs() { Args = pageSearchArgs };

            var provider = ClaimsProviderTest.WithAdminClaims();
            var client = Factory.CreateClientWithTokenAuth(provider);
            var httpResponse = await client.PostAsync("/api/product/search", new StringContent(JsonConvert.SerializeObject(searchPageRequest), Encoding.UTF8, "application/json"));

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}

 

