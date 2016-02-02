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
        List<Student> m_students;
        SchemaField[] m_schema;
        int[] m_placements;
        string m_dataLocation;
        string[] m_enumLanguage;
        string[] m_enumLevel;
        string[] m_enumSource;
        string[] m_enumStatus;

        

        public Form1(string dataLocation)
        {
            m_students = new List<Student>();
            m_dataLocation = dataLocation;
            ReadSchemas();
            InitializeComponent();
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

        private void openToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "csv files (*.csv)|*.csv";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            Stream myStream = null;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fpath = openFileDialog1.FileName;
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            ReadCsvFile();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ReadCsvFile();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCsvFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
