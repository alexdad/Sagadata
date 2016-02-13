using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class LessonType : RecordType
    {
        public LessonType(FormGlob glob) : base(glob)
        {
        }

        public override Modes Mode { get { return Modes.Lessons; } }

        public override bool ReadFile()
        {
            return ReadRecordsFile<Lesson>();
        }
        public override bool DownloadFile()
        {
            return Download<Lesson>();
        }
        public override bool UploadFile()
        {
            return Upload<Lesson>();
        }


        public override void ShowCount()
        {
            m_glob.ShowLessonCount();
        }

        public override void DoSelection()
        {
            m_glob.DoLessonSelection();
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortLessons(hdr, temp as Lesson[]);
        }
        public void SortLessons(string hdr, Lesson[] temp)
        {
            switch (hdr)
            {
                case "Day":
                    Array.Sort(temp, new Lesson.ComparerByDay());
                    break;
                case "End":
                    Array.Sort(temp, new Lesson.ComparerByEnd());
                    break;
                case "Program":
                    Array.Sort(temp, new Lesson.ComparerByProgram());
                    break;
                case "Room":
                    Array.Sort(temp, new Lesson.ComparerByRoom());
                    break;
                case "Start":
                    Array.Sort(temp, new Lesson.ComparerByStart());
                    break;
                case "State":
                    Array.Sort(temp, new Lesson.ComparerByState());
                    break;
                case "Student1":
                    Array.Sort(temp, new Lesson.ComparerByStudent1());
                    break;
                case "Student2":
                    Array.Sort(temp, new Lesson.ComparerByStudent2());
                    break;
                case "Student3":
                    Array.Sort(temp, new Lesson.ComparerByStudent3());
                    break;
                case "Student4":
                    Array.Sort(temp, new Lesson.ComparerByStudent4());
                    break;
                case "Student5":
                    Array.Sort(temp, new Lesson.ComparerByStudent5());
                    break;
                case "Student6":
                    Array.Sort(temp, new Lesson.ComparerByStudent6());
                    break;
                case "Student7":
                    Array.Sort(temp, new Lesson.ComparerByStudent7());
                    break;
                case "Student8":
                    Array.Sort(temp, new Lesson.ComparerByStudent8());
                    break;
                case "Student9":
                    Array.Sort(temp, new Lesson.ComparerByStudent9());
                    break;
                case "Student10":
                    Array.Sort(temp, new Lesson.ComparerByStudent10());
                    break;
                case "Teacher1":
                    Array.Sort(temp, new Lesson.ComparerByTeacher1());
                    break;
                case "Teacher2":
                    Array.Sort(temp, new Lesson.ComparerByTeacher2());
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