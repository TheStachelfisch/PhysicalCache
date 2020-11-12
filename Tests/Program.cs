using System;
using System.IO;
using PhysicalCache;
using PhysicalCache.Items;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var cache = new PhysicalCache.PhysicalCache();

            Console.WriteLine(((DateTimeOffset)cache.Get("client")?.CreationTimeUtc).ToUnixTimeMilliseconds());
        }
    }
}