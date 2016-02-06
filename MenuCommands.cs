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
            ReadStudentsFile(m_studentsAsRead);

            Student[] temp = ReadCloudFile(m_cloudLocation);
            MergeBack(temp);
            ShowStudentCount();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndSelectionMode();
            WriteStudentsFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student[] temp = ReadCloudFile(m_cloudLocation);
            MergeBack(temp);
            WriteStudentsFile();

            if (File.Exists(m_cloudLocation))
                File.Delete(m_cloudLocation);

            File.Copy(FilePath, m_cloudLocation);
        }
    }
}