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
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (Student s in SavedFullListDuringSelection)
            {
                if (!CurrentType.Fit(m_StudentSelectionStatus, s.Status, true))
                    continue;
                if (!CurrentType.Fit(m_StudentSelectionLearns, s.LearningLanguage, true))
                    continue;
                if (!CurrentType.Fit(m_StudentSelectionSpeaks, s.NativeLanguage, true))
                    continue;
                if (!CurrentType.Fit(m_StudentSelectionFirstName, s.FirstName, false))
                    continue;
                if (!CurrentType.Fit(m_StudentSelectionLastName, s.LastName, false))
                    continue;
                if (!CurrentType.Fit(m_StudentSelectionSource, s.Source, true))
                    continue;
                if (!CurrentType.Fit(SelectionLevel, s.Level, true))
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
            Modified = true;
        }
    }
}
