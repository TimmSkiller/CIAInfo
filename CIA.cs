using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            PID = data[0];
            TID = data[1];
            Version = GetCIAVersion(data[2]);
            ShortN = data[3];
            LongN = data[4];
            Pub = data[5];
            FilePath = new DirectoryInfo(data[6]).Name;
            FileSize = Tools.GetSize((int)new FileInfo(data[6]).Length);

            Region = GetCIARegion(PID);
            FileType = GetCIAType(PID);
        }

        //converts the hexadecimal number to a readable version number

        private static string GetCIAVersion(string hexvalue)
        {
            string hex = $"1{hexvalue}";
            int dec = 0;
            string bin = string.Empty;
            dec = Convert.ToInt32(hex, 16);
            bin = Convert.ToString(dec, 2).Remove(0, 1);
            int[] indexes = { 2, 5, 10 };
            List<string> str = new List<string>();

            foreach (int index in indexes)
            {
                string temp = String.Empty;

                for (int i = index; i < index + 4; i++)
                {
                    temp += (bin[i]);
                }
                str.Add(Convert.ToInt32(temp, 2).ToString());
            }

            return $"{str[0]}.{str[1]}.{str[2]}";
        }

        private static string GetCIAType(string productId)
        {
            char model = productId[0];
            string result = "";

            //checks if the read product code starts with K or C.
            //CTR: Original 2/3DS, KTR: New 2/3DS
            switch (model)
            {
                case 'C':
                    result += "Original 3DS Game | ";
                    break;

                case 'K':
                    result += "New 3DS Game or New 3DS Virtual Console | ";
                    break;
            }

            //checks what kind of CIA is being read, the same way as above.
            //format: C/KTR - P/U/M/N
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

        private static string GetCIARegion(string productId)
        {
            //checks what region the CIA is by reading the last letter of the product code and matching it with the corresponding region.
            string result = productId.Split('-')[2][3] switch
            {
                'P' => "Europe (E)",
                'Z' => "Europe (Z)",
                'X' => "Europe (X)",
                'V' => "Europe (V)",
                'Y' => "Europe (Y)",
                'D' => "Europe (D)",
                'E' => "North America (U)",
                'J' => "Japan (J)",
                'K' => "Korea (K)",
                'W' => "China (CN)",
                'A' => "Global (Region Free) (A or W)",
                _ => "Unknown (UNK)",
            };
            return result;
        }

        //reading the CIA
        public static string[] Read(string path)
        {
            //getting an available drive letter to use as a mount point for ninfs
            string mountPoint = Tools.GetAvailableDriveLetter();

            //starting ninfs, mounting CIA and reading data
            Process ninfs = new Process();

            ninfs.StartInfo.FileName = Tools.RequiredFiles[0];
            ninfs.StartInfo.Arguments = $"cia \"{path}\" {mountPoint}";
            ninfs.StartInfo.UseShellExecute = false;
            ninfs.StartInfo.CreateNoWindow = true;

            ninfs.Start();

            mountPoint += "\\";

            //wating for ninfs to mount CIA
            while (true)
            {
                if (DriveInfo.GetDrives().Any(c => c.Name == mountPoint))
                {
                    break;
                }
            }

            List<string> data = new List<string>();

            //paths for hex reader

            //content 0
            string gamedata = Tools.MinNumFolder(mountPoint, true);

            //NCCH
            string NcchPath = $"{gamedata}\\ncch.bin";

            //Icon (SMDH)
            string IconPath = $"{gamedata}\\exefs\\icon.bin";

            //TMD (Title Meta Data)
            string TmdPath = $"{mountPoint}\\tmd.bin";

            //Ticket
            string TicketPath = $"{mountPoint}\\ticket.bin";

            //start populating data list with data

            //Product Code (ncch.bin): 0x150-0x160 UTF-8
            data.Add(Tools.ReadHexUTF8(NcchPath, 0x150, 0x160, true));

            //Title ID (ticket.bin): 0x1DC-0x1E4 UTF-8
            data.Add(Tools.ReadHexUTF8(TicketPath, 0x1DC, 0x1E4, false));

            //Title Version (ticket.bin): 0x1DC-0x1DE UTF-8
            data.Add(Tools.ReadHexUTF8(TmdPath, 0x1DC, 0x1DE, false));

            //Short Name (SMDH (icon.bin)): 0x208-0x287 UTF-16
            data.Add(Tools.ReadHexUTF16(IconPath, 0x208, 0x287, true));

            //Long Name (SMDH (icon.bin)): 0x288-0x387 UTF-16
            data.Add(Tools.ReadHexUTF16(IconPath, 0x288, 0x387, true));

            //Publisher (SMDH (icon.bin)): 0x388-0x407 UTF-16
            data.Add(Tools.ReadHexUTF16(IconPath, 0x388, 0x407, true));

            //path to CIA
            data.Add(path);

            //stopping & disposing ninfs
            ninfs.Dispose();
            Tools.KillNinfs();

            //returning the resulting data
            return data.ToArray();
        }
    }
}