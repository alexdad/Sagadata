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
        string m_teacherSelectionStatus;
        string m_teacherSelectionLanguage;
        string m_teacherSelectionFirstName;
        string m_teacherSelectionLastname;

        private void TeacherToFormConst1()
        {
            m_dataTypes.Add(Modes.Teachers, typeof(Lesson));
            m_recordTypes.Add(Modes.Teachers, new TeacherType(this));
        }
        private void TeacherToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Teachers.ToString());
        }

        public void DoTeacherSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (Teacher s in SavedFullListDuringSelection)
            {
                if (!CurrentType.Fit(m_teacherSelectionStatus, s.Status, true))
                    continue;
                if (!CurrentType.Fit(m_teacherSelectionLanguage, s.Language, true) &&
                    !CurrentType.Fit(m_teacherSelectionLanguage, s.Language2, true))
                    continue;
                if (!CurrentType.Fit(m_teacherSelectionFirstName, s.FirstName, false))
                    continue;
                if (!CurrentType.Fit(m_teacherSelectionLastname, s.LastName, false))
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
            Modified = true;
        }

    }
}