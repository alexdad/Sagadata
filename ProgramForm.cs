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
        string m_ProgramSelectionLanguage;
        string m_ProgramSelectionLevel;

        private void DropProgramSelection()
        {
            m_ProgramSelectionLanguage = null;
            m_ProgramSelectionLevel = null;
        }

        private void ProgramToFormConst1()
        {
            m_dataTypes.Add(Modes.Programs, typeof(Program));
            m_recordTypes.Add(Modes.Programs, new ProgramType(this));
        }
        private void ProgramToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Programs.ToString());
        }

        public void DoProgramSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (Program s in m_recordTypes[Modes.Programs].SavedFullListDuringSelection)
            {
                if (!CurrentType.Fit(m_ProgramSelectionLanguage, s.Language, true))
                    continue;
                if (!CurrentType.Fit(m_ProgramSelectionLevel, s.Level, true))
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
            Modified = true;
        }
    }
}