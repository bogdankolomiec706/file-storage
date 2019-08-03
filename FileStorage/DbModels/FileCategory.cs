using System;
using System.Collections.Generic;

namespace FileStorage.DbModels
{
    public class FileCategory
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public ICollection<FileInfo> Files { get; set; }

        private FileCategory()
        { }

        public FileCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }
    }
}
