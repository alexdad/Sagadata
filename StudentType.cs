using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class StudentType : RecordType
    {
        public StudentType(FormGlob glob) : base(glob)
        {
        }

        public override string Class()
        {
            return "Student";
        }
        public override void Initialize()
        {

        }
        public override void ShowCount()
        {
            m_glob.ShowStudentCount();
        }

        public override void DoSelection()
        {
            m_glob.DoStudentSelection();
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortStudents(hdr, temp as Student[]);
        }
        public void SortStudents(string hdr, Student[] temp)
        {
            switch (hdr)
            {
                case "Address":
                    Array.Sort(temp, new Student.ComparerByAddress());
                    break;
                case "Birthday":
                    Array.Sort(temp, new Student.ComparerByBirthday());
                    break;
                case "Changed":
                    Array.Sort(temp, new Student.ComparerByChanged());
                    break;
                case "Email":
                    Array.Sort(temp, new Student.ComparerByEmail());
                    break;
                case "First Name":
                    Array.Sort(temp, new Student.ComparerByFirstName());
                    break;
                case "Last Name":
                    Array.Sort(temp, new Student.ComparerByLastName());
                    break;
                case "Learning":
                    Array.Sort(temp, new Student.ComparerByLearns());
                    break;
                case "Level":
                    Array.Sort(temp, new Student.ComparerByLevel());
                    break;
                case "Native":
                    Array.Sort(temp, new Student.ComparerBySpeaks());
                    break;
                case "Other":
                    Array.Sort(temp, new Student.ComparerByOther());
                    break;
                case "Phone":
                    Array.Sort(temp, new Student.ComparerByCellPhone());  // ignoring home phones
                    break;
                case "Source":
                    Array.Sort(temp, new Student.ComparerBySource());
                    break;
                case "Status":
                    Array.Sort(temp, new Student.ComparerByStatus());
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