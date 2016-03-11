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
        string m_ProgramSelectionType;

        delegate bool EvaluateProgram(Program t);

        private void EditProgramDetailsChanged()
        {
            EditTrap = true;
            StaleComboLists = true;
        }

        private void DropProgramSelection()
        {
            m_ProgramSelectionType = null;
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
                DataList.Add(s);
            }
            ShowCurrentCount();
        }

        List<Program> ActivePrograms()
        {
            return FindPrograms(t => t.Actual);
        }

        List<Program> SpecificProgram(string desc)
        {
            return FindPrograms(t => t.Description == desc);
        }

        List<Program> FindPrograms(EvaluateProgram comp)
        {
            List<Program> programs = new List<Program>();
            foreach (var tt in this.programList.List)
            {
                Program t = tt as Program;
                if (comp(t))
                    programs.Add(t);
            }
            return programs;
        }

        public string GetProgramPrice(string desc)
        {
            List<Program> lst = SpecificProgram(desc);
            string res = "";
            foreach (Program t in lst)
                res = t.Price;

            return res;
        }
        public string GetProgramType(string desc)
        {
            List<Program> lst = SpecificProgram(desc);
            string res = "";
            foreach (Program t in lst)
                res = t.Type;

            return res;
        }
    }
}