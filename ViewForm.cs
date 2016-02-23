﻿using System;
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

        public void ViewShowMonth()
        {
            DisposeChildren(panelViewMonth);
            int days = s_DaysPerMonth[m_view_chosenDate.Month - 1];
            int cellWidth = panelViewMonth.Width / days;
            int cellHight = panelViewMonth.Height / m_enumTimeSlot.Length;
            int cellRoomWidth = cellWidth / roomList.Count;

            DrawMonthDaysAsTopRow(m_view_chosenDate, panelViewMonth, cellWidth);
            DrawTimeSlotsAsLeftColumn(panelViewMonth, cellHight);
            foreach (Lesson l in FindLessonsForView())
            {
                int x, ys, ye;
                l.GetLocationInMonth(out x, out ys, out ye);

                Label lb = new Label()
                {
                    Text = l.State.Substring(0, 1),
                    Width = cellRoomWidth,
                    Height = cellHight * (ye - ys + 1) - 10,
                    Location = new Point(
                        60 + cellWidth * x + cellRoomWidth * RoomIndex(l.Room),
                        40 + cellHight * ys + 5),
                    Parent = panelViewMonth,
                    BackColor = RoomColor(l.Room),
                    ForeColor = ComplementColor(RoomColor(l.Room)),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = l
                };
                lb.ContextMenuStrip = ctxMenuLesson;
                lb.MouseHover += new System.EventHandler(this.butViewShowLesson_MouseHover);
            }

            ExpandLabels(
                panelViewMonth, 
                60, 40, 
                days, m_enumTimeSlot.Length,
                cellRoomWidth, roomList.Count,
                cellWidth, cellHight);
        }

        public void ViewShowWeek()
        {
            DisposeChildren(panelViewWeek);
            int cellWidth = panelViewWeek.Width / WeekdayNames().Length;
            int cellHight = panelViewWeek.Height / m_enumTimeSlot.Length;
            int cellRoomWidth = cellWidth / roomList.Count;  // we need it for uniquity

            DrawWeekDaysAsTopRow(m_view_chosenDate, panelViewWeek, cellWidth);
            DrawTimeSlotsAsLeftColumn(panelViewWeek, cellHight);
            foreach (Lesson l in FindLessonsForView())
            {
                int x, ys, ye;
                l.GetLocationInWeek(out x, out ys, out ye);

                Label lb = new Label()
                {
                    Text = l.ShortDescription,
                    Width = cellRoomWidth,
                    Height = cellHight * (ye - ys + 1) - 10,
                    Location = new Point(
                        60 + cellWidth * x + cellRoomWidth * RoomIndex(l.Room),
                        40 + cellHight * ys + 5),
                    Parent = panelViewWeek,
                    BackColor = RoomColor(l.Room),
                    ForeColor = ComplementColor(RoomColor(l.Room)),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = l
                };
                lb.ContextMenuStrip = ctxMenuLesson;
                lb.MouseHover += new System.EventHandler(this.butViewShowLesson_MouseHover);
            }

            ExpandLabels(
                panelViewWeek,
                60, 40,
                7, m_enumTimeSlot.Length,
                cellRoomWidth, roomList.Count,
                cellWidth, cellHight);
        }

        public void ViewShowDay()
        {
            if (roomList.Count == 0)
                return;
            DisposeChildren(panelViewDay);
            int cellWidth = (panelViewDay.Width - 60) / roomList.Count;
            int cellHight = (panelViewDay.Height - 20) / m_enumTimeSlot.Length;

            DrawRoomsAsTopRow(panelViewDay, cellWidth);
            DrawTimeSlotsAsLeftColumn(panelViewDay, cellHight);
            foreach (Lesson l in FindLessonsForView())
            {
                int x, ys, ye;
                Color roomColor;
                GetLocationInRooms(l, out x, out ys, out ye, out roomColor);

                Button b = new Button()
                {
                    Text = l.Description,
                    Width = cellWidth - 10,
                    Height = cellHight * (ye - ys + 1) - 10,
                    Location = new Point(
                        60 + cellWidth * x + 5, 
                        40 + cellHight * ys + 5),
                    Parent = panelViewDay,
                    BackColor = LessonStateColor(l.State), 
                    ForeColor = ComplementColor(LessonStateColor(l.State)),
                    Tag = l
                };
                b.ContextMenuStrip = ctxMenuLesson;
                b.MouseHover += new System.EventHandler(this.butViewShowLesson_MouseHover);
            }
            panelViewDay.Refresh();
        }
        public void ViewShowSlots()
        {
            viewSlotList.Clear();
            m_dvgViewTags.Clear();
            for (int i = 0; i < m_enumTimeSlot.Length; i++)
                viewSlotList.Add(new RecordKeeper.ViewSlot(m_enumTimeSlot[i]));

            DateTime dts = WorkDayStart(m_view_chosenDate);
            foreach (Lesson l in FindLessonsForView())
            {
                int slot1Index = FormGlob.Slots(dts, l.DateTimeStart);
                if (slot1Index < 0 || slot1Index >= m_enumTimeSlot.Length)
                    continue;

                int slot2Index = FormGlob.Slots(dts, l.DateTimeEnd);
                if (slot2Index < 0 || slot2Index >= m_enumTimeSlot.Length)
                    continue;

                for (int slotIndex = slot1Index; slotIndex <= slot2Index; slotIndex++)
                {
                    string text = GetSlotText(l, slotIndex - slot1Index);
                    ViewSlot slot = new RecordKeeper.ViewSlot(m_enumTimeSlot[slotIndex]);
                    int roomIndex = slot.SetViewSlot(l.Room, text);
                    if (roomIndex < 0 || roomIndex > 7)
                        continue;

                    viewSlotList.Add(slot);
                    Color c1 = LessonStateColor(l.State);
                    Color c2 = ComplementColor(LessonStateColor(l.State));
                    dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Style.BackColor = c1;
                    dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Style.ForeColor = c2;
                    dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Value = text;

                    m_dvgViewTags[slotIndex * 100 + roomIndex] = l;
                }
            }
        }

        private List<Lesson> FindLessonsForView()
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = DateTime.Now;
            switch ((TabScales)tabControlViewScales.SelectedIndex)
            {
                case TabScales.Month:
                    dt1 = MonthStart(m_view_chosenDate);
                    dt2 = MonthEnd(m_view_chosenDate);
                    break;
                case TabScales.Week:
                    dt1 = WeekStart(m_view_chosenDate);
                    dt2 = WeekEnd(m_view_chosenDate);
                    break;
                case TabScales.Day:
                    dt1 = DayStart(m_view_chosenDate);
                    dt2 = DayEnd(m_view_chosenDate);
                    break;
                case TabScales.Slot:
                    dt1 = DayStart(m_view_chosenDate);
                    dt2 = DayEnd(m_view_chosenDate);
                    break;
            }

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

            return lsn;
        }

        private Color LessonStateColor(string state)
        {
            switch (state)
            {
                case "Done":   // blue - done
                    return StateColors[(int)StatusColors.Irrelevant];
                case "Planned":   // green - planned
                    return StateColors[(int)StatusColors.Good];
                case "Cancelled":   // reddish - cancelled
                    return StateColors[(int)StatusColors.Bad];
                default:
                    return StateColors[(int)StatusColors.Unknown];
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
            int ind = row * 100 + col;
            if (m_dvgViewTags.ContainsKey(ind))
            {
                ShowCurrentLesson(m_dvgViewTags[ind]);
            }
        }

        public void ShowCurrentLesson(Lesson l)
        {
            lbViewGbDate.Text = l.DateTimeStart.ToShortDateString();
            lbViewGbState.Text = l.State;
            lbViewGbRoom.Text = l.Room;
            lbViewGbProg.Text = l.Program;
            lbViewGbComment.Text = l.Comments;
            lbViewGbStart.Text = l.DateTimeStart.ToShortTimeString();
            lbViewGbEnd.Text = l.DateTimeEnd.ToShortTimeString();
            lbViewGbTeacher.Text = l.Teacher1;
            lbViewGbTeacher2.Text = l.Teacher2;
            lbViewGbStudent1.Text = l.Student1;
            lbViewGbStudent2.Text = l.Student2;
            lbViewGbStudent3.Text = l.Student3;
            lbViewGbStudent4.Text = l.Student4;

            lbViewDetailTeacher.Text = l.Teacher1;
            tbViewDetailTeacher.Text = GetTeacherComment(l.Teacher1);
            lbViewDetailStudent.Text = l.Student1;
            tbViewDetailStudent.Text = GetStudentComment(l.Student1);
            cbViewDetailRoom.Text = l.Room;
            cbViewDetailProgram.Text = l.Program;
            tbViewDetailComment.Text = l.Comments;

            ViewLessonDetailsSet(l.Key);
        }

        private string GetSlotText(Lesson l, int slotIndex)
        {
            string text = "";
            switch (slotIndex)
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
            return text;
        }
        public void GetLocationInRooms(Lesson l, 
            out int col, out int row1, out int row2, out Color color)
        {
            color = Color.Gray;
            l.GetLocationInWeek(out col, out row1, out row2);
            int i = 0;
            col = -1;
            foreach (Room r in roomList)
            {
                if (r.Name == l.Room)
                {
                    col = i;
                    color = r.RoomColor;
                }
                i++; 
            }

            if (col == -1)
                col = roomList.Count - 1;
        }

        private void DrawWeekDaysAsTopRow(DateTime dt, Panel panel, int cellWidth)
        {
            int i = 0;
            foreach (string d in WeekOf(dt))
            {
                Label l = new Label()
                {
                    Text = d,
                    ForeColor = Color.Black,
                    BackColor = Color.White,
                    Width = cellWidth,
                    Height = 20,
                    Location = new Point(
                        60 + cellWidth * i + 5,
                        5),
                    Parent = panel,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                i++;
            }
        }

        private void DrawMonthDaysAsTopRow(DateTime dt, Panel panel, int cellWidth)
        {
            int i = 0;
            foreach (string d in MonthOf(dt))
            {
                Label l = new Label()
                {
                    Text = d,
                    ForeColor = Color.Black,
                    BackColor = Color.White,
                    Width = cellWidth,
                    Height = 20,
                    Location = new Point(
                        60 + cellWidth * i + 5,
                        5),
                    Parent = panel,
                    TextAlign = ContentAlignment.TopLeft
                };
                i++;
            }
        }

        private void DrawRoomsAsTopRow(Panel panel, int cellWidth)
        {
            int i = 0;
            foreach (Room r in roomList)
            {
                Label l = new Label()
                {
                    Text = r.Name,
                    Width = cellWidth,
                    Height = 20,
                    Location = new Point(
                        60 + cellWidth * i + 5,
                        5),
                    Parent = panel,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = r.RoomColor,
                    ForeColor = ComplementColor(r.RoomColor)
                };
                i++;
            }
        }

        private void DrawTimeSlotsAsLeftColumn(Panel panel, int cellHight)
        {
            int prev = -1000;
            for (int j = 0; j < m_enumTimeSlot.Length; j++)
            {
                int y = 40 + cellHight * j + 5;
                if (y - prev < 100)
                    continue;
                prev = y;
                Label l = new Label()
                {
                    Text = m_enumTimeSlot[j],
                    Width = 50,
                    Height = 20,
                    Location = new Point(5, y),
                    Parent = panel,
                    TextAlign = ContentAlignment.MiddleLeft
                };
            }
        }

        private void ExpandLabels(Panel panel, 
            int minX, int minY, 
            int cols, int rows,
            int cellRoomWidth,  int nRooms,
            int cellWidth, int cellHeight)
        {
            int ncols = (cols + 1) * nRooms + 1;
            int nrows = rows;
            int[,] hits = new int[ncols, nrows];
            foreach (Control c in panel.Controls)
            {
                Label l = c as Label;
                if (l == null)
                    continue;
                if (l.Location.Y < minY || l.Location.X < minX)
                    continue;

                int x1 = (l.Location.X - minX) / cellRoomWidth;
                int y1 = (l.Location.Y - minY) / cellHeight;
                int x2 = (l.Location.X + l.Width - minX) / cellRoomWidth;
                int y2 = (l.Location.Y + l.Height - minY) / cellHeight;

                if (x1 < 0 || x2 < 0 || x1 > ncols || x2 > ncols ||
                    y1 < 0 || y2 < 0 || y1 > nrows || y2 > nrows)
                    continue; 

                for (int x = x1; x < x2; x++)
                    for (int y = y1; y < y2; y++)
                        hits[x, y] = hits[x, y] + 1;
            }

            foreach (Control c in panel.Controls)
            {
                Label l = c as Label;
                if (l == null)
                    continue;
                if (l.Location.Y < minY || l.Location.X < minX)
                    continue;

                int x1 = (l.Location.X - minX) / cellRoomWidth;
                int y1 = (l.Location.Y - minY) / cellHeight;
                int x2 = (l.Location.X + l.Width - minX) / cellRoomWidth;
                int y2 = (l.Location.Y + l.Height - minY) / cellHeight;

                if (x1 < 0 || x2 < 0 || x1 > ncols || x2 > ncols ||
                    y1 < 0 || y2 < 0 || y1 > nrows || y2 > nrows)
                    continue;

                int x2min = (x2 / nRooms) * nRooms;
                int x2max = x2min + nRooms;

                bool hit = false;
                for (int x = x2; x < x2max && !hit; x++)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        if (x + 1 < 0 || y < 0 || x + 1 >= ncols || y >= nrows)
                            return;
                        if (hits[x + 1, y] > 0)
                        {
                            hit = true;
                            break;
                        }
                    }
                    if (hit)
                        break;
                    l.Width += cellRoomWidth;
                    for (int y = y1; y < y2; y++)
                    {
                        if (x + 1 < 0 || y < 0 || x + 1 >= ncols || y >= nrows)
                            return;
                        hits[x + 1, y] = 1;
                    }
                }
            }
        }

        private void menuItemViewLessonCancel_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender(sender);
            l.State = "Cancelled";
            ShowView();
        }

        private void MoveLesson(object sender, int days)
        {
            InitializePlan(false);
            Lesson l = GetLessonFromSender(sender);
            m_plan_chosenLanguage = GetStudentLearningLanguage(l.Student1);

            dtpPlan.Value = l.DateTimeStart.AddDays(days);
            cbPlanDuration.SelectedIndex = l.SlotsNumber;
            SetComboBoxIndexByValue(cbPlanLanguage, m_plan_chosenLanguage);
            SetComboBoxIndexByValue(cbPlanTeacher, l.Teacher1);
            SetComboBoxIndexByValue(cbPlanStud1, l.Student1);

            PopulateTeacherVacation(l.Teacher1, lbPlanTeachVacation);
            PopulateStudentPossibleSchedule(l.Student1, lbPlanStudSchedule1);
            m_lessonInMove = l;
            tabControlOps.SelectedIndex = (int)TabControlOps.Plan;
            PlanShowDataIfReady();
        }

        private void menuItemViewLessonMove0_Click(object sender, EventArgs e)
        {
            MoveLesson(sender, 1);
        }

        private void menuItemViewLessonMove1_Click(object sender, EventArgs e)
        {
            MoveLesson(sender, 7);
        }

        private void menuItemViewLessonMove2_Click(object sender, EventArgs e)
        {
            MoveLesson(sender, 14);
        }

        private void menuItemViewLessonDone_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender(sender);
            l.State = "Done";
            ShowView();
        }

        private void menuItemViewLessonPlanned_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender(sender);
            l.State = "Planned";
            ShowView();
        }

        private Lesson GetLessonFromSender(object sender)
        {
            Control c = ((ContextMenuStrip)((ToolStripItem)sender).Owner).SourceControl;
            Label lb = c as Label;
            Button bt = c as Button;
            Lesson l = (lb == null ? bt.Tag : lb.Tag) as Lesson;
            return l;
        }

    }
}