using CTR.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CTRInfo
{
    public class CIA
    {
        public string Path { get; private set; }
        public CIAInfo CIAMeta { get; private set; }
        public List<Tuple<string, NCCHInfo>> Contents { get; private set; }
        public List<SMDHInfo> Icons { get; private set; }
        public TMDInfo TMD { get; private set; }
        public TicketInfo Ticket { get; private set; }

        public CIA(string pathToCia, bool useNinfs)
        {
            this.Path = pathToCia;

            try
            {
                this.CIAMeta = new CIAInfo(pathToCia);
            }
            catch (Exception)
            {
                throw;
            }

            FileStream fs = File.OpenRead(pathToCia);
            this.Contents = new List<Tuple<string, NCCHInfo>>();
            this.Icons = new List<SMDHInfo>();

            if (useNinfs)
            {
                if (!Tools.CanRunNinfs())
                {
                    throw new FileNotFoundException($"boot9.bin or seeddb.bin were not found inside folder {Tools.ThreeDsFolder}");
                }

                Process p = new Process();

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    Directory.CreateDirectory("mount");
                    p.StartInfo.FileName = "mount_cia";
                    p.StartInfo.Arguments = $"\"{pathToCia}\" mount";
                }
                else
                {
                    if (Directory.Exists("mount"))
                    {
                        Tools.KillNinfs();
                        Directory.Delete("mount", true);
                    }

                    p.StartInfo.FileName = "ninfs";
                    p.StartInfo.Arguments = $"cia \"{pathToCia}\" mount";
                }

                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;

                try
                {
                    p.Start();
                }
                catch (FileNotFoundException)
                {
                    throw;
                }

                while (true)
                {
                    if (File.Exists("mount/tmd.bin"))
                    {
                        break;
                    }
                }

                foreach (string dir in Directory.GetDirectories("mount"))
                {
                    if (File.Exists($"{dir}/exefs/icon.bin"))
                    {
                        this.Icons.Add(new SMDHInfo(File.ReadAllBytes($"{dir}/exefs/icon.bin")));
                    }
                    else
                    {
                        this.Icons.Add(new SMDHInfo());
                    }
                }

                Tools.KillNinfs();

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    p = new Process();
                    p.StartInfo.FileName = "fusermount";
                    p.StartInfo.Arguments = "-u mount";
                }
            }
            else
            {
                for (int i = 0; i < CIAMeta.Contents.Count; i++)
                {
                    this.Icons.Add(new SMDHInfo());
                }
            }

            for (int i = 0; i < CIAMeta.Contents.Count; i++)
            {
                CIASectionInfo csi = this.CIAMeta.Contents[i];
                fs.Seek(csi.Offset, 0);

                this.Contents.Add(Tuple.Create(csi.SectionName, new NCCHInfo(new MemoryStream(fs.ReadBytes(5000000)))));
            }

            this.TMD = this.CIAMeta.TitleMetadata;

            fs.Seek(this.CIAMeta.TicketInfo.Offset, 0);

            this.Ticket = new TicketInfo(fs.ReadBytes(this.CIAMeta.TicketInfo.Size));

            fs.Close();
        }

        public string GetGodMode9Name()
        {
            SMDHInfo contentZeroSMDH = this.Icons[0];
            NCCHInfo contentZeroNCCH = this.Contents[0].Item2;

            return $"{this.CIAMeta.TitleMetadata.TitleId.Hex()} {Tools.CleanName(contentZeroSMDH.TitleNames[1].ShortTitle)} ({Tools.CleanName(contentZeroNCCH.ProductCode.ProductCode)}) {contentZeroNCCH.ProductCode.Region.Split(" ")[1]}.cia";
        }
    }
}