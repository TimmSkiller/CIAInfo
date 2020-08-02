using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace CIAInfo
{
    internal class Program
    {
        //imports native dll calls to change the current code page
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCP(uint wCodePageID);

        private static void Main()
        {
            //gets the current active encoding of the user before changing it to 932 (japanese)
            int userencoding = System.Text.Encoding.Default.CodePage;

            //changes the console code page to japanese so that the console can display japanese text
            SetConsoleCP(932);
            SetConsoleOutputCP(932);

            if (Process.GetProcessesByName("CIAInfo").Length > 1)
            {
                Console.WriteLine("Another instance of CIAInfo is running. Please close the other instance and try again.\nPress any key to exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Console.WriteLine("CIAInfo 1.3 - Made by TimmSkiller, credit goes to ihaveamac for ninfs");

            string[] ciaPaths = Array.Empty<string>();

            try
            {
                ciaPaths = Tools.Initialize();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine("A fatal error occurred.\nPlease note the error and create an issue on my github page: https://github.com/TimmSkiller/CIAInfo/issues");
                Console.WriteLine($"Error: {e.GetType()}");
                Console.WriteLine($"Stacktrace: {e.StackTrace}");
            }

            foreach (string path in ciaPaths)
            {
                CIA c = new CIA();

                try
                {
                    c = CIA.Read(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: CIA could not be read and might be corrupt!\n");
                    Console.WriteLine($"Exception Message: {e.Message}");
                    Console.WriteLine($"Exception Stack Trace: {e.StackTrace}");
                    continue;
                }

                Console.WriteLine($"\nFile Name: {c.FileName}\n");
                Console.WriteLine($"Long Name: {c.LongName}");
                Console.WriteLine($"Short Name: {c.ShortName}");
                Console.WriteLine($"Publisher : {c.Publisher}");
                Console.WriteLine($"Product Code: {c.ProductCode}");
                Console.WriteLine($"Title ID: {c.TitleId}");
                Console.WriteLine($"Region: {c.Region}");
                Console.WriteLine($"CIA Type: {c.FileType}");
                Console.WriteLine($"Version: {c.Version}");
                Console.WriteLine($"Size: {c.FileSize}\n");
                Console.WriteLine();
                Console.WriteLine($"GodMode9 File Name: {c.Gm9FileName}");
                Console.WriteLine();

                if (c.FileName != c.Gm9FileName)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Renaming to GodeMode9 Format...");
                    Tools.KillNinfs();
                    try
                    {
                        File.Move(c.FilePath, $"{Tools.RequiredDirectories[0]}\\{c.Gm9FileName}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error renaming file.");
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("\nFile is already in GodeMode9 Name Format, no need to rename.\n");
                }
            }

            Console.WriteLine($"Files read: {ciaPaths.Length}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();

            //reverts the original code page the user had active before running CIAInfo
            SetConsoleCP(Convert.ToUInt32(userencoding));
            SetConsoleOutputCP(Convert.ToUInt32(userencoding));
        }
    }
}