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

            bool success = false;
            switch(m_cloudType) 
            {
                case Clouds.Google:
                    success = GDrive.Ops.DownloadStudentsFile(m_fileName + ".csv", m_cloudLocation);
                    if (!success)
                        MessageBox.Show("Cannot download from Google. Opening local file.");
                    break;
                case Clouds.Azure:
                case Clouds.Dir:
                default:
                    break;
            }

            if (success)
            {
                labelLastDownload.Text = "Last download: " + DateTime.Now.ToShortTimeString();
                m_bSynced = true;

                Student[] temp = ReadCloudFile(m_cloudLocation);
                MergeBack(temp);
            }
            ShowStudentCount();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Student[] temp = ReadCloudFile(m_cloudLocation);
            MergeBack(temp);
            WriteStudentsFile();
            labelLastDownload.Text = "Last download: " + DateTime.Now.ToShortTimeString();
            m_bSynced = true;

            if (File.Exists(m_cloudLocation))
                File.Delete(m_cloudLocation);

            File.Copy(FilePath, m_cloudLocation);

            bool success = false;
            switch (m_cloudType)
            {
                case Clouds.Google:
                    success = GDrive.Ops.UploadStudentsFile(m_cloudLocation, m_fileName + ".csv");
                    break;
                case Clouds.Azure:
                case Clouds.Dir:
                default:
                    break;
            }
            if (!success)
                MessageBox.Show("Cannot upload to the cloud. Local file is OK.");
            else
                labelLastUpload.Text = "Last upload: " + DateTime.Now.ToShortTimeString();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndSelectionMode();
            WriteStudentsFile();
            m_bChanged = false;
            m_bSynced = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_bChanged)
            {
                DialogResult result = MessageBox.Show(
                    "Should I save?",  "You have unsaved changes",  MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                    return;
                else if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
            }

            if (!m_bSynced)
            {
                DialogResult result = MessageBox.Show(
                    "Should I upload?", "You have local changes", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                    return;
                else if (result == DialogResult.Yes)
                {
                    uploadToolStripMenuItem_Click(sender, e);
                }
            }
            Application.Exit();
        }

    }
}