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

        string m_mode;
        RecordType m_curType;

        public Clouds CloudType { get; set; }
        public string CloudLocation { get; set; }
        public string FileName { get; set; }
        public bool Changed { get; set; }

        public Dictionary<string, Record> RecordsAsRead { get; set; }

        public int[] Placements { get; set; }

        public SchemaField[] Schema { get; set; }

        public List<string> DeletedKeys { get; set;  }

        public Record[] SavedFullListDuringSelection { get; set; }

        public string SelectionLevel { get; set; }
        public string DataLocation { get; set; }

        public string BackupLocation { get; set; }
        public int BackupLimit{ get; set; }

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
                if (!m_curType.Fit(SelectionLevel, s.Level, true))
                    continue;

                DataList.Add(s);
            }
            m_curType.ShowCount();
            Changed = true;
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
            Changed = true;
            tbStudFirstName.Select();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Student s = (Student)studentList.Current;
            DeletedKeys.Add(s.Key);
            studentList.RemoveCurrent();
            ShowStudentCount();
            Changed = true;
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
            SelectionLevel = (string)comboBox.SelectedItem;
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
    }
}
