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
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandDownload();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandUpload();
        }
        private void reopenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadAllFiles();
        }

        private void slotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlViewScales.SelectedIndex = (int)TabControlScales.Slots;
            tabControlOps.SelectedIndex = (int)TabControlOps.View;
            FollowFocusedDay();
            ShowView();
        }

        private void dayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlViewScales.SelectedIndex = (int)TabControlScales.Day;
            tabControlOps.SelectedIndex = (int)TabControlOps.View;
            FollowFocusedDay();
            ShowView();
        }

        private void weekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlViewScales.SelectedIndex = (int)TabControlScales.Week;
            tabControlOps.SelectedIndex = (int)TabControlOps.View;
            FollowFocusedDay();
            ShowView();
        }

        private void monthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlViewScales.SelectedIndex = (int)TabControlScales.Month;
            tabControlOps.SelectedIndex = (int)TabControlOps.View;
            ShowView();
        }

        private void studentsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.Edit;
            cbGlobMode.Text = "Students";
            ChangeMode(Modes.Students);
        }

        private void teachersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.Edit;
            cbGlobMode.Text = "Teachers";
            ChangeMode(Modes.Teachers);
        }

        private void lessonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.Edit;
            cbGlobMode.Text = "Lessons";
            ChangeMode(Modes.Lessons);
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.Edit;
            cbGlobMode.Text = "Programs";
            ChangeMode(Modes.Programs);
        }
        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlOps.SelectedIndex = (int)TabControlOps.Edit;
            cbGlobMode.Text = "Rooms";
            ChangeMode(Modes.Rooms);
        }

        private void planToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitializePlan(false);
            tabControlOps.SelectedIndex = (int)TabControlOps.Plan;
        }
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dayToolStripMenuItem_Click(sender, e);
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO - future - check the schedule for errors
        }

        private void publishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO- future - publish schedule to the web
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
        private void CommandSave()
        {
            if (!CheckSafety())
                return;
            CurrentType.EndSelectionMode();
            SaveAll();
        }
        private void CommandDownload()
        {
            if (!CheckSafety())
                return;
            DownloadCurrentFile();
        }
        private void CommandSync()
        {
            HideWorkout = true;

            Modes modeWas = CurrentMode;
            CommandUpload();
            SetMode(modeWas);

            HideWorkout = false;
        }
        private void CommandUpload()
        {
            if (!CheckSafety())
                return;
            CurrentType.EndSelectionMode();
            SaveAll();
            UploadAll();
        }

        private void AskAndUploadChangedFiles()
        {
            if (Modified)
            {
                DialogResult result = MessageBox.Show(
                    "Should I save?", "You have unsaved changes", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    SaveAll();
                    result = MessageBox.Show(
                        "Should I upload?", "You have local changes", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                        UploadAll();
                }
            }
        }

        private void UploadAll()
        {
            HideWorkout = true;

            Modes modeWas = CurrentMode;
            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                UploadCurrentFile();
            }
            CurrentMode = modeWas;
            Synced = !Modified;

            HideWorkout = false;
        }

        private bool DownloadAll()
        {
            HideWorkout = true;

            bool success = true;
            Modes modeWas = CurrentMode;

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                if (!DownloadCurrentFile())
                    success = false;
            }
            CurrentMode = modeWas;
            Synced = !Modified;

            HideWorkout = false;
            return success;  
        }

        private bool ReadAllFiles()
        {
            HideWorkout = true;

            bool success = true;
            Modes modeWas = CurrentMode;

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                if (!ReadCurrentFile())
                    success = false;
            }
            SetMode(modeWas);

            HideWorkout = false;
            return success;
        }

        private void SaveAll()
        {
            HideWorkout = true;

            Modes was = CurrentMode;
            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetMode(i);
                CurrentType.WriteRecordsFile();
            }
            Modified = false;
            SetMode(was);

            HideWorkout = false;
        }
    }
}