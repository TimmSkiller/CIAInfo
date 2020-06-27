using System;

namespace CIAInfo
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("CIAInfo 1.0 - Made by TimmSkiller, credit goes to ihaveamac for ninfs");
            string[] ciaPaths = Tools.Initialize();

            foreach (string path in ciaPaths)
            {
                CIA c = new CIA(CIA.Read(path));

                Console.WriteLine($"\nFile Name: {c.FilePath}\n");
                Console.WriteLine($"Long Name: {c.LongN}");
                Console.WriteLine($"Short Name: {c.ShortN}");
                Console.WriteLine($"Publisher : {c.Pub}");
                Console.WriteLine($"Product Code: {c.PID}");
                Console.WriteLine($"Title ID: {c.TID}");
                Console.WriteLine($"Region: {c.Region}");
                Console.WriteLine($"CIA Type: {c.FileType}");
                Console.WriteLine($"Version (WIP): {c.Version}");
                Console.WriteLine($"Size: {c.FileSize}\n");
            }
        }
    }
}