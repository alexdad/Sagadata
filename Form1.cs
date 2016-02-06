using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Students
{
    public partial class Form1 : Form
    {
        public static string Client;

        Student m_curStudent;
        int m_curIndex;
        Student[] m_savedFullListDuringSelection;
        Dictionary<string, Student> m_studentsAsRead;
        List<string> m_deletedKeys;

        SchemaField[] m_schema;
        int[] m_placements;
        string m_dataLocation;

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

        public Form1(string dataLocation)
        {
            m_dataLocation = dataLocation;
            ReadSchemas();
            InitializeComponent();
            AssignEnums();
            SelectionMode = false;
            m_deletedKeys = new List<string>();
            Client = Environment.MachineName;
            m_studentsAsRead = new Dictionary<string, Student>();
        }

        public bool SelectionMode
        {
            get { return buttonShowAll.Enabled; }
            set { buttonShowAll.Enabled = value; }
        }



        private void SetCurrentStudent(int index)
        {
            m_curIndex = index;

            buttonNext.Enabled = (m_curIndex < studentList.Count - 1);
            buttonPrev.Enabled = (m_curIndex > 0);

            m_curStudent = (Student)studentList[m_curIndex];

            dataGridView1.ClearSelection();
            dataGridView1.Rows[m_curIndex].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[m_curIndex].Cells[0];

            ShowCurrentStudent();
        }

        private void SetFirstCurrentStudent()
        {
            SetCurrentStudent(0);
        }
        private void SetLastCurrentStudent()
        {
            SetCurrentStudent(studentList.Count - 1);
        }
        private void SetNextCurrentStudent()
        {
            SetCurrentStudent(m_curIndex + 1);
        }
        private void SetPrevCurrentStudent()
        {
            SetCurrentStudent(m_curIndex - 1);
        }

        private void EndSelectionMode()
        {
            if (SelectionMode)
            {
                CaptureStudentEditing();
                Student[] temp = ForkOut(0);
                RestoreStudentList();
                MergeBack(temp);
                SetFirstCurrentStudent();
                SelectionMode = false;
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            CaptureStudentEditing();
            SetNextCurrentStudent();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            CaptureStudentEditing();
            SetPrevCurrentStudent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Student[] temp = ForkOut(1);
            temp[studentList.Count] = Student.Factory();
            MergeBack(temp);
            SetLastCurrentStudent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CaptureStudentEditing();
            if (e.RowIndex >= 0 && e.RowIndex < studentList.Count)
                SetCurrentStudent(e.RowIndex);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Student s = (Student)studentList[m_curIndex];
            m_deletedKeys.Add(s.Key);
            studentList.RemoveAt(m_curIndex);
            if (m_curIndex >= studentList.Count)
                m_curIndex = studentList.Count - 1;
            SetCurrentStudent(m_curIndex);
            ShowCurrentStudent();
        }

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
            MergeBack(temp);
            SetFirstCurrentStudent();
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

        private void comboBoxSelectSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionSource = (string)comboBox.SelectedItem;
            DoSelection();
        }

        private void comboBoxSelectLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_selectionLevel= (string)comboBox.SelectedItem;
            DoSelection();
        }
    }
}
 