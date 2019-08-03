using FileStorage.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FileStorage
{
    public class AppContext : DbContext
    {
        public DbSet<FileInfo> Files { get; set; }
        public DbSet<FileCategory> FileCategories { get; set; }


        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureFileInfo();
            modelBuilder.ConfigureFileCategory();
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void ConfigureFileInfo(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileInfo>().HasKey(p => new { p.Level, p.SubIndex });

            modelBuilder.Entity<FileInfo>().HasIndex(p => p.FullIndex).IsUnique();
            modelBuilder.Entity<FileInfo>().Property(b => b.FullIndex).IsRequired();
            modelBuilder.Entity<FileInfo>().Property(p => p.FullIndex).HasMaxLength(FileInfo.MaxIndexLength);

            modelBuilder.Entity<FileInfo>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<FileInfo>().Property(p => p.Name).HasMaxLength(FileInfo.MaxNameLength);

            modelBuilder.Entity<FileInfo>().Property(p => p.Size).IsRequired();
        }

        public static void ConfigureFileCategory(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileCategory>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<FileCategory>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<FileCategory>().Property(p => p.Name).HasMaxLength(FileInfo.MaxCategoryNameLength);
        }
    }
}
