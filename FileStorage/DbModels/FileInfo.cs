using FileStorage.Models;
using System.Collections.Generic;
using System.Linq;

namespace FileStorage.DbModels
{
    public class FileInfo
    {
        public int Level { get; private set; }
        public int SubIndex { get; private set; }

        public string FullIndex { get; private set; }
        public string Name { get; private set; }
        public long Size { get; private set; }

        public FileCategory Category { get; private set; }

        public void MakeIndexUniqueConsidering(IEnumerable<int> usedSubIndexes)
        {
            SubIndex = FindFreeSubIndexFor(usedSubIndexes, SubIndex);
            UpdateIndexWith(SubIndex);
        }
        private void UpdateIndexWith(int subIndex)
        {
            var parts = FullIndex.Split('.');
            parts[parts.Length - 1] = subIndex.ToString();
            FullIndex = string.Join('.', parts);
        }
        private static int FindFreeSubIndexFor(IEnumerable<int> used, int withCurrent)
        {
            if (used == null || !used.Any())
                return withCurrent;

            var max = used.Max();
            if (withCurrent > max)
                return withCurrent;

            var free = Enumerable.Range(1, max).Except(used);
            return free.Any() ? free.First() : max + 1;
        }

        private FileInfo() { }
        private FileInfo(string index, string name, long size, FileCategory category)
        {
            Name = name;
            Size = size;
            Category = category;

            FullIndex = index;
            Level = GetLevel();
            SubIndex = GetSubIndex();
        }
        private int GetLevel() => FullIndex.Split('.').Count() - 1;
        private int GetSubIndex() => int.Parse(FullIndex.Split('.').Last());

        public static FileInfo From(FileInfoDto dto, FileCategory category) =>
            new FileInfo(dto.Index, dto.Name, dto.Size, category);

        public const int MaxIndexLength = 500;
        public const int MaxNameLength = 255;
        public const int MaxCategoryNameLength = 255;
    }


    public static class FileInfoExtensions
    {
        public static FileInfoDto ToDto(this FileInfo fileInfo)
        {
            return new FileInfoDto
            {
                Index = fileInfo.FullIndex,
                Name = fileInfo.Name,
                Size = fileInfo.Size,
                Category = fileInfo.Category.Name
            };
        }
    }
}
