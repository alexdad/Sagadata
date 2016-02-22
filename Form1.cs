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
        public static long SlotInTicks = 0;

        // Local fields
        string[] m_enumLanguage;
        string[] m_enumLevel;
        string[] m_enumSource;
        string[] m_enumStatus;
        string[] m_enumState;
        string[] m_enumTimeSlot;
        string[] m_enumDurations;

        Dictionary<Modes, RecordType> m_recordTypes;
        Dictionary<Modes, Type> m_dataTypes;

        string m_remoteDir;             // place for remote file copy
        string m_recordKeeperDir;       // place for local file subdirs

        // Public Propertirs 
        public Modes CurrentMode { get; set;   }
        public RecordType CurrentType
        {
            get { return m_recordTypes[CurrentMode]; }
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

        // WinForm form child control-related
        public string LastDownloadText { set { labelGlobLastDownload.Text = value; } }
        public string LastUploadText { set { labelGlobLastUpload.Text = value; } }

        public System.Windows.Forms.BindingSource DataList
        {
            get
            {
                // Here are links between manually crafted per-record-type UI and modes
                switch(CurrentMode)
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
            CurrentMode = (Modes)0 ;      
            Client = Environment.MachineName;
            ReadSettings();
            ReadSchemas();
            PrepareDataDirectories();
            InitializeComponent();
            AssignEnums();
            SetMode(CurrentMode);
            RecordsToFormConst2();
            cbGlobMode.SelectedIndex = (int)CurrentMode;
            SelectionMode = false;

            DateTime dts = DateTime.Now;
            DateTime dtn = dts.AddMinutes(15);
            SlotInTicks = (dtn - dts).Ticks;
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

        private void AssignListsToComboBoxes()
        {
            List<Student> students = ActiveStudents();
            List<String> studentNames = students.ConvertAll(x => x.Description);
            cbSearchLessonStudent.Items.AddRange(studentNames.ToArray());

            List<Teacher> teachers = ActiveTeachers();
            List<String> teacherNames = teachers.ConvertAll(x => x.Description);
            cbSearchLessonTeacher.Items.AddRange(teacherNames.ToArray());

            List<Program> programs = ActivePrograms();
            List<String> programNames = programs.ConvertAll(x => x.Description);
            cbSearchLessonProgram.Items.AddRange(programNames.ToArray());
            cbLessonProg.Items.AddRange(programNames.ToArray());
            cbViewDetailProgram.Items.AddRange(programNames.ToArray());

            List<Room> rooms = ActiveRooms();
            List<String> roomNames = rooms.ConvertAll(x => x.Description);
            cbSearchLessonRoom.Items.AddRange(roomNames.ToArray());
            cbLessonRoom.Items.AddRange(roomNames.ToArray());
            cbViewDetailRoom.Items.AddRange(roomNames.ToArray());
            cbPlanProgram.Items.AddRange(programNames.ToArray());

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.InitialDownload.ToLower() != "no")
            {
                if (!DownloadAllFiles())
                    ReadAllFiles();
            }
            else
                ReadAllFiles();

            ShowCurrentCount();

            this.Size = Properties.Settings.Default.Form1Size;
            splitContainerGlobDataControls.SplitterDistance = Properties.Settings.Default.SplitDC;
            splitContainerGlobMasterDetail.SplitterDistance = Properties.Settings.Default.SplitMD;
            AssignListsToComboBoxes();
            Modified = false;

            // Start as View day
            tabControlOps.SelectedIndex = (int)TabControlOps.View;
            ShowView();
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
            CurrentMode = mode;
            tabControlModesBottom.SelectedIndex = (int)CurrentMode;
            tabControlModesTop.SelectedIndex = (int)CurrentMode;
            tabControlSearch.SelectedIndex = (int)CurrentMode;

        }

        private void ChangeMode(Modes newMode)
        {
            if (!CheckSafety())
                return;

            this.splitContainerGlobDataControls.Panel1.Visible = false;

            CurrentType.EndSelectionMode();

            if (AnyFileChanged)
                SaveChangedFiles();

            SelectionMode = false;
            CurrentType.SavedFullListDuringSelection = null;
            SetMode(newMode);
            if (!Loaded)
                ReadCurrentFile();

            ShowCurrentCount();
            ManageSearchWindow();

            this.splitContainerGlobDataControls.Panel1.Visible = true;
        }

        public bool CheckSafety()
        {
            if (m_unsavedAvailabilityChanges)
            {
                if (MessageBox.Show(
                    "Want to lose them?",
                    "You did not grab changes in availability form",
                    MessageBoxButtons.YesNo)
                                            != DialogResult.Yes)
                    return false;
                else
                    DropFlagUnsavedAvailabilityChanges();
            }
            return true;
        }

        void ManageSearchWindow()
        {
            // For now we may only supply few searches
            this.panelGlobSearch.Visible = 
                (CurrentMode == Modes.Students ||
                 CurrentMode == Modes.Teachers ||
                 CurrentMode == Modes.Lessons  ||
                 CurrentMode == Modes.Programs);
            
        }

        private void cbGlobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            Modes newMode = (Modes)comboBox.SelectedIndex;
            if (newMode == CurrentMode)
                return;
            ChangeMode(newMode);
        }
        #endregion
        
        #region "Data Grid Clicks"
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
        private void dgvTeachers__CurrentChanged(object sender, EventArgs e)
        {
            if (m_unsavedAvailabilityChanges)
            {
                MessageBox.Show("You've lost changes in the availability form!");
                DropFlagUnsavedAvailabilityChanges();
            }
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

        private void dgvPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProposeNewLesson(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvViewSlots_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewSelectLesson(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        #endregion

        #region "Global Button Clicks"
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;
            if (DataList.CurrencyManager.Position < DataList.Count - 1)
                DataList.CurrencyManager.Position++;
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;
            if (DataList.CurrencyManager.Position > 0)
                DataList.CurrencyManager.Position--;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;
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
            if (!CheckSafety())
                return;
            Record s = (Record)DataList.Current;
            CurrentType.DeletedKeys.Add(s.Key);
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

            cbSearchLessonStudent.SelectedIndex = -1;
            cbSearchLessonTeacher.SelectedIndex = -1;
            cbSearchLessonProgram.SelectedIndex = -1;
            cbSearchLessonRoom.SelectedIndex = -1;
            tbSearchLessonDate.Text = "";



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
        private void dgvTeachers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTeachers.CurrentRow == null)
                return;  
            if (dgvTeachers.CurrentRow.Index != m_teacherDgvCurrentRow)
            {
                m_teacherDgvCurrentRow = dgvTeachers.CurrentRow.Index;
                ShowAvailabilityForm();
            }
        }

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
        private void availAcceptButton_Click(object sender, EventArgs e)
        {
            AcceptAvailabilityEdits();
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

        private void tbLessonComment_TextChanged(object sender, EventArgs e)
        {
            Modified = true;
        }


        private void cbSearchLessonStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_LessonSelectionStudent = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void cbSearchLessonTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_LessonSelectionTeacher = comboBox.Text;
            CurrentType.DoSelection();
        }

        private void cbSearchLessonProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_LessonSelectionProgram = comboBox.Text;
            CurrentType.DoSelection();
        }

        private void cbSearchLessonRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_LessonSelectionRoom = comboBox.Text;
            CurrentType.DoSelection();
        }

        private void tbSearchLessonDate_TextChanged(object sender, EventArgs e)
        {
            TextBox comboBox = (TextBox)sender;
            m_LessonSelectionDay = comboBox.Text;
            CurrentType.DoSelection();
        }

        #endregion

        #region Plan-related UI
        private void dtpPlan_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            m_plan_chosenDate = dtp.Value.Date;
            if (m_lessonInMove == null)
                PlanShowDataIfReady();
        }
        private void cbPlanLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            PlanLessonLanguageChosen();
            if (m_lessonInMove == null)
                PlanShowDataIfReady();
        }
        private void cbPlanTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            PopulateTeacherVacation(cb.SelectedItem as string, lbPlanTeachVacation);
            if (m_lessonInMove == null)
                PlanShowDataIfReady();
        }
        private void cbPlanStud1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            PopulateStudentPossibleSchedule(cb.SelectedItem as string, lbPlanStudSchedule1);
            if (m_lessonInMove == null)
                PlanShowDataIfReady();
        }

        private void cbPlanStud2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            PopulateStudentPossibleSchedule(cb.SelectedItem as string, lbPlanStudSchedule2);
            if (m_lessonInMove == null)
                PlanShowDataIfReady();
        }

        private void cbPlanStud3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            PopulateStudentPossibleSchedule(cb.SelectedItem as string, lbPlanStudSchedule3);
            if (m_lessonInMove == null)
                PlanShowDataIfReady();
        }

        private void cbPlanStud4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            PopulateStudentPossibleSchedule(cb.SelectedItem as string, lbPlanStudSchedule4);
            if (m_lessonInMove == null)
                PlanShowDataIfReady();
        }
        private void butPlanAccept_Click(object sender, EventArgs e)
        {
            AcceptNewLesson();
            m_lessonInMove = null;
        }
        private void cbPlanRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            butPlanAccept.Visible = true; 
        }

        #endregion

        #region View-related UI
        private void butViewZoomOut_Click(object sender, EventArgs e)
        {
            if (tabControlViewScales.SelectedIndex > 0)
                tabControlViewScales.SelectedIndex--;
        }
        private void butViewZoomIn_Click(object sender, EventArgs e)
        {
            if (tabControlViewScales.SelectedIndex < tabControlViewScales.TabCount-1)
                tabControlViewScales.SelectedIndex++;
        }
        private void butViewNext_Click(object sender, EventArgs e)
        {
            int step = StepPerScale();
            dtpViewSlot.Value = dtpViewSlot.Value.Date.AddDays(step);
            m_view_chosenDate = dtpViewSlot.Value;
            ShowView();
        }

        private void butViewPrev_Click(object sender, EventArgs e)
        {
            int step = StepPerScale();
            dtpViewSlot.Value = dtpViewSlot.Value.Date.AddDays(-step);
            m_view_chosenDate = dtpViewSlot.Value;
            ShowView();
        }

        private void butViewShowLesson_MouseHover(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b != null && (b.Tag as Lesson) != null)
                ShowCurrentLesson(b.Tag as Lesson);
            else
            {
                Label lb = sender as Label;
                if (lb != null && (lb.Tag as Lesson) != null)
                    ShowCurrentLesson(lb.Tag as Lesson);
            }
        }
        
        private void dtpViewSlot_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            m_view_chosenDate = dtp.Value.Date;
            ShowView();
        }

        private void cbViewSelectState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            m_view_chosen_state = cb.SelectedItem as string;
            m_view_selection_mode = true;
            ShowView();
        }

        private void cbViewSelectStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            m_view_chosen_student= cb.SelectedItem as string;
            m_view_selection_mode = true;
            ShowView();
        }

        private void cbViewSelectTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            m_view_chosen_teacher= cb.SelectedItem as string;
            m_view_selection_mode = true;
            ShowView();
        }

        private void cbViewSelectRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            m_view_chosen_room= cb.SelectedItem as string;
            m_view_selection_mode = true;
            ShowView();
        }
        private void cbViewSelectProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            m_view_chosen_program = cb.SelectedItem as string;
            m_view_selection_mode = true;
            ShowView();
        }

        void viewDropSelection()
        {
            m_view_chosen_room = null;
            m_view_chosen_teacher = null;
            m_view_chosen_student = null;
            m_view_chosen_state = null;
            m_view_selection_mode = false;

            cbViewSelectProgram.SelectedIndex = -1;
            cbViewSelectRoom.SelectedIndex = -1;
            cbViewSelectTeacher.SelectedIndex = -1;
            cbViewSelectStudent.SelectedIndex = -1;
            cbViewSelectState.SelectedIndex = -1;
        }
        private void butViewShowAll_Click(object sender, EventArgs e)
        {
            viewDropSelection();
            ShowView();
        }

        private void tabControlViewScales_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowView();
        }

        private void tbViewDetailTeacher_TextChanged(object sender, EventArgs e)
        {
            ViewLessonDetailsChanged();
        }

        private void tbViewDetailStudent_TextChanged(object sender, EventArgs e)
        {
            ViewLessonDetailsChanged();
        }

        private void cbViewDetailRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewLessonDetailsChanged();
        }

        private void cbViewDetailProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewLessonDetailsChanged();
        }

        private void tbViewDetailComment_TextChanged(object sender, EventArgs e)
        {
            ViewLessonDetailsChanged();
        }

        private void ViewLessonDetailsChanged()
        {
            Modified = true;
            m_lessonDetailsChanged = true;
            butViewDetailSet.Visible = true;

        }
        private void ViewLessonDetailsSet(string key)
        {
            m_lessonDetailsChanged = false;
            butViewDetailSet.Visible = false;
            m_currentLessonKey = key;
        }

        private void butViewDetailSet_Click(object sender, EventArgs e)
        {
            SetTeacherComment(
                lbViewDetailTeacher.Text, 
                tbViewDetailTeacher.Text);
            SetStudentComment(
                lbViewDetailStudent.Text, 
                tbViewDetailStudent.Text);
            SetLessonDetails(
                m_currentLessonKey,
                (string)cbViewDetailProgram.SelectedItem,
                (string)cbViewDetailRoom.SelectedItem,
                tbViewDetailComment.Text);

            m_lessonDetailsChanged = false;
            butViewDetailSet.Visible = false;
            ShowView();
        }

        private void ShowView()
        {
            switch (tabControlViewScales.SelectedIndex)
            {
                case (int)TabControlScales.Month:
                    ViewShowMonth();
                    break;
                case (int)TabControlScales.Week:
                    ViewShowWeek();
                    break;
                case (int)TabControlScales.Day:
                    ViewShowDay();
                    break;
                case (int)TabControlScales.Slots:
                    ViewShowSlots();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
