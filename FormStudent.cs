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
        string m_StudentSelectionStatus;
        string m_StudentSelectionLearns;
        string m_StudentSelectionSpeaks;
        string m_StudentSelectionFirstName;
        string m_StudentSelectionLastName;
        string m_StudentSelectionSource;

        private void StudentToFormConst1()
        {
            m_dataTypes.Add(Modes.Student, typeof(Student));
            m_recordTypes.Add(Modes.Student, new StudentType(this));
            m_changed.Add(Modes.Student, false);
            m_loaded.Add(Modes.Student, false);

        }
        private void StudentToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Student.ToString());
        }

        public void ReplaceStudentList(Student[] target)
        {
            studentList.Clear();
            foreach (Student s in target)
                studentList.Add(s);
        }

        public void DoStudentSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                m_curType.StashRecordList();
            }

            DataList.Clear();
            foreach (Student s in SavedFullListDuringSelection)
            {
                if (!m_curType.Fit(m_StudentSelectionStatus, s.Status, true))
                    continue;
                if (!m_curType.Fit(m_StudentSelectionLearns, s.LearningLanguage, true))
                    continue;
                if (!m_curType.Fit(m_StudentSelectionSpeaks, s.NativeLanguage, true))
                    continue;
                if (!m_curType.Fit(m_StudentSelectionFirstName, s.FirstName, false))
                    continue;
                if (!m_curType.Fit(m_StudentSelectionLastName, s.LastName, false))
                    continue;
                if (!m_curType.Fit(m_StudentSelectionSource, s.Source, true))
                    continue;
                if (!m_curType.Fit(SelectionLevel, s.Level, true))
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
            Changed = true;
        }
    }
}
