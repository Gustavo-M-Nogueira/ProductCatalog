using Application.DTOs.Requests;
using Application.Services.Categories.Commands.CreateCategory;
using Application.Services.Products.Commands.CreateProduct;
using Infrastructure.Repositories;
using ProductCatalog.API.Tests.InMemoryDb;

namespace ProductCatalog.API.Tests.Handlers.Commands
{
    public class ProductCommandHandlerTests
    {
        private readonly InMemoryDatabase _InMemoryDb;

        public ProductCommandHandlerTests()
        {
            _InMemoryDb = new InMemoryDatabase();
        }

        private async Task<ProductRepository> GetProductRepository()
        {
            var dbContext = await _InMemoryDb.GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new ProductRepository(fakeDbFactory);
        }

        public static IEnumerable<object[]> ProductTestData =>
        new List<object[]>
        {
            new object[]
            {
                "New Title",
                10.99,
                "New Description",
                8,
                1,
                Guid.NewGuid(),
                new List<int> { 1, 2, 3 }
            }
        };

        [Theory]
        [MemberData(nameof(ProductTestData))]
        public async Task CreateCategoryHandler_ShouldCreate(
            string title, 
            decimal price, 
            string description,
            int stockQuantity,
            int categoryId,
            Guid supplierId,
            List<int> tagIds)
        {
            var product = new ProductRequestDto 
            { 
                Title = title,
                Price = price,
                Description = description,
                StockQuantity = stockQuantity,
                CategoryId = categoryId,
                SupplierId = supplierId,
                TagIds = tagIds
            };

            var command = new CreateProductCommand(product);
            var productRepository = await GetProductRepository();
            var handler = new CreateProductHandler(productRepository);

            var result = await handler.Handle(command, default);

            Assert.IsType<CreateProductResult>(result);
            Assert.NotNull(result);
            Assert.True(result.Product.Id != Guid.Empty);
            Assert.True(result.Product.Title == product.Title);
            Assert.True(title == result.Product.Title);
        }
    }
}
