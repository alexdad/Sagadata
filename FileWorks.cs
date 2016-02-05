using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Students
{
    public partial class Form1 
    {
        string[] ReadEnum(string name)
        {
            return File.ReadAllLines(m_dataLocation + "\\Enum" + name + ".csv");
        }

        void ReadSchemas()
        {
            m_enumLanguage = ReadEnum("Language");
            m_enumLevel = ReadEnum("Level");
            m_enumSource = ReadEnum("Source");
            m_enumStatus = ReadEnum("Status");

            string[] schema = File.ReadAllLines(m_dataLocation + "\\SchemaStudent.csv");
            List<SchemaField> schemaList = new List<SchemaField>();
            foreach (string s in schema)
            {
                string[] vals = s.Split(',');
                if (vals.Length != 3)
                    throw new Exception("ILlegal schema line: " + s);
                schemaList.Add(new SchemaField(vals[0], vals[1]));
            }
            m_schema = schemaList.ToArray();

            m_enumColumnNames = new string[]
                { "Status", "First Name", "Last Name", "Email", "Phone", "Learning", "Level",
                  "Native", "Other", "Birthday", "Source", "Address"};

            m_enumSortDirection = new string[2]
                { "Asc", "Desc"};


        }

        void ParseHeaders(string[] hdrs)
        {
            m_placements = new int[hdrs.Length];

            for (int j = 0; j < hdrs.Length; j++)
                m_placements[j] = -1;

            for (int i = 0; i < hdrs.Length; i++)
            {
                for (int j = 0; j < m_schema.Length; j++)
                {
                    if (m_schema[j].Header.ToLower() == hdrs[i].ToLower())
                        m_placements[i] = j;
                }
            }
        }

        void AssignMissingID()
        {
            int maxId = 1;
            foreach (Student st in studentList)
            {
                if (st.Id > maxId)
                    maxId = st.Id;
            }

            foreach (Student st in studentList)
            {
                if (st.Id <= 0)
                    st.Id = ++maxId;
            }
        }


        Student ParseValues(int ind, string[] vals)
        {
            Student st = Student.Factory();

            for (int i = 0; i < m_placements.Length; i++)
            {
                int fld = m_placements[i];
                if (fld < 0)
                    continue;
                Validations tp = m_schema[fld].Validation;
                string val = vals[i];
                if (tp == Validations.Ignore)
                    continue;
    
                // Check validations here

                bool success = st.Set(m_schema[fld].Header, val );
                if (!success)
                    MessageBox.Show("Bad value");
            }

            return st;
        }
        void ReadStudentsFile()
        {
            this.studentList.Clear();
            string[] sts = File.ReadAllLines(m_dataLocation + "\\Students.csv");

            ParseHeaders (sts[0].Split(','));

            for (int s = 1; s < sts.Length; s++)
            {
                string safeStr = SafeGuard(sts[s]);
                studentList.Add(ParseValues(s, safeStr.Split(',')));
            }

            AssignMissingID();
        }

        private string SafeGuard(string s)
        {
            int quotes = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ',' && quotes % 2 == 1)
                    sb.Append(' ');
                else
                {
                    sb.Append(s[i]);
                    if (s[i] == '\"')
                        quotes++;
                }
            }
            return sb.ToString();
        }

        void WriteHeader(StreamWriter sw)
        {
            StringBuilder sb = new StringBuilder();
            for (int i=0; i < m_schema.Length; i++)
            {
                sb.Append(m_schema[i].Header);
                sb.Append(",");
            }
            sw.WriteLine(sb.ToString());
        }
        void WriteValues(StreamWriter sw)
        {
            foreach(Student s in studentList)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < m_schema.Length; i++)
                {
                    string safeValue = s.Get(m_schema[i].Header);
                    safeValue = safeValue.Replace('"', ' ');
                    safeValue = safeValue.Replace(',', ';');
                    sb.Append("\"");
                    sb.Append(safeValue);
                    sb.Append("\",");
                }
                sw.WriteLine(sb.ToString());
            }
        }

        void WriteStudentsFile()
        {
            string bkup = m_dataLocation + "\\Students.backup.csv";
            string target = m_dataLocation + "\\Students.csv";
            if (File.Exists(bkup))
                File.Delete(bkup);
            File.Move( target, bkup );

            using (StreamWriter sw = new StreamWriter(target))
            {
                WriteHeader(sw);
                WriteValues(sw);
            }
        }

        private void CaptureStudentEditing()
        {
            m_curStudent.Birthday = textBoxBirthday.Text;
            m_curStudent.Email = textBoxEmail.Text;
            m_curStudent.MailingAddress = textBoxAddress1.Text;
            m_curStudent.FirstName = textBoxFirstName.Text;
            m_curStudent.LastName = textBoxLastName.Text;
            m_curStudent.PossibleSchedule = textBoxSchedule.Text;
            m_curStudent.HomePhone = textBoxHomePhone.Text;
            m_curStudent.CellPhone = textBoxCellPhone.Text;
            m_curStudent.Comments = textBoxComments.Text;
            m_curStudent.Interests = textBoxInterests.Text;
            m_curStudent.Goals = textBoxGoals.Text;
            m_curStudent.Background = textBoxBackground.Text;
            m_curStudent.SourceDetail = textBoxSourceDetail.Text;
            m_curStudent.Birthday = textBoxBirthday.Text;
            m_curStudent.LanguageDetail = textBoxLanguageDetail.Text;
            m_curStudent.LearningLanguage = comboBoxLearns.Text;
            m_curStudent.Level = comboBoxLevel.Text;
            m_curStudent.OtherLanguage = comboBoxOther.Text;
            m_curStudent.Source = comboBoxSource.Text;
            m_curStudent.NativeLanguage =comboBoxSpeaks.Text;
            m_curStudent.Status = comboBoxStatus.Text;
            m_curStudent.Changed = DateTime.Now;
        }
    }
}
