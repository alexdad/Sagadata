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
            ReadStudentsFile(m_studentsAsRead);

            Student[] temp = ReadCloudFile(cloudFile);
            MergeBack(temp);
            SetFirstCurrentStudent();
            ShowCurrentStudent();
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

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelHelp.Visible = true;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}