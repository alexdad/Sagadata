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

        public override Modes Mode { get { return Modes.Teachers; } }

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
                case "Birthday":
                    Array.Sort(temp, new Teacher.ComparerByBirthday());
                    break;
                case "Email":
                    Array.Sort(temp, new Teacher.ComparerByEmail());
                    break;
                case "FirstName":
                    Array.Sort(temp, new Teacher.ComparerByFirstName());
                    break;
                case "LastName":
                    Array.Sort(temp, new Teacher.ComparerByLastName());
                    break;
                case "Language":
                    Array.Sort(temp, new Teacher.ComparerByLanguage());
                    break;
                case "Language2":
                    Array.Sort(temp, new Teacher.ComparerByLanguage2());
                    break;
                case "LanguageDetail":
                    Array.Sort(temp, new Teacher.ComparerByLanguageDetail());
                    break;
                case "MailingAddress":
                    Array.Sort(temp, new Teacher.ComparerByMailingAddress());
                    break;
                case "Phone":
                    Array.Sort(temp, new Teacher.ComparerByPhone());
                    break;
                case "Status":
                    Array.Sort(temp, new Teacher.ComparerByStatus());
                    break;
                case "Vacations":
                    Array.Sort(temp, new Teacher.ComparerByVacations());
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