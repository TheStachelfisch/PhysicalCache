using System;
using System.Collections.Generic;
using System.IO;
using PhysicalCache.Items;
using PhysicalCache.Utils;

namespace PhysicalCache
{
    public class PhysicalCache : IDisposable
    {
        private static bool _ClearOnStartUp;
        private static bool _IsFolderHidden;

        internal List<CacheItem> _cacheItems;

        public DirectoryInfo CacheDirectoryInfo { get; private set; }

        public PhysicalCache(bool isFolderHidden = true, bool clearOnStartUp = true)
        {
            _IsFolderHidden = isFolderHidden;
            _ClearOnStartUp = clearOnStartUp;

            SetupCache();
        }

        private void SetupCache()
        {
            // Can't use this.CacheDirectoryInfo yet, since it's not initialized yet
            if (!Directory.Exists(Constants.CacheFolderName))
                FolderUtils.CreateHiddenFolder(Constants.CacheFolderName);
            else if (Directory.Exists(Constants.CacheFolderName) && _ClearOnStartUp)
                FolderUtils.EmptyFolder(new DirectoryInfo(Constants.CacheFolderName));

            CacheDirectoryInfo = new DirectoryInfo(Constants.CacheFolderName);
        }

        public void Add(CacheItem item)
        {
            if (Contains(item.Key)) return;

            if (item.RawBytes != null)
            {
                File.WriteAllBytes($"{Constants.CacheFolderName}/{item.Key}", item.RawBytes);
                _cacheItems.Add(item);
                return;
            }

            item.File?.MoveTo($"{Constants.CacheFolderName}/{item.Key}");
        }

        public CacheItem Get(string key)
        {
            if (key == null) throw new ArgumentException(nameof(key));

            foreach (var file in CacheDirectoryInfo.GetFiles())
                if (file.Name == key)
                    return new CacheItem(key, file);

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

        public bool Contains(CacheItem item)
        {
            return Contains(item.Key);
        }

        public void Dispose()
        {
            FolderUtils.EmptyFolder(CacheDirectoryInfo);
        }
    }
}