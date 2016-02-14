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
            foreach (Lesson s in SavedFullListDuringSelection)
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

    }
}