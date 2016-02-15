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

        delegate bool EvaluateTeacher(Teacher t);

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
            int marginx = 10;
            int marginy = 10;
            int bW = 100;
            int bH = 20;

            string[] days = new string[7] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            int slots = m_enumTimeSlot.Length;
            Button[] top = new Button[7];
            Button[] left = new Button[slots];
            m_AvailabilityCells = new Button[slots, 7];

            for (int i = 0; i < slots; i++)
            {
                left[i] = new Button()
                {
                    Text = m_enumTimeSlot[i],
                    Width = bW,
                    Height = bH,
                    Location = new Point(marginx, (i + 1) * bH + marginy),
                    Parent = this.panelTeachMatrix,
                    Tag = i
                };
                left[i].Click += new System.EventHandler(this.availLeftButton_Click);
            }

            for (int j = 0; j < 7; j++)
            {
                top[j] = new Button()
                {
                    Text = days[j],
                    Width = bW,
                    Height = bH,
                    Location = new Point((j + 1) * bW + marginx, marginy),
                    Parent = this.panelTeachMatrix,
                    Tag = j
                };
                top[j].Click += new System.EventHandler(this.availTopButton_Click);

                for (int i = 0; i < slots; i++)
                {
                    m_AvailabilityCells[i, j] = new Button()
                    {
                        Text = "?",
                        Width = bW,
                        Height = bH,
                        Location = new Point((j + 1) * bW + marginx,
                                             (i + 1) * bH + marginy),
                        Parent = this.panelTeachMatrix,
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
            buttonTeach_GrabAvailChanges.Visible = true;
        }
        private void DropFlagUnsavedAvailabilityChanges()
        {
            m_unsavedAvailabilityChanges = false;
            buttonTeach_GrabAvailChanges.Visible = false;
        }

        private void availLeftButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            for (int i = 0; i < 7; i++)
            {
                availabilityButton_Click(
                    (object)m_AvailabilityCells[(int)b.Tag, i],
                    null);
            }
        }

        private void availTopButton_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            for (int j = 0; j < m_enumTimeSlot.Length; j++)
            {
                availabilityButton_Click(
                    (object)m_AvailabilityCells[j, (int)b.Tag],
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
                    m_AvailabilityCells[j, i].Text = f.Substring(j, 1);
                }
            }
        }
        private void AcceptAvailabilityEdits()
        {
            for (int i = 0; i < 7; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < m_enumTimeSlot.Length; j++)
                    sb.Append(m_AvailabilityCells[j, i].Text[0]);

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


        List<Teacher> ActiveTeachersByLanguage(string lang)
        {
            return FindTeachers(t =>
                    (t.Status == "Active" &&
                    (t.Language == lang || t.Language2 == lang ||
                    t.Language2 == "Other" && t.LanguageDetail == lang)));
        }

        List<Teacher> SpecificTeacher(string desc)
        {
            return FindTeachers(t => t.Description == desc);
        }

        List<Teacher> FindTeachers(EvaluateTeacher comp)
        {
            List<Teacher> teachers = new List<Teacher>();
            foreach (var tt in this.teacherList.List)
            {
                Teacher t = tt as Teacher;
                if (comp(t))
                    teachers.Add(t);
            }
            return teachers;
        }
    }
}