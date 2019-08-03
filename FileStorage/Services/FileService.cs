using FileStorage.DbModels;
using FileStorage.Models;
using FileStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Services
{
    public class FileService: IDisposable
    {
        private readonly AppDatabase db;
        public FileService(AppDatabase db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<FileInfoDto>> GetAllFilesAsync()
        {
            var list = await db.Files.GetAllAsync();
            return list
                .OrderBy(f => f.Level).ThenBy(f => f.SubIndex)
                .Select(f => f.ToDto());
        }

        public async Task SaveAsync(IEnumerable<FileInfoDto> newDtoFiles)
        {
            var categories = await GetOrCreateCategoriesFor(newDtoFiles);

            var newFiles = newDtoFiles.Select(dto => 
            {
                var withCategory = categories.Single(c => c.Name == dto.Category);
                return FileInfo.From(dto, withCategory);
            }).ToList();

            await MakeIndexesUniqueFor(newFiles);
            await db.Files.SaveAsync(newFiles);

            await db.CommitChangesAsync();
        }
        private async Task<IEnumerable<FileCategory>> GetOrCreateCategoriesFor(IEnumerable<FileInfoDto> newDtoFiles)
        {
            var categoryNames = newDtoFiles.Select(f => f.Category).Distinct();
            var existedCategories = await db.FileCategories.GetCategoriesBy(categoryNames);

            var namesNotPresentInDb = categoryNames.Except(existedCategories.Select(c => c.Name));
            var savedCategories = await db.FileCategories.SaveCategoriesWith(namesNotPresentInDb);

            return existedCategories.Union(savedCategories);
        }
        private async Task MakeIndexesUniqueFor(IList<FileInfo> newFiles)
        {
            var levelsOfNewFiles = newFiles.Select(f => f.Level).Distinct();
            var dbFilesInLevels = await db.Files.GetFilesInLevelsAsync(levelsOfNewFiles);

            foreach (var newFile in newFiles)
            {
                var usedSubIndexesInLevel = dbFilesInLevels.Union(newFiles).Except(new[] { newFile })
                    .Where(f => f.Level == newFile.Level)
                    .Select(f => f.SubIndex);

                newFile.MakeIndexUniqueConsidering(usedSubIndexesInLevel);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
