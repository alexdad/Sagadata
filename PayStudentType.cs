using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class PayStudentType : RecordType
    {
        public PayStudentType(FormGlob glob) : base(glob)
        {
        }

        public override Modes Mode { get { return Modes.PayStudents; } }
        
        public override bool ReadFile()
        {
            return ReadRecordsFile<PayStudent>();
        }
        public override bool DownloadFile()
        {
            return Download<PayStudent>();
        }
        public override bool UploadFile()
        {
            return Upload<PayStudent>();
        }


        public override void ShowCount()
        {
            m_glob.ShowPayStudentCount();
        }

        public override void DoSelection()
        {
            if (!m_glob.CheckSafety())
                return;
            m_glob.DoPayStudentSelection();
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortPayStudents(hdr, temp as PayStudent[]);
        }
        public void SortPayStudents(string hdr, PayStudent[] temp)
        {
            switch (hdr)
            {
                case "Student":
                    Array.Sort(temp, new PayStudent.ComparerByStudent());
                    break;
                case "Day":
                    Array.Sort(temp, new PayStudent.ComparerByDay());
                    break;
                case "Sum":
                    Array.Sort(temp, new PayStudent.ComparerBySum());
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