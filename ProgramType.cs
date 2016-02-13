using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public class ProgramType : RecordType
    {
        public ProgramType(FormGlob glob) : base(glob)
        {
        }

        public override Modes Mode { get { return Modes.Programs; } }
        
        public override bool ReadFile()
        {
            return ReadRecordsFile<Program>();
        }
        public override bool DownloadFile()
        {
            return Download<Program>();
        }
        public override bool UploadFile()
        {
            return Upload<Program>();
        }


        public override void ShowCount()
        {
            m_glob.ShowProgramCount();
        }

        public override void DoSelection()
        {
            m_glob.DoProgramSelection();
        }

        public override void SortRecords(string hdr, Record[] temp)
        {
            SortPrograms(hdr, temp as Program[]);
        }
        public void SortPrograms(string hdr, Program[] temp)
        {
            switch (hdr)
            {
                case "Code":
                    Array.Sort(temp, new Program.ComparerByCode());
                    break;
                case "Language":
                    Array.Sort(temp, new Program.ComparerByLanguage());
                    break;
                case "Level":
                    Array.Sort(temp, new Program.ComparerByLevel());
                    break;
                case "Name":
                    Array.Sort(temp, new Program.ComparerByName());
                    break;
                case "Price":
                    Array.Sort(temp, new Program.ComparerByPrice());
                    break;
                case "Summary":
                    Array.Sort(temp, new Program.ComparerBySummary());
                    break;
                default:
                    Record.NeedToReverse = false;
                    break;
            }
            if (Record.NeedToReverse)
                Array.Reverse(temp);
        }

    }
}