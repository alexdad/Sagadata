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

namespace Students
{
    public partial class Form1 : Form
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

        Student[] m_savedFullListDuringSelection;
        Dictionary<string, Student> m_studentsAsRead;
        List<string> m_deletedKeys;

        SchemaField[] m_schema;
        int[] m_placements;
        string m_dataLocation;
        string m_cloudLocation;
        string m_backupLocation;
        string m_fileName;
        Clouds m_cloudType;
        int m_backupLimit;

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

        bool m_bChanged;

        #region "Form"
        public Form1()
        {
            m_bChanged = false;
            ReadSettings();
            ReadSchemas();
            PrepareDataDirectories();
            InitializeComponent();
            AssignEnums();
            SelectionMode = false;
            m_deletedKeys = new List<string>();
            Client = Environment.MachineName;
            m_studentsAsRead = new Dictionary<string, Student>();

            if (Properties.Settings.Default.InitialDownload.ToLower() != "no")
            {
                if (!Download())
                    ReadStudentsFile(m_studentsAsRead);
            }
            else
                ReadStudentsFile(m_studentsAsRead);

            ShowStudentCount();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = Properties.Settings.Default.Form1Size;
            splitContainerDataControls.SplitterDistance = Properties.Settings.Default.SplitDC;
            splitContainerMasterDetail.SplitterDistance = Properties.Settings.Default.SplitMD;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Form1Size = this.Size;
            Properties.Settings.Default.SplitDC = splitContainerDataControls.SplitterDistance;
            Properties.Settings.Default.SplitMD = splitContainerMasterDetail.SplitterDistance;
            Properties.Settings.Default.Save();
        }
        #endregion
        #region "Navigation"
        public bool SelectionMode
        {
            get { return buttonShowAll.Enabled; }
            set { buttonShowAll.Enabled = value; }
        }

        private void EndSelectionMode()
        {
            if (SelectionMode)
            {
                Student[] temp = ForkOut(0);
                RestoreStudentList();
                MergeBack(temp);
                SelectionMode = false;
            }
        }
        #endregion
        #region "DataGridClicks"
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != s_lastColumnSorted)
            {
                s_lastColumnSorted = e.ColumnIndex;
                s_needToReverse = false;
            }
            else
                s_needToReverse = !s_needToReverse;

            DataGridViewColumn col = dataGridView1.Columns[e.ColumnIndex];
            Student[] temp = ForkOut(0);
            SortStudents(col.HeaderText, temp);
            ReplaceStudentList(temp);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView1.ColumnCount &&
                e.RowIndex >= 0 && e.RowIndex < dataGridView1.RowCount &&
                dataGridView1[e.ColumnIndex, e.RowIndex].Value != null)

                Clipboard.SetText(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
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
            st.Id = Form1.AllocateID();
            ShowStudentCount();
            m_bChanged = true;
            textBoxFirstName.Select();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Student s = (Student)studentList.Current;
            m_deletedKeys.Add(s.Key);
            studentList.RemoveCurrent();
            ShowStudentCount();
            m_bChanged = true;
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            EndSelectionMode();

            m_selectionStatus = null;
            m_selectionLearns = null;
            m_selectionSpeaks = null;
            m_selectionFirstName = null;
            m_selectionLastName = null;
            m_selectionSource = null;
            m_selectionLevel = null;

            comboBoxSelectStatus.SelectedIndex = 0;
            comboBoxSelectLearns.SelectedIndex = 0;
            comboBoxSelectSpeaks.SelectedIndex = 0;
            textBoxSelectFirstName.Text = "";
            textBoxSelectLastName.Text = "";
            comboBoxSelectSource.SelectedIndex = 0;
            comboBoxSelectLevel.SelectedIndex = 0;
            // Changed
        }

        private void buttonToExcel_Click(object sender, EventArgs e)
        {
            string tempCsv = WriteTempFile();
            System.Diagnostics.Process.Start(tempCsv);
        }

        #endregion
        #region "ComboBoxClicks"
        private void comboBoxSelectStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionStatus = (string)comboBox.SelectedItem;
            DoSelection();
        }

        private void comboBoxSelectLearns_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionLearns = (string)comboBox.SelectedItem;
            DoSelection();
        }

        private void comboBoxSelectSpeaks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionSpeaks = (string)comboBox.SelectedItem;
            DoSelection();
        }
        private void comboBoxSelectSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionSource = (string)comboBox.SelectedItem;
            DoSelection();
        }
        private void comboBoxSelectLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionLevel = (string)comboBox.SelectedItem;
            DoSelection();
        }
        #endregion
        #region "TextBoxClicks"
        private void textBoxSelectFirstName_TextChanged(object sender, EventArgs e)
        {
            TextBox testBox = (TextBox)sender;
            m_selectionFirstName = testBox.Text;
            DoSelection();
        }

        private void textBoxSelectLastName_TextChanged(object sender, EventArgs e)
        {
            TextBox testBox = (TextBox)sender;
            m_selectionLastName = (string)testBox.Text;
            DoSelection();
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
