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
    public enum Realms
    {
        Live,
        Test
    }
    public enum Clouds
    {
        None,
        Dir,
        Google,
        Azure
    }
    public enum LessonStates
    {
        Unknown,
        Planned,
        Cancelled,
        Done
    }
    public enum MatchingState
    {
        Unknown,
        Linked,
        Match,
        Similar,
        NoMatch,
        LinkedToOther
    }
    public enum RepeatMode
    {
        None,
        Weekly,
        Biweekly,
        Monthly
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
        PricingType,
        Source
    }

    public struct RealmBindings
    {
        public RealmBindings(
            string realm,
            string applicationName,
            string authFile, 
            string calUser,
            string driveUser, 
            string systemCalendarI, 
            string operationalCalendarID)
        {
            this.Realm = realm;
            this.ApplicationName = applicationName;
            this.AuthFile = authFile;
            this.CalUser = calUser;
            this.DriveUser = driveUser;
            this.SystemCalendarID = systemCalendarI;
            this.OperationalCalendarID = operationalCalendarID;
        }

        public string Realm;
        public string ApplicationName;
        public string AuthFile;
        public string CalUser;
        public string DriveUser;
        public string SystemCalendarID;
        public string OperationalCalendarID;
    };

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
                case "PricingType":
                    Validation = Validations.PricingType;
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
        public static RealmBindings[] SagalinguaBindings =
        {
            new  RealmBindings(
                    "Live",
                    "Sagalingua1",
                    "client_id.json",
                    "Galia Dadiomova",
                    "sagalingua",
                    "6tgvdlnrp7i7h7uhjtnt9cjh8o@group.calendar.google.com",
                    "tptpj1po5oebik762sdpsvpum8@group.calendar.google.com"),

            new  RealmBindings(
                    "Test",
                    "Sagalingua1",
                    "client_id.json",
                    "Galia Dadiomova",
                    "alexdad",
                    "ujtnlq130tnjclegterqrh0ao8@group.calendar.google.com",
                    "tptpj1po5oebik762sdpsvpum8@group.calendar.google.com")
        };
        
        public static string ClientName;
        public static string ClientCode;
        public static int MaxID { get; set; }
        public static int AllocateID()
        {
            return ++MaxID;
        }
        public static void AccumulateID(int id)
        {
            MaxID = Math.Max(MaxID, id);
        }

        private void SetBindings()
        {
            for (int i = 0; i < SagalinguaBindings.Length; i++)
            {
                if (SagalinguaBindings[i].Realm == Properties.Settings.Default.Realm)
                {
                    Bindings = SagalinguaBindings[i];
                    return;
                }
            }
            throw new Exception("Unknow realm");
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

            cbPricingType.Items.AddRange(m_enumPricingType);

            cbLessonState.Items.AddRange(m_enumState);
            cbLessonStart.Items.AddRange(m_enumTimeSlot);
            cbLessonEnd.Items.AddRange(m_enumTimeSlot);

            cbPlanDuration.Items.AddRange(m_enumDurations);
            //cbPlanDuration.SelectedIndex = 5;   // Starting from 1:30

            cbViewSelectState.Items.AddRange(m_enumState);
        }

        public bool SelectionMode
        {
            get { return buttGlobalShowAll.Enabled; }
            set { buttGlobalShowAll.Enabled = value; }
        }

        public void ShowVersionAndRealm()
        {
            if (Properties.Settings.Default.Realm == "Test")
            {
                labelGlobalVersion.Text = "Test";
                lbViewVersion.Text = "Test";
            }
            else
            {
                labelGlobalVersion.Text = "v1.1";
                lbViewVersion.Text = "v1.1";
            }
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

            m_recordKeeperDir = Path.Combine(m_recordKeeperDir, 
                Properties.Settings.Default.Realm);

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
            m_enumTimeSlot = new string[4 * (23 - 7)];
            int i = 0;
            for (int h = 7; h < 12; h++)
                for (int m = 0; m < 60; m += 15)
                    m_enumTimeSlot[i++] = h.ToString() + ":" + m.ToString("00") + " AM";
            for (int m = 0; m < 60; m += 15)
                m_enumTimeSlot[i++] = "12:" + m.ToString("00") + " PM";
            for (int h = 1; h < 11; h++)
                for (int m = 0; m < 60; m += 15)
                    m_enumTimeSlot[i++] = h.ToString() + ":" + m.ToString("00") + " PM";
        }

        void PopulateEnumWeekdays()
        {
            m_enumWeekdayNames = new string[7] {"Monday", "Tuesday", "Wednesday",
                "Thursday", "Friday", "Saturday", "Sunday" };
        }

        void PopulateEnumDurations()
        {
            m_enumDurations = new string[8 * 4 - 1];
            int i = 0;
            for (int h = 0; h < 8; h++)
                for (int m = 0; m < 60; m += 15)
                {
                    if (h == 0 && m == 0)
                        continue;
                    m_enumDurations[i++] =
                        h.ToString() + ":" + m.ToString();
                }
        }

        public static string GetDurationString(DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;
            long slot = ts.Ticks / FormGlob.SlotInTicks;
            return (slot / 4).ToString() + ":" + (15 * (slot % 4)).ToString();
        }
        
        void ReadSchemas()
        {
            string binLocation = Directory.GetCurrentDirectory();
            m_enumLanguage = File.ReadAllLines(Path.Combine(binLocation, "EnumLanguage.csv").ToString());
            m_enumLevel = File.ReadAllLines(Path.Combine(binLocation, "EnumLevel.csv").ToString());
            m_enumSource = File.ReadAllLines(Path.Combine(binLocation, "EnumSource.csv").ToString());
            m_enumStatus = File.ReadAllLines(Path.Combine(binLocation, "EnumStatus.csv").ToString());
            m_enumState = File.ReadAllLines(Path.Combine(binLocation, "EnumState.csv").ToString());
            m_enumPricingType = File.ReadAllLines(Path.Combine(binLocation, "EnumPricingType.csv").ToString());

            PopulateEnumTimeslots();
            PopulateEnumWeekdays();
            PopulateEnumDurations();

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