using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class PayTeacherType : RecordType
    {
        public PayTeacherType(FormGlob glob) : base(glob)
        {
        }

        public override Modes Mode { get { return Modes.PayTeachers; } }
        
        public override bool ReadFile()
        {
            return ReadRecordsFile<PayTeacher>();
        }
        public override bool DownloadFile()
        {
            return Download<PayTeacher>();
        }
        public override bool UploadFile()
        {
            return Upload<PayTeacher>();
        }


        public override void ShowCount()
        {
            m_glob.ShowPayTeacherCount();
        }

        public override void DoSelection()
        {
            if (!m_glob.CheckSafety())
                return;
            m_glob.DoPayTeacherSelection();
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortPayTeachers(hdr, temp as PayTeacher[]);
        }
        public void SortPayTeachers(string hdr, PayTeacher[] temp)
        {
            switch (hdr)
            {
                case "Teacher":
                    Array.Sort(temp, new PayTeacher.ComparerByTeacher());
                    break;
                case "Day":
                    Array.Sort(temp, new PayTeacher.ComparerByDay());
                    break;
                case "Sum":
                    Array.Sort(temp, new PayTeacher.ComparerBySum());
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