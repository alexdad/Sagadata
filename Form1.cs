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
        string[] m_enumPricingType;
        string[] m_enumTimeSlot;
        string[] m_enumDurations;
        string[] m_enumWeekdayNames;

        Dictionary<Modes, RecordType> m_recordTypes;
        Dictionary<Modes, Type> m_dataTypes;

        string m_remoteDir;             // place for remote file copy
        string m_recordKeeperDir;       // place for local file subdirs

        bool m_modified;
        bool m_synced;
        bool m_editSavingTrap;

        #region "Main controls"
        public bool Modified
        {
            get
            {
                return m_modified;
            }
            set
            {
                m_modified = value;
                buttonSave.Visible = m_modified;
                if (value)
                    Synced = false;
            }
        }

        public bool Synced
        {
            get
            {
                return m_synced;
            }
            set
            {
                m_synced = value;
            }
        }

        public Modes CurrentMode { get; set; }
        public RecordType CurrentType
        {
            get { return m_recordTypes[CurrentMode]; }
        }
        public string CurrentModeName
        {
            get { return CurrentMode.ToString(); }
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

        public string BackupLocation
        {
            get { return Path.Combine(DataLocation, "Backup").ToString(); }
        }
        public int BackupLimit { get; set; }

        public Clouds CloudType { get; set; }

        public string CloudLocation
        {
            get { return Path.Combine(m_remoteDir, CurrentModeName + ".csv").ToString(); }
        }

        // WinForm form child control-related
        public string LastDownloadText { set { labelGlobLastDownload.Text = value; } }
        public string LastUploadText { set { labelGlobLastUpload.Text = value; } }

        public System.Windows.Forms.BindingSource DataList
        {
            get
            {
                // Here are links between manually crafted per-record-type UI and modes
                switch (CurrentMode)
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
                    case Modes.Clients:
                        return clientList;

                    default:
                        return null;
                }
            }
        }
        #endregion

        #region "Form"
        public FormGlob()
        {
            m_dataTypes = new Dictionary<Modes, Type>();
            m_recordTypes = new Dictionary<Modes, RecordType>();

            RecordsToFormConst1();

            // Initial mode is the first in the Modes enum
            CurrentMode = (Modes)0;
            ClientName = Environment.MachineName;
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
            m_chosenDate = DateTime.Now;
        }

        private void RecordsToFormConst1()
        {
            StudentToFormConst1();
            TeacherToFormConst1();
            ProgramToFormConst1();
            RoomToFormConst1();
            LessonToFormConst1();
            ClientToFormConst1();
        }
        private void RecordsToFormConst2()
        {
            StudentToFormConst2();
            TeacherToFormConst2();
            ProgramToFormConst2();
            RoomToFormConst2();
            LessonToFormConst2();
            ClientToFormConst2();
        }

        private void AssignListsToComboBoxes()
        {
            List<Student> students = ActiveStudents();
            List<String> studentNames = students.ConvertAll(x => x.Description);
            studentNames.Add("");
            string[] orderedStudentNames = studentNames.OrderBy(q => q).ToArray();
            cbSearchLessonStudent.Items.Clear();
            cbSearchLessonStudent.Items.AddRange(orderedStudentNames);
            cbLessonStudent1.Items.AddRange(orderedStudentNames);
            cbLessonStudent2.Items.AddRange(orderedStudentNames);
            cbLessonStudent3.Items.AddRange(orderedStudentNames);
            cbLessonStudent4.Items.AddRange(orderedStudentNames);
            cbLessonStudent5.Items.AddRange(orderedStudentNames);
            cbLessonStudent6.Items.AddRange(orderedStudentNames);
            cbLessonStudent7.Items.AddRange(orderedStudentNames);
            cbLessonStudent8.Items.AddRange(orderedStudentNames);
            cbLessonStudent9.Items.AddRange(orderedStudentNames);
            cbLessonStudent10.Items.AddRange(orderedStudentNames);

            List<Teacher> teachers = ActiveTeachers();
            List<String> teacherNames = teachers.ConvertAll(x => x.Description);
            teacherNames.Add("");
            string[] orderedTeacherNames = teacherNames.OrderBy(q => q).ToArray();

            cbSearchLessonTeacher.Items.Clear();
            cbSearchLessonTeacher.Items.AddRange(orderedTeacherNames);

            cbLessonTeacher1.Items.Clear();
            cbLessonTeacher1.Items.AddRange(orderedTeacherNames);

            cbLessonTeacher2.Items.Clear();
            cbLessonTeacher2.Items.AddRange(orderedTeacherNames);

            List<Program> programs = ActivePrograms();
            List<String> programNames = programs.ConvertAll(x => x.Description);
            programNames.Add("");
            string[] orderedProgramNames = programNames.OrderBy(q => q).ToArray();

            cbSearchLessonProgram.Items.Clear();
            cbSearchLessonProgram.Items.AddRange(orderedProgramNames);

            cbLessonProg.Items.Clear();
            cbLessonProg.Items.AddRange(orderedProgramNames);

            cbViewDetailProgram.Items.Clear();
            cbViewDetailProgram.Items.AddRange(orderedProgramNames);

            cbStudProg1.Items.Clear();
            cbStudProg1.Items.Add("");
            cbStudProg1.Items.AddRange(orderedProgramNames);

            cbStudProg2.Items.Clear();
            cbStudProg2.Items.Add("");
            cbStudProg2.Items.AddRange(orderedProgramNames);

            cbStudProg3.Items.Clear();
            cbStudProg3.Items.Add("");
            cbStudProg3.Items.AddRange(orderedProgramNames);

            cbPlanProgram.Items.Clear();
            cbPlanProgram.Items.AddRange(orderedProgramNames);

            List<Room> rooms = ActiveRooms();
            List<String> roomNames = rooms.ConvertAll(x => x.Description);
            roomNames.Add("");
            string[] orderedRoomNames = roomNames.OrderBy(q => q).ToArray();

            cbSearchLessonRoom.Items.Clear();
            cbSearchLessonRoom.Items.AddRange(orderedRoomNames);

            cbLessonRoom.Items.Clear();
            cbLessonRoom.Items.AddRange(orderedRoomNames);

            cbViewDetailRoom.Items.Clear();
            cbViewDetailRoom.Items.AddRange(orderedRoomNames);

            m_assignedListsChanged = false;
        }

        private string GetClientCode()
        {
            StringBuilder sb = new StringBuilder("?");
            foreach (Client client in AllClients())
            {
                sb.Append(client.Code);
                if (client.MachineName.ToLower() == ClientName.ToLower())
                    return client.Code;
            }

            string usedCodes = sb.ToString();
            Client st = (Client)clientList.AddNew();
            st.MachineName = ClientName;
            st.LastTouch = DateTime.Now.ToString();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (usedCodes.IndexOf(c) < 0)
                {
                    Modified = true;
                    st.Code = new string(c, 1);
                    return st.Code;
                }
            }
            MessageBox.Show("Exhausted all available client codes");
            Application.Exit();
            return "?";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ClientCode = "?";
            CreateLabelWorking();
            ReadAllFiles();
            if (Properties.Settings.Default.InitialDownload.ToLower() != "no")
            {
                if (!DownloadAll())
                {
                    ReadAllFiles();
                    Synced = false;
                }
            }
            else
                ReadAllFiles();

            ShowCurrentCount();

            ClientCode = GetClientCode();

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

            if (Modified)
                SaveAll();

            if (m_assignedListsChanged)
                AssignListsToComboBoxes();

            SelectionMode = false;
            CurrentType.SavedFullListDuringSelection = null;
            SetMode(newMode);
            ShowCurrentCount();
            ManageSearchWindow();

            this.splitContainerGlobDataControls.Panel1.Visible = true;
        }

        public bool HideWorkout
        {
            set
            {
                if (value)
                {
                    tabControlOps.Visible = false;
                    m_labelWorking.Visible = true;
                }
                else
                {
                    tabControlOps.Visible = true;
                    m_labelWorking.Visible = false;
                }
            }
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

            if (m_editSavingTrap)
            {
                if (MessageBox.Show(
                    "Want to lose them?",
                    "You did not Accept editing changes",
                    MessageBoxButtons.YesNo)
                                            != DialogResult.Yes)
                    return false;
                else
                    m_editSavingTrap = false; 
            }

            return true;
        }

        void ManageSearchWindow()
        {
            // For now we may only supply few searches
            this.panelGlobSearch.Visible =
                (CurrentMode == Modes.Students ||
                 CurrentMode == Modes.Teachers ||
                 CurrentMode == Modes.Lessons ||
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

        private void dgvStudents_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            EditTrap = false;
            lbStudStdProgPrice1.Text = "";
            lbStudStdProgPrice2.Text = "";
            lbStudStdProgPrice3.Text = "";
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
        private void dgvTeachers_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            EditTrap = false;
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

        private void dgvPrograms_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            EditTrap = false;
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

        private void dgvRooms_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            EditTrap = false;
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

        private void dgvLesson_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            EditTrap = false;
        }

        private void dgvPlan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProposeNewLesson(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvViewSlots_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ViewSelectLesson(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvViewSlots_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView dgv = sender as DataGridView;
                if (dgv != null)
                {
                    DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);
                    if (hit.Type == DataGridViewHitTestType.Cell)
                    {
                        ViewSelectLesson(sender as DataGridView, hit.RowIndex, hit.ColumnIndex);
                        m_slotLessonFromRightClick =
                            m_dvgViewTags[hit.RowIndex * 100 + hit.ColumnIndex];

                        dgv.CurrentCell = dgv[hit.ColumnIndex, hit.RowIndex];
                        ctxMenuLesson.Show(dgv, e.X, e.Y);
                    }
                }
            }
        }

        #endregion

        #region "Global Button Clicks"

        public bool EditTrap
        {
            set
            {
                m_editSavingTrap = value;
                if (value)
                {
                    m_assignedListsChanged = true;
                    buttonGlobEditAccept.Visible = true;
                }
                else
                {
                    buttonGlobEditAccept.Visible = false;
                }
            }
        }
        private void buttonGlobEditAccept_Click(object sender, EventArgs e)
        {
            Modified = true;
            if (DataList.Position != DataList.Count - 1)
            {
                DataList.Position++;
                DataList.Position--;
            }
            if (DataList.Position != 0)
            {
                DataList.Position--;
                DataList.Position++;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (tabControlOps.SelectedIndex == (int)TabControlOps.Edit)
            {
                if (m_editSavingTrap)
                {
                    MessageBox.Show("Please click orange Accept to push your edits, then save again");
                    return;
                }
            }
            CommandSave();
            buttonSync.Visible = !m_synced;
        }

        private void buttonSync_Click(object sender, EventArgs e)
        {
            CommandSync();
            buttonSync.Visible = !m_synced;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;
            if (DataList.CurrencyManager.Position < DataList.Count - 1)
                DataList.CurrencyManager.Position++;
            SetEditFocus();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;
            if (DataList.CurrencyManager.Position > 0)
                DataList.CurrencyManager.Position--;
            SetEditFocus();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;
            Record st = (Record)DataList.AddNew();
            st.Id = FormGlob.AllocateID();
            st.ChangedBy = ClientCode;
            st.CreatedBy = ClientCode;
            ShowCurrentCount();

            Modified = true;
            EditTrap = true;

            SetEditFocus();
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
            EditTrap = true;

            SetEditFocus();
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

            cbPricingType.SelectedIndex = 0;

            cbSearchLessonStudent.SelectedIndex = -1;
            cbSearchLessonTeacher.SelectedIndex = -1;
            cbSearchLessonProgram.SelectedIndex = -1;
            cbSearchLessonRoom.SelectedIndex = -1;
            chkBoxSearchLessonDate.Checked = false;
            dtpSearchLessonDate.Value = DateTime.Now;


        }

        private void buttonToExcel_Click(object sender, EventArgs e)
        {
            string tempCsv = CurrentType.WriteTempFile();
            System.Diagnostics.Process.Start(tempCsv);
        }

        void SetEditFocus()
        {
            switch (CurrentMode)
            {
                case Modes.Students:
                    tbStudFirstName.Focus();
                    break;
                case Modes.Teachers:
                    tbTeachFirstName.Focus();
                    break;
                case Modes.Programs:
                    tbProgCode.Focus();
                    break;
                case Modes.Rooms:
                    tbRoomName.Focus();
                    break;
                case Modes.Lessons:
                    cbLessonState.Focus();
                    break;
                case Modes.Clients:
                    //
                    break;
                default:
                    break;
            }
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

        // Edit Student 
        private void tbStudFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudStatus_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudCellPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudLanguageDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudSourceDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudBackground_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudBirthday_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudGoals_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudInterests_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudSchedule_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudComments_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudLearns_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudLevel_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudSpeaks_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudOther_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudSource_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudProg1_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }
        private void cbStudProg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudProgPrice(sender, 1);
        }

        private void cbStudProg2_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudProg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudProgPrice(sender, 2);
        }

        private void cbStudProg3_Click(object sender, EventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void cbStudProg3_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudProgPrice(sender, 3);
        }

        void SetStudProgPrice(object sender, int progIndex)
        {
            ComboBox cb = sender as ComboBox;
            string prog = cb.SelectedItem as string;
            if (IsStringEmpty(prog))
                return;

            string price = GetProgramPrice(prog);
            if (IsStringEmpty(price))
                return;

            SetStdStdPriceLabel(progIndex, price);
        }

        void SetStdStdPriceLabel(int index, string price)
        {
            switch(index)
            {
                case 1:
                    lbStudStdProgPrice1.Text = price;
                    break;
                case 2:
                    lbStudStdProgPrice2.Text = price;
                    break;
                case 3:
                    lbStudStdProgPrice3.Text = price;
                    break;
            }
        }

        private void tbStudPrice1_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudPrice2_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void tbStudPrice3_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditStudentDetailsChanged();
        }

        private void buttonStudGrabStdPrices_Click(object sender, EventArgs e)
        {
            if (!IsStringEmpty(lbStudStdProgPrice1.Text))
            {
                tbStudPrice1.Text = lbStudStdProgPrice1.Text;
                tbStudPrice1_KeyPress(null, null);
            }
            if (!IsStringEmpty(lbStudStdProgPrice2.Text))
            {
                tbStudPrice2.Text = lbStudStdProgPrice2.Text;
                tbStudPrice2_KeyPress(null, null);
            }
            if (!IsStringEmpty(lbStudStdProgPrice3.Text))
            {
                tbStudPrice3.Text = lbStudStdProgPrice3.Text;
                tbStudPrice3_KeyPress(null, null);
            }
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

        private void cbSetAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb != null)
                SetTeacherAvailabilityDefault((string)cb.SelectedItem);
        }
        private void availAcceptButton_Click(object sender, EventArgs e)
        {
            AcceptAvailabilityEdits();
        }
        private void tbTeachFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void tbTeachLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void tbTeachEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void tbTeachPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void tbTeachLastBirthday_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void tbTeachLanguageDetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void tbTeachAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void tbTeachVacations_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void tbTeachComment_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void cbTeachStatus_Click(object sender, EventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void cbTeachLanguage_Click(object sender, EventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        private void cbTeachLanguage2_Click(object sender, EventArgs e)
        {
            EditTeacherDetailsChanged();
        }

        #endregion

        #region Room-related UI

        private void tbRoomName_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditRoomDetailsChanged();
        }

        private void tbRoomCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditRoomDetailsChanged();
        }

        private void tbRoomPreferrability_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditRoomDetailsChanged();
        }

        private void tbRoomTags_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditRoomDetailsChanged();
        }

        private void tbRoomComments_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditRoomDetailsChanged();
        }

        #endregion

        #region Program-related UI

        private void tbProgCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditProgramDetailsChanged();
        }

        private void tbProgName_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditProgramDetailsChanged();
        }

        private void tbProgPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditProgramDetailsChanged();
        }

        private void tbProgSummary_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditProgramDetailsChanged();
        }

        private void tbProgComments_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditProgramDetailsChanged();
        }

        private void cbPricingType_Click(object sender, EventArgs e)
        {
            EditProgramDetailsChanged();
        }

        private void cbSearchPricingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_ProgramSelectionType = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }
        #endregion

        #region Lesson-related UI

        private void tbLEssonComment_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonState_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonProg_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonRoom_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStart_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonEnd_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonTeacher1_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonTeacher2_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent1_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent2_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent3_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent5_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent7_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent9_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent4_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent6_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent8_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void cbLessonStudent10_Click(object sender, EventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void tbLessonPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void monthCalendar1_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void monthCalendar1_MouseDown(object sender, MouseEventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void dtpLessonCancellationTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            EditLessonDetailsChanged();
        }

        private void dtpLessonCancellationTime_MouseDown(object sender, MouseEventArgs e)
        {
            EditLessonDetailsChanged();
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

        private void chkBoxSearchLessonDate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            m_LessonSelectionDay = dtpSearchLessonDate.Value;
            m_use_LessonSelectionDay = cb.Checked;
            CurrentType.DoSelection();
        }


        private void buttonReconcileStart_Click(object sender, EventArgs e)
        {
            m_OperationalEvents = GCal.Ops.GetOperationalEvents(
                DayStart(this.dtpReconcileFrom.Value), 
                DayEnd(this.dtpReconcileTo.Value)).ToArray();

            m_curOperationalevent = 0;
            if (SetCurrentOperationalevent())
                buttonReconcileNext_Click(null, null);
        }
        private void buttonReconcileLink_Click(object sender, EventArgs e)
        {
            Lesson l = lessonList.Current as Lesson;
            if (l == null)
                return;
            l.GoogleId = lbReconcileGoogleCalId.Text;
            buttonGlobEditAccept_Click(null, null);
            buttonReconcileNext_Click(null, null);
        }
        private void buttonReconcileNext_Click(object sender, EventArgs e)
        {
            if (m_OperationalEvents == null)
                return;
            while (m_curOperationalevent < m_OperationalEvents.Length - 1)
            {
                m_curOperationalevent++;
                if (!SetCurrentOperationalevent())
                    break;
            }
        }

        private void buttonReconcilePrev_Click(object sender, EventArgs e)
        {
            if (m_OperationalEvents == null)
                return;
            if (m_curOperationalevent >= 1)
            {
                m_curOperationalevent--;
                SetCurrentOperationalevent();
            }
        }

        private void buttonReconcileCreate_Click(object sender, EventArgs e)
        {
            buttonAdd_Click(null, null);
            Lesson l = lessonList.Current as Lesson;
            if (l == null)
                return;
            l.Day = lbReconcileDate.Text;
            l.Start = lbReconcileFrom.Text;
            l.End = lbReconcileTo.Text;
            l.Comments = lbReconcileDescription.Text;
            l.GoogleId = lbReconcileGoogleCalId.Text;

            SetComboBoxIndexByValue(cbLessonStart, l.Start);
            SetComboBoxIndexByValue(cbLessonEnd, l.End);
            buttonGlobEditAccept_Click(null, null);

            EditLessonDetailsChanged();
        }
        private void lbReconcileDescription_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lbReconcileDescription.Text);
        }

        private void dtpReconcileFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            foreach (Control c in panelReconcile.Controls)
                c.Visible = true;
        }

        #endregion

        #region Plan-related UI
        private void dtpPlan_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            m_chosenDate = dtp.Value.Date;
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
            string studDesc = cb.SelectedItem as string;
            PopulateStudentPossibleSchedule(studDesc, lbPlanStudSchedule1);
            PopulatePlanFieldsFromLastLesson(studDesc);
            PopulateLessonPrice(studDesc, cbPlanProgram.Text);
            InitializeStudentPlan(true);

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
            if (tabControlViewScales.SelectedIndex < tabControlViewScales.TabCount - 1)
                tabControlViewScales.SelectedIndex++;
            FollowFocusedDay();
        }
        private void butViewNext_Click(object sender, EventArgs e)
        {
            int step = StepPerScale();
            dtpViewSlot.Value = dtpViewSlot.Value.Date.AddDays(step);
            m_chosenDate = dtpViewSlot.Value;
            ShowView();
        }

        private void butViewPrev_Click(object sender, EventArgs e)
        {
            int step = StepPerScale();
            dtpViewSlot.Value = dtpViewSlot.Value.Date.AddDays(-step);
            m_chosenDate = dtpViewSlot.Value;
            ShowView();
        }

        private void butViewShowLesson_MouseHover(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b != null && (b.Tag as Lesson) != null)
            {
                ShowCurrentLesson(b.Tag as Lesson);
            }
            else
            {
                Label lb = sender as Label;
                if (lb != null && (lb.Tag as Lesson) != null)
                {
                    DropHighlight();
                    ShowCurrentLesson(lb.Tag as Lesson);
                    SetHighlight(lb);
                }
            }
        }

        private void dtpViewSlot_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            m_chosenDate = dtp.Value.Date;
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
            m_view_chosen_student = cb.SelectedItem as string;
            m_view_selection_mode = true;
            ShowView();
        }

        private void cbViewSelectTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            m_view_chosen_teacher = cb.SelectedItem as string;
            m_view_selection_mode = true;
            ShowView();
        }

        private void cbViewSelectRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            m_view_chosen_room = cb.SelectedItem as string;
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

        private void ViewLessonDetailsSet(string key)
        {
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

            Modified = true;
            butViewDetailSet.Visible = false;
            ShowView();
        }

        private void tbViewDetailTeacher_KeyPress(object sender, KeyPressEventArgs e)
        {
            butViewDetailSet.Visible = true;
        }

        private void tbViewDetailStudent_KeyPress(object sender, KeyPressEventArgs e)
        {
            butViewDetailSet.Visible = true;
        }

        private void tbViewDetailComment_KeyPress(object sender, KeyPressEventArgs e)
        {
            butViewDetailSet.Visible = true;
        }

        private void cbViewDetailRoom_Click(object sender, EventArgs e)
        {
            butViewDetailSet.Visible = true;
        }

        private void cbViewDetailProgram_Click(object sender, EventArgs e)
        {
            butViewDetailSet.Visible = true;
        }

        private void ShowView()
        {
            if (m_assignedListsChanged)
                AssignListsToComboBoxes();

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
