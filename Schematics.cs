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
    public enum Modes
    {
        Students = 0,
        Teachers = 1,
        Programs = 2,
        Rooms = 3,
        Lessons = 4,
        MaxMode = 5
    }
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
        Date,
        Time,
        State,
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
                case "time":
                    Validation = Validations.Time;
                    break;
                case "date":
                    Validation = Validations.Date;
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
                case "State":
                    Validation = Validations.State;
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
            cbStudSearchLearns.Items.AddRange(m_enumLanguage);
            cbStudSearchSpeaks.Items.AddRange(m_enumLanguage);
            cbStudSelectStatus.Items.AddRange(m_enumStatus);
            cbStudSearchSource.Items.AddRange(m_enumSource);
            cbStudSearchLevel.Items.AddRange(m_enumLevel);

            cbStudLearns.Items.AddRange(m_enumLanguage);
            cbStudOther.Items.AddRange(m_enumLanguage);
            cbStudSpeaks.Items.AddRange(m_enumLanguage);
            cbStudLevel.Items.AddRange(m_enumLevel);
            cbStudSource.Items.AddRange(m_enumSource);
            cbStudStatus.Items.AddRange(m_enumStatus);

            cbTeachLanguage.Items.AddRange(m_enumLanguage);
            cbTeachLanguage2.Items.AddRange(m_enumLanguage);
            cbTeachStatus.Items.AddRange(m_enumStatus);

            cbSearchTeachLang1.Items.AddRange(m_enumLanguage);
            cbSearchTeachStatus.Items.AddRange(m_enumStatus);

            cbProgLanguage.Items.AddRange(m_enumLanguage);
            cbProgLevel.Items.AddRange(m_enumLevel);

            cbSearchProgLanguage.Items.AddRange(m_enumLanguage);
            cbSearchProgLevel.Items.AddRange(m_enumLevel);

            cbLessonState.Items.AddRange(m_enumState);
            cbLessonStart.Items.AddRange(m_enumTimeSlot);
            cbLessonEnd.Items.AddRange(m_enumTimeSlot);

            cbSchedNewLanguage.Items.AddRange(m_enumLanguage);
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
        public void ShowRoomCount()
        {
            labelGlobCount.Text = roomList.Count.ToString();
        }
        public void ShowTeacherCount()
        {
            labelGlobCount.Text = teacherList.Count.ToString();
        }
        public void ShowProgramCount()
        {
            labelGlobCount.Text = programList.Count.ToString();
        }
        public void ShowLessonCount()
        {
            labelGlobCount.Text = lessonList.Count.ToString();
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

            // Here will reside subdirs per mode for local files
            m_recordKeeperDir = Path.Combine(sd, "RecordKeeper").ToString();
            if (!Directory.Exists(m_recordKeeperDir))
                Directory.CreateDirectory(m_recordKeeperDir);

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                string dir = Path.Combine(m_recordKeeperDir, i.ToString());
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string backupdir  = Path.Combine(dir, "Backup").ToString();
                if (!Directory.Exists(backupdir))
                    Directory.CreateDirectory(backupdir);
            }

            // Here all remote file copies will sit
            m_remoteDir = Path.Combine(m_recordKeeperDir, "Remote");
            if (!Directory.Exists(m_remoteDir))
                Directory.CreateDirectory(m_remoteDir);
        }

        SchemaField[] ReadSchema(string name, SchemaField[] addition)
        {
            string[] schema = File.ReadAllLines(
                Path.Combine(Directory.GetCurrentDirectory(), 
                "Schema" + name + ".csv").ToString());

            List<SchemaField> schemaList = new List<SchemaField>();
            foreach (string s in schema)
            {
                string[] vals = s.Split(',');
                if (vals.Length != 3)
                    throw new Exception("ILlegal schema line: " + s);
                schemaList.Add(new SchemaField(vals[0], vals[1]));
            }

            if (addition != null)
            {
                foreach (var f in addition)
                    schemaList.Add(f);
            }

            return schemaList.ToArray();
        }

        void PopulateEnumTimeslots()
        {
            m_enumTimeSlot = new string[2 * (23 - 7)];
            int i = 0;
            for (int h = 7; h < 12; h++)
                for (int m = 0; m < 60; m += 30)
                    m_enumTimeSlot[i++] = h.ToString() + ":" + m.ToString() + " am";
            m_enumTimeSlot[i++] = "12:00 pm";
            m_enumTimeSlot[i++] = "12:30 pm";
            for (int h = 1; h < 11; h++)
                for (int m = 0; m < 60; m += 30)
                    m_enumTimeSlot[i++] = h.ToString() + ":" + m.ToString() + " pm";
        }

        void ReadSchemas()
        {
            string binLocation = Directory.GetCurrentDirectory();
            m_enumLanguage = File.ReadAllLines(Path.Combine(binLocation, "EnumLanguage.csv").ToString());
            m_enumLevel = File.ReadAllLines(Path.Combine(binLocation, "EnumLevel.csv").ToString());
            m_enumSource = File.ReadAllLines(Path.Combine(binLocation, "EnumSource.csv").ToString());
            m_enumStatus = File.ReadAllLines(Path.Combine(binLocation, "EnumStatus.csv").ToString());
            m_enumState = File.ReadAllLines(Path.Combine(binLocation, "EnumState.csv").ToString());

            PopulateEnumTimeslots();

            SchemaField[] schemaRecord = ReadSchema("Record", null);  // common fields

            foreach (var r in m_recordTypes.Values)
                r.Schema = ReadSchema(r.Mode.ToString(), schemaRecord);
        }

        private void ReadSettings()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            CloudType = Clouds.None;
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
    }
}