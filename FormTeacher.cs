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
        //string m_RoomSelectionCapacity;

        private void TeacherToFormConst1()
        {
            m_dataTypes.Add(Modes.Teacher, typeof(Lesson));
            m_recordTypes.Add(Modes.Teacher, new LessonType(this));
            m_changed.Add(Modes.Teacher, false);
            m_loaded.Add(Modes.Teacher, false);
        }
        private void TeacherToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Teacher.ToString());
        }

        public void DoTeacherSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                m_curType.StashRecordList();
            }

            DataList.Clear();
            foreach (Teacher s in SavedFullListDuringSelection)
            {
                // TODO - make it >= number
                //if (!m_curType.Fit(m_RoomSelectionCapacity, s.Capacity.ToString(), true))
                //    continue;
                DataList.Add(s);
            }
            ShowCurrentCount();
            Changed = true;
        }

    }
}