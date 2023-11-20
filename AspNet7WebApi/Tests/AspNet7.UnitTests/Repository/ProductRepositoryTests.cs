using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using AspNet7.Infrastructure.Data;
using AspNet7.Infrastructure.Repository;
using AspNet7.UnitTests.Builders;

namespace AspNet7.UnitTests.Repository
{
    public class ProductRepositoryTests
    {
        private readonly AspNet7DataContext _aspNet5DataContext;
        private readonly ProductRepository _productRepository;
        private readonly ITestOutputHelper _output;
        private ProductBuilder ProductBuilder { get; } = new ProductBuilder();

        public ProductRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<AspNet7DataContext>()
                .UseInMemoryDatabase(databaseName: "AspNet5RepositoryTest")
                .Options;
            _aspNet5DataContext = new AspNet7DataContext(dbOptions);
            AspNet7DataSeed.SeedAsync(_aspNet5DataContext, false).Wait();
            _productRepository = new ProductRepository(_aspNet5DataContext);
        }

        [Fact]
        public async Task Test_Create_New_Product()
        {
            var newProduct = ProductBuilder.NewProductValues();
            _output.WriteLine($"New Product: {newProduct.Name}");

            var productFromRepo = await _productRepository.AddAsync(newProduct);
            Assert.True(productFromRepo.ProductId > 0);
            Assert.Equal(productFromRepo.Code, productFromRepo.Code);
            Assert.Equal(productFromRepo.Name, productFromRepo.Name);
            Assert.Equal(productFromRepo.Summary, productFromRepo.Summary);
            Assert.Equal(productFromRepo.UnitPrice, productFromRepo.UnitPrice);
            Assert.Equal(productFromRepo.UnitsInStock, productFromRepo.UnitsInStock);
        }

        [Fact]
        public async Task Test_Get_Existing_Product()
        {
            var product = _aspNet5DataContext.Products.First();
            var productId = product.ProductId;
            _output.WriteLine($"ProductId: {productId}");

            var productFromRepo = await _productRepository.GetByIdAsync(productId);
            Assert.Equal(product.ProductId, productFromRepo.ProductId);
            Assert.Equal(product.Code, productFromRepo.Code);
            Assert.Equal(product.Name, productFromRepo.Name);
            Assert.Equal(productFromRepo.Summary, productFromRepo.Summary);
            Assert.Equal(productFromRepo.UnitPrice, productFromRepo.UnitPrice);
            Assert.Equal(productFromRepo.UnitsInStock, productFromRepo.UnitsInStock);
        }

        [Fact]
        public async Task Test_Get_Product_By_Id()
        {
            var product = _aspNet5DataContext.Products.First();
            var productId = product.ProductId;
            _output.WriteLine($"ProductId: {productId}");

            var productFromRepo = await _productRepository.GetByIdAsync(productId);
            Assert.NotNull(productFromRepo);

            Assert.Equal(product.ProductId, productFromRepo.ProductId);
            Assert.Equal(product.Code, productFromRepo.Code);
            Assert.Equal(product.Name, productFromRepo.Name);
            Assert.Equal(productFromRepo.Summary, productFromRepo.Summary);
            Assert.Equal(productFromRepo.UnitPrice, productFromRepo.UnitPrice);
            Assert.Equal(productFromRepo.UnitsInStock, productFromRepo.UnitsInStock);
        }
    }
}
