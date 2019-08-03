using FileStorage.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Repositories
{
    public class FileCategoryRepository
    {
        private readonly AppContext _db;
        public FileCategoryRepository(AppContext db)
        {
            _db = db;
        }

        public async Task<IList<FileCategory>> GetCategoriesBy(IEnumerable<string> names)
        {
            return await _db.FileCategories
                .Where(c => names.Contains(c.Name))
                .ToListAsync();
        }

        public async Task<IEnumerable<FileCategory>> SaveCategoriesWith(IEnumerable<string> names)
        {
            var categories = names.Select(name => new FileCategory(name)).ToList();
            await _db.FileCategories.AddRangeAsync(categories);

            return categories;
        }
    }
}
