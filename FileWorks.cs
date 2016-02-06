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

        Student ParseValues(int ind, string[] vals)
        {
            Student st = new Student();

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
        void ReadStudentsFile(Dictionary<string, Student> studentsAsRead )
        {
            this.studentList.Clear();
            studentsAsRead.Clear();
            string[] sts = File.ReadAllLines(FilePath);

            ParseHeaders (sts[0].Split(','));

            for (int s = 1; s < sts.Length; s++)
            {
                string safeStr = SafeGuard(sts[s]);
                Student st = ParseValues(s, safeStr.Split(','));
                studentList.Add(new Student(st));
                studentsAsRead.Add(st.Key, st);
                Form1.AccumulateID(st.Id);
            }
        }

        Student[] ReadCloudFile(string cloudFile)
        {
            if (!File.Exists(cloudFile))
                throw new Exception("Cannot find cloud file");

            string[] sts = File.ReadAllLines(cloudFile);
            Student[] studs = new Student[sts.Length - 1];

            ParseHeaders(sts[0].Split(','));

            for (int s = 1; s < sts.Length; s++)
            {
                string safeStr = SafeGuard(sts[s]);
                studs[s - 1] = ParseValues(s, safeStr.Split(','));
            }

            return studs;
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
                Student st = s;
                if (m_studentsAsRead.ContainsKey(st.Key))
                {
                    Student oldSt = m_studentsAsRead[st.Key];
                    if (StudentDiffers(st, oldSt))
                    {
                        st.Changed = DateTime.Now;
                        st.ChangedBy = Client;
                    }
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < m_schema.Length; i++)
                {
                    string safeValue = st.Get(m_schema[i].Header);
                    if (safeValue == null)
                        safeValue = "";
                    safeValue = safeValue.Replace('"', ' ');
                    safeValue = safeValue.Replace(',', ';');
                    sb.Append("\"");
                    sb.Append(safeValue);
                    sb.Append("\",");
                }
                sw.WriteLine(sb.ToString());
            }
        }

        string DecideBackup()
        {
            string prefix = Path.Combine(m_backupLocation, m_fileName + ".backup");
            DateTime maxDt = DateTime.MinValue;
            const int maxBackup = 100;
            int index = -1;
            for (int i=0; i < maxBackup; i++)
            {
                string fl = prefix + i.ToString() + ".csv";
                if (File.Exists(fl) &&
                    File.GetLastWriteTime(fl) > maxDt)
                {
                    maxDt = File.GetLastWriteTime(fl);
                    index = i;
                }
            }
            if (index >= 0)
                index++;
            else
                index = 0;

            return prefix + index.ToString() + ".csv";
        }
        void WriteStudentsFile()
        {
            string bkup = DecideBackup();
            if (File.Exists(bkup))
                File.Delete(bkup);
            File.Move(FilePath, bkup );

            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                WriteHeader(sw);
                WriteValues(sw);
            }
        }
    }
}
