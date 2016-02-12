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

        public override Modes Mode { get { return Modes.Lesson; } }

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
                case "Name":
                    //Array.Sort(temp, new Lesson.ComparerByName());
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