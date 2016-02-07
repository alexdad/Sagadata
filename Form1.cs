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
        string m_gdriveUrl;
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

        // Temporary - it should be cloud
        const string s_cloudLocation = @"C:\Users\Sasha\Documents\Visual Studio 2015\Projects\Students\Remote";

        public Form1()
        {
            ReadSettings();
            ReadSchemas();
            InitializeComponent();
            AssignEnums();
            SelectionMode = false;
            m_deletedKeys = new List<string>();
            Client = Environment.MachineName;
            m_studentsAsRead = new Dictionary<string, Student>();
            ReadStudentsFile(m_studentsAsRead);
            ShowStudentCount();
        }

        private void ReadSettings()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            m_dataLocation = Path.Combine(dir, Properties.Settings.Default.LocalDir);
            m_backupLocation = Path.Combine(dir, Properties.Settings.Default.BackupDir);
            m_cloudType = Clouds.None;
            m_fileName = Properties.Settings.Default.FileName;
            m_cloudLocation = Path.Combine(Properties.Settings.Default.CloudLocation, m_fileName + ".csv");
            switch (Properties.Settings.Default.CloudType.ToLower())
            {
                case "dir":
                    m_cloudType = Clouds.Dir;
                    break;
                case "azure":
                    m_cloudType = Clouds.Azure;
                    break;
                case "google":
                    m_cloudType = Clouds.Google;
                    m_gdriveUrl = Properties.Settings.Default.GDriveUrl;
                    break;
            }

            m_backupLimit = Properties.Settings.Default.BackupLimit;
        }

        public string FilePath
        {
            get { return Path.Combine(m_dataLocation, m_fileName + ".csv"); }
        }

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
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Student s = (Student)studentList.Current;
            m_deletedKeys.Add(s.Key);
            studentList.RemoveCurrent();
            ShowStudentCount();
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
        #endregion
    }
}
