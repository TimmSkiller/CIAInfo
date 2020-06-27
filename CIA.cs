using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CIAInfo
{
    internal class CIA
    {
        public string TID { get; }
        public string PID { get; }
        public string Version { get; }
        public string Region { get; }
        public string FileType { get; }
        public string FilePath { get; }
        public string FileSize { get; }
        public string ShortN { get; }
        public string LongN { get; }
        public string Pub { get; }

        public CIA(string[] data)
        {
            TID = data[1];
            PID = data[0];
            Version = data[2];
            Region = CIARegion(PID);
            FileType = CIAType(PID);
            ShortN = data[3];
            LongN = data[4];
            Pub = data[5];
            FilePath = new DirectoryInfo(data[6]).Name;
            FileSize = Tools.GetSize((int)new FileInfo(data[6]).Length);
        }

        public static string CIAType(string productId)
        {
            char model = productId[0];
            string result = "";

            switch (model)
            {
                case 'C':
                    result += "Original 3DS | ";
                    break;

                case 'K':
                    result += "New 3DS or Virtual Console | ";
                    break;
            }

            result += productId.Split('-')[1] switch
            {
                "P" => "Game data",
                "U" => "Update data",
                "M" => "DLC (Downloadable Content)",
                "N" => "Game data/Misc",
                _ => "Unknown",
            };
            return result;
        }

        public static string CIARegion(string productId)
        {
            string result = productId.Split('-')[2][3] switch
            {
                'P' => "Europe (E)",
                'E' => "North America (U)",
                'J' => "Japan (J)",
                'K' => "Korea (K)",
                'W' => "China (CN)",
                'Z' => "Australia (Z)",
                'A' => "Global (Region Free) (A or W)",
                _ => "Unknown (UNK)",
            };
            return result;
        }

        public static string[] Read(string path)
        {
            List<string> data;
            string ciaPath = path;
            string mountPoint = Tools.GetAvailableDriveLetter();
            string ninfsPath = Tools.AppPaths[2];

            //paths that will be defined after CIA is mounted

            string gamedata;
            string NcchPath;
            string TmdPath;
            string TicketPath;
            string IconPath;

            Console.WriteLine($"Mounting {new DirectoryInfo(ciaPath).Name}...");

            //starting ninfs, mounting CIA and reading data
            Process ninfs = new Process();

            ninfs.StartInfo.FileName = ninfsPath;
            ninfs.StartInfo.Arguments = $"cia \"{ciaPath}\" {mountPoint}";
            ninfs.StartInfo.UseShellExecute = false;
            ninfs.StartInfo.CreateNoWindow = true;
            ninfs.Start();

            mountPoint += "\\";

            while (true)
            {
                if (DriveInfo.GetDrives().Any(c => c.Name == mountPoint))
                {
                    break;
                }
            }
            gamedata = "";
            data = new List<string>();

            //mounted paths

            try
            {
                gamedata = Tools.MinNumFolder(mountPoint, true);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("\nError: No game data found inside mounted CIA.\nPlease make sure that your seeddb.bin is up to date and contains the seed required for the game you are trying to mount.");
                KillNinfs();
                Environment.Exit(0);
            }

            NcchPath = $"{gamedata}\\ncch.bin";
            TmdPath = $"{mountPoint}\\tmd.bin";
            TicketPath = $"{mountPoint}\\ticket.bin";
            IconPath = $"{gamedata}\\exefs\\icon.bin";

            //Product Code (ncch.bin)
            try
            {
                data.Add(Tools.ReadHexUTF8(NcchPath, 0x150, 0x160, true));
            }
            catch (FileNotFoundException)
            {
                data.Add("Could not find game executable CXI in CIA");
            }

            //Title ID (ticket.bin)
            try
            {
                data.Add(Tools.ReadHexUTF8(TicketPath, 0x1DC, 0x1E4, false));
            }
            catch (FileNotFoundException)
            {
                data.Add("Could not find Ticket in CIA");
            }

            //Title Version (ticket.bin)
            try
            {
                data.Add(Tools.ReadHexUTF8(TmdPath, 0x1DC, 0x1DE, false));
            }
            catch (FileNotFoundException)
            {
                data.Add("Could not find TMD (Title Meta Data) in CIA");
            }

            //short name, long name, publisher (SMDH (icon.bin))
            try
            {
                //SMDH (icon.bin):0x208-0x287 short name
                data.Add(Tools.ReadHexUTF16(IconPath, 0x208, 0x287, true));

                //SMDH (icon.bin):0x288-0x387 long name
                data.Add(Tools.ReadHexUTF16(IconPath, 0x288, 0x347, true));

                //SMDH (icon.bin): 0x388-0x407 publisher
                data.Add(Tools.ReadHexUTF16(IconPath, 0x388, 0x3E7, true));
            }
            catch (Exception)
            {
                data.Add("Could not read SMDH / icon.bin in CIA");
                data.Add("Could not read SMDH / icon.bin in CIA");
                data.Add("Could not read SMDH / icon.bin in CIA");
            }
            //path to cia
            data.Add(ciaPath);

            //stopping & disposing ninfs
            ninfs.Dispose();
            KillNinfs();

            //returning the resulting data
            return data.ToArray();
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