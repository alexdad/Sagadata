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
    enum Validations
    {
        Ignore,
        Number,
        Text,
        Timing,
        Phone,
        Email,
        Status,
        Language,
        Level,
        Source
    }

    struct SchemaField
    {
        public SchemaField(string h, string v)
        {
            Header = h;
            switch(v)
            {
                case "number":
                    Validation = Validations.Number;
                    break;
                case "text":
                    Validation = Validations.Text;
                    break;
                case "timing":
                    Validation = Validations.Timing;
                    break;
                case "phone":
                    Validation = Validations.Phone;
                    break;
                case "email":
                    Validation = Validations.Email;
                    break;
                case "Status":
                    Validation = Validations.Status;
                    break;
                case "Language":
                    Validation = Validations.Language;
                    break;
                case "Level":
                    Validation = Validations.Level;
                    break;
                case "Source":
                    Validation = Validations.Source;
                    break;
                default:
                    throw new Exception("Unknown schema type " + v);
            }
        }
        public string Header;
        public Validations Validation;
    }
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

        private void AssignEnums()
        {
            comboBoxSelectLearns.Items.AddRange(m_enumLanguage);
            comboBoxSelectSpeaks.Items.AddRange(m_enumLanguage);
            comboBoxSelectStatus.Items.AddRange(m_enumStatus);
            comboBoxSelectSource.Items.AddRange(m_enumSource);
            comboBoxSelectLevel.Items.AddRange(m_enumLevel);

            comboBoxLearns.Items.AddRange(m_enumLanguage);
            comboBoxOther.Items.AddRange(m_enumLanguage);
            comboBoxSpeaks.Items.AddRange(m_enumLanguage);
            comboBoxLevel.Items.AddRange(m_enumLevel);
            comboBoxSource.Items.AddRange(m_enumSource);
            comboBoxStatus.Items.AddRange(m_enumStatus);
        }

        private void ShowCurrentStudent()
        {
            textBoxBirthday.Text = m_curStudent.Birthday;
            textBoxEmail.Text = m_curStudent.Email;
            textBoxAddress1.Text = m_curStudent.MailingAddress;
            textBoxFirstName.Text = m_curStudent.FirstName;
            textBoxLastName.Text = m_curStudent.LastName;
            textBoxSchedule.Text = m_curStudent.PossibleSchedule;
            textBoxHomePhone.Text = m_curStudent.HomePhone;
            textBoxCellPhone.Text = m_curStudent.CellPhone;
            textBoxComments.Text = m_curStudent.Comments;
            textBoxInterests.Text = m_curStudent.Interests;
            textBoxGoals.Text = m_curStudent.Goals;
            textBoxBackground.Text = m_curStudent.Background;
            textBoxSourceDetail.Text = m_curStudent.SourceDetail;
            textBoxBirthday.Text = m_curStudent.Birthday;
            textBoxLanguageDetail.Text = m_curStudent.LanguageDetail;
            comboBoxLearns.Text = m_curStudent.LearningLanguage;
            comboBoxLevel.Text = m_curStudent.Level;
            comboBoxOther.Text = m_curStudent.OtherLanguage;
            comboBoxSource.Text = m_curStudent.Source;
            comboBoxSpeaks.Text = m_curStudent.NativeLanguage;
            comboBoxStatus.Text = m_curStudent.Status;

            labelCount.Text = studentList.Count.ToString();
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

        private void SaveCurrentStudentToArray()
        {
            if (m_curStudent.FirstName.Length > 0 && m_curStudent.LastName.Length > 0)
                studentList[m_curIndex] = m_curStudent;
        }

        private Student[] ForkOut(int addedPositions)
        {
            Student[] temp = new Student[studentList.Count + addedPositions];
            int i = 0;
            foreach (Student s in studentList)
                temp[i++] = s.Duplicate();
            return temp;
        }
        private bool IsStudentDeleted(Student s)
        {
            return m_deletedKeys.Contains(s.Key);
        }

        public bool StudentDiffers(Student s1, Student s2)
        {
            for (int i = 0; i < m_schema.Length; i++)
            {
                string hdr = m_schema[i].Header;
                if (hdr == "DateTimeChanged" ||
                    hdr == "ChangedBy")
                    continue;

                if (s1.Get(hdr) != s2.Get(hdr))
                    return true;
            }
            return false;
        }

        private void MergeBack(Student[] target)
        {
            Dictionary<string, Student> dict = new Dictionary<string, Student>();
            foreach (Student s in studentList)
            {
                if (!IsStudentDeleted(s))
                    dict.Add(s.Key, s);
            }

            foreach (Student t in target)
            {
                if (!IsStudentDeleted(t))
                {
                    if (!dict.ContainsKey(t.Key))
                        dict.Add(t.Key, t);
                    else if (StudentDiffers(t, dict[t.Key]))
                    {
                        if (dict[t.Key].Changed < t.Changed)
                        {
                            dict[t.Key] = t;        // Target had newer, restoring back
                            MessageBox.Show( String.Format (
                                "Merge conflict: {0} edited {1} at {2}; not taking yours!",
                                               t.ChangedBy,
                                               t.FirstName + " " + t.LastName,
                                               t.Changed));
                        }
                    }
                }
            }

            studentList.Clear();
            foreach (Student s in dict.Values)
                studentList.Add(s);

            m_deletedKeys.Clear();
        }

        private void StashStudentList()
        {
            m_savedFullListDuringSelection = ForkOut(0);
        }

        private void RestoreStudentList()
        {
            studentList.Clear();
            foreach (Student s in m_savedFullListDuringSelection)
                studentList.Add(s);
            m_savedFullListDuringSelection = null;
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
        static int s_lastColumnSorted = -1;
        static bool s_needToReverse = false;

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

            //m_enumColumnNames = new string[]
            //    { "Status", "First Name", "Last Name", "Email", "Phone", "Learning", "Level",
            //      "Native", "Other", "Birthday", "Source", "Address"};

            switch (col.HeaderText)
            {
                case "Status":
                    Array.Sort(temp, new ComparerByStatus());
                    break;
                case "Changed":
                    Array.Sort(temp, new ComparerByChanged());
                    break;
                case "Learning":
                    Array.Sort(temp, new ComparerByLearns());
                    break;
                case "Other":
                    Array.Sort(temp, new ComparerByOther());
                    break;
                case "Level":
                    Array.Sort(temp, new ComparerByLevel());
                    break;
                case "Native":
                    Array.Sort(temp, new ComparerBySpeaks());
                    break;
                case "First Name":
                    Array.Sort(temp, new ComparerByFirstName());
                    break;
                case "Last Name":
                    Array.Sort(temp, new ComparerByLastName());
                    break;
                case "Source":
                    Array.Sort(temp, new ComparerBySource());
                    break;
                default:
                    s_needToReverse = false;
                    break;
            }
            if (s_needToReverse)
                Array.Reverse(temp);

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

        private bool Fit(string what, string where)
        {
            return (what == null || what == "" || what == "?" || 
                    where.ToLower().Contains(what.ToLower()));
        }
        private void DoSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                StashStudentList();
            }

            studentList.Clear();
            foreach (Student s in m_savedFullListDuringSelection)
            {
                if (!Fit(m_selectionStatus, s.Status))
                    continue;
                if (!Fit(m_selectionLearns, s.LearningLanguage))
                    continue;
                if (!Fit(m_selectionSpeaks, s.NativeLanguage))
                    continue;
                if (!Fit(m_selectionFirstName, s.FirstName))
                    continue;
                if (!Fit(m_selectionLastName, s.LastName))
                    continue;
                if (!Fit(m_selectionSource, s.Source))
                    continue;
                if (!Fit(m_selectionLevel, s.Level))
                    continue;

                studentList.Add(s);
            }
            if (studentList.Count > 0)
                SetFirstCurrentStudent();
            else
                m_curStudent = Student.Factory();
            ShowCurrentStudent();
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

        //----------------------------------------------------
        // Menu strip
        //----------------------------------------------------
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TEMP - it should be real cloud
            string cloudFile = s_cloudLocation + @"\Students.csv";
            if (!File.Exists(cloudFile))
            {
                MessageBox.Show("Cannot find cloud file");
                return;
            }
            string studFile = m_dataLocation + @"\Students.csv";
            if (File.Exists(studFile))
                File.Delete(studFile);

            File.Copy(cloudFile, studFile);
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReadStudentsFile(m_studentsAsRead);
            SetFirstCurrentStudent();
            ShowCurrentStudent();
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureStudentEditing();
            EndSelectionMode();
            WriteStudentsFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string studFile = m_dataLocation + @"\Students.csv";
            string cloudFile = s_cloudLocation + @"\Students.csv";

            if (!File.Exists(cloudFile))
            {
                MessageBox.Show("Cannot find cloud file");
                return;
            }

            Student[] temp = ReadCloudFile(cloudFile);
            MergeBack(temp);
            WriteStudentsFile();

            if (File.Exists(cloudFile))
                File.Delete(cloudFile);

            File.Copy(studFile, cloudFile);
        }
    }
}
 