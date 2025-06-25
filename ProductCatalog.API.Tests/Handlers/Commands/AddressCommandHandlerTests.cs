using Application.DTOs.Requests;
using Application.Services.Addresses.Commands.CreateAddress;
using Infrastructure.Repositories;
using ProductCatalog.API.Tests.InMemoryDb;

namespace ProductCatalog.API.Tests.Handlers.Commands
{
    public class AddressCommandHandlerTests
    {
        private readonly InMemoryDatabase _InMemoryDb;

        public AddressCommandHandlerTests()
        {
            _InMemoryDb = new InMemoryDatabase();
        }

        private async Task<AddressRepository> GetAddressRepository()
        {
            var dbContext = await _InMemoryDb.GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new AddressRepository(fakeDbFactory);
        }

        [Theory]
        [InlineData("New Address Line", "New Country", "New State", "New Zip Code")]
        public async Task CreateAddressHandler_ShouldCreate(
            string addressLine,
            string country,
            string state,
            string zipCode)
        {
            var address = new AddressRequestDto
            {
                AddressLine = addressLine,
                Country = country,
                State = state,
                ZipCode = zipCode
            };

            var command = new CreateAddressCommand(address);
            var addressRepository = await GetAddressRepository();
            var handler = new CreateAddressHandler(addressRepository);

            var result = await handler.Handle(command, default);

            Assert.IsType<CreateAddressResult>(result);
            Assert.NotNull(result);
            Assert.True(result.Address.Id != Guid.Empty);
            Assert.True(result.Address.AddressLine == address.AddressLine);
            Assert.True(result.Address.Country == address.Country);
            Assert.True(result.Address.State == address.State);
            Assert.True(result.Address.ZipCode == address.ZipCode);
            Assert.True(addressLine == result.Address.AddressLine);
            Assert.True(country == result.Address.Country);
            Assert.True(state == result.Address.State);
            Assert.True(zipCode == result.Address.ZipCode);
        }
    }
}
