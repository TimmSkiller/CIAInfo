using CommandLine;
using CTR.NET;
using System;
using System.Diagnostics;
using System.IO;

namespace CTRInfo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (Process.GetProcessesByName("CTRInfo").Length > 1)
            {
                Console.WriteLine("Another instance of CTRInfo is running. Please close the other instance and try again.\nPress any key to exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Console.WriteLine("CTRInfo 2.0 - Made by TimmSkiller\n");

            Parser.Default.ParseArguments<CLIArgs.SingleCiaReadOptions, CLIArgs.DirectoryReadOptions>(args)
                .WithParsed<CLIArgs.SingleCiaReadOptions>(options => ReadSingleCIA(options))
                .WithParsed<CLIArgs.DirectoryReadOptions>(options => ReadCiaDirectory(options));
        }

        public static void ReadSingleCIA(CLIArgs.SingleCiaReadOptions options)
        {
            if (!File.Exists(options.CiaPath))
            {
                Console.WriteLine("ERROR: Specified file does not exist.");
                Environment.Exit(-1);
            }

            if (options.ChangeToGm9Format && (!options.UseNinfs))
            {
                Console.WriteLine("ERROR: Invalid Arguments. -g (--gm9-name-format) can only be used in combination with -n (--use-ninfs).");
                Environment.Exit(0);
            }

            try
            {
                CIA c = new CIA(options.CiaPath, options.UseNinfs);

                if (options.Verbose)
                {
                    DisplayCIAVerbose(c, options.ChangeToGm9Format);
                }
                else
                {
                    DisplayCIA(c, options.ChangeToGm9Format);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
                Console.WriteLine("Please make sure you have ninfs, it's requirements (boot9.bin and seeddb.bin) in their respective locations.");
                Environment.Exit(-1);
            }
        }

        public static void ReadCiaDirectory(CLIArgs.DirectoryReadOptions options)
        {
            if (!Directory.Exists(options.CiaDirPath))
            {
                Console.WriteLine("ERROR: Specified file does not exist.");
                Environment.Exit(-1);
            }

            if (options.ChangeToGm9Format && (!options.UseNinfs))
            {
                Console.WriteLine("ERROR: Invalid Arguments. -g (--gm9-name-format) can only be used in combination with -n (--use-ninfs).");
                Environment.Exit(0);
            }

            try
            {
                DirectoryInfo dir = new DirectoryInfo(options.CiaDirPath);

                if (dir.GetFiles("*.cia").Length == 0)
                {
                    throw new ArgumentException("Specified directory did not contain any CIAs.");
                }

                foreach (FileInfo file in dir.GetFiles("*.cia"))
                {
                    try
                    {
                        CIA c = new CIA(file.FullName, options.UseNinfs);

                        if (options.Verbose)
                        {
                            DisplayCIAVerbose(c, options.ChangeToGm9Format);
                        }
                        else
                        {
                            DisplayCIA(c, options.ChangeToGm9Format);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error reading CIA at {file.FullName}.");
                        Console.WriteLine($"ERROR: {e.Message}");
                        continue;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
        }

        public static void DisplayCIA(CIA c, bool rename)
        {
            SMDHTitleNameStructure contentZeroSMDH = c.Icons[0].TitleNames[0];
            NCCHInfo contentZeroNCCH = c.Contents[0].Item2;

            Console.WriteLine($"Long Name: {contentZeroSMDH.LongTitle}");
            Console.WriteLine($"Short Name: {contentZeroSMDH.ShortTitle}");
            Console.WriteLine($"Publisher {contentZeroSMDH.Publisher}");
            Console.WriteLine($"Product Code: {contentZeroNCCH.ProductCode.ProductCode}");
            Console.WriteLine($"Title ID: {c.TMD.TitleId.Hex()}");
            Console.WriteLine($"Region: {contentZeroNCCH.ProductCode.Region}");
            Console.WriteLine($"CIA Type: {contentZeroNCCH.ProductCode.Console} | {contentZeroNCCH.ProductCode.ContentType}");
            Console.WriteLine($"Title Version (Taken from TMD): {c.TMD.TitleVersion}");
            Console.WriteLine($"Total Size: {c.CIAMeta.ContentInfo.Size} (0x{c.CIAMeta.ContentInfo.Size:X}) | {c.CIAMeta.ContentInfo.Size / 1024 / 128} blocks\n");

            if (rename)
            {
                Console.WriteLine($"GodMode9 Naming Scheme: {c.GetGodMode9Name()}\n");
                File.Move(c.Path, $"{new DirectoryInfo(c.Path).Parent}/{c.GetGodMode9Name()}");
            }
        }

        public static void DisplayCIAVerbose(CIA c, bool rename)
        {
            Console.WriteLine(c.TMD);
            Console.WriteLine(c.Ticket);

            for (int i = 0; i < c.Contents.Count; i++)
            {
                Console.WriteLine($"Content {c.Contents[i].Item1}:\n\n");
                Console.WriteLine(c.Contents[i].Item2);
                Console.WriteLine(c.Icons[i]);
            }

            if (rename)
            {
                SMDHTitleNameStructure contentZeroSMDH = c.Icons[0].TitleNames[0];
                NCCHInfo contentZeroNCCH = c.Contents[0].Item2;

                Console.WriteLine($"GodMode9 Naming Scheme: {c.TMD.TitleId.Hex()} {Tools.CleanName(contentZeroSMDH.ShortTitle)} ({Tools.CleanName(contentZeroNCCH.ProductCode.ProductCode)}) {contentZeroNCCH.ProductCode.Region.Split(" ")[1]}.cia");
                File.Move(c.Path, $"{new DirectoryInfo(c.Path).Name}/{c.TMD.TitleId.Hex()} {Tools.CleanName(contentZeroSMDH.ShortTitle)} ({Tools.CleanName(contentZeroNCCH.ProductCode.ProductCode)}) {contentZeroNCCH.ProductCode.Region.Split(" ")[1]}.cia");
            }
        }
    }
}