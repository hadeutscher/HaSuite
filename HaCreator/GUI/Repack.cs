using MapleLib.WzLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HaCreator.GUI
{
    public partial class Repack : Form
    {
        List<WzFile> toRepack = new List<WzFile>();

        public Repack()
        {
            InitializeComponent();
            infoLabel.Text = "Files to repack:\r\n";
            foreach (WzFile wzf in Program.WzManager.wzFiles)
            {
                if (Program.WzManager.wzFilesUpdated[wzf])
                {
                    toRepack.Add(wzf);
                    infoLabel.Text += wzf.Name + "\r\n";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(RepackerThread));
            t.Start();
        }

        private void ShowErrorMessage(string data)
        {
            MessageBox.Show(data, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ChangeRepackState(string state)
        {
            stateLabel.Text = state;
        }

        private void FinishSuccess()
        {
            MessageBox.Show("Repacked successfully, press OK to restart.");
            Program.Restarting = true;
            Close();
        }

        private void RepackerThread()
        {
            Invoke((Action)delegate { ChangeRepackState("Deleting old backups..."); });
            string backupDir = Path.Combine(Program.WzManager.BaseDir, "HaCreator");
            foreach (FileInfo fi in new DirectoryInfo(backupDir).GetFiles())
            {
                fi.Delete();
            }
            foreach (WzFile wzf in toRepack)
            {
                Invoke((Action)delegate { ChangeRepackState("Saving " + wzf.Name + "..."); });
                string orgFile = wzf.FilePath;
                string tmpFile = orgFile + "$tmp";
                try
                {
                    wzf.SaveToDisk(tmpFile);
                    wzf.Dispose();
                    File.Move(orgFile, Path.Combine(backupDir, Path.GetFileName(orgFile)));
                    File.Move(tmpFile, orgFile);
                }
                catch (Exception e)
                {
                    Invoke((Action)delegate { ChangeRepackState("ERROR While saving " + wzf.Name + ", aborted."); });
                    Invoke((Action)delegate { ShowErrorMessage(e.Message + "\r\n" + e.StackTrace); });
                    return;
                }
            }
            Invoke((Action)delegate { ChangeRepackState("Finished"); });
        }
    }
}
