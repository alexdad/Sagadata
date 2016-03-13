using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class PayExpenseType : RecordType
    {
        public PayExpenseType(FormGlob glob) : base(glob)
        {
        }

        public override Modes Mode { get { return Modes.PayExpenses; } }
        
        public override bool ReadFile()
        {
            return ReadRecordsFile<PayExpense>();
        }
        public override bool DownloadFile()
        {
            return Download<PayExpense>();
        }
        public override bool UploadFile()
        {
            return Upload<PayExpense>();
        }


        public override void ShowCount()
        {
            m_glob.ShowPayExpenseCount();
        }

        public override void DoSelection()
        {
            if (!m_glob.CheckSafety())
                return;
            m_glob.DoPayExpenseSelection();
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortPayExpenses(hdr, temp as PayExpense[]);
        }
        public void SortPayExpenses(string hdr, PayExpense[] temp)
        {
            switch (hdr)
            {
                case "Day":
                    Array.Sort(temp, new PayExpense.ComparerByDay());
                    break;
                case "Sum":
                    Array.Sort(temp, new PayExpense.ComparerBySum());
                    break;
                case "Category":
                    Array.Sort(temp, new PayExpense.ComparerByCategory());
                    break;
                default:
                    Record.NeedToReverse = false;
                    break;
            }
            if (Record.NeedToReverse)
                Array.Reverse(temp);
        }

    }
}