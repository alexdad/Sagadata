﻿using System;
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
        public static RealmBindings Bindings;
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
        string[] m_enumCancellation;
        string[] m_enumAccountingCategories;

        Dictionary<Modes, RecordType> m_recordTypes;
        Dictionary<Modes, Type> m_dataTypes;

        string m_remoteDir;             // place for remote file copy
        string m_recordKeeperDir;       // place for local file subdirs

        private Control m_rightClickedControl;

        #region "Main controls"
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

        #endregion

        #region "Form"
        public FormGlob()
        {
            SetBindings();

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
            SetEditMode(CurrentMode);
            RecordsToFormConst2();
            cbGlobMode.SelectedIndex = (int)CurrentMode;
            SelectionMode = false;

            DateTime dts = DateTime.Now;
            DateTime dtn = dts.AddMinutes(15);
            SlotInTicks = (dtn - dts).Ticks;
            m_chosenDate = DateTime.Now;
            m_readDataHash = "";
            m_rnd = new Random(DateTime.Now.Second);
        }

        private void RecordsToFormConst1()
        {
            StudentToFormConst1();
            TeacherToFormConst1();
            ProgramToFormConst1();
            RoomToFormConst1();
            LessonToFormConst1();
            ClientToFormConst1();
            PayExpenseToFormConst1();
            PayStudentToFormConst1();
            PayTeacherToFormConst1();
        }
        private void RecordsToFormConst2()
        {
            StudentToFormConst2();
            TeacherToFormConst2();
            ProgramToFormConst2();
            RoomToFormConst2();
            LessonToFormConst2();
            ClientToFormConst2();
            PayExpenseToFormConst2();
            PayStudentToFormConst2();
            PayTeacherToFormConst2();
        }

        private void AssignListsToComboBoxes()
        {
            List<Student> students = ActiveStudents();
            List<String> studentNames = students.ConvertAll(x => x.Description);
            studentNames.Add("");
            string[] orderedStudentNames = studentNames.OrderBy(q => q).ToArray();
            cbSearchLessonStudent.Items.Clear();
            cbSearchLessonStudent.Items.AddRange(orderedStudentNames);
            cbLessonStudent1.Items.Clear();
            cbLessonStudent1.Items.AddRange(orderedStudentNames);
            cbLessonStudent2.Items.Clear();
            cbLessonStudent2.Items.AddRange(orderedStudentNames);
            cbLessonStudent3.Items.Clear();
            cbLessonStudent3.Items.AddRange(orderedStudentNames);
            cbLessonStudent4.Items.Clear();
            cbLessonStudent4.Items.AddRange(orderedStudentNames);
            cbLessonStudent5.Items.Clear();
            cbLessonStudent5.Items.AddRange(orderedStudentNames);
            cbLessonStudent6.Items.Clear();
            cbLessonStudent6.Items.AddRange(orderedStudentNames);
            cbLessonStudent7.Items.Clear();
            cbLessonStudent7.Items.AddRange(orderedStudentNames);
            cbLessonStudent8.Items.Clear();
            cbLessonStudent8.Items.AddRange(orderedStudentNames);
            cbLessonStudent9.Items.Clear();
            cbLessonStudent9.Items.AddRange(orderedStudentNames);
            cbLessonStudent10.Items.Clear();
            cbLessonStudent10.Items.AddRange(orderedStudentNames);

            cbPayStudentStudent.Items.Clear();
            cbPayStudentStudent.Items.AddRange(orderedStudentNames);
            cbSearchPayStudentName.Items.Clear();
            cbSearchPayStudentName.Items.AddRange(orderedStudentNames);

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

            cbPayTeacherTeacher.Items.Clear();
            cbPayTeacherTeacher.Items.AddRange(orderedTeacherNames);
            cbSearchPayTeacherName.Items.Clear();
            cbSearchPayTeacherName.Items.AddRange(orderedTeacherNames);

            List<Program> programs = ActivePrograms();
            List<String> programNames = programs.ConvertAll(x => x.Description);
            programNames.Add("");
            var uniqueProgramNames = programNames.Distinct<String>();
            string[] orderedProgramNames = uniqueProgramNames.OrderBy(q => q).ToArray();

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




            StaleComboLists = false;
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
            st.SetGlob(this);
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

            ShowVersionAndRealm();
            ShowCurrentCount();

            ClientCode = GetClientCode();

            this.Size = Properties.Settings.Default.Form1Size;
            splitContWorkValid.SplitterDistance = splitContWorkValid.Height;
            splitContainerGlobDataControls.SplitterDistance = Properties.Settings.Default.SplitDC;
            splitContainerGlobMasterDetail.SplitterDistance = Properties.Settings.Default.SplitMD;
            AssignListsToComboBoxes();
            Modified = false;

            // Start as View day
            ChangeOperMode(Ops.View);
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
            DropPayExpenseSelection();
            DropPayStudentSelection();
            DropPayTeacherSelection();
        }


        public bool HideWorkout
        {
            set
            {
                if (value)
                {
                    tabControlOps.Visible = false;
                    m_labelWorking.Visible = true;
                    m_labelWorking.BringToFront();
                }
                else
                {
                    tabControlOps.Visible = true;
                    m_labelWorking.Visible = false;
                }
            }
        }

        void ManageSearchWindow()
        {
            // For now we may only supply few searches
            this.panelGlobSearch.Visible =
                (CurrentMode == Modes.Students ||
                 CurrentMode == Modes.Teachers ||
                 CurrentMode == Modes.Lessons ||
                 CurrentMode == Modes.Programs ||
                 CurrentMode == Modes.PayExpenses ||
                 CurrentMode == Modes.PayStudents ||
                 CurrentMode == Modes.PayTeachers);

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


        private void dgvPayExpenses_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvColumnSort<PayExpense>(
                sender as DataGridView,
                CurrentType.ForkOut<PayExpense>(0),
                e.ColumnIndex);
        }

        private void dgvPayStudents_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvColumnSort<PayStudent>(
                sender as DataGridView,
                CurrentType.ForkOut<PayStudent>(0),
                e.ColumnIndex);
        }

        private void dgvPayTeacher_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvColumnSort<PayTeacher>(
                sender as DataGridView,
                CurrentType.ForkOut<PayTeacher>(0),
                e.ColumnIndex);
        }

        private void dgvPayExpenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCellCopy(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvPayStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCellCopy(sender as DataGridView, e.RowIndex, e.ColumnIndex);
        }

        private void dgvPayTeacher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvCellCopy(sender as DataGridView, e.RowIndex, e.ColumnIndex);
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

                        m_rightClickedControl = dgv;
                        ctxMenuLesson.Show(dgv, e.X, e.Y);
                    }
                }
            }
        }

        #endregion

        #region "Global Button Clicks"

        private void cbGlobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbGlobType_SelectedIndexChanged_Actual(sender, e);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
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
            if (!Datalist_Complete())
                return;

            Datalist_StepForward();
            SetEditFocus();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (!Datalist_Complete())
                return;

            Datalist_StepBack();
            SetEditFocus();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!Datalist_Complete())
                return;

            Datalist_AddRecord();
            ShowCurrentCount();

            Modified = true;

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

            cbSearchPayExpenseCategory.SelectedIndex = -1;
            cbSearchPayStudentName.SelectedIndex = -1;
            cbSearchPayTeacherName.SelectedIndex = -1;
            tbSearchPayExpenseSum.Text = "";
            tbSearchPayStudentSum.Text = "";
            tbSearchPayTeacherSum.Text = "";
            dtpSearchPayExpenseDate.Value = DateTime.Now;
            dtpSearchPayStudentDate.Value = DateTime.Now;
            dtpSearchPayTeacherDate.Value = DateTime.Now;

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
                case Modes.PayExpenses:
                    dtpPayExpenseDay.Focus();
                    break;
                case Modes.PayStudents:
                    dtpPayStudentDate.Focus();
                    break;
                case Modes.PayTeachers:
                    dtpPayTeacherDate.Focus();
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

        private void cbStudProg1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudProgPrice(sender, 1);
        }

        private void cbStudProg2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudProgPrice(sender, 2);
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
            switch (index)
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

        private void buttonStudGrabStdPrices_Click(object sender, EventArgs e)
        {
            if (!IsStringEmpty(lbStudStdProgPrice1.Text))
                tbStudPrice1.Text = lbStudStdProgPrice1.Text;
            if (!IsStringEmpty(lbStudStdProgPrice2.Text))
                tbStudPrice2.Text = lbStudStdProgPrice2.Text;
            if (!IsStringEmpty(lbStudStdProgPrice3.Text))
                tbStudPrice3.Text = lbStudStdProgPrice3.Text;
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

        #endregion

        #region Program-related UI

        private void cbSearchPricingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_ProgramSelectionType = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }
        #endregion

        #region Lesson-related UI

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


        private void buttonReconcileImport_Click(object sender, EventArgs e)
        {
            m_OperationalEvents = GCal.Ops.GetOperationalEvents(
                DayStart(this.dtpReconcileFrom.Value),
                DayEnd(this.dtpReconcileTo.Value)).ToArray();

            m_curOperationalevent = 0;
            SetCurrentOperationalEvent();
            MakeReconcileVisible();
        }
        private void buttonReconcileLink_Click(object sender, EventArgs e)
        {
            Lesson l = lessonList.Current as Lesson;
            if (l == null)
                return;
            l.GoogleId = lbReconcileGoogleCalId.Text;
            if (!Datalist_Complete())
                return;
            //buttonReconcileNext_Click(null, null);
        }

        private void buttonReconcileUnlink_Click(object sender, EventArgs e)
        {
            Lesson l = lessonList.Current as Lesson;
            if (l == null)
                return;
            l.GoogleId = "";
            ShowMatching(MatchingState.Unknown, 0.0);
            Datalist_Complete();
        }

        private void buttonReconcileNext2_Click(object sender, EventArgs e)
        {
            if (m_OperationalEvents == null)
                return;
            if (!Datalist_Complete())
                return;
            while (m_curOperationalevent < m_OperationalEvents.Length - 1)
            {
                m_curOperationalevent++;
                if (SetCurrentOperationalEvent() != MatchingState.Linked)
                    break;
            }
        }

        private void buttonReconcileNext_Click(object sender, EventArgs e)
        {
            if (m_OperationalEvents == null)
                return;
            if (!Datalist_Complete())
                return;
            if (m_curOperationalevent < m_OperationalEvents.Length - 1)
            {
                m_curOperationalevent++;
                SetCurrentOperationalEvent();
            }
        }

        private void buttonReconcilePrev2_Click(object sender, EventArgs e)
        {
            if (m_OperationalEvents == null)
                return;
            if (!Datalist_Complete())
                return;
            while (m_curOperationalevent >= 1)
            {
                m_curOperationalevent--;
                if (SetCurrentOperationalEvent() != MatchingState.Linked)
                    break;
            }
        }

        private void buttonReconcilePrev_Click(object sender, EventArgs e)
        {
            if (m_OperationalEvents == null)
                return;
            if (!Datalist_Complete())
                return;
            if (m_curOperationalevent >= 1)
            {
                m_curOperationalevent--;
                SetCurrentOperationalEvent();
            }
        }

        private void buttonReconcileCreate_Click(object sender, EventArgs e)
        {
            buttonAdd_Click(null, null);
            Lesson l = lessonList.Current as Lesson;
            if (l == null)
                return;

            FillLessonFromCalendar(l);
            ShowMatching(MatchingState.Linked, 0.0);
            Datalist_Complete();
        }
        private void lbReconcileDescription_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lbReconcileDescription.Text);
        }

        private bool IsLabelDiff(Control c)
        {
            Label l = c as Label;
            return ((l != null) && l.Text == "*");
        }
        private void MakeReconcileVisible()
        {
            foreach (Control c in panelReconcile.Controls)
            {
                if (!IsLabelDiff(c))
                    c.Visible = true;
            }
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
            dtpViewChosen.Value = dtpViewChosen.Value.Date.AddDays(step);
            m_chosenDate = dtpViewChosen.Value;
            ShowView();
        }

        private void butViewPrev_Click(object sender, EventArgs e)
        {
            int step = StepPerScale();
            dtpViewChosen.Value = dtpViewChosen.Value.Date.AddDays(-step);
            m_chosenDate = dtpViewChosen.Value;
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

        private void butViewShowLesson_DoubleClick(object sender, EventArgs e)
        {
            Lesson lsn = null;
            Label lb = sender as Label;
            Button b = sender as Button;
            if (b != null && (b.Tag as Lesson) != null)
                lsn = b.Tag as Lesson;
            else if (lb != null && (lb.Tag as Lesson) != null)
                lsn = lb.Tag as Lesson;

            if (lsn != null)
            {
                ChangeOperMode(Ops.Edit);
                ChangeEditMode(Modes.Lessons);
                Datalist_Find(lsn.Key);
            }
        }

        private void butViewShowLesson_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                m_rightClickedControl = sender as Control;
        }

        private void dtpViewChosen_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            m_chosenDate = dtp.Value.Date;
            if (m_chosenDate < m_viewMinDate || m_chosenDate > m_viewMaxDate)
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
            UpdateComboLists();

            switch (tabControlViewScales.SelectedIndex)
            {
                case (int)Scales.Month:
                    ViewShowMonth();
                    break;
                case (int)Scales.Week:
                    ViewShowWeek();
                    break;
                case (int)Scales.Day:
                    ViewShowDay();
                    break;
                case (int)Scales.Slots:
                    ViewShowSlots();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region "Pay-related UI"
        private void chkSearchPayExpenseDate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            m_use_PayExpenseSelectionDate = cb.Checked;
            if (m_use_PayExpenseSelectionDate)
                m_PayExpenseSelectionDate = dtpSearchPayExpenseDate.Value;
            else
                m_PayExpenseSelectionDate = DateTime.Now;

            CurrentType.DoSelection();
        }

        private void tbSearchPayExpenseSum_TextChanged(object sender, EventArgs e)
        {
            double s = 0;
            if (double.TryParse(tbSearchPayExpenseSum.Text, out s))
            {
                m_PayExpenseSelectionSum = s;
                CurrentType.DoSelection();
            }
        }

        private void cbSearchPayExpenseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_PayExpenseSelectionCategory = (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void cbSearchPayStudentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_PayStudentSelectionName= (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void tbSearchPayStudentSum_TextChanged(object sender, EventArgs e)
        {
            double s = 0;
            if (double.TryParse(tbSearchPayStudentSum.Text, out s))
            {
                m_PayStudentSelectionSum = s;
                CurrentType.DoSelection();
            }
        }

        private void chkSearchPayStudentDate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            m_use_PayStudentSelectionDate = cb.Checked;
            if (m_use_PayStudentSelectionDate)
                m_PayStudentSelectionDate = dtpSearchPayStudentDate.Value;
            else
                m_PayStudentSelectionDate = DateTime.Now;

            CurrentType.DoSelection();
        }

        private void dtpSearchPayStudentDate_ValueChanged(object sender, EventArgs e)
        {
            if (m_use_PayStudentSelectionDate)
            {
                m_PayStudentSelectionDate = dtpSearchPayStudentDate.Value;
                CurrentType.DoSelection();
            }
        }

        private void dtpSearchPayExpenseDate_ValueChanged(object sender, EventArgs e)
        {
            if (m_use_PayExpenseSelectionDate)
            {
                m_PayExpenseSelectionDate = dtpSearchPayExpenseDate.Value;
                CurrentType.DoSelection();
            }
        }

        private void cbSearchPayTeacherName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_PayTeacherSelectionName= (string)comboBox.SelectedItem;
            CurrentType.DoSelection();
        }

        private void tbSearchPayTeacherSum_TextChanged(object sender, EventArgs e)
        {
            double s = 0;
            if (double.TryParse(tbSearchPayTeacherSum.Text, out s))
            {
                m_PayTeacherSelectionSum = s;
                CurrentType.DoSelection();
            }
        }

        private void dtpSearchPayTeacherDate_ValueChanged(object sender, EventArgs e)
        {
            if (m_use_PayTeacherSelectionDate)
            {
                m_PayTeacherSelectionDate = dtpSearchPayTeacherDate.Value;
                CurrentType.DoSelection();
            }
        }

        private void chkSearchPayTeacherDate_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            m_use_PayTeacherSelectionDate = cb.Checked;
            if (m_use_PayTeacherSelectionDate)
                m_PayTeacherSelectionDate = dtpSearchPayTeacherDate.Value;
            else
                m_PayTeacherSelectionDate = DateTime.Now;

            CurrentType.DoSelection();
        }
        #endregion

        private void tbStudPrice1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudPrice2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudPrice3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbStudProg3_Click(object sender, EventArgs e)
        {

        }

        private void cbStudProg2_Click(object sender, EventArgs e)
        {

        }

        private void cbStudProg1_Click(object sender, EventArgs e)
        {

        }

        private void tbStudAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudLanguageDetail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudSourceDetail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbStudSpeaks_Click(object sender, EventArgs e)
        {

        }

        private void cbStudOther_Click(object sender, EventArgs e)
        {

        }

        private void tbStudLastName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbStudStatus_Click(object sender, EventArgs e)
        {

        }

        private void cbStudLearns_Click(object sender, EventArgs e)
        {

        }

        private void tbStudBirthday_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudEmail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbStudLevel_Click(object sender, EventArgs e)
        {

        }

        private void tbStudCellPhone_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudHomePhone_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbStudSource_Click(object sender, EventArgs e)
        {

        }

        private void tbStudComments_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudSchedule_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudInterests_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudGoals_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbStudBackground_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbTeachComment_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbTeachVacations_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbTeachAddress_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbTeachLanguageDetail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbTeachLanguage2_Click(object sender, EventArgs e)
        {

        }

        private void cbTeachLanguage_Click(object sender, EventArgs e)
        {

        }

        private void cbTeachStatus_Click(object sender, EventArgs e)
        {

        }

        private void tbTeachLastBirthday_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbTeachPhone_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbTeachEmail_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbTeachLastName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbTeachFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbProgComments_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbProgSummary_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbProgPrice_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbPricingType_Click(object sender, EventArgs e)
        {

        }

        private void tbProgName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbProgCode_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbRoomPreferrability_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbRoomCapacity_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbRoomName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbRoomComments_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tbRoomTags_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbLessonCancellation_Click(object sender, EventArgs e)
        {

        }

        private void chkLessonLink_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent10_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent9_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent8_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent7_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent6_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent5_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent4_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent3_Click(object sender, EventArgs e)
        {

        }

        private void tbLEssonComment_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbLessonStudent2_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStudent1_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonTeacher2_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonTeacher1_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonProg_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonState_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonEnd_Click(object sender, EventArgs e)
        {

        }

        private void cbLessonStart_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void monthCalendar1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void cbLessonRoom_Click(object sender, EventArgs e)
        {

        }
    }
}
