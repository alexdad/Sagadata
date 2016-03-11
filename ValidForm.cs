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

        #region "general validation routines"
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
            if (OperMode() == Ops.View)
            {
                ChangeEditMode(Modes.Lessons);
                ChangeOperMode(Ops.Edit);
            }
            if (OperMode() != Ops.Edit)
                return;

            ValidateDatalist();
        }

        private void AddProblem(string text, Record link)
        {
            problemList.Add(new RecordKeeper.ProblemItem(text, link.Description, link.Key));
        }
        private bool ValidateDatalist()
        {
            for (int i = 0; i < DataList.Count; i++)
            {
                Record l = DataList[i] as Record;
                m_validationTempDict.Add(l.Key, i);

                string prob = l.Validate2FirstProblem(this);
                if (!IsStringEmpty(prob))
                    AddProblem(prob, l);            }
            return (problemList.Count == 0);
        }

        private void dgvValidation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (OperMode() == Ops.View)
            {
                ChangeEditMode(Modes.Lessons);
                ChangeOperMode(Ops.Edit);
            }
            if (OperMode() != Ops.Edit)
                return;

            ViewSelectProblem(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        void ViewSelectProblem(DataGridView dgv, int row, int col)
        {
            if (row < 0 || row >= dgvValidation.RowCount)
                return;
            string key = (problemList[row] as ProblemItem).Key;
            if (m_validationTempDict.Keys.Contains(key))
                DataList.CurrencyManager.Position = m_validationTempDict[key];
        }
        #endregion
    }
}