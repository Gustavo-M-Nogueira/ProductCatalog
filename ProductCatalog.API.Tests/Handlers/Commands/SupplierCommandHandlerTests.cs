using Application.DTOs.Requests;
using Application.Services.Suppliers.Commands.CreateSupplier;
using Infrastructure.Repositories;
using ProductCatalog.API.Tests.InMemoryDb;

namespace ProductCatalog.API.Tests.Handlers.Commands
{
    public class SupplierCommandHandlerTests
    {
        private readonly InMemoryDatabase _InMemoryDb;

        public SupplierCommandHandlerTests()
        {
            _InMemoryDb = new InMemoryDatabase();
        }

        private async Task<SupplierRepository> GetSupplierRepository()
        {
            var dbContext = await _InMemoryDb.GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new SupplierRepository(fakeDbFactory);
        }
        public static IEnumerable<object[]> SupplierTestData =>
            new List<object[]>
            {
                new object[]
                {
                    "New Nmae",
                    "New Description",
                    Guid.NewGuid()
                }
            };

        [Theory]
        [MemberData(nameof(SupplierTestData))]
        public async Task CreateSupplierHandler_ShouldCreate(string name, string description, Guid addressId)
        {
            var supplier = new SupplierRequestDto
            {
                Name = name,
                Description = description,
                AddressId = addressId,
            };

            var command = new CreateSupplierCommand(supplier);
            var supplierRepository = await GetSupplierRepository();
            var handler = new CreateSupplierHandler(supplierRepository);

            var result = await handler.Handle(command, default);

            Assert.IsType<CreateSupplierResult>(result);
            Assert.NotNull(result);
            Assert.True(result.Supplier.Id != Guid.Empty);
            Assert.True(result.Supplier.Name == supplier.Name);
            Assert.True(result.Supplier.Description == supplier.Description);
            Assert.True(result.Supplier.AddressId == supplier.AddressId);
            Assert.True(name == result.Supplier.Name);
            Assert.True(description == result.Supplier.Description);
            Assert.True(addressId == result.Supplier.AddressId);
        }
    }
}
