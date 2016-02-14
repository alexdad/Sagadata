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
        string[] m_enumState;
        string[] m_enumTimeSlot;

        Modes m_mode;
        Dictionary<Modes, RecordType> m_recordTypes;
        Dictionary<Modes, Type> m_dataTypes;

        string m_remoteDir;             // place for remote file copy
        string m_recordKeeperDir;       // place for local file subdirs

        // Public Propertirs 
        public Modes CurrentMode
        {
            get { return m_mode; }
        }
        public RecordType CurrentType
        {
            get { return m_recordTypes[m_mode]; }
        }
        public string CurrentModeName
        {
            get { return CurrentMode.ToString(); }
        }
        public bool Modified
        {
            get { return CurrentType.Modified; }
            set { CurrentType.Modified = value; }
        }
        public bool Loaded
        {
            get { return CurrentType.Loaded; }
            set { CurrentType.Loaded = value; }
        }
        public SchemaField[] Schema
        {
            get { return CurrentType.Schema; }
            set { CurrentType.Schema = value; }
        }
        public int[] Placements
        {
            get { return CurrentType.Placements; }
            set { CurrentType.Placements = value; }
        }
        public string DataLocation
        {
            get { return Path.Combine(m_recordKeeperDir, CurrentModeName).ToString(); }
        }
        public string FilePath
        {
            get { return Path.Combine(DataLocation, CurrentModeName + ".csv"); }
        }

        public string BackupLocation {
            get { return Path.Combine(DataLocation, "Backup").ToString(); }
        }
        public int BackupLimit { get; set; }

        public Clouds CloudType { get; set; }

        public string CloudLocation
        {
            get { return Path.Combine(m_remoteDir, CurrentModeName + ".csv").ToString(); }
        }
        
        public bool AnyFileChanged
        {
            get
            {
                foreach (RecordType r in m_recordTypes.Values)
                {
                    if (r.Modified)
                    {
                        Modes dt = r.Mode;
                        return true;
                    }
                }
                return false;
            }
        }

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
                    case Modes.Programs:
                        return programList;
                    case Modes.Rooms:
                        return roomList;
                    case Modes.Students:
                        return studentList;
                    case Modes.Teachers:
                        return teacherList;
                    case Modes.Lessons:
                        return lessonList;

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

            RecordsToFormConst1();

            // Initial mode is the first in the Modes enum
            m_mode = (Modes)0 ;      
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

            Modified = false;
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
            return CurrentType.DownloadFile();
        }
        private bool UploadCurrentFile()
        {
            return CurrentType.UploadFile();
        }
        private bool ReadCurrentFile()
        {
            return CurrentType.ReadFile();
        }
        private void ShowCurrentCount()
        {
            CurrentType.ShowCount();
        }
        private void ShowAllCurrent()
        {
            DropStudentSelections();
            DropTeacherSelection();
            DropRoomSelection();
            DropProgramSelection();
            DropLessonSelection();
        }

        private void SetMode(int mode)
        {
            SetMode((Modes)mode);
        }
        private void SetMode(Modes mode)
        {
            m_mode = mode;
            tabControlModesBottom.SelectedIndex = (int)m_mode;
            tabControlModesTop.SelectedIndex = (int)m_mode;
            tabControlSearch.SelectedIndex = (int)m_mode;

        }

        private void ChangeMode(Modes newMode)
        {
            CurrentType.EndSelectionMode();

            if (AnyFileChanged)
                SaveChangedFiles();

            SelectionMode = false;
            SavedFullListDuringSelection = null;

            SetMode(newMode);
            if (!Loaded)
                ReadCurrentFile();

            ShowCurrentCount();
            ManageSearchWindow();
        }

        void ManageSearchWindow()
        {
            // For now we may only supply few searches
            this.panelGlobSearch.Visible = 
                (m_mode == Modes.Students || 
                 m_mode == Modes.Teachers ||
                 m_mode == Modes.Lessons  ||
                 m_mode == Modes.Programs);
            
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
        private void dgvColumnSort<T>(DataGridView dgv, T[] temp, int column) where T : Record
        {
            if (column != Record.LastColumnSorted)
            {
                Record.LastColumnSorted = column;
                Record.NeedToReverse = false;
            }
            else
                Record.NeedToReverse = !Record.NeedToReverse;

            DataGridViewColumn col = dgv.Columns[column];
            CurrentType.SortRecords(col.HeaderText, temp);
            CurrentType.ReplaceRecordList(temp);
        }

        private void dgvCellCopy(DataGridView dgv, int row, int column)
        {
            if (column >= 0 && column < dgv.ColumnCount &&
                row >= 0 && row < dgv.RowCount &&
                dgv[column, row].Value != null)

                Clipboard.SetText(dgv[column, row].Value.ToString());
        }


        private void dgvStudents_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvColumnSort<Student>(
                sender as DataGridView,
                CurrentType.ForkOut<Student>(0),
                e.ColumnIndex);
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCellCopy(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvTeachers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvColumnSort<Teacher>(
                sender as DataGridView,
                CurrentType.ForkOut<Teacher>(0),
                e.ColumnIndex);
        }

        private void dgvTeachers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCellCopy(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }


        private void dgvPrograms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCellCopy(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvPrograms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvColumnSort<Program>(
                sender as DataGridView,
                CurrentType.ForkOut<Program>(0),
                e.ColumnIndex);
        }

        private void dgvRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCellCopy(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvRooms_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvColumnSort<Room>(
                sender as DataGridView,
                CurrentType.ForkOut<Room>(0),
                e.ColumnIndex);
        }

        private void dgvLesson_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCellCopy(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvLesson_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvColumnSort<Lesson>(
                sender as DataGridView,
                CurrentType.ForkOut<Lesson>(0),
                e.ColumnIndex);
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
            st.ChangedBy = Client;
            st.CreatedBy = Client;
            ShowCurrentCount();
            Modified = true;
            //tbStudFirstName.Select();  -- TODO
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Record s = (Record)DataList.Current;
            DeletedKeys.Add(s.Key);
            DataList.RemoveCurrent();
            ShowCurrentCount();
            Modified = true;
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            CurrentType.EndSelectionMode();

            ShowAllCurrent();

            cbStudSelectStatus.SelectedIndex = 0;
            cbStudSearchLearns.SelectedIndex = 0;
            cbStudSearchSpeaks.SelectedIndex = 0;
            tbStudSearchFirstName.Text = "";
            tbStudSearchLastName.Text = "";
            cbStudSearchSource.SelectedIndex = 0;
            cbStudSearchLevel.SelectedIndex = 0;

            cbTeachLanguage.SelectedIndex = 0;
            cbTeachLanguage2.SelectedIndex = 0;
            cbTeachStatus.SelectedIndex = 0;
            tbSearchTeachFirstName.Text = "";
            tbSearchTeachLastName.Text = "";

            cbProgLanguage.SelectedIndex = 0;
            cbProgLevel.SelectedIndex = 0;

            // Changed
        }

        private void buttonToExcel_Click(object sender, EventArgs e)
        {
            string tempCsv = CurrentType.WriteTempFile();
            System.Diagnostics.Process.Start(tempCsv);
        }


        #endregion

        #region Student-related UI 
        private void cbSearchStudStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionStatus = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void cbSearchStudLearns_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionLearns = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void cbSearchStudSpeaks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionSpeaks = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }
        private void cbSearchStudSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionSource = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }
        private void cbSearchStudLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_StudentSelectionLevel = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }


        private void tbSearchStudFirstName_TextChanged(object sender, EventArgs e)
        {
            TextBox testBox = (TextBox)sender;
            m_StudentSelectionFirstName = testBox.Text;
            CurrentType.DoSelection();
        }

        private void tbSearchStudLastName_TextChanged(object sender, EventArgs e)
        {
            TextBox testBox = (TextBox)sender;
            m_StudentSelectionLastName = (string)testBox.Text;
            CurrentType.DoSelection();
        }
        private void tbStudComments_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudEmail_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudFirstName_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudLastName_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudCellPhone_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudHomePhone_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudAddress_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudLanguageDetail_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudBirthday_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudxSourceDetail_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudSchedule_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudInterests_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudGoals_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbStudBackground_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }


        #endregion

        #region Teacher-related UI
        private void cbSearchTeachStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_teacherSelectionStatus = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void cbSearchTeachLang1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_teacherSelectionLanguage = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void tbSearchTeachFirstName_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            m_teacherSelectionFirstName = (string)tb.Text;
            CurrentType.DoSelection();
        }

        private void tbSearchTeachLastName_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            m_teacherSelectionLastname = (string)tb.Text;
            CurrentType.DoSelection();
        }

        private void tbTeachFirstName_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbTeachLastName_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbTeachEmail_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbTeachLastBirthday_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbTeachPhone_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbTeachStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbTeachLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbTeachLanguage2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbTeachLanguageDetail_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbTeachAddress_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbTeachVacations_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbTeachComment_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        #endregion

        #region Room-related UI
        private void tbRoomName_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbRoomCapacity_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbRoomPreferrability_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbRoomTags_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbRoomComments_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }
        #endregion

        #region Program-related UI
        private void tbProgCode_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbProgName_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbProgLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbProgLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbProgPrice_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbProgSummary_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbProgComments_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbSearchProgLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_ProgramSelectionLanguage = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void cbSearchProgLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_ProgramSelectionLevel = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }
        #endregion

        #region Lesson-related UI
        private void cbLessonState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            Modified = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStudent3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStudent4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStudent5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStudent6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStudent7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStudent8_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStudent9_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void cbLessonStudent10_SelectedIndexChanged(object sender, EventArgs e)
        {
            Modified = true;
        }

        private void tbLEssonComment_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }


        private void tbSearchLessonStudent_TextChanged(object sender, EventArgs e)
        {
            TextBox comboBox = (TextBox)sender;
            m_LessonSelectionStudent = comboBox.Text;
            CurrentType.DoSelection();
        }

        private void tbSearchLessonTeacher_TextChanged(object sender, EventArgs e)
        {
            TextBox comboBox = (TextBox)sender;
            m_LessonSelectionTeacher = comboBox.Text;
            CurrentType.DoSelection();
        }

        private void tbSearchLessonDate_TextChanged(object sender, EventArgs e)
        {
            TextBox comboBox = (TextBox)sender;
            m_LessonSelectionDay = comboBox.Text;
            CurrentType.DoSelection();
        }

        private void cbSearchLessonProgram_TextChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_LessonSelectionProgram = comboBox.Text;
            CurrentType.DoSelection();
        }

        private void cbSearchLessonRoom_TextChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_LessonSelectionRoom = comboBox.Text;
            CurrentType.DoSelection();
        }
        #endregion
    }
}
