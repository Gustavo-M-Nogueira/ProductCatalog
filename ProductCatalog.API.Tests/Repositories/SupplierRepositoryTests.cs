using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.API.Tests.InMemoryDb;

namespace ProductCatalog.API.Tests.Repositories
{
    public class SupplierRepositoryTests
    {
        private readonly static int supplierNumber = 5;
        private readonly InMemoryDatabase _InMemoryDb;
        private static readonly List<Guid> _validGuids = Enumerable
            .Range(1, supplierNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        private static readonly List<Guid> _newGuids = Enumerable
            .Range(1, supplierNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        private static readonly List<Guid> _invalidGuids = Enumerable
            .Range(1, supplierNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        public static List<object[]> ValidSupplierIds =>
            _validGuids.Select(g => new object[] { g }).ToList();
        public static List<object[]> NewValidSupplierIds =>
            _newGuids.Select(g => new object[] { g }).ToList();
        public static List<object[]> InvalidSupplierIds =>
            _invalidGuids.Select(g => new object[] { g }).ToList();

        public SupplierRepositoryTests()
        {
            _InMemoryDb = new InMemoryDatabase();
        }

        private async Task<SupplierRepository> GetSupplierRepository()
        {
            var dbContext = await GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new SupplierRepository(fakeDbFactory);
        }

        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var dbContext = await _InMemoryDb.GetDatabaseContext();

            if (await dbContext.Suppliers.CountAsync() <= 0)
            {
                for (int i = 0; i < supplierNumber; i++)
                {
                    dbContext.Suppliers.Add(
                    new Supplier()
                    {
                        Id = (Guid)ValidSupplierIds[i][0],
                        Name = $"Name {i}",
                        Description = $"Description {i}",
                        AddressId = new Guid()
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
            return dbContext;
        }

        [Fact]
        public async Task SupplierRepository_GetAll()
        {
            var supplierRepository = await GetSupplierRepository();

            var result = await supplierRepository.GetAll();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }


        [Theory]
        [MemberData(nameof(ValidSupplierIds))]
        public async Task SupplierRepository_Find_ShouldFindById(Guid id)
        {
            var supplierRepository = await GetSupplierRepository();

            var result = await supplierRepository.Find(id);

            Assert.NotNull(result);
            Assert.IsType<Supplier>(result);
            Assert.Equal(id, result.Id);
        }


        [Theory]
        [MemberData(nameof(InvalidSupplierIds))]
        public async Task SupplierRepository_Find_ShouldNotFindWrongId(Guid id)
        {
            var supplierRepository = await GetSupplierRepository();

            var result = await supplierRepository.Find(id);

            Assert.Null(result);
        }

        [Theory]
        [InlineData
            ("New Name",
            "New Description",
            1)]
        public async Task SupplierRepository_Create
            (string newName,
            string newDescription,
            int newAddressId)
        {
            var supplier = new Supplier();

            supplier.Id = Guid.NewGuid();
            supplier.Name = newName;
            supplier.Description = newDescription;
            supplier.AddressId = Guid.NewGuid();

            var supplierRepository = await GetSupplierRepository();

            var result = await supplierRepository.Create(supplier);

            Assert.NotNull(result);
            Assert.IsType<Supplier>(result);
            Assert.IsType<Guid>(result.Id);
            Assert.Equal(supplier.Id, result.Id);
            Assert.Equal(newName, result.Name);
            Assert.Equal(newDescription, result.Description);
            Assert.Equal(supplier.AddressId, result.AddressId);
        }

        public static IEnumerable<object[]> UpdateSupplierData =>
            new List<object[]>
            {
                new object[]
                {
                    ValidSupplierIds[0][0],
                    "Updated Name",
                    "Updated Description",
                    "b9b57f40-d304-4206-a66a-566a0603bc0b"
                }
            };

        [Theory]
        [MemberData(nameof(UpdateSupplierData))]
        public async Task SupplierRepository_Update
            (Guid id,
            string newName,
            string newDescription,
            Guid newAddressId)
        {
            var dbContext = await GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            var supplierRepository = new SupplierRepository(fakeDbFactory);

            var supplier = await dbContext.Suppliers.FirstAsync(t => t.Id == id);

            string oldName = supplier.Name;
            string oldDescription = supplier.Description;
            Guid oldAddressId = supplier.AddressId;

            supplier.Name = newName;
            supplier.Description = newDescription;
            supplier.AddressId = Guid.NewGuid();

            var result = await supplierRepository.Update(supplier);

            Assert.NotNull(result);
            Assert.IsType<Supplier>(result);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(oldName, result.Name);
            Assert.NotEqual(oldDescription, result.Description);
            Assert.NotEqual(oldAddressId, result.AddressId);
            Assert.Equal(newName, result.Name);
            Assert.Equal(newDescription, result.Description);
            Assert.Equal(supplier.AddressId, result.AddressId);
        }

        [Theory]
        [MemberData(nameof(ValidSupplierIds))]
        public async Task SupplierRepository_Delete_ShouldWork(Guid id)
        {
            var supplierRepository = await GetSupplierRepository();

            bool result = await supplierRepository.Delete(id);

            Assert.True(result);
        }


        [Theory]
        [MemberData(nameof(InvalidSupplierIds))]
        public async Task SupplierRepository_Delete_ShouldNotFound(Guid id)
        {
            var supplierRepository = await GetSupplierRepository();

            bool result = await supplierRepository.Delete(id);

            Assert.False(result);
        }
    }
}
