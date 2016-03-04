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
    public partial class RepeatForm : Form
    {
        public bool UpToTheEOY { get; set; }
        public int Repeats { get; set; }

        public RepeatForm()
        {
            InitializeComponent();
        }

        private void rbRepeatEOY_CheckedChanged(object sender, EventArgs e)
        {
            UpToTheEOY = rbRepeatEOY.Checked;
            if (UpToTheEOY)
                tbRepeatTimes.Text = "";
        }

        private void tbRepeatTimes_TextChanged(object sender, EventArgs e)
        {
            int repeats = 0;
            if (int.TryParse(tbRepeatTimes.Text, out repeats))
            {
                Repeats = repeats;
            }
            else if (!UpToTheEOY)
                MessageBox.Show("Invalid value of repeats number");
        }

        private void tbRepeatTimes_Click(object sender, EventArgs e)
        {
            UpToTheEOY = false;
            rbRepeatEOY.Checked = false;
            rbRepeatTimes.Checked = true;
        }
    }
}
