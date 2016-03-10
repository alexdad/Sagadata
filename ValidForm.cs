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
    public class ProblemItem
    {
        public ProblemItem(string problem, string descr, string key)
        {
            this.Problem = problem;
            this.Description = descr;
            this.Key = key;
        }
        public string Problem { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
    }

    public partial class FormGlob : Form
    {
        private Dictionary<string, int> m_validationTempDict = new Dictionary<string, int>();

        private void buttonValidRerun_Click(object sender, EventArgs e)
        {
            RunValidation();
        }

        private void buttonValidClose_Click(object sender, EventArgs e)
        {
            panelValidation.Visible = false;
            splitContWorkValid.SplitterDistance = splitContWorkValid.Height;
        }

        private void RunValidation()
        {
            problemList.Clear();
            m_validationTempDict.Clear();

            switch (OperMode())
            {
                case Ops.View:
                    ValidateLessons();
                    break;
                case Ops.Edit:
                    {
                        switch (CurrentMode)
                        {
                            case Modes.Lessons:
                                ValidateLessons();
                                break;
                            default:
                                return;
                        }
                    }
                    break;
                default:
                    return;
            }
        }

        private void AddProblem(string text, Record link)
        {
            problemList.Add(new RecordKeeper.ProblemItem(text, link.Description, link.Key));
        }

        private bool ValidateLessons()
        {
            for (int i = 0; i < lessonList.Count; i++)
            {
                Lesson l = lessonList[i] as Lesson;
                m_validationTempDict.Add(l.Key, i);

                string prob = ValidateLesson(l);
                if (!IsStringEmpty(prob))
                    AddProblem(prob, l);
            }
            return (problemList.Count == 0);
        }

        private string ValidateLesson(Lesson l)
        {
            if (IsStringEmpty(l.Program))
                return "Lesson has no program";
            if (IsStringEmpty(l.Student1))
                return "Lesson has no first student";
            if (IsStringEmpty(l.Teacher1))
                return "Lesson has no teacher";
            if (IsStringEmpty(l.Day))
                return "Lesson has no date";
            DateTime start, end;
            if (!IsStringEmpty(l.Start) || !DateTime.TryParse(l.Start, out start))
                return "Lesson has no start";
            if (!IsStringEmpty(l.End) || !DateTime.TryParse(l.End, out end))
                return "Lesson has no end";
            if (start >= end)
                return "Lesson starts after its end";

            return null;
        }
        private void dgvValidation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangeOperMode(Ops.Edit);
            ChangeEditMode(Modes.Lessons);
            switch (CurrentMode)
            {
                case Modes.Lessons:
                    ViewSelectProblemLesson(sender as DataGridView, e.RowIndex, e.ColumnIndex);
                    break;
                default:
                    break; 
            }
        }

        void ViewSelectProblemLesson(DataGridView dgv, int row, int col)
        {
            if (row < 0 || row >= dgvValidation.RowCount)
                return;
            string key = (problemList[row] as ProblemItem).Key;
            if (m_validationTempDict.Keys.Contains(key))
                lessonList.CurrencyManager.Position = m_validationTempDict[key];
        }

    }
}