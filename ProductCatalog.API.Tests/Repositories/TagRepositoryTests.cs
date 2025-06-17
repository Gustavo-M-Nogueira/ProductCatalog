using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Tag = Domain.Entities.Tag;

namespace ProductCatalog.API.Tests.Repositories
{
    public class TagRepositoryTests
    {
        private readonly static int tagsNumber = 5;
        private readonly InMemoryDatabase _InMemoryDb;
        public static IEnumerable<object[]> ValidTagsIds => Enumerable.Range(1, tagsNumber).Select(id => new object[] { id });
        public static IEnumerable<object[]> NewValidTagsIds => Enumerable.Range(11, 10).Select(id => new object[] { id });
        public static IEnumerable<object[]> InvalidTagsIds => Enumerable.Range(-10, 10).Select(id => new object[] { id });

        public TagRepositoryTests()
        {
            _InMemoryDb = new InMemoryDatabase();    
        }

        private async Task<TagRepository> GetTagRepository()
        {
            var dbContext = await GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            return new TagRepository(fakeDbFactory);
        } 

        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var dbContext = await _InMemoryDb.GetDatabaseContext();

            if (await dbContext.Tags.CountAsync() <= 0)
            {
                for (int i = 0; i < tagsNumber; i++)
                {
                    dbContext.Tags.Add(
                    new Tag()
                    {
                        Title = $"Tag {i}"
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
            return dbContext;
        }


        [Fact]
        public async Task TagRepository_GetAll()
        {
            var tagRepository = await GetTagRepository();

            var result = await tagRepository.GetAll();

            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
        }


        [Theory]
        [MemberData(nameof(ValidTagsIds))]
        public async Task TagRepository_Find_ShouldFindById(int id)
        {
            var tagRepository = await GetTagRepository();

            var result = await tagRepository.Find(id);

            Assert.NotNull(result);
            Assert.IsType<Tag>(result);
            Assert.Equal(id, result.Id);
        }


        [Theory]
        [MemberData(nameof(InvalidTagsIds))]
        public async Task TagRepository_Find_ShouldNotFindWrongId(int id)
        {
            var tagRepository = await GetTagRepository();

            var result = await tagRepository.Find(id);

            Assert.Null(result);
        }


        [Theory]
        [InlineData(6, "New Tag")]
        public async Task TagRepository_Create(int id, string title)
        {
            var tag = new Tag();
            tag.Id = id;
            tag.Title = title;
            var tagRepository = await GetTagRepository();

            var result = await tagRepository.Create(tag);

            Assert.NotNull(result);
            Assert.IsType<Tag>(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(title, result.Title);
        }


        [Theory]
        [InlineData(1, "New Title")]
        public async Task TagRepository_Update(int id, string newTitle)
        {
            var dbContext = await GetDatabaseContext();
            var fakeDbFactory = new FakeDbContextFactory(dbContext);
            var tagRepository = new TagRepository(fakeDbFactory);

            var tag = await dbContext.Tags.FirstAsync(t => t.Id == id);
            string oldTitle = tag.Title;
            tag.Title = newTitle;
            var result = await tagRepository.Update(tag);

            Assert.NotNull(result);
            Assert.IsType<Tag>(result);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(oldTitle, result.Title);
            Assert.Equal(newTitle, result.Title);
        }

        [Theory]
        [MemberData(nameof(ValidTagsIds))]
        public async Task TagRepository_Delete_ShouldWork(int id)
        {
            var tagRepository = await GetTagRepository();

            bool result = await tagRepository.Delete(id);

            Assert.True(result);
        }


        [Theory]
        [MemberData(nameof(InvalidTagsIds))]
        [MemberData(nameof(NewValidTagsIds))]
        public async Task TagRepository_Delete_ShouldNotFound(int id)
        {
            var tagRepository = await GetTagRepository();

            bool result = await tagRepository.Delete(id);

            Assert.False(result);
        }
    }
}
