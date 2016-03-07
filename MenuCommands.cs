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
        #region "Top menu strip"
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            if (!CheckSafety())
                return;
            if (AskAndUploadChangedFiles())
                Application.Exit();
        }

        #endregion

        #region "View submenu strip"
        private void slotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            tabControlViewScales.SelectedIndex = (int)Scales.Slots;
            ChangeOperMode(Ops.View);
            FollowFocusedDay();
            ShowView();
        }

        private void dayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            tabControlViewScales.SelectedIndex = (int)Scales.Day;
            ChangeOperMode(Ops.View);
            FollowFocusedDay();
            ShowView();
        }

        private void weekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            tabControlViewScales.SelectedIndex = (int)Scales.Week;
            ChangeOperMode(Ops.View);
            FollowFocusedDay();
            ShowView();
        }

        private void monthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            tabControlViewScales.SelectedIndex = (int)Scales.Month;
            ChangeOperMode(Ops.View);
            ShowView();
        }

        #endregion

        #region "Edit submenu strip"
        private void studentsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ChangeOperMode(Ops.Edit);
            ChangeEditMode(Modes.Students);
        }

        private void teachersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ChangeOperMode(Ops.Edit);
            ChangeEditMode(Modes.Teachers);
        }

        private void lessonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ChangeOperMode(Ops.Edit);
            ChangeEditMode(Modes.Lessons);
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ChangeOperMode(Ops.Edit);
            ChangeEditMode(Modes.Programs);
        }
        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ChangeOperMode(Ops.Edit);
            ChangeEditMode(Modes.Rooms);
        }
        #endregion

        #region "Schedule submenu strip"

        private void planToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            InitializePlan(true, false);
            ChangeOperMode(Ops.Plan);
        }
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            dayToolStripMenuItem_Click(sender, e);
        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            // TODO - future - check the schedule for errors
        }

        private void publishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            DateTime dtMin = WeekStart(m_chosenDate);
            DateTime dtMax = WeekEnd(m_chosenDate);

            string msg = String.Format("You are publishing week of {0} - {1}", 
                dtMin.ToShortDateString(), dtMax.ToShortDateString());
            DialogResult result = MessageBox.Show("Continue?", msg, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                bool res = GCal.Ops.DeleteAllSystemCalendarEvents(dtMin, dtMax);
                if (res)
                {
                    res = GCal.Ops.WriteSystemCalendarEvents(
                                    GetEventsForPublishing(dtMin, dtMax));
                }
                if (!res)
                    MessageBox.Show("Failed to publish, sorry");
            }
        }
        #endregion

        #region "Pay submenu strip"

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ChangeOperMode(Ops.PayStud);
        }

        private void teachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ChangeOperMode(Ops.PayTeach);
        }

        private void reportExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ChangeOperMode(Ops.PayExpense);
        }
        #endregion

        #region "Advanced menu strip"
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            CommandDownload();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            CommandUpload();
        }
        private void reopenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            ReadAllFiles();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonSave_Click(null, null);
        }

        private void syncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonSync_Click(null, null);
        }

        #endregion

        #region "Relocate submenu of Lesson ctx menu"
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Red";
            Modified = true;
            ShowView();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Green";
            Modified = true;
            ShowView();
        }

        private void tealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Teal";
            Modified = true;
            ShowView();
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Yellow";
            Modified = true;
            ShowView();
        }
        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.Room = "White";
            Modified = true;
            ShowView();
        }
        private void pinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Pink";
            Modified = true;
            ShowView();
        }

        private void nAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.Room = "N/A";
            Modified = true;
            ShowView();
        }
        #endregion

        #region "Move submenu of Lesson ctx menu"
        private void sameWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, 0);
        }

        private void weekForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, 7);
        }

        private void twoWeeksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, 14);
        }

        private void weekBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, -7);
        }

        private void twoWeeksBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, -14);
        }

        #endregion

        #region "Mark submenu of Lesson ctx menu"
        private void plannedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.State = "Planned";
            Modified = true;
            ShowView();
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.State = "Done";
            Modified = true;
            ShowView();
        }

        private void cancelledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            Lesson l = GetLessonFromSender2(sender);
            l.State = "Cancelled";
            l.CancellationTime = DateTime.Now.ToString();
            Modified = true;
            ShowView();
        }
        #endregion

        #region "Repeat submenu of Lesson ctx menu"
        private void weeklyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            if (!CheckSafety())
                return;

            using (RepeatForm rf = new RepeatForm())
            {
                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    Lesson l = GetLessonFromSender2(sender);
                    RepeatLesson(l,
                        RepeatMode.Weekly,
                        rf.UpToTheEOY,
                        rf.Repeats);
                    Modified = true;
                    ShowView();
                }
            }
        }

        private void biweeklyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            if (!CheckSafety())
                return;

            using (RepeatForm rf = new RepeatForm())
            {
                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    Lesson l = GetLessonFromSender2(sender);
                    RepeatLesson(l,
                            RepeatMode.Biweekly,
                            rf.UpToTheEOY,
                            rf.Repeats);
                    Modified = true;
                    ShowView();
                }
            }
        }

        private void monthlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            if (!CheckSafety())
                return;

            using (RepeatForm rf = new RepeatForm())
            {
                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    Lesson l = GetLessonFromSender2(sender);
                    RepeatLesson(l,
                            RepeatMode.Monthly,
                            rf.UpToTheEOY,
                            rf.Repeats);

                    Modified = true;
                    ShowView();
                }
            }
        }

        private void deleteFuturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operation_CompletePrevious();

            if (!CheckSafety())
                return;

            Lesson l = GetLessonFromSender2(sender);
            DeleteFutures(l);
            Modified = true;
            ShowView();
        }

        #endregion

        #region "Commands"

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
            SetEditMode(modeWas);

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

        private bool AskAndUploadChangedFiles()
        {
            if (Modified)
            {
                DialogResult result = MessageBox.Show(
                    "Should I save?", "You have unsaved changes", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                    return false;
                else if (result == DialogResult.Yes)
                {
                    SaveAll();
                    result = MessageBox.Show(
                        "Should I upload?", "You have local changes", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Cancel)
                        return false;
                    else if (result == DialogResult.Yes)
                        UploadAll();
                }
            }
            return true;
        }

        #endregion
    }
}