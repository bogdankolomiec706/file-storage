using System;
using System.Threading.Tasks;

namespace FileStorage.Repositories
{
    public class AppDatabase : IDisposable
    {
        private readonly AppContext context;
        public AppDatabase(AppContext context)
        {
            this.context = context;
        }

        private FileRepository fileRepo;
        public FileRepository Files => 
            fileRepo = fileRepo ?? new FileRepository(context);

        private FileCategoryRepository fileCategoryRepo;
        public FileCategoryRepository FileCategories => 
            fileCategoryRepo = fileCategoryRepo ?? new FileCategoryRepository(context);

        public Task CommitChangesAsync() => context.SaveChangesAsync();


        ~AppDatabase()
        {
            GC.SuppressFinalize(this);
            Dispose();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
