using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CTRInfo
{
    internal static class Tools
    {
        public static string ThreeDsFolder = Environment.OSVersion.Platform == (PlatformID)4 ? $"{Environment.GetEnvironmentVariable("HOME")}/3ds/" : $"{Environment.GetEnvironmentVariable("userprofile")}/AppData/Roaming/3ds/";
        public static string[] RequiredFiles = { "boot9.bin", "seeddb.bin" };

        public static bool CanRunNinfs()
        {
            foreach (string file1 in RequiredFiles)
            {
                Console.WriteLine($"{ThreeDsFolder}{file1}");
            }
            
            if (RequiredFiles.Any(file => !File.Exists($"{ThreeDsFolder}{file}")))
            {
                return false;
            }

            return true;
        }

        //returns the first folder of a directory
        public static string MinNumFolder(string path, bool withPath)
        {
            DirectoryInfo[] dirs = new DirectoryInfo(path).GetDirectories();
            return withPath ? dirs[0].FullName : dirs[0].Name;
        }

        public static string CleanName(string nameToClean) => nameToClean.Replace('\u0000', ' ').Trim().Replace(":", " -").Replace("/", "-").Replace("\\", "-");

        public static void KillNinfs()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                foreach (Process prs in Process.GetProcessesByName("mount_cia"))
                {
                    prs.Kill();
                }
            }
            else
            {
                foreach (Process prs in Process.GetProcessesByName("ninfs"))
                {
                    prs.Kill();
                }
            }
            
        }
    }
}