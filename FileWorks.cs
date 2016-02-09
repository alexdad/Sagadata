using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace RecordKeeper
{
    public partial class FormGlob 
    {
        void PrepareDataDirectories()
        {
            string userDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string requestedDir = Properties.Settings.Default.DataDir;
            string sd = (requestedDir.Trim().Length == 0 ? 
                Path.Combine(userDir,      "Sagalingua").ToString() :
                Path.Combine(requestedDir, "Sagalingua").ToString() );

            if (!Directory.Exists(sd))
                Directory.CreateDirectory(sd);

            string rl = Path.Combine(sd, "Remote").ToString();
            if (!Directory.Exists(rl))
                Directory.CreateDirectory(rl);
            m_cloudLocation = Path.Combine(rl, m_fileName + ".csv");

            m_dataLocation = Path.Combine(sd, "Students").ToString();
            if (!Directory.Exists(m_dataLocation))
                Directory.CreateDirectory(m_dataLocation);

            m_backupLocation = Path.Combine(m_dataLocation, "Backup").ToString();
            if (!Directory.Exists(m_backupLocation))
                Directory.CreateDirectory(m_backupLocation);
        }
        void ReadSchemas()
        {
            string binLocation = Directory.GetCurrentDirectory();
            m_enumLanguage = File.ReadAllLines(Path.Combine(binLocation, "EnumLanguage.csv").ToString());
            m_enumLevel = File.ReadAllLines(Path.Combine(binLocation, "EnumLevel.csv").ToString());
            m_enumSource = File.ReadAllLines(Path.Combine(binLocation, "EnumSource.csv").ToString());
            m_enumStatus = File.ReadAllLines(Path.Combine(binLocation, "EnumStatus.csv").ToString());
            string[] schema = File.ReadAllLines(Path.Combine(binLocation, "SchemaStudent.csv").ToString());

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
                FormGlob.AccumulateID(st.Id);
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
            if (File.Exists(FilePath))
            {
                string bkup = DecideBackup();
                if (File.Exists(bkup))
                    File.Delete(bkup);
                File.Move(FilePath, bkup);
            }

            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                WriteHeader(sw);
                WriteValues(sw);
            }
        }

        string  WriteTempFile()
        {
            string tempFile = Path.GetTempFileName().ToLower().Replace(".tmp", ".csv");
            using (StreamWriter sw = new StreamWriter(tempFile))
            {
                WriteHeader(sw);
                WriteValues(sw);
            }
            return tempFile;
        }

        private bool Download()
        {
            bool success = false;
            switch (m_cloudType)
            {
                case Clouds.Google:
                    success = GDrive.Ops.DownloadStudentsFile(m_fileName + ".csv", m_cloudLocation);
                    if (!success)
                        MessageBox.Show("Cannot download from Google. Continuing to use local file.");
                    break;
                case Clouds.Azure:
                case Clouds.Dir:
                default:
                    break;
            }

            if (success)
            {
                labelGlobLastDownload.Text = "Last download: " + DateTime.Now.ToShortTimeString();
                Student[] temp = ReadCloudFile(m_cloudLocation);
                MergeBack(temp);
            }
            ShowStudentCount();
            return success;
        }

        private bool Upload()
        {
            Student[] temp = ReadCloudFile(m_cloudLocation);
            MergeBack(temp);
            WriteStudentsFile();
            labelGlobLastDownload.Text = "Last download: " + DateTime.Now.ToShortTimeString();

            if (File.Exists(m_cloudLocation))
                File.Delete(m_cloudLocation);

            File.Copy(FilePath, m_cloudLocation);

            bool success = false;
            switch (m_cloudType)
            {
                case Clouds.Google:
                    success = GDrive.Ops.UploadStudentsFile(m_cloudLocation, m_fileName + ".csv");
                    break;
                case Clouds.Azure:
                case Clouds.Dir:
                default:
                    break;
            }
            if (!success)
                MessageBox.Show("Cannot upload to the cloud. Local file is OK.");
            else
                labelGlobLastUpload.Text = "Last upload: " + DateTime.Now.ToShortTimeString();

            return success;
        }

        private void ReadSettings()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            m_cloudType = Clouds.None;
            m_fileName = Properties.Settings.Default.FileName;
            switch (Properties.Settings.Default.CloudType.ToLower())
            {
                case "dir":
                    m_cloudType = Clouds.Dir;
                    break;
                case "azure":
                    m_cloudType = Clouds.Azure;
                    break;
                case "google":
                    m_cloudType = Clouds.Google;
                    break;
            }

            m_backupLimit = Properties.Settings.Default.BackupLimit;
        }

        public string FilePath
        {
            get { return Path.Combine(m_dataLocation, m_fileName + ".csv"); }
        }

    }
}
