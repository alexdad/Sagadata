using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class TeacherType : RecordType
    {
        public TeacherType(FormGlob glob) : base(glob)
        {
        }

        public override Modes Mode { get { return Modes.Teacher; } }

        public override bool ReadFile()
        {
            return ReadRecordsFile<Teacher>();
        }
        public override bool DownloadFile()
        {
            return Download<Teacher>();
        }
        public override bool UploadFile()
        {
            return Upload<Teacher>();
        }


        public override void ShowCount()
        {
            m_glob.ShowTeacherCount();
        }

        public override void DoSelection()
        {
            m_glob.DoTeacherSelection();
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortTeachers(hdr, temp as Teacher[]);
        }
        public void SortTeachers(string hdr, Teacher[] temp)
        {
            switch (hdr)
            {
                case "Name":
                    //Array.Sort(temp, new Teacher.ComparerByName());
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