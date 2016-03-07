using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    public partial class CancelForm : Form
    {
        public string Situation { get; set; }
        public string Comment { get; set; }

        private string[] m_codes;

        public CancelForm(string[] codes)
        {
            m_codes = codes;
            InitializeComponent();
        }

        private void radioButtonCancelOnTime_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCancelOnTime.Checked)
                Situation = m_codes[1];
        }
        private void radioButtonCancelLate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCancelLate.Checked)
                Situation = m_codes[2];
        }

        private void radioButtonCancelNoShow_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCancelNoShow.Checked)
                Situation = m_codes[3];
        }

        private void radioButtonCancelExcused_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCancelExcused.Checked)
                Situation = m_codes[4];
        }

        private void tbCancelComment_TextChanged(object sender, EventArgs e)
        {
            Comment = tbCancelComment.Text;
        }

    }
}
