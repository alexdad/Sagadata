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
        string m_PayStudentSelectionName;
        double m_PayStudentSelectionSum;
        DateTime m_PayStudentSelectionDate;
        bool m_use_PayStudentSelectionDate;

        delegate bool EvaluatePayStudent(PayStudent t);

        private void DropPayStudentSelection()
        {
            m_PayStudentSelectionName = null;
            m_PayStudentSelectionSum = 0;
            m_PayStudentSelectionDate = DateTime.Now;
            m_use_PayStudentSelectionDate = false;

            cbSearchPayStudentName.SelectedIndex = -1;
            tbSearchPayStudentSum.Text = "";
            chkSearchPayStudentDate.Checked = false;
            dtpSearchPayStudentDate.Value = DateTime.Now;
        }

        private void PayStudentToFormConst1()
        {
            m_dataTypes.Add(Modes.PayStudents, typeof(PayStudent));
            m_recordTypes.Add(Modes.PayStudents, new PayStudentType(this));
        }
        private void PayStudentToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.PayStudents.ToString());
        }

        public void DoPayStudentSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (PayStudent s in m_recordTypes[Modes.PayStudents].SavedFullListDuringSelection)
            {
                if (!IsStringEmpty(m_PayStudentSelectionName) &&
                    !CurrentType.Fit(m_PayStudentSelectionName, s.Student, true))
                    continue;
                if ( m_use_PayStudentSelectionDate &&
                    !RecordType.FitDate(m_PayStudentSelectionDate, s.Day))
                    continue;
                double sum = 0;
                if (m_PayStudentSelectionSum > 0 &&
                    double.TryParse(s.Sum, out sum) &&
                    sum != m_PayStudentSelectionSum)
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
        }

        List<PayStudent> FindPayStudents(EvaluatePayStudent comp)
        {
            List<PayStudent> PayStudents = new List<PayStudent>();
            foreach (var tt in this.payStudentList.List)
            {
                PayStudent t = tt as PayStudent;
                if (comp(t))
                    PayStudents.Add(t);
            }
            return PayStudents;
        }
    }
}