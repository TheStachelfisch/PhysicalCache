using System.IO;

namespace PhysicalCache.Items
{
    public interface ICacheItem
    {
        /// <summary>
        /// The key the CacheItem should be addressed with
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// The File to be cached
        /// </summary>
        public FileInfo File { get; set; }
        
        /// <summary>
        /// The Raw bytes that should represent the file
        /// </summary>
        public byte[] RawBytes { get; set; }
    }
}