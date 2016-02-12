using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    public partial class FormGlob : Form
    {
        //----------------------------------------------------
        // Menu strip
        //----------------------------------------------------
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadCurrentFile();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentType.EndSelectionMode();
            List<Modes> temp = SaveChangedFiles();
            if (!temp.Contains(CurrentMode))
                temp.Add(CurrentMode);
            UploadFiles(temp);
            Modified = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentType.EndSelectionMode();
            SaveChangedFiles();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AskAndUploadChangedFiles();
            Application.Exit();
        }

        // Support functions
        private void AskAndUploadChangedFiles()
        {
            if (AnyFileChanged)
            {
                DialogResult result = MessageBox.Show(
                    "Should I save?", "You have unsaved changes", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    List<Modes> saved = SaveChangedFiles();
                    result = MessageBox.Show(
                        "Should I upload?", "You have local changes", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                        UploadFiles(saved);
                }
            }
        }

        private void UploadFiles(List<Modes> files)
        {
            Modes modeWas = m_mode;
            foreach (Modes m in files)
            {
                SetMode(m);
                UploadCurrentFile();
            }
            m_mode = modeWas;
        }

        private void DownloadAllFiles()
        {
            Modes modeWas = m_mode;

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                DownloadCurrentFile();
            }
            m_mode = modeWas;
        }

        private List<Modes> AskAndSaveChangedFiles()
        {
            if (AnyFileChanged)
            {
                DialogResult result = MessageBox.Show(
                    "Should I save?", "You have unsaved changes", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    return SaveChangedFiles();
            }
            return null;
        }
        private List<Modes> SaveChangedFiles()
        {
            List<Modes> saved = new List<Modes>();
            Modes modeWas = m_mode;
            //this.Visible = false;
            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                if (Modified)
                {
                    CurrentType.WriteRecordsFile();
                    saved.Add(i);
                    Modified = false;
                }
            }

            SetMode(modeWas);
            //this.Visible = true;
            return saved;
        }
    }
}