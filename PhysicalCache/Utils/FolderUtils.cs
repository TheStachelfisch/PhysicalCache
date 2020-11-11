using System;
using System.Diagnostics;
using System.IO;

namespace PhysicalCache.Utils
{
    public class FolderUtils
    {
        public static bool CreateHiddenFolder(string name)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(name);  
                di.Create();
                di.Attributes |= FileAttributes.Hidden;

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                
                return false;
            }
        }

        public static void EmptyFolder(DirectoryInfo directoryInfo, bool recursive = true)
        {
            foreach (var file in directoryInfo.GetFiles())
                file.Delete();
            foreach (var directory in directoryInfo.GetDirectories())
                directory.Delete(recursive);
        }
    }
}