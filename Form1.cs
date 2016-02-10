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

        //Record[] m_savedFullListDuringSelection;
        //Dictionary<string, Record> m_recordsAsRead;
        //List<string> m_deletedKeys;
        //SchemaField[] m_schema;
        //int[] m_placements;
        string m_dataLocation;
        string m_cloudLocation;
        string m_backupLocation;
        string m_fileName;
        Clouds m_cloudType;
        int m_backupLimit;

        string[] m_enumModes;
        string[] m_enumLanguage;
        string[] m_enumLevel;
        string[] m_enumSource;
        string[] m_enumStatus;
        string[] m_enumColumnNames;
        string[] m_enumSortDirection;

        string m_selectionStatus;
        string m_selectionLearns;
        string m_selectionSpeaks;
        string m_selectionFirstName;
        string m_selectionLastName;
        string m_selectionSource;
        string m_selectionLevel;

        string m_mode;
        RecordType m_curType;
        bool m_bChanged;

        public Clouds CloudType { get { return m_cloudType; } }
        public string CloudLocation { get { return m_cloudLocation; } }

        public string FileName { get { return m_fileName; } }

        public bool Changed { get { return m_bChanged; } set { m_bChanged = value;  } }

        public string LastDownloadText { set { labelGlobLastDownload.Text = value; } }
        public string LastUploadText { set { labelGlobLastUpload.Text = value; } }

        public Dictionary<string, Record> RecordsAsRead { get; set; }

        public int[] Placements { get; set; }

        public SchemaField[] Schema { get; set; }

        public List<string> DeletedKeys { get; set;  }

        public Record[] SavedFullListDuringSelection { get; set; }

        public string SelectionLevel { get { return m_selectionLevel; } set { m_selectionLevel = value; } }

        public string BackupLocation { get { return m_backupLocation; } set { m_backupLocation = value; }  }

        public System.Windows.Forms.BindingSource DataList
        {
            get
            {
                // Here are links between manually crafted per-record-type UI and modes
                switch(m_mode)
                {
                    case "Students":
                        return studentList;
                    default:
                        return null;
                }
            }
        }


        #region "Form"
        public FormGlob()
        {
            Client = Environment.MachineName;
            ReadSettings();
            ReadSchemas();
            PrepareDataDirectories();
            InitializeComponent();
            AssignEnums();

            m_mode = m_enumModes[0];
            m_curType = new StudentType(this);

            SelectionMode = false;
            DeletedKeys = new List<string>();
            RecordsAsRead = new Dictionary<string, Record>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.InitialDownload.ToLower() != "no")
            {
                if (!m_curType.Download<Student>())
                    m_curType.ReadRecordsFile<Student>(RecordsAsRead);
            }
            else
                m_curType.ReadRecordsFile<Student>(RecordsAsRead);

            ShowStudentCount();

            this.Size = Properties.Settings.Default.Form1Size;
            splitContainerGlobDataControls.SplitterDistance = Properties.Settings.Default.SplitDC;
            splitContainerGlobMasterDetail.SplitterDistance = Properties.Settings.Default.SplitMD;

            m_bChanged = false;
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
        private void cbGlobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string mode = (string)comboBox.SelectedItem;


        }
        #endregion
        #region TypeSpecificFormControlRelated
        public void ReplaceStudentList(Student[] target)
        {
            studentList.Clear();
            foreach (Student s in target)
                studentList.Add(s);
        }
        #endregion

        #region "Selection"
        public bool SelectionMode
        {
            get { return buttGlobalShowAll.Enabled; }
            set { buttGlobalShowAll.Enabled = value; }
        }


        public void DoStudentSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                m_curType.StashRecordList();
            }

            DataList.Clear();
            foreach (Student s in SavedFullListDuringSelection)
            {
                if (!m_curType.Fit(m_selectionStatus, s.Status, true))
                    continue;
                if (!m_curType.Fit(m_selectionLearns, s.LearningLanguage, true))
                    continue;
                if (!m_curType.Fit(m_selectionSpeaks, s.NativeLanguage, true))
                    continue;
                if (!m_curType.Fit(m_selectionFirstName, s.FirstName, false))
                    continue;
                if (!m_curType.Fit(m_selectionLastName, s.LastName, false))
                    continue;
                if (!m_curType.Fit(m_selectionSource, s.Source, true))
                    continue;
                if (!m_curType.Fit(m_selectionLevel, s.Level, true))
                    continue;

                DataList.Add(s);
            }
            m_curType.ShowCount();
            m_bChanged = true;
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
            if (studentList.CurrencyManager.Position < studentList.Count - 1)
                studentList.CurrencyManager.Position++;
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (studentList.CurrencyManager.Position > 0)
                studentList.CurrencyManager.Position--;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Student st = (Student)studentList.AddNew();
            st.Id = FormGlob.AllocateID();
            ShowStudentCount();
            m_bChanged = true;
            tbStudFirstName.Select();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Student s = (Student)studentList.Current;
            DeletedKeys.Add(s.Key);
            studentList.RemoveCurrent();
            ShowStudentCount();
            m_bChanged = true;
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            m_curType.EndSelectionMode();

            m_selectionStatus = null;
            m_selectionLearns = null;
            m_selectionSpeaks = null;
            m_selectionFirstName = null;
            m_selectionLastName = null;
            m_selectionSource = null;
            m_selectionLevel = null;

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
        #region "ComboBoxClicks"
        private void comboBoxSelectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionStatus = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }

        private void comboBoxSelectLearns_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionLearns = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }

        private void comboBoxSelectSpeaks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionSpeaks = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }
        private void comboBoxSelectSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionSource = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }
        private void comboBoxSelectLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionLevel = (string)comboBox.SelectedItem;
            m_curType.DoSelection();
        }
        #endregion
        #region "TextBoxClicks"
        private void textBoxSelectFirstName_TextChanged(object sender, EventArgs e)
        {
            TextBox testBox = (TextBox)sender;
            m_selectionFirstName = testBox.Text;
            m_curType.DoSelection();
        }

        private void textBoxSelectLastName_TextChanged(object sender, EventArgs e)
        {
            TextBox testBox = (TextBox)sender;
            m_selectionLastName = (string)testBox.Text;
            m_curType.DoSelection();
        }
        private void textBoxComments_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxCellPhone_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxHomePhone_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxAddress1_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxLanguageDetail_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxBirthday_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxSourceDetail_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxSchedule_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxInterests_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxGoals_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        private void textBoxBackground_TextChanged(object sender, EventArgs e)
        {
            m_bChanged = true;
        }

        #endregion
    }
}
