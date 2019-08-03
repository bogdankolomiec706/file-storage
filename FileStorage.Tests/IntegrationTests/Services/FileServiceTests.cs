using FileStorage.Models;
using FileStorage.Repositories;
using FileStorage.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FileStorage.Tests.IntegrationTests.Services
{
    public class FileServiceTests : IDisposable
    {
        FileService _fileServ;
        public FileServiceTests()
        {
            var context = SetupAppContextWithSqlLite();
            _fileServ = new FileService(new AppDatabase(context));
        }

        [Fact]
        public async Task GetAllFilesAsync_WithEmptyInput_NothingSaved()
        {
            await _fileServ.SaveAsync(new List<FileInfoDto>());

            var filesFromDb = await _fileServ.GetAllFilesAsync();

            Assert.Empty(filesFromDb);
        }

        [Fact]
        public async Task GetAllFilesAsync_SavesDataCorrectly()
        {
            var newFile = new FileInfoDto
            {
                Index = "1.1",
                Name = "Name1.1",
                Category = "cat1",
                Size = 4
            };
            await _fileServ.SaveAsync(new List<FileInfoDto> { newFile });

            var filesFromDb = await _fileServ.GetAllFilesAsync();

            Assert.Single(filesFromDb);
            var fileFromDb = filesFromDb.Single();
            Assert.True(AreObjectsEqualByValue(newFile, fileFromDb));
        }

        [Fact]
        public async Task GetAllFilesAsync_WithDuplicatedIndexFromInput_SavedIndexesMadeUnique()
        {
            await _fileServ.SaveAsync(new List<FileInfoDto>
            {
                new FileInfoDto
                {
                    Index = "1.1",
                    Name = "Name1.1",
                    Category = "cat1",
                    Size = 4
                },
                new FileInfoDto
                {
                    Index = "1.1",
                    Name = "Name1.2",
                    Category = "cat2",
                    Size = 4
                },
            });

            var filesFromDb = await _fileServ.GetAllFilesAsync();

            var firstFile = filesFromDb.First();
            var lastFile = filesFromDb.Last();

            Assert.True(firstFile.Index != lastFile.Index);
        }

        [Fact]
        public async Task GetAllFilesAsync_WithDuplicatedIndexInDatabase_SavedIndexesMadeUnique()
        {
            await _fileServ.SaveAsync(new List<FileInfoDto>
            {
                new FileInfoDto
                {
                    Index = "1.1",
                    Name = "Name1.1",
                    Category = "cat1",
                    Size = 4
                }
            });
            await _fileServ.SaveAsync(new List<FileInfoDto>
            {
                new FileInfoDto
                {
                    Index = "1.1",
                    Name = "Name1.1",
                    Category = "cat1",
                    Size = 4
                }
            });


            var filesFromDb = await _fileServ.GetAllFilesAsync();

            var firstFile = filesFromDb.First();
            var lastFile = filesFromDb.Last();

            Assert.True(firstFile.Index != lastFile.Index);
        }


        private static AppContext SetupAppContextWithSqlLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var dbContextOptions = new DbContextOptionsBuilder<AppContext>()
                .UseSqlite(connection)
                .Options;

            var context = new AppContext(dbContextOptions);
            context.Database.EnsureCreated();

            return context;
        }
        private bool AreObjectsEqualByValue(object obj1, object obj2) =>
            JsonConvert.SerializeObject(obj1) == JsonConvert.SerializeObject(obj2);

        public void Dispose()
        {
            _fileServ.Dispose();
        }
    }
}
