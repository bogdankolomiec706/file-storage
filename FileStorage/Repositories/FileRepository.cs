using FileStorage.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Repositories
{
    public class FileRepository
    {
        private readonly AppContext _db;
        internal FileRepository(AppContext db)
        {
            _db = db;
        }

        public async Task<IList<FileInfo>> GetAllAsync()
        {
            return await _db.Files.AsNoTracking()
                .Include(f => f.Category)
                .ToListAsync();
        }

        public Task<List<FileInfo>> GetFilesInLevelsAsync(IEnumerable<int> levels)
        {
            return _db.Files.AsNoTracking()
                .Where(f => levels.Contains(f.Level)).ToListAsync();
        }

        public async Task SaveAsync(IList<FileInfo> files)
        {
            await _db.Files.AddRangeAsync(files);
        }
    }
}
