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
            m_enumModes = File.ReadAllLines(Path.Combine(binLocation, "EnumModes.csv").ToString());
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
            Schema = schemaList.ToArray();


            // TODO - generalize into a file
            m_enumColumnNames = new string[]
                { "Status", "First Name", "Last Name", "Email", "Phone", "Learning", "Level",
                  "Native", "Other", "Birthday", "Source", "Address"};

            m_enumSortDirection = new string[2]
                { "Asc", "Desc"};
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
