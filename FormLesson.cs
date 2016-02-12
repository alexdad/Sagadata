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

        private void LessonToFormConst1()
        {
            m_dataTypes.Add(Modes.Lesson, typeof(Lesson));
            m_recordTypes.Add(Modes.Lesson, new LessonType(this));
            m_changed.Add(Modes.Lesson, false);
            m_loaded.Add(Modes.Lesson, false);
        }
        private void LessonToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Lesson.ToString());
        }

        public void DoLessonSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                m_curType.StashRecordList();
            }

            DataList.Clear();
            foreach (Lesson s in SavedFullListDuringSelection)
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