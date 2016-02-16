﻿using System;
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
        private void syncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandUpload();
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandDownload();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;
            CurrentType.EndSelectionMode();
            SaveChangedFiles();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = 0;
        }
        private void planToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializeSchedNew(false);
            tabControlOps.SelectedIndex = 1;
        }
        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = 4;

        }

        private void teachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = 5;

        }

        private void reportExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = 6;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;
            AskAndUploadChangedFiles();
            Application.Exit();
        }

        // Support functions
        private void CommandDownload()
        {
            if (!CheckSafety())
                return;
            DownloadCurrentFile();
        }

        private void CommandUpload()
        {
            if (!CheckSafety())
                return;
            CurrentType.EndSelectionMode();
            List<Modes> temp = SaveChangedFiles();
            if (!temp.Contains(CurrentMode))
                temp.Add(CurrentMode);
            UploadFiles(temp);
            Modified = false;
        }

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
            Modes modeWas = CurrentMode;
            foreach (Modes m in files)
            {
                SetMode(m);
                UploadCurrentFile();
            }
            CurrentMode = modeWas;
        }

        private void DownloadAllFiles()
        {
            Modes modeWas = CurrentMode;

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                DownloadCurrentFile();
            }
            CurrentMode = modeWas;
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
            Modes modeWas = CurrentMode;
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