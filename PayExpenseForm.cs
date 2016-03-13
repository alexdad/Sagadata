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
        string m_PayExpenseSelectionCategory;
        double m_PayExpenseSelectionSum;
        DateTime m_PayExpenseSelectionDate;
        bool m_use_PayExpenseSelectionDate;

        delegate bool EvaluatePayExpense(PayExpense t);


        private void DropPayExpenseSelection()
        {
            m_PayExpenseSelectionCategory = null;
            m_PayExpenseSelectionSum = 0;
            m_PayExpenseSelectionDate = DateTime.Now;
            m_use_PayExpenseSelectionDate = false;

            cbSearchPayExpenseCategory.SelectedIndex = -1;
            tbSearchPayExpenseSum.Text = "";
            chkSearchPayExpenseDate.Checked = false;
            dtpSearchPayExpenseDate.Value = DateTime.Now;

        }

        private void PayExpenseToFormConst1()
        {
            m_dataTypes.Add(Modes.PayExpenses, typeof(PayExpense));
            m_recordTypes.Add(Modes.PayExpenses, new PayExpenseType(this));
        }
        private void PayExpenseToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.PayExpenses.ToString());
        }

        public void DoPayExpenseSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (PayExpense s in m_recordTypes[Modes.PayExpenses].SavedFullListDuringSelection)
            {
                if (!IsStringEmpty(m_PayExpenseSelectionCategory) &&
                    !CurrentType.Fit(m_PayExpenseSelectionCategory, s.Category, true))
                    continue;
                if ( m_use_PayExpenseSelectionDate &&
                    !RecordType.FitDate(m_PayExpenseSelectionDate, s.Day))
                    continue;
                double sum = 0;
                if (m_PayExpenseSelectionSum > 0 && 
                    double.TryParse(s.Sum, out sum) &&
                    sum != m_PayExpenseSelectionSum)
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
        }


        List<PayExpense> FindPayExpense(EvaluatePayExpense comp)
        {
            List<PayExpense> expenses = new List<PayExpense>();
            foreach (var tt in this.payExpenseList.List)
            {
                PayExpense t = tt as PayExpense;
                if (comp(t))
                    expenses.Add(t);
            }
            return expenses;
        }

    }
}