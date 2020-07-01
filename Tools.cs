using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CIAInfo
{
    internal static class Tools
    {
        //does needed work after app launch
        public static string[] Initialize()
        {
            //checking for directories needed to run app, and creating them if they do not exist
            for (int i = 0; i < RequiredDirectories.Length; i++)
            {
                if (!Directory.Exists(RequiredDirectories[i]))
                {
                    Directory.CreateDirectory(RequiredDirectories[i]);
                }
            }

            //checking if required files exist

            if (!File.Exists(RequiredFiles[0]))
            {
                throw new FileNotFoundException("The ninfs executable was not found!\nPlease download the Windows (.exe) version of ninfs from github.com/ihaveamac/ninfs and put it inside the ninfs folder.");
            }
            else if (!File.Exists(RequiredFiles[1]))
            {
                throw new FileNotFoundException("ARM9 Boot rom not found! Please dump it from your 3DS and put it inside the \"3ds\" folder in AppData.");
            }
            else if (!File.Exists(RequiredFiles[2]))
            {
                throw new FileNotFoundException("No seeddb.bin found in the ninfs folder.");
            }
            string[] ciaPaths = Directory.GetFiles(RequiredDirectories[0], "*.cia");

            if (ciaPaths.Length < 1)
            {
                throw new FileNotFoundException("Error: No CIAs found. Please put at least one CIA in the folder \"Put CIAs Here\".");
            }

            return ciaPaths;
        }

        public static string[] RequiredDirectories =
        {
            $"{Environment.CurrentDirectory}\\Put CIAs here",
            $"{Environment.CurrentDirectory}\\ninfs"
        };

        public static string[] RequiredFiles =
        {
            $"{Environment.CurrentDirectory}\\ninfs\\ninfs.exe",
            $"{Environment.GetEnvironmentVariable("userprofile")}\\AppData\\Roaming\\3ds\\boot9.bin",
            $"{Environment.GetEnvironmentVariable("userprofile")}\\AppData\\Roaming\\3ds\\seeddb.bin"
        };

        //reads any file from a given start and end hex offset
        public static string ReadHexUTF16(string path, Int32 startOffset, Int32 endOffset, bool decode)
        {
            if (!File.Exists(path)) { throw new FileNotFoundException($"Could not find {path}"); }

            string result = "";

            List<byte> bytes = new List<byte>();
            BinaryReader reader = new BinaryReader(File.OpenRead(path));
            reader.BaseStream.Position = startOffset;

            for (int i = startOffset; i < endOffset - 0x3F; i++)
            {
                bytes.Add(reader.ReadByte());
            }

            byte[] c = bytes.ToArray();

            if (decode)
            {
                for (int i = 0; i < c.Length - 1; i += 2)
                {
                    if (c[i] == 0 && c[i + 1] == 0)
                    {
                        break;
                    }
                    result += BitConverter.ToChar(c, i);
                }
            }
            else
            {
                foreach (byte b in bytes)
                {
                    result += b.ToString("X2");
                }
            }
            reader.Close();
            return result;
        }

        public static string ReadHexUTF8(string path, Int32 startOffset, Int32 endOffset, bool decode)
        {
            if (!File.Exists(path)) { throw new FileNotFoundException($"Could not find {path}"); }

            string result = "";

            List<byte> bytes = new List<byte>();
            BinaryReader reader = new BinaryReader(File.OpenRead(path));
            reader.BaseStream.Position = startOffset;

            for (int i = startOffset; i < endOffset; i++)
            {
                bytes.Add(reader.ReadByte());
            }

            if (decode)
            {
                result = Encoding.UTF8.GetString(bytes.ToArray());
            }
            else
            {
                foreach (byte b in bytes)
                {
                    result += b.ToString("X2");
                }
            }
            reader.Close();
            return result;
        }

        //returns a string array of the available drive letters. example: "A:"
        public static string GetAvailableDriveLetter()
        {
            var availableDrives = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i + ":").Except(DriveInfo.GetDrives().Select(s => s.Name.Replace("\\", ""))).Cast<string>().ToArray();
            return availableDrives[0];
        }

        //returns the first folder of a directory
        public static string MinNumFolder(string path, bool withPath)
        {
            string result;
            string[] dirs = Directory.GetDirectories(path);
            if (dirs.Length == 1)
            {
                return dirs[0];
            }
            Array.Sort(dirs);

            if (!withPath)
            {
                result = new DirectoryInfo(dirs[0]).Name;
            }
            else
            {
                result = dirs[0];
            }
            return result;
        }

        public static string GetSize(int sizeInBytes)
        {
            decimal size = sizeInBytes / 1024;
            return $"{size} KB | {Math.Round(size / 128)} blocks (estimated)";
        }

        public static void KillNinfs()
        {
            foreach (Process prs in Process.GetProcessesByName("ninfs"))
            {
                prs.Kill();
            }
        }
    }
}