using FileStorage.DbModels;
using FileStorage.Models;
using Xunit;

namespace FileStorage.Tests.UnitTests.Models
{
    public class FileInfoTests
    {
        [Theory]
        [InlineData(new int[] { }, "1.1", "1.1")]
        [InlineData(new[] { 1 }, "1.2", "1.2")]
        [InlineData(new[] { 1 }, "1.1", "1.2")]
        [InlineData(new[] { 1, 2, 3 }, "1.1", "1.4")]
        [InlineData(new[] { 1, 3, 5 }, "1.1", "1.2")]
        [InlineData(new[] { 1, 4, 5 }, "1.1", "1.2")]
        public void Test(int[] usedSubIndexes, string initialIndex, string changedIndex)
        {
            var file = GetFile(initialIndex);

            file.MakeIndexUniqueConsidering(usedSubIndexes);

            Assert.Equal(file.FullIndex, changedIndex);
        }

        private FileInfo GetFile(string withFullIndex)
        {
            var dto = new FileInfoDto
            {
                Index = withFullIndex,
                Name = "Name1",
                Category = "cat1",
                Size = 5
            };

            return FileInfo.From(dto, new FileCategory(dto.Category));
        }
    }
}
