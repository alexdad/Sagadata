using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    public partial class FormGlob : Form
    {
        string m_PayTeacherSelectionName;
        double m_PayTeacherSelectionSum;
        DateTime m_PayTeacherSelectionDate;
        bool m_use_PayTeacherSelectionDate;

        delegate bool EvaluatePayTeacher(PayTeacher t);


        private void DropPayTeacherSelection()
        {
            m_PayTeacherSelectionName = null;
            m_PayTeacherSelectionSum = 0;
            m_PayTeacherSelectionDate = DateTime.Now;
            m_use_PayTeacherSelectionDate = false;

            cbSearchPayTeacherName.SelectedIndex = -1;
            tbSearchPayTeacherSum.Text = "";
            chkSearchPayTeacherDate.Checked = false;
            dtpSearchPayTeacherDate.Value = DateTime.Now;
        }

        private void PayTeacherToFormConst1()
        {
            m_dataTypes.Add(Modes.PayTeachers, typeof(PayTeacher));
            m_recordTypes.Add(Modes.PayTeachers, new PayTeacherType(this));
        }
        private void PayTeacherToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.PayTeachers.ToString());
        }

        public void DoPayTeacherSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (PayTeacher s in m_recordTypes[Modes.PayTeachers].SavedFullListDuringSelection)
            {
                if (!IsStringEmpty(m_PayTeacherSelectionName) &&
                    !CurrentType.Fit(m_PayTeacherSelectionName, s.Teacher, true))
                    continue;
                if ( m_use_PayTeacherSelectionDate &&
                    !RecordType.FitDate(m_PayTeacherSelectionDate, s.Day))
                    continue;
                double sum = 0;
                if (m_PayTeacherSelectionSum > 0 &&
                    double.TryParse(s.Sum, out sum) &&
                    sum != m_PayTeacherSelectionSum)
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
        }

        List<PayTeacher> FindPayTeachers(EvaluatePayTeacher comp)
        {
            List<PayTeacher> PayTeachers = new List<PayTeacher>();
            foreach (var tt in this.payTeacherList.List)
            {
                PayTeacher t = tt as PayTeacher;
                if (comp(t))
                    PayTeachers.Add(t);
            }
            return PayTeachers;
        }
    }
}