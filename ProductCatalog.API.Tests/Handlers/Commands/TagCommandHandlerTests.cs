using Application.DTOs.Requests;
using Application.Services.Tags.Commands.CreateTag;
using Infrastructure.Repositories;
using ProductCatalog.API.Tests.InMemoryDb;

namespace ProductCatalog.API.Tests.Handlers.Commands
{
    public class TagCommandHandlerTests
    {
        private readonly InMemoryDatabase _InMemoryDb;

        public TagCommandHandlerTests()
        {
            _InMemoryDb = new InMemoryDatabase();
        }

        private async Task<TagRepository> GetTagRepository()
        {
            var dbContext = await _InMemoryDb.GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new TagRepository(fakeDbFactory);
        }

        [Theory]
        [InlineData("New Title")]
        public async Task CreateTagHandler_ShouldCreate(string title)
        {
            var tag = new TagRequestDto { Title = title };
            var command = new CreateTagCommand(tag);
            var tagRepository = await GetTagRepository();
            var handler = new CreateTagHandler(tagRepository);

            var result = await handler.Handle(command, default);

            Assert.IsType<CreateTagResult>(result);
            Assert.NotNull(result);
            Assert.True(result.Tag.Id > 0);
            Assert.True(result.Tag.Title == tag.Title);
            Assert.True(title == result.Tag.Title);
        }
    }
}
