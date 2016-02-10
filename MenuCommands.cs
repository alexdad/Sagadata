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

namespace RecordKeeper
{
    public partial class FormGlob : Form
    {
        //----------------------------------------------------
        // Menu strip
        //----------------------------------------------------
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ReadStudentsFile(m_studentsAsRead);
            m_curType.Download<Student>();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_curType.Upload<Student>();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_curType.EndSelectionMode();
            m_curType.WriteRecordsFile();
            m_bChanged = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_bChanged)
            {
                DialogResult result = MessageBox.Show(
                    "Should I save?", "You have unsaved changes", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                    return;
                else if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    result = MessageBox.Show(
                        "Should I upload?", "You have local changes", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Cancel)
                        return;
                    else if (result == DialogResult.Yes)
                        m_curType.Upload<Student>();
                }

            }
            Application.Exit();
        }

    }
}