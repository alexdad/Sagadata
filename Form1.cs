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
        Student m_curStudent;
        int m_curIndex;
        List<Student> m_students;
        SchemaField[] m_schema;
        int[] m_placements;
        string m_dataLocation;
        string[] m_enumLanguage;
        string[] m_enumLevel;
        string[] m_enumSource;
        string[] m_enumStatus;
        string[] m_enumSortBy;
        string[] m_enumSortDirection;

        public Form1(string dataLocation)
        {
            m_students = new List<Student>();
            m_dataLocation = dataLocation;
            ReadSchemas();
            InitializeComponent();
            AssignEnums();
        }

        private void AssignEnums()
        {
            comboBoxSelectLearns.Items.AddRange(m_enumLanguage);
            comboBoxSelectSpeaks.Items.AddRange(m_enumLanguage);
            comboBoxSelectStatus.Items.AddRange(m_enumStatus);
            comboBoxSelectSource.Items.AddRange(m_enumSource);
            comboBoxSortBy.Items.AddRange(m_enumSortBy);
            comboBoxSortAscDesc.Items.AddRange(m_enumSortDirection);

            comboBoxLearns.Items.AddRange(m_enumLanguage);
            comboBoxOther.Items.AddRange(m_enumLanguage);
            comboBoxSpeaks.Items.AddRange(m_enumLanguage);
            comboBoxLevel.Items.AddRange(m_enumLevel);
            comboBoxSource.Items.AddRange(m_enumSource);
            comboBoxStatus.Items.AddRange(m_enumStatus);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReadStudentsFile();
            ShowStudents(m_students);
            SetFirstCurrentStudent();
            ShowCurrentStudent();
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
        }

        private void SetCurrentStudent(int index)
        {
            m_curIndex = index;

            buttonNext.Enabled = (m_curIndex < m_students.Count - 1);
            buttonPrev.Enabled = (m_curIndex > 0);

            m_curStudent = m_students[m_curIndex];

            dataGridView1.ClearSelection();
            dataGridView1.Rows[m_curIndex].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[m_curIndex].Cells[0];
        }

        private void SetFirstCurrentStudent()
        {
            SetCurrentStudent(0);
        }
        private void SetNextCurrentStudent()
        {
            SetCurrentStudent(m_curIndex + 1);
        }
        private void SetPrevCurrentStudent()
        {
            SetCurrentStudent(m_curIndex - 1);
        }

        private void SetCurrentStudentFromArray(int index)
        {
            m_curIndex = index;
            m_curStudent = m_students[index];
        }
        private void SaveCurrentStudentToArray()
        {
            if (m_curStudent.FirstName.Length > 0 && m_curStudent.LastName.Length > 0)
                m_students[m_curIndex] = m_curStudent;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteStudentsFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            CaptureStudentEditing();
            SaveCurrentStudentToArray();
            SetNextCurrentStudent();
            ShowCurrentStudent();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            CaptureStudentEditing();
            SaveCurrentStudentToArray();
            SetPrevCurrentStudent();
            ShowCurrentStudent();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            CaptureStudentEditing();
            SaveCurrentStudentToArray();

            Student newStud = Student.Factory();
            m_students.Add(newStud);
            studentListBindingSource.Add(newStud);
            SetCurrentStudent(m_students.Count - 1);
            ShowCurrentStudent();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < m_students.Count)
            {
                SetCurrentStudent(e.RowIndex);
                ShowCurrentStudent();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            m_students.RemoveAt(m_curIndex);
            studentListBindingSource.RemoveAt(m_curIndex);
            if (m_curIndex >= studentListBindingSource.Count)
                m_curIndex = studentListBindingSource.Count - 1;
            SetCurrentStudent(m_curIndex);
            ShowCurrentStudent();
        }
    }
}
