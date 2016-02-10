using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public enum Clouds
    {
        None,
        Dir,
        Google,
        Azure
    }
    public enum Validations
    {
        Ignore,
        Number,
        Text,
        Timing,
        Phone,
        Email,
        Status,
        Language,
        Level,
        Source
    }

    public struct SchemaField
    {
        public SchemaField(string h, string v)
        {
            Header = h;
            switch (v)
            {
                case "number":
                    Validation = Validations.Number;
                    break;
                case "text":
                    Validation = Validations.Text;
                    break;
                case "timing":
                    Validation = Validations.Timing;
                    break;
                case "phone":
                    Validation = Validations.Phone;
                    break;
                case "email":
                    Validation = Validations.Email;
                    break;
                case "Status":
                    Validation = Validations.Status;
                    break;
                case "Language":
                    Validation = Validations.Language;
                    break;
                case "Level":
                    Validation = Validations.Level;
                    break;
                case "Source":
                    Validation = Validations.Source;
                    break;
                default:
                    throw new Exception("Unknown schema type " + v);
            }
        }
        public string Header;
        public Validations Validation;
    }

    public partial class FormGlob : Form
    {
        public static string Client;
        public static int MaxID { get; set; }
        public static int AllocateID()
        {
            return ++MaxID;
        }
        public static void AccumulateID(int id)
        {
            MaxID = Math.Max(MaxID, id);
        }

        private void AssignEnums()
        {
            cbStudSelectLearns.Items.AddRange(m_enumLanguage);
            cbStudSelectSpeaks.Items.AddRange(m_enumLanguage);
            cbStudSelectStatus.Items.AddRange(m_enumStatus);
            cbStudSelectSource.Items.AddRange(m_enumSource);
            cbStudSelectLevel.Items.AddRange(m_enumLevel);

            cbStudLearns.Items.AddRange(m_enumLanguage);
            cbStudOther.Items.AddRange(m_enumLanguage);
            cbStudSpeaks.Items.AddRange(m_enumLanguage);
            cbStudLevel.Items.AddRange(m_enumLevel);
            cbStudSource.Items.AddRange(m_enumSource);
            cbStudStatus.Items.AddRange(m_enumStatus);

            cbGlobMode.Items.AddRange(m_enumModes);
        }

        public bool SelectionMode
        {
            get { return buttGlobalShowAll.Enabled; }
            set { buttGlobalShowAll.Enabled = value; }
        }

        public void ShowStudentCount()
        {
            labelGlobCount.Text = studentList.Count.ToString();
        }

        void PrepareDataDirectories()
        {
            string userDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string requestedDir = Properties.Settings.Default.DataDir;
            string sd = (requestedDir.Trim().Length == 0 ?
                Path.Combine(userDir, "Sagalingua").ToString() :
                Path.Combine(requestedDir, "Sagalingua").ToString());

            if (!Directory.Exists(sd))
                Directory.CreateDirectory(sd);

            string rl = Path.Combine(sd, "Remote").ToString();
            if (!Directory.Exists(rl))
                Directory.CreateDirectory(rl);

            CloudLocation = Path.Combine(rl, FileName + ".csv");

            DataLocation = Path.Combine(sd, "Students").ToString();
            if (!Directory.Exists(DataLocation))
                Directory.CreateDirectory(DataLocation);

            BackupLocation = Path.Combine(DataLocation, "Backup").ToString();
            if (!Directory.Exists(BackupLocation))
                Directory.CreateDirectory(BackupLocation);
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
            CloudType = Clouds.None;
            FileName = Properties.Settings.Default.FileName;
            switch (Properties.Settings.Default.CloudType.ToLower())
            {
                case "dir":
                    CloudType = Clouds.Dir;
                    break;
                case "azure":
                    CloudType = Clouds.Azure;
                    break;
                case "google":
                    CloudType = Clouds.Google;
                    break;
            }

            BackupLimit = Properties.Settings.Default.BackupLimit;
        }

        public string FilePath
        {
            get { return Path.Combine(DataLocation, FileName + ".csv"); }
        }

    }
}