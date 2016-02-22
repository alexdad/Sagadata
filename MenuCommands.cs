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
        public enum TabControlOps
        {
            Edit,
            Plan,
            View,
            SchedCancel,
            PayStud,
            PayTeach,
            PayExpense,
            page8
        };

        public enum TabControlScales
        {
            Month,
            Week,
            Day,
            Slots
        };

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

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.View;
            ShowView();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.Edit;
            ChangeMode(CurrentMode);
        }
        private void planToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializePlan(false);
            tabControlOps.SelectedIndex = (int)TabControlOps.Plan;
        }
        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.PayStud;

        }

        private void teachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.PayTeach;

        }

        private void reportExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.PayExpense;

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
            SaveChangedFiles();

            List<Modes> temp = new List<Modes>();
            for (Modes m = Modes.Students; m < Modes.MaxMode; m++)
                temp.Add(m);
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

        private bool DownloadAllFiles()
        {
            bool success = true;
            Modes modeWas = CurrentMode;

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                if (!DownloadCurrentFile())
                    success = false;
            }
            CurrentMode = modeWas;
            return success;  
        }

        private bool ReadAllFiles()
        {
            bool success = true;
            Modes modeWas = CurrentMode;

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                if (!ReadCurrentFile())
                    success = false;
            }
            SetMode(modeWas);
            return success;
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