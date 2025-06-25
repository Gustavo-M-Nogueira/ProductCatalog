using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Tests.InMemoryDb;

namespace ProductCatalog.API.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly static int productNumber = 5;
        private readonly InMemoryDatabase _InMemoryDb;
        private static readonly List<Guid> _validGuids = Enumerable
            .Range(1, productNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        private static readonly List<Guid> _newGuids = Enumerable
            .Range(1, productNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        private static readonly List<Guid> _invalidGuids = Enumerable
            .Range(1, productNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        public static List<object[]> ValidProductIds =>
            _validGuids.Select(g => new object[] { g }).ToList();
        public static List<object[]> NewValidProductIds =>
            _newGuids.Select(g => new object[] { g }).ToList();
        public static List<object[]> InvalidProductIds =>
            _invalidGuids.Select(g => new object[] { g }).ToList();

        public ProductRepositoryTests()
        {
            _InMemoryDb = new InMemoryDatabase();
        }

        private async Task<ProductRepository> GetProductRepository()
        {
            var dbContext = await GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new ProductRepository(fakeDbFactory);
        }

        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var dbContext = await _InMemoryDb.GetDatabaseContext();

            if (await dbContext.Products.CountAsync() <= 0)
            {
                for (int i = 0; i < productNumber; i++)
                {
                    dbContext.Products.Add(
                    new Product()
                    {
                        Id = (Guid)ValidProductIds[i][0],
                        Title = $"Title {i}",
                        Price = i,
                        Description = $"Description {i}",
                        StockQuantity = i,
                        CategoryId = i,
                        SupplierId = new Guid()
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
            return dbContext;
        }

        [Fact]
        public async Task ProductRepository_GetAll()
        {
            var productRepository = await GetProductRepository();

            var result = await productRepository.GetAll();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }


        [Theory]
        [MemberData(nameof(ValidProductIds))]
        public async Task ProductRepository_Find_ShouldFindById(Guid id)
        {
            var productRepository = await GetProductRepository();

            var result = await productRepository.Find(id);

            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(id, result.Id);
        }


        [Theory]
        [MemberData(nameof(InvalidProductIds))]
        public async Task ProductRepository_Find_ShouldNotFindWrongId(Guid id)
        {
            var productRepository = await GetProductRepository();

            var result = await productRepository.Find(id);

            Assert.Null(result);
        }

        [Theory]
        [InlineData
            ("New Title",
            10.10,
            "New Description",
            10,
            1)]
        public async Task ProductRepository_Create
            (string newTitle,
            decimal newPrice,
            string newDescription,
            int newStockQuantity,
            int newCategoryId)
        {
            var product = new Product();

            product.Id = Guid.NewGuid();
            product.Title = newTitle;
            product.Price = newPrice;
            product.Description = newDescription;
            product.StockQuantity = newStockQuantity;
            product.CategoryId = newCategoryId;
            product.SupplierId = Guid.NewGuid();

            var productRepository = await GetProductRepository();

            var result = await productRepository.Create(product);

            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.IsType<Guid>(result.Id);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(newTitle, result.Title);
            Assert.Equal(newPrice, result.Price);
            Assert.Equal(newDescription, result.Description);
            Assert.Equal(newStockQuantity, result.StockQuantity);
            Assert.Equal(newCategoryId, result.CategoryId);
            Assert.Equal(product.SupplierId, result.SupplierId);
        }

        public static IEnumerable<object[]> UpdateProductData =>
            new List<object[]>
            {
                new object[]
                {
                    ValidProductIds[0][0],
                    "Updated Title",
                    20.99,
                    "Updated Description",
                    13,
                    2
                }
            };

        [Theory]
        [MemberData(nameof(UpdateProductData))]
        public async Task ProductRepository_Update
            (Guid id,
            string newTitle,
            decimal newPrice,
            string newDescription,
            int newStockQuantity,
            int newCategoryId)
        {
            var dbContext = await GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            var productRepository = new ProductRepository(fakeDbFactory);

            var product = await dbContext.Products.FirstAsync(t => t.Id == id);

            string oldTitle = product.Title;
            decimal oldPrice = product.Price;
            string oldDescription = product.Description;
            int oldStockQuantity = product.StockQuantity;
            int oldCategoryId = product.CategoryId;
            Guid oldSupplierId = product.SupplierId;

            product.Title = newTitle;
            product.Price = newPrice;
            product.Description = newDescription;
            product.StockQuantity = newStockQuantity;
            product.CategoryId = newCategoryId;
            product.SupplierId = Guid.NewGuid();

            var result = await productRepository.Update(product);

            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(oldTitle, result.Title);
            Assert.NotEqual(oldPrice, result.Price);
            Assert.NotEqual(oldDescription, result.Description);
            Assert.NotEqual(oldStockQuantity, result.StockQuantity);
            Assert.NotEqual(oldCategoryId, result.CategoryId);
            Assert.NotEqual(oldSupplierId, result.SupplierId);
            Assert.Equal(newTitle, result.Title);
            Assert.Equal(newPrice, result.Price);
            Assert.Equal(newDescription, result.Description);
            Assert.Equal(newStockQuantity, result.StockQuantity);
            Assert.Equal(newCategoryId, result.CategoryId);
            Assert.Equal(product.SupplierId, result.SupplierId);
        }

        [Theory]
        [MemberData(nameof(ValidProductIds))]
        public async Task ProductRepository_Delete_ShouldWork(Guid id)
        {
            var productRepository = await GetProductRepository();

            bool result = await productRepository.Delete(id);

            Assert.True(result);
        }


        [Theory]
        [MemberData(nameof(InvalidProductIds))]
        public async Task ProductRepository_Delete_ShouldNotFound(Guid id)
        {
            var productRepository = await GetProductRepository();

            bool result = await productRepository.Delete(id);

            Assert.False(result);
        }
    }
}
