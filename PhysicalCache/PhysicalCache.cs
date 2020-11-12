using System;
using System.Collections.Generic;
using System.IO;
using PhysicalCache.Items;
using PhysicalCache.Utils;

namespace PhysicalCache
{
    public class PhysicalCache : IDisposable
    {
        internal List<ICacheItem> _cacheItems;

        public DirectoryInfo CacheDirectoryInfo { get; private set; }

        public PhysicalCache() => SetupCache();


        private void SetupCache()
        {
            // Can't use this.CacheDirectoryInfo yet, since it's not initialized yet
            if (!Directory.Exists(Constants.CacheFolderName))
                FolderUtils.CreateHiddenFolder(Constants.CacheFolderName);
            else
                FolderUtils.EmptyFolder(new DirectoryInfo(Constants.CacheFolderName));

            CacheDirectoryInfo = new DirectoryInfo(Constants.CacheFolderName);
        }

        internal string GenerateDirectory(string key) => $"{Constants.CacheFolderName}/{key}";

        internal string GenerateDirectory(CacheItem item) => GenerateDirectory(item.Key);

        public FileInfo AddAndGet(CacheItem item, bool moveItem = true)
        {
            if (Contains(item.Key))
                foreach (var file in CacheDirectoryInfo.GetFiles())
                    if (file.Name == item.Key)
                        return new FileInfo(file.FullName);

            string directory = GenerateDirectory(item);
            
            if (item.RawBytes != null)
            {
                File.WriteAllBytes(directory, item.RawBytes);
                _cacheItems.Add(item);
                return new FileInfo(directory);
            }

            if (moveItem)
                item.File?.MoveTo(directory);
            else
                item.File.CopyTo(directory);

            return new FileInfo(directory);
        }

        public void Add(CacheItem item, bool moveItem = true) => AddAndGet(item, moveItem);

        public FileInfo Get(string key)
        {
            if (key == null) throw new ArgumentException(nameof(key));

            foreach (var file in CacheDirectoryInfo.GetFiles())
                if (file.Name == key)
                    return new FileInfo(file.FullName);

            return null;
        }

        public bool Contains(string key)
        {
            if (key == null) throw new ArgumentException(nameof(key));

            foreach (var file in CacheDirectoryInfo.GetFiles())
                if (file.Name == key)
                    return true;

            return false;
        }

        public bool Contains(CacheItem item) => Contains(item.Key);

        public void Dispose()
        {
            FolderUtils.EmptyFolder(CacheDirectoryInfo);
        }
    }
}