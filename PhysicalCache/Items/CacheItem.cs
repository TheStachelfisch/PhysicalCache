using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace PhysicalCache.Items
{
    public class CacheItem : ICacheItem
    {
        public string Key { get; set; }

        [MaybeNull] public FileInfo File { get; set; } = null;

        [MaybeNull] public byte[] RawBytes { get; set; } = null;

        private CacheItem() { }

        private CacheItem(string key) => Key = key;

        public CacheItem(string key, [NotNull]FileInfo file) : this(key) => File = file;

        public CacheItem(string key, [NotNull]byte[] rawFileBytes) : this(key) => RawBytes = rawFileBytes;
    }
}