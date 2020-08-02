using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CIAInfo
{
    internal class CIA
    {
        public string TitleId { get; private set; }
        public string ProductCode { get; private set; }
        public string Version { get; private set; }
        public string Region { get; private set; }
        public string FileType { get; private set; }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string FileSize { get; private set; }
        public string Gm9FileName { get; private set; }
        public string ShortName { get; private set; }
        public string LongName { get; private set; }
        public string Publisher { get; private set; }

        public CIA(string productCode, string gm9FileName, string fileType, string titleId, string version, string shortName, string longName, string publisher, string fileName, string filePath, string fileSize, string region)
        {
            ProductCode = productCode;
            TitleId = titleId;
            Version = version;
            ShortName = shortName;
            LongName = longName;
            Publisher = publisher;
            FileName = fileName;
            FilePath = filePath;
            FileSize = fileSize;
            Region = region;
            FileType = fileType;
            Gm9FileName = gm9FileName;
        }

        public CIA()
        {
            ProductCode = "XXX-X-XXXX / XXX-X-XXXX-XX";
            TitleId = "0004000000000000";
            Version = "0.0.0";
            Region = "Unknown";
            ShortName = "N/A";
            LongName = "N/A";
            Gm9FileName = "0004000000000000 N/A (XXX-X-XXX) (UNK)";
            Publisher = "N/A";
            FileName = "N/A";
            FilePath = "N/A";
            FileSize = "0 B";
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

        private static string GetCIAType(string productCode)
        {
            string result = "";

            //checks if the read product code starts with CTR, KTR, or TWL.
            //CTR: Original 2/3DS, KTR: New 2/3DS, TWL: DSi

            string consoleCode = productCode.Split('-')[0];

            switch (consoleCode)
            {
                case "CTR":
                    result += "Original 3DS Game | ";
                    break;

                case "KTR":
                    result += "New 3DS Game or New 3DS Virtual Console | ";
                    break;

                case "TWL":
                    result += "Nintendo DSiWare | ";
                    break;
            }

            //checks what kind of CIA is being read, the same way as above.
            //format: C/KTR/TWL - P/U/M/N
            result += productCode.Split('-')[1] switch
            {
                "P" => "Game data",
                "U" => "Update data",
                "M" => "DLC (Downloadable Content)",
                "N" => "Game data",
                _ => "Unknown",
            };
            return result;
        }

        private static string GetCIARegion(string productCode)
        {
            //checks what region the CIA is by reading the last letter of the product code and matching it with the corresponding region.
            string result = productCode.Split('-')[2][3] switch
            {
                'P' => "Europe (E)",
                'Z' => "Europe (Z) / (E)",
                'X' => "Europe (X) / (E)",
                'V' => "Europe (V) / (E)",
                'Y' => "Europe (Y) / (E)",
                'D' => "Europe (D) / (E)",
                'E' => "North America (U)",
                'J' => "Japan (J)",
                'K' => "Korea (K)",
                'W' => "China (CN)",
                'A' => "Global (Region Free) (W)",
                _ => "Unknown (UNK)",
            };
            return result;
        }

        //reading the CIA
        public static CIA Read(string path)
        {
            if (Tools.ReadHexUTF8(path, 0x0, 0x2, false).Trim() != "2020")
            {
                throw new ArgumentException($"The current CIA File in path {path} does not contain a valid CIA header.");
            }

            string mountPoint = $"{Environment.CurrentDirectory}\\ninfs_mount";
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
                if (File.Exists($"{mountPoint}\\tmd.bin"))
                {
                    break;
                }
            }

            CIA c = new CIA();

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

            //Title ID (ticket.bin): 0x1DC-0x1E4 UTF-8
            c.TitleId = Tools.ReadHexUTF8(TicketPath, 0x1DC, 0x1E4, false);

            if (c.TitleId.StartsWith("00048"))
            {
                //DSiWare Product Code (ticket.bin): 0x1E0-0x1E3 UTF-8
                c.ProductCode = $"TWL-N-{Tools.ReadHexUTF8($"{mountPoint}\\ticket.bin", 0x1E0, 0x1E4, true)}";

                //DSiWare Name structure (banner.bin): 0x240-0x33F UTF-16, split by \u0a00
                string[] nameData = Array.Empty<string>();
                try
                {
                    nameData = Tools.ReadHexUTF16($"{gamedata}\\banner.bin", 0x240, 0x33F, true).Split('\n');
                }
                catch (IndexOutOfRangeException)
                {
                    nameData = Tools.ReadHexUTF16($"{gamedata}\\banner.bin", 0x340, 0x43F, true).Split('\n');
                }
                c.LongName = nameData[0];
                c.ShortName = nameData[0];
                c.Publisher = nameData[1];
            }
            else
            {
                //Product Code (ncch.bin): 0x150-0x160 UTF-8
                c.ProductCode = Tools.ReadHexUTF8(NcchPath, 0x150, 0x160, true);

                //Title Version (ticket.bin): 0x1DC-0x1DE UTF-8
                c.Version = GetCIAVersion(Tools.ReadHexUTF8(TmdPath, 0x1DC, 0x1DE, false));

                //Short Name (SMDH (icon.bin)): 0x208-0x287 UTF-16
                c.ShortName = Tools.ReadHexUTF16(IconPath, 0x208, 0x287, true);

                //Long Name (SMDH (icon.bin)): 0x288-0x387 UTF-16
                c.LongName = Tools.ReadHexUTF16(IconPath, 0x288, 0x387, true);

                //Publisher (SMDH (icon.bin)): 0x388-0x407 UTF-16
                c.Publisher = Tools.ReadHexUTF16(IconPath, 0x388, 0x407, true);
            }

            //CIA Region
            c.Region = GetCIARegion(c.ProductCode);

            //CIA File Type
            c.FileType = GetCIAType(c.ProductCode);

            c.FileSize = Tools.GetSize(new FileInfo(path).Length);

            //path to CIA
            c.FilePath = path;

            //file name
            c.FileName = new DirectoryInfo(path).Name;

            //GM9 Format File Name
            c.Gm9FileName = $"{c.TitleId} {Tools.CleanName(c.ShortName)} ({Tools.CleanName(c.ProductCode)}) {c.Region.Split(' ')[c.Region.Split(' ').Length - 1]}.cia";

            //stopping & disposing ninfs
            ninfs.Dispose();
            Tools.KillNinfs();

            //returning the resulting data
            return c;
        }
    }
}