using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Windows.Forms;

namespace reconstructIt
{
    public partial class MainForm : Form
    {
        public void xExtractToDirectory(ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }

            DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
            string destinationDirectoryFullPath = di.FullName;

            foreach (ZipArchiveEntry file in archive.Entries)
            {
                try
                {
                    string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                    if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                    }

                    if (file.Name == "")
                    {// Assuming Empty for Directory
                        Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                        continue;
                    }
                    file.ExtractToFile(completeFileName, true);
                }catch(Exception ex) { }
            }
        }
        public MainForm()
        {
            InitializeComponent();
        }
        void logger(string t)
        {
            log.Text +=t+"\r\n";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            log.Text = "";
            logger("Gathering resource...");
            System.IO.File.WriteAllBytes("cache.zip", reconstructIt.Properties.Resources.db);
            logger("Resource written to \"cache.zip\".");
            logger("Beginning extraction...");
            FileStream zipfstream = new FileStream("cache.zip",FileMode.Open, FileAccess.Read);
            ZipArchive zip = new ZipArchive(zipfstream);
            xExtractToDirectory(zip, "..\\..\\..\\", true);
            logger("Reconstruction completed.");
            
            logger("Finished.");
        }
    }
}
