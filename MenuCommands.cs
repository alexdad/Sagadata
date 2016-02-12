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
            m_curType.EndSelectionMode();
            UploadFiles(SaveChangedFiles());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_curType.EndSelectionMode();
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
                    bool[] saved = SaveChangedFiles();
                    result = MessageBox.Show(
                        "Should I upload?", "You have local changes", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                        UploadFiles(saved);
                }
            }
        }

        private void UploadFiles(bool[] files)
        {
            Modes modeWas = m_mode;

            for (int i = 0; i < (int)Modes.MaxMode; i++)
            {
                if (files[i])
                {
                    SetMode(i);
                    UploadCurrentFile();
                }
            }
            m_mode = modeWas;
        }

        private void DownloadAllFiles()
        {
            Modes modeWas = m_mode;

            for (int i = 0; i < (int)Modes.MaxMode; i++)
            {
                SetMode(i);
                DownloadCurrentFile();
            }
            m_mode = modeWas;
        }

        private bool[] AskAndSaveChangedFiles()
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
        private bool[] SaveChangedFiles()
        {
            bool[] saved = new bool[(int)Modes.MaxMode];
            for (int i = 0; i < (int)Modes.MaxMode; i++)
                saved[i] = false;

            Modes modeWas = m_mode;

            for (int i = 0; i < (int)Modes.MaxMode; i++)
            {
                SetMode(i);
                if (Changed)
                {
                    m_curType.WriteRecordsFile();
                    saved[i] = true;
                }
                Changed = false;
            }

            SetMode(modeWas);
            return saved;
        }
    }
}