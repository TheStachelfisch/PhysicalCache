using System;
using PhysicalCache;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var cache = new PhysicalCache.PhysicalCache(clearOnStartUp:false);

            Console.WriteLine(cache.Get("client")?.File.FullName);
        }
    }
}