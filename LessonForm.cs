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
        string m_currentLessonKey;

        delegate bool EvaluateLesson(Lesson t);
        private void EditLessonDetailsChanged()
        {
            Modified = true;
            EditTrap = true;
        }

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

        public bool SetLessonDetails(string key, string program, string room, string comment)
        {
            foreach (var tt in this.lessonList.List)
            {
                Lesson t = tt as Lesson;
                if (t.Key == key)
                {
                    t.Program = program;
                    t.Room = room;
                    t.Comments = comment;
                    return true;
                }
            }
            return false;
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

        List<Lesson> FindLessonSlots(
            string state,
            string student,
            string teacher,
            string room,
            string program,
            DateTime t1,
            DateTime t2)
        {
            List<Lesson> lessons = new List<Lesson>();
            foreach (var tt in this.lessonList.List)
            {
                Lesson t = tt as Lesson;
                if (t.DateTimeStart >= t2 || t.DateTimeEnd < t1)
                    continue;
                if (state != null && state.Trim().Length > 1 &&
                    state != t.State)
                    continue;
                if (room != null && room.Trim().Length > 1 &&
                    room != t.Room)
                    continue;
                if (program != null && program.Trim().Length > 1 &&
                    program != t.Program)
                    continue;
                if (teacher != null && teacher.Trim().Length > 1 &&
                    teacher != t.Teacher1 && teacher != t.Teacher2)
                    continue;
                if (student != null && student.Trim().Length > 1 &&
                    student != t.Student1 && student != t.Student2 &&
                    student != t.Student3 && student != t.Student4 &&
                    student != t.Student5 && student != t.Student6 &&
                    student != t.Student7 && student != t.Student8 &&
                    student != t.Student9 && student != t.Student10)
                    continue;

                lessons.Add(t);
            }
            return lessons;
        }
    }
}