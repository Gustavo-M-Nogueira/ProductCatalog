using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalog.API.Tests.Repositories
{
    public class AddressRepositoryTests
    {
        private readonly static int addressNumber = 5;
        private readonly InMemoryDatabase _InMemoryDb;
        private static readonly List<Guid> _validGuids = Enumerable
            .Range(1, addressNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        private static readonly List<Guid> _newGuids = Enumerable
            .Range(1, addressNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        private static readonly List<Guid> _invalidGuids = Enumerable
            .Range(1, addressNumber)
            .Select(_ => Guid.NewGuid())
            .ToList();
        public static List<object[]> ValidAddressIds =>
            _validGuids.Select(g => new object[] { g }).ToList();
        public static List<object[]> NewValidAddressIds =>
            _newGuids.Select(g => new object[] { g }).ToList();
        public static List<object[]> InvalidAddressIds =>
            _invalidGuids.Select(g => new object[] { g }).ToList();

        public AddressRepositoryTests()
        {
            _InMemoryDb = new InMemoryDatabase();
        }

        private async Task<AddressRepository> GetAddressRepository()
        {
            var dbContext = await GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new AddressRepository(fakeDbFactory);
        }

        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var dbContext = await _InMemoryDb.GetDatabaseContext();

            if (await dbContext.Addresses.CountAsync() <= 0)
            {
                for (int i = 0; i < addressNumber; i++)
                {
                    dbContext.Addresses.Add(
                    new Address()
                    {
                        Id = (Guid)ValidAddressIds[i][0],
                        AddressLine = $"Address Line {i}",
                        Country = $"Country {i}",
                        State = $"State {i}",
                        ZipCode = $"ZipCode {i}"
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
            return dbContext;
        }

        [Fact]
        public async Task AddressRepository_GetAll()
        {
            var addressRepository = await GetAddressRepository();

            var result = await addressRepository.GetAll();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }


        [Theory]
        [MemberData(nameof(ValidAddressIds))]
        public async Task AddressRepository_Find_ShouldFindById(Guid id)
        {
            var addressRepository = await GetAddressRepository();

            var result = await addressRepository.Find(id);

            Assert.NotNull(result);
            Assert.IsType<Address>(result);
            Assert.Equal(id, result.Id);
        }


        [Theory]
        [MemberData(nameof(InvalidAddressIds))]
        public async Task AddressRepository_Find_ShouldNotFindWrongId(Guid id)
        {
            var addressRepository = await GetAddressRepository();

            var result = await addressRepository.Find(id);

            Assert.Null(result);
        }

        [Theory]
        [InlineData
            ("New Address Line",
            "New Country",
            "New State",
            "New Zip Code")]
        public async Task AddressRepository_Create
            (string addressLine,
            string country,
            string state,
            string zipCode)
        {
            var address = new Address();

            address.Id = new Guid();
            address.AddressLine = addressLine;
            address.Country = country;
            address.State = state;
            address.ZipCode = zipCode;

            var addressRepository = await GetAddressRepository();

            var result = await addressRepository.Create(address);

            Assert.NotNull(result);
            Assert.IsType<Address>(result);
            Assert.IsType<Guid>(result.Id);
            Assert.Equal(address.Id, result.Id);
            Assert.Equal(addressLine, result.AddressLine);
            Assert.Equal(country, result.Country);
            Assert.Equal(state, result.State);
            Assert.Equal(zipCode, result.ZipCode);
        }

        public static IEnumerable<object[]> UpdateAddressData =>
            new List<object[]>
            {
                new object[]
                {
                    ValidAddressIds[0][0],
                    "Updated Address Line",
                    "Updated Country",
                    "Updated State",
                    "Updated Zip Code"
                }
            };

        [Theory]
        [MemberData(nameof(UpdateAddressData))]
        public async Task AddressRepository_Update
            (Guid id, 
            string newAddressLine, 
            string newCountry, 
            string newState, 
            string newZipCode)
        {
            var dbContext = await GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            var addressRepository = new AddressRepository(fakeDbFactory);

            var address = await dbContext.Addresses.FirstAsync(t => t.Id == id);
            
            string oldAddressLine = address.AddressLine;
            string oldCountry = address.Country;
            string oldState = address.State;
            string oldZipCode = address.ZipCode;

            address.AddressLine = newAddressLine;
            address.Country = newCountry;
            address.State = newState;
            address.ZipCode = newZipCode;

            var result = await addressRepository.Update(address);

            Assert.NotNull(result);
            Assert.IsType<Address>(result);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(oldAddressLine, result.AddressLine);
            Assert.NotEqual(oldCountry, result.Country);
            Assert.NotEqual(oldState, result.State);
            Assert.NotEqual(oldZipCode, result.ZipCode);
            Assert.Equal(newAddressLine, result.AddressLine);
            Assert.Equal(newCountry, result.Country);
            Assert.Equal(newState, result.State);
            Assert.Equal(newZipCode, result.ZipCode);
        }

        [Theory]
        [MemberData(nameof(ValidAddressIds))]
        public async Task AddressRepository_Delete_ShouldWork(Guid id)
        {
            var addressRepository = await GetAddressRepository();

            bool result = await addressRepository.Delete(id);

            Assert.True(result);
        }


        [Theory]
        [MemberData(nameof(InvalidAddressIds))]
        public async Task AddressRepository_Delete_ShouldNotFound(Guid id)
        {
            var addressRepository = await GetAddressRepository();

            bool result = await addressRepository.Delete(id);

            Assert.False(result);
        }
    }
}
