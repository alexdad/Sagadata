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
    public class ViewSlot
    {
        public ViewSlot()
        {
            Red = "";
            Teal = "";
            Yellow = "";
            Green = "";
            White = "";
            Pink = "";
            Outside = "";
            Slot = "";
        }

        public ViewSlot(string hdr)
        {
            Red = "";
            Teal = "";
            Yellow = "";
            Green = "";
            White = "";
            Pink = "";
            Outside = "";
            Slot = hdr;
        }

        public string Slot { get; set; }
        public string Red { get; set; }
        public string Teal { get; set; }
        public string Yellow { get; set; }
        public string Green { get; set; }
        public string White { get; set; }
        public string Pink { get; set; }
        public string Outside { get; set; }

        public int SetViewSlot(string room, string text)
        {
            switch (room)
            {
                case "Red":
                    Red = text;
                    return 1;
                case "Teal":
                    Teal = text;
                    return 2;
                case "Yellow":
                    Yellow = text;
                    return 3;
                case "Green":
                    Green = text;
                    return 4;
                case "White":
                    White = text;
                    return 5;
                case "Pink":
                    Pink = text;
                    return 6;
                case "Outside":
                    Outside = text;
                    return 7;
                default:
                    return 7;   // No room = outside
            }
        }
    }


    public partial class FormGlob : Form
    {
        DateTime m_view_chosenDate = DateTime.Today;
        string m_view_chosen_state = "";
        string m_view_chosen_student = "";
        string m_view_chosen_teacher = "";
        string m_view_chosen_room = "";
        string m_view_chosen_program = "";

        bool m_view_selection_mode = false;
        Dictionary<int, Lesson> m_dvgViewTags = new Dictionary<int, Lesson>();

        int m_viewslots_row = -1;
        int m_viewslots_col = -1;

        Color[] m_viewSlotColors = new Color[7]
        {
                Color.FromArgb(182,10,70),      //red
                Color.FromArgb(67,124,121),     //teal
                Color.FromArgb(251,209,85),     // yellow
                Color.FromArgb(158,231,105),    // green
                Color.FromArgb(255,255,255),    // white
                Color.FromArgb(255,213,234),    // pink
                Color.FromArgb(220,220,220)     // outside
        };

        public void ShowWeek()
        {

        }
        public void ShowDay()
        {

            while (panelViewDay.Controls.Count > 0)
                panelViewDay.Controls[0].Dispose();

            panelViewDay.Controls.Clear();

            // Now we can draw here buttons per lesson in right places 

            DateTime t = DateTime.Today;
            DateTime t1 = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0);
            DateTime t2 = new DateTime(t.Year, t.Month, t.Day, 23, 59, 59);
            List<Lesson> lessons = LessonsByTime(t1, t2);
            if (!m_view_selection_mode)
                FillChoices(lessons);
            lbViewCount.Text = lessons.Count.ToString();

            int i = 1;
            foreach (Lesson l in lessons)
            {
                Button b = new Button()
                {
                    Text = i.ToString(),
                    Width = 100,
                    Height = 30,
                    Location = new Point(i * 30, i * 30),
                    Parent = panelViewDay,
                    Tag = i
                };
                // b.MouseHover
                i++;

            }
        }
        public void ViewShowSlots()
        {
            viewSlotList.Clear();
            m_dvgViewTags.Clear();
            for (int i = 0; i < m_enumTimeSlot.Length; i++)
                viewSlotList.Add(new RecordKeeper.ViewSlot(m_enumTimeSlot[i]));

            DateTime dt1 = new DateTime(
                m_view_chosenDate.Year,
                m_view_chosenDate.Month,
                m_view_chosenDate.Day,
                0, 0, 0);

            DateTime dt2 = new DateTime(
                m_view_chosenDate.Year,
                m_view_chosenDate.Month,
                m_view_chosenDate.Day,
                23, 59, 59);

            List<Lesson> lsn = FindLessonSlots(
                m_view_chosen_state,
                m_view_chosen_student,
                m_view_chosen_teacher,
                m_view_chosen_room,
                m_view_chosen_program,
                dt1, dt2);

            if (!m_view_selection_mode)
                FillChoices(lsn);
            lbViewCount.Text = lsn.Count.ToString();

            DateTime dts = new DateTime(
               m_view_chosenDate.Year,
               m_view_chosenDate.Month,
               m_view_chosenDate.Day,
               7, 0, 0);  // slots start at 7 am 

            DateTime dtn = new DateTime(
               m_view_chosenDate.Year,
               m_view_chosenDate.Month,
               m_view_chosenDate.Day,
               7, 15, 0);

            TimeSpan ts = dtn - dts;

            foreach (Lesson l in lsn)
            {
                int slot1Index = (int)((l.DateTimeStart.Ticks - dts.Ticks) / ts.Ticks);
                if (slot1Index < 0 || slot1Index >= m_enumTimeSlot.Length)
                    continue;

                int slot2Index = (int)((l.DateTimeEnd.Ticks - dts.Ticks) / ts.Ticks);
                if (slot2Index < 0 || slot2Index >= m_enumTimeSlot.Length)
                    continue;

                for (int slotIndex = slot1Index; slotIndex <= slot2Index; slotIndex++)
                {
                    string text = "";
                    switch (slotIndex - slot1Index)
                    {
                        case 0:
                            text = l.Student1;
                            break;
                        case 1:
                            text = "w/ " + l.Teacher1;
                            break;
                        case 2:
                            text = "   " + l.Program;
                            break;
                        case 3:
                            text = "   " + l.State;
                            break;
                        case 4:
                            text = "+ " + l.Student2;
                            break;
                        case 5:
                            text = "+ " + l.Student3;
                            break;
                        case 6:
                            text = "+ " + l.Student4;
                            break;
                        default:
                            break;
                    }

                    ViewSlot slot = new RecordKeeper.ViewSlot(m_enumTimeSlot[slotIndex]);
                    int roomIndex = slot.SetViewSlot(l.Room, text);
                    if (roomIndex < 0 || roomIndex > 7)
                        continue;


                    viewSlotList.Add(slot);
                    Color c1 = LessonStateColor(l.State);
                    Color c2 = Color.FromArgb(255 - c1.R, 255 - c1.G, 255 - c1.B);
                    dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Style.BackColor = c1;
                    dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Style.ForeColor = c2;
                    dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Value = text;

                    m_dvgViewTags[slotIndex * 100 + roomIndex] = l;
                }
            }
        }
        private Color LessonStateColor(string state)
        {
            switch (state)
            {
                case "Done":   // light green - confirmed
                    return m_statusColors[(int)StatusColors.Good];
                case "Planned":   // yellow - planned
                    return m_statusColors[(int)StatusColors.Warning];
                case "Cancelled":   // reddish - cancelled
                    return m_statusColors[(int)StatusColors.Bad];
                case "Confirmed":   // red-brown - lesson    
                    return m_statusColors[(int)StatusColors.Attention];
                default:
                    return m_statusColors[(int)StatusColors.Unknown];
            }
        }


        private void CountString(string s, SortedDictionary<string, int> dict)
        {
            if (s != null && s.Trim().Length > 1)
            {
                if (dict.ContainsKey(s))
                    dict[s] = dict[s] + 1;
                else
                    dict[s] = 1;
            }
        }

        void FillChoices(List<Lesson> lsn)
        {
            SortedDictionary<string, int> students = new SortedDictionary<string, int>();
            SortedDictionary<string, int> teachers = new SortedDictionary<string, int>();
            SortedDictionary<string, int> rooms = new SortedDictionary<string, int>();
            SortedDictionary<string, int> programs = new SortedDictionary<string, int>();

            foreach (Lesson l in lsn)
            {
                CountString(l.Room, rooms);

                CountString(l.Program, programs);

                CountString(l.Teacher1, teachers);
                CountString(l.Teacher2, teachers);

                CountString(l.Student1, students);
                CountString(l.Student2, students);
                CountString(l.Student3, students);
                CountString(l.Student4, students);
                CountString(l.Student5, students);
                CountString(l.Student6, students);
                CountString(l.Student7, students);
                CountString(l.Student8, students);
                CountString(l.Student9, students);
                CountString(l.Student10, students);
            }
            cbViewSelectProgram.Items.Clear();
            cbViewSelectProgram.Items.AddRange(programs.Keys.ToArray());

            cbViewSelectRoom.Items.Clear();
            cbViewSelectRoom.Items.AddRange(rooms.Keys.ToArray());

            cbViewSelectStudent.Items.Clear();
            cbViewSelectStudent.Items.AddRange(students.Keys.ToArray());

            cbViewSelectTeacher.Items.Clear();
            cbViewSelectTeacher.Items.AddRange(teachers.Keys.ToArray());
        }

        void ViewSelectLesson(DataGridView dgv, int row, int col)
        {
            if (row < 0 || row >= dgvViewSlots.RowCount ||
                col < 0 || col > dgvViewSlots.ColumnCount)
                return;
            Lesson l = m_dvgViewTags[row * 100 + col];
            lbViewGbDate.Text = l.DateTimeStart.ToShortDateString();
            lbViewGbRoom.Text = l.Room;
            lbViewGbProg.Text = l.Program;
            lbViewGbStart.Text = l.DateTimeStart.ToShortTimeString();
            lbViewGbEnd.Text = l.DateTimeEnd.ToShortTimeString();
            lbViewGbTeacher.Text = l.Teacher1;
            lbViewGbStudent1.Text = l.Student1;
            lbViewGbStudent2.Text = l.Student2;
            lbViewGbStudent3.Text = l.Student3;
            lbViewGbStudent4.Text = l.Student4;
        }
    }
}