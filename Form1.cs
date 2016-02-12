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
        // Local fields
        string[] m_enumLanguage;
        string[] m_enumLevel;
        string[] m_enumSource;
        string[] m_enumStatus;

        Modes m_mode;
        RecordType m_curType;
        Dictionary<Modes, bool> m_changed;
        Dictionary<Modes, bool> m_loaded;
        Dictionary<Modes, RecordType> m_recordTypes;
        Dictionary<Modes, Type> m_dataTypes;
        Dictionary<Modes, SchemaField[]> m_schemas;

        string m_remoteDir;             // place for remote file copy
        string m_recordKeeperDir;       // place for local file subdirs

        // Public Propertirs 
        public string DataLocation
        {
            get
            {
                return Path.Combine(m_recordKeeperDir, CurrentModeName).ToString();
            }
        }
        public string BackupLocation {
            get
            {
                return Path.Combine(DataLocation, "Backup").ToString();
            }
        }
        public int BackupLimit { get; set; }

        public Clouds CloudType { get; set; }

        public string CloudLocation
        {
            get
            {
                return Path.Combine(m_remoteDir, CurrentModeName + ".csv").ToString();
            }
        }
        public string CurrentModeName {
            get
            {
                return m_mode.ToString();
            } 
        }
        public bool Changed
        {
            get { return m_changed[m_mode];  }
            set { m_changed[m_mode] = value; }
        }

        public bool Loaded
        {
            get { return m_loaded[m_mode]; }
            set { m_loaded[m_mode] = value; }
        }

        
        public bool AnyFileChanged
        {
            get
            {
                for (int i = 0; i < (int)Modes.MaxMode; i++)
                    if (m_changed[(Modes)i])
                        return true;
                return false;
            }
        }

        public string SelectionLevel { get; set; }

        public SchemaField[] Schema { get; set; }
        public int[] Placements { get; set; }

        public List<string> DeletedKeys { get; set;  }
        public Dictionary<string, Record> RecordsAsRead { get; set; }
        public Record[] SavedFullListDuringSelection { get; set; }

        // WinForm form child control-related
        public string LastDownloadText { set { labelGlobLastDownload.Text = value; } }
        public string LastUploadText { set { labelGlobLastUpload.Text = value; } }

        public System.Windows.Forms.BindingSource DataList
        {
            get
            {
                // Here are links between manually crafted per-record-type UI and modes
                switch(m_mode)
                {
                    case Modes.Student:
                        return studentList;
                    case Modes.Room:
                        return roomList;
                    default:
                        return null;
                }
            }
        }


        #region "Form"
        public FormGlob()
        {
            m_dataTypes = new Dictionary<Modes, Type>();
            m_recordTypes = new Dictionary<Modes, RecordType>();
            m_schemas = new Dictionary<Modes, SchemaField[]>();
            m_changed = new Dictionary<Modes, bool>();
            m_loaded = new Dictionary<Modes, bool>();

            RecordsToFormConst1();

            // Initial mode is the first in the Modes enum
            SetModeNoUI( (Modes)0 );      
            Client = Environment.MachineName;
            ReadSettings();
            ReadSchemas();
            PrepareDataDirectories();
            InitializeComponent();
            AssignEnums();
            SetMode(m_mode);
            RecordsToFormConst2();
            cbGlobMode.SelectedIndex = (int)m_mode;
            SelectionMode = false;
            DeletedKeys = new List<string>();
            RecordsAsRead = new Dictionary<string, Record>();
        }

        private void RecordsToFormConst1()
        {
            StudentToFormConst1();
            TeacherToFormConst1();
            ProgramToFormConst1();
            RoomToFormConst1();
            LessonToFormConst1();

        }
        private void RecordsToFormConst2()
        {
            StudentToFormConst2();
            TeacherToFormConst2();
            ProgramToFormConst2();
            RoomToFormConst2();
            LessonToFormConst2();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.InitialDownload.ToLower() != "no")
            {
                if (!DownloadCurrentFile())
                    ReadCurrentFile();
            }
            else
                ReadCurrentFile();

            ShowCurrentCount();

            this.Size = Properties.Settings.Default.Form1Size;
            splitContainerGlobDataControls.SplitterDistance = Properties.Settings.Default.SplitDC;
            splitContainerGlobMasterDetail.SplitterDistance = Properties.Settings.Default.SplitMD;

            Changed = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Form1Size = this.Size;
            Properties.Settings.Default.SplitDC = splitContainerGlobDataControls.SplitterDistance;
            Properties.Settings.Default.SplitMD = splitContainerGlobMasterDetail.SplitterDistance;
            Properties.Settings.Default.Save();
        }
        #endregion

        #region "Mode navigation"

        private bool DownloadCurrentFile()
        {
            return m_curType.DownloadFile();
        }
        private bool UploadCurrentFile()
        {
            return m_curType.UploadFile();
        }
        private bool ReadCurrentFile()
        {
            return m_curType.ReadFile();
        }
        private void ShowCurrentCount()
        {
            m_curType.ShowCount();
        }
        private void ShowAllCurrent()
        {
            // TODO make it only for current type. Maybe new friend of Form object per rec type.
            m_StudentSelectionStatus = null;
            m_StudentSelectionLearns = null;
            m_StudentSelectionSpeaks = null;
            m_StudentSelectionFirstName = null;
            m_StudentSelectionLastName = null;
            m_StudentSelectionSource = null;

            m_RoomSelectionCapacity = null;
            m_RoomSelectionName = null;
            m_RoomSelectionRank = null;
            m_RoomSelectionTags = null;


        }

        private void SetMode(int mode)
        {
            SetMode((Modes)mode);
        }
        private void SetMode(Modes mode)
        {
            SetModeNoUI(mode);
            Schema = m_schemas[m_mode];
            tabControlModesBottom.SelectedIndex = (int)m_mode;
            tabControlModesTop.SelectedIndex = (int)m_mode;

        }

        private void SetModeNoUI(Modes mode)
        {
            m_mode = mode;
            m_curType = m_recordTypes[m_mode];
        }

        private void ChangeMode(Modes newMode)
        {
            if (AnyFileChanged)
                SaveChangedFiles();

            SetMode(newMode);
            if (!Loaded)
                ReadCurrentFile();
        }

        private void cbGlobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Modes newMode = (Modes)comboBox.SelectedIndex;
            if (newMode == m_mode)
                return;
            ChangeMode(newMode);
        }
        #endregion


        #region "DataGridClicks"
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != Record.LastColumnSorted)
            {
                Record.LastColumnSorted = e.ColumnIndex;
                Record.NeedToReverse = false;
            }
            else
                Record.NeedToReverse = !Record.NeedToReverse;

            DataGridViewColumn col = dataGridViewStudents.Columns[e.ColumnIndex];
            Record[] temp = m_curType.ForkOut<Record>(0);
            m_curType.SortRecords(col.HeaderText, temp);
            m_curType.ReplaceRecordList(temp);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex < dataGridViewStudents.ColumnCount &&
                e.RowIndex >= 0 && e.RowIndex < dataGridViewStudents.RowCount &&
                dataGridViewStudents[e.ColumnIndex, e.RowIndex].Value != null)

                Clipboard.SetText(dataGridViewStudents[e.ColumnIndex, e.RowIndex].Value.ToString());
        }

        #endregion

        #region "ButtonClicks"
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (DataList.CurrencyManager.Position < DataList.Count - 1)
                DataList.CurrencyManager.Position++;
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (DataList.CurrencyManager.Position > 0)
                DataList.CurrencyManager.Position--;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Record st = (Record)DataList.AddNew();
            st.Id = FormGlob.AllocateID();
            ShowCurrentCount();
            Changed = true;
            //tbStudFirstName.Select();  -- TODO
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Record s = (Record)DataList.Current;
            DeletedKeys.Add(s.Key);
            DataList.RemoveCurrent();
            ShowCurrentCount();
            Changed = true;
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            m_curType.EndSelectionMode();

            ShowAllCurrent();
            SelectionLevel = null;

            cbStudSelectStatus.SelectedIndex = 0;
            cbStudSelectLearns.SelectedIndex = 0;
            cbStudSelectSpeaks.SelectedIndex = 0;
            tbStudSelectFirstName.Text = "";
            tbStudSelectLastName.Text = "";
            cbStudSelectSource.SelectedIndex = 0;
            cbStudSelectLevel.SelectedIndex = 0;
            // Changed
        }

        private void buttonToExcel_Click(object sender, EventArgs e)
        {
            string tempCsv = m_curType.WriteTempFile();
            System.Diagnostics.Process.Start(tempCsv);
        }


        #endregion

        #region Student-related UI 
        private void comboBoxSelectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionStatus = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }

        private void comboBoxSelectLearns_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionLearns = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }

        private void comboBoxSelectSpeaks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionSpeaks = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }
        private void comboBoxSelectSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionSource = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }
        private void comboBoxSelectLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            SelectionLevel = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }


        private void textBoxSelectFirstName_TextChanged(object sender, EventArgs e)
        {
            TextBox testBox = (TextBox)sender;
            m_StudentSelectionFirstName = testBox.Text;
            m_curType.DoSelection();
        }

        private void textBoxSelectLastName_TextChanged(object sender, EventArgs e)
        {
            TextBox testBox = (TextBox)sender;
            m_StudentSelectionLastName = (string)testBox.Text;
            m_curType.DoSelection();
        }
        private void textBoxComments_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxCellPhone_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxHomePhone_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxAddress1_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxLanguageDetail_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxBirthday_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxSourceDetail_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxSchedule_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxInterests_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxGoals_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void textBoxBackground_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }


        #endregion

        #region Room-related UI
        private void tbRoomName_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void tbRoomCapacity_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void tbRoomPreferrability_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void tbRoomTags_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }

        private void tbRoomComments_TextChanged(object sender, EventArgs e)
        {
            Changed = true;
        }
        #endregion
    }
}
