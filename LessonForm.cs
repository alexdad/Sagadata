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
        string m_LessonSelectionTeacher;
        string m_LessonSelectionStudent;
        string m_LessonSelectionDay;
        string m_LessonSelectionProgram;
        string m_LessonSelectionRoom;

        delegate bool EvaluateLesson(Lesson t);

        private void DropLessonSelection()
        {
            m_LessonSelectionTeacher = null;
            m_LessonSelectionStudent = null;
            m_LessonSelectionDay = null;
            m_LessonSelectionProgram = null;
            m_LessonSelectionRoom = null;
        }
        private void LessonToFormConst1()
        {
            m_dataTypes.Add(Modes.Lessons, typeof(Lesson));
            m_recordTypes.Add(Modes.Lessons, new LessonType(this));
        }
        private void LessonToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Lessons.ToString());
        }

        public void DoLessonSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (Lesson s in m_recordTypes[Modes.Lessons].SavedFullListDuringSelection)
            {
                if (!CurrentType.Fit(m_LessonSelectionTeacher, s.Teacher1, false) &&
                    !CurrentType.Fit(m_LessonSelectionTeacher, s.Teacher2, false))
                    continue;
                if (!CurrentType.Fit(m_LessonSelectionStudent, s.Student1, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student2, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student3, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student4, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student5, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student6, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student7, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student8, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student9, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student10, false)                    )
                    continue;
                if (!CurrentType.Fit(m_LessonSelectionDay, s.Day, true))
                    continue;
                if (!CurrentType.Fit(m_LessonSelectionProgram, s.Program, false))
                    continue;
                if (!CurrentType.Fit(m_LessonSelectionRoom, s.Room, false))
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
            Modified = true;
        }

        List<Lesson> LessonsByTime(DateTime t1, DateTime t2)
        {
            return FindLessons(l =>
                    DateTime.Parse(l.Start) <= t2 && 
                    DateTime.Parse(l.End) >= t1);
        }
        List<Lesson> LessonsByTeacher(string desc)
        {
            return FindLessons(l => l.Teacher1 == desc || l.Teacher2 == desc);
        }
        List<Lesson> LessonsByStudent(string desc)
        {
            return FindLessons(l => 
                l.Student1 == desc || l.Student2 == desc || l.Student3 == desc ||
                l.Student4 == desc || l.Student5 == desc || l.Student6 == desc ||
                l.Student7 == desc || l.Student8 == desc || l.Student9 == desc ||
                l.Student10 == desc);
        }

        List<Lesson> LessonsByTeacher(string desc, DateTime t1, DateTime t2)
        {
            return FindLessons(l =>
                    (l.Teacher1 == desc || l.Teacher2 == desc) &&
                    l.DateTimeStart <= t2 && l.DateTimeEnd >= t1);
        }

        List<Lesson> LessonsByStudent(string desc, DateTime t1, DateTime t2)
        {
            return FindLessons(l =>
                   (l.Student1 == desc || l.Student2 == desc || l.Student3 == desc ||
                    l.Student4 == desc || l.Student5 == desc || l.Student6 == desc ||
                    l.Student7 == desc || l.Student8 == desc || l.Student9 == desc ||
                    l.Student10 == desc) &&
                    l.DateTimeStart <= t2 &&
                    l.DateTimeEnd >= t1);
        }

        List<Lesson> FindLessons(EvaluateLesson comp)
        {
            List<Lesson> lessons = new List<Lesson>();
            foreach (var tt in this.lessonList.List)
            {
                Lesson t = tt as Lesson;
                if (comp(t))
                    lessons.Add(t);
            }
            return lessons;
        }

    }
}