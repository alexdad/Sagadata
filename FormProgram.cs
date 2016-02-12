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

        private void ProgramToFormConst1()
        {
            m_dataTypes.Add(Modes.Program, typeof(Program));
            m_recordTypes.Add(Modes.Program, new ProgramType(this));
            m_changed.Add(Modes.Program, false);
            m_loaded.Add(Modes.Program, false);
        }
        private void ProgramToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Program.ToString());
        }

        public void DoProgramSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                m_curType.StashRecordList();
            }

            DataList.Clear();
            foreach (Program s in SavedFullListDuringSelection)
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