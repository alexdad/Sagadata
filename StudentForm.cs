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
        string m_StudentSelectionLevel;

        delegate bool EvaluateStudent(Student t);

        private void DropStudentSelections()
        {
            m_StudentSelectionStatus = null;
            m_StudentSelectionLearns = null;
            m_StudentSelectionSpeaks = null;
            m_StudentSelectionFirstName = null;
            m_StudentSelectionLastName = null;
            m_StudentSelectionSource = null;
            m_StudentSelectionLevel = null;
        }

        private void StudentToFormConst1()
        {
            m_dataTypes.Add(Modes.Students, typeof(Student));
            m_recordTypes.Add(Modes.Students, new StudentType(this));

        }
        private void StudentToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Students.ToString());
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
                if (!CurrentType.Fit(m_StudentSelectionLevel, s.Level, true))
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
            Modified = true;
        }

        List<Student> SpecificStudent(string desc)
        {
            return FindStudents(t => (t.Description == desc));
        }

        List<Student> ActiveStudentsByLanguage(string lang)
        {
            return FindStudents(t =>
                    (t.Status == "Active" &&
                    (t.LearningLanguage== lang )));
        }

        List<Student> FindStudents(EvaluateStudent comp)
        {
            List<Student> students = new List<Student>();
            foreach (var tt in this.studentList.List)
            {
                Student t = tt as Student;
                if (comp(t))
                    students.Add(t);
            }
            return students;
        }

    }
}
