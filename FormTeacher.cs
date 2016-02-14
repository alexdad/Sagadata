using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    public partial class FormGlob : Form
    {
        string m_teacherSelectionStatus;
        string m_teacherSelectionLanguage;
        string m_teacherSelectionFirstName;
        string m_teacherSelectionLastname;

        int m_teacherDgvCurrentRow = -1;
        Button[,] m_AvailabilityCells = null;
        bool m_unsavedAvailabilityChanges = false;

        private void DropTeacherSelection()
        {
            m_teacherSelectionFirstName = null;
            m_teacherSelectionLanguage = null;
            m_teacherSelectionLastname = null;
            m_teacherSelectionStatus = null;
        }


        private void TeacherToFormConst1()
        {
            m_dataTypes.Add(Modes.Teachers, typeof(Lesson));
            m_recordTypes.Add(Modes.Teachers, new TeacherType(this));
        }
        private void TeacherToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Teachers.ToString());
        }

        public void DoTeacherSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (Teacher s in SavedFullListDuringSelection)
            {
                if (!CurrentType.Fit(m_teacherSelectionStatus, s.Status, true))
                    continue;
                if (!CurrentType.Fit(m_teacherSelectionLanguage, s.Language, true) &&
                    !CurrentType.Fit(m_teacherSelectionLanguage, s.Language2, true))
                    continue;
                if (!CurrentType.Fit(m_teacherSelectionFirstName, s.FirstName, false))
                    continue;
                if (!CurrentType.Fit(m_teacherSelectionLastname, s.LastName, false))
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
            Modified = true;
        }

        const int bW = 30;
        const int bH = 20;
        const int marginx = 10;
        const int marginy = 10;

        private void ShowAvailabilityForm()
        {
            Teacher t = teacherList.Current as Teacher;
            if (t == null)
                return;

            if (m_AvailabilityCells == null)
                PrepareAvailabilityMatrix();

            FillAvailabilityMatrix();
        }

        void PrepareAvailabilityMatrix()
        {
            string[] days = new string[7] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            int slots = m_enumTimeSlot.Length;
            Button[] top = new Button[slots];
            Button[] left = new Button[7];
            m_AvailabilityCells = new Button[7, slots];

            for (int j = 0; j < slots; j++)
            {
                top[j] = new Button()
                {
                    Text = m_enumTimeSlot[j],
                    Width = bW,
                    Height = bH,
                    Location = new Point((j + 1) * bW + marginx, marginy),
                    Parent = this.panelTeacherSecondary,
                    Tag = j
                };
                top[j].Click += new System.EventHandler(this.availTopButton_Click);
            }

            for (int i = 0; i < 7; i++)
            {
                left[i] = new Button()
                {
                    Text = days[i],
                    Width = bW,
                    Height = bH,
                    Location = new Point(marginx, (i + 1) * bH + marginy),
                    Parent = this.panelTeacherSecondary,
                    Tag = i
                };
                left[i].Click += new System.EventHandler(this.availLeftButton_Click);

                for (int j = 0; j < slots; j++)
                {
                    m_AvailabilityCells[i, j] = new Button()
                    {
                        Text = "?",
                        Width = bW,
                        Height = bH,
                        Location = new Point((j + 1) * bW + marginx,
                                              (i + 1) * bH + marginy),
                        Parent = this.panelTeacherSecondary,
                    };

                    m_AvailabilityCells[i, j].Click += 
                        new System.EventHandler(this.availabilityButton_Click);

                    m_AvailabilityCells[i, j].TextChanged +=
                         new System.EventHandler(this.availabilityButton_TextChanged);
                }
            }
        }

        private void availabilityButton_TextChanged(object sender, EventArgs e)
        {
            Button b = sender as Button;
            Color c = Color.Gray;
            switch(b.Text)
            {
                case "0":
                    c = Color.Green;
                    break;
                case "1":
                    c = Color.Yellow;
                    break;
                case "2":
                    c = Color.Red;
                    break;
            }
            b.BackColor = c;
            b.ForeColor = c;
        }

        private void availabilityButton_Click(object sender, EventArgs e)
        {
            FlagUnsavedAvailabilityChanges();

            Button b = sender as Button;
            switch (b.Text)
            {
                case "0":
                    b.Text = "2";
                    break;
                case "1":
                    b.Text = "0";
                    break;
                case "2":
                    b.Text = "1";
                    break;
                default:
                    b.Text = "0";
                    break;
            }
        }

        private void FlagUnsavedAvailabilityChanges()
        {
            m_unsavedAvailabilityChanges = true;
            buttonTeach_GrabAvailChanges.BackColor =
                System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(39)))));

        }
        private void DropFlagUnsavedAvailabilityChanges()
        {
            m_unsavedAvailabilityChanges = false;
            buttonTeach_GrabAvailChanges.BackColor = Color.Gray;
        }

        private void availTopButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            for (int i = 0; i < 7; i++)
            {
                availabilityButton_Click(
                    (object)m_AvailabilityCells[i, (int)b.Tag],
                    null);
            }
        }

        private void availLeftButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            for (int j = 0; j < m_enumTimeSlot.Length; j++)
            {
                availabilityButton_Click(
                    (object)m_AvailabilityCells[(int)b.Tag, j],
                    null);
            }
        }

        void FillAvailabilityMatrix()
        {
            Teacher t = teacherList.Current as Teacher;
            int slots = m_enumTimeSlot.Length;
            string[] vals = new string[7];
            for (int i = 0; i < 7; i++)
            {
                string f = null;
                switch (i)
                {
                    case 0:
                        f = t.Monday;
                        break;
                    case 1:
                        f = t.Tuesday;
                        break;
                    case 2:
                        f = t.Wednesday;
                        break;
                    case 3:
                        f = t.Thursday;
                        break;
                    case 4:
                        f = t.Friday;
                        break;
                    case 5:
                        f = t.Saturday;
                        break;
                    case 6:
                        f = t.Sunday;
                        break;
                }
                if (f.Length < slots)
                    f = f + new string('0', slots - f.Length);

                for (int j = 0; j < slots; j++)
                {
                    m_AvailabilityCells[i, j].Text = f.Substring(j, 1);
                }
            }
        }
        private void availabilityGrabButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < m_enumTimeSlot.Length; j++)
                    sb.Append(m_AvailabilityCells[i, j].Text[0]);

                Teacher t = teacherList.Current as Teacher;
                switch (i)
                {
                    case 0:
                        t.Monday = sb.ToString();
                        break;
                    case 1:
                        t.Tuesday = sb.ToString();
                        break;
                    case 2:
                        t.Wednesday = sb.ToString();
                        break;
                    case 3:
                        t.Thursday = sb.ToString();
                        break;
                    case 4:
                        t.Friday = sb.ToString();
                        break;
                    case 5:
                        t.Saturday = sb.ToString();
                        break;
                    case 6:
                        t.Sunday = sb.ToString();
                        break;
                    case 7:
                        t.Monday = sb.ToString();
                        break;
                }
            }

            DropFlagUnsavedAvailabilityChanges();
        }
    }
}