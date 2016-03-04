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

    public struct ViewContext
    {
        public int days;
        public int cellWidth;
        public int cellHight;
        public int cellRoomWidth;
        public int minX;
        public int minY;
        public int labelHeight;

        public ViewContext(
            int days, 
            int cellWidth, 
            int cellHight, 
            int cellRoomWidth,
            int minX,
            int minY,
            int labelHeight)
        {
            this.days = days;
            this.cellWidth = cellWidth;
            this.cellHight = cellHight;
            this.cellRoomWidth = cellRoomWidth;
            this.minX = minX;
            this.minY = minY;
            this.labelHeight = labelHeight;
        }
    }

    public class LabelsExpander
    {
        private Panel panel;
        ViewContext vc;
        private int cols;
        private int rows;
        private int cellRoomWidth;
        private int nRooms;
        private int cellWidth;
        private int cellHeight;

        private int ncols;
        private int nrows;
        private int[,] hits;

        private int x1;
        private int y1;
        private int x2;
        private int y2;
        private int x1min;
        private int x1max;
        private int x2min;
        private int x2max;

        public LabelsExpander(
            Panel panel,
            ViewContext vc,
            int cols,
            int rows,
            int cellRoomWidth,
            int nRooms,
            int cellWidth,
            int cellHeight)
        {
            this.panel = panel;
            this.vc = vc;
            this.cols = cols;
            this.rows = rows;
            this.cellRoomWidth = cellRoomWidth;
            this.nRooms = nRooms;
            this.cellWidth = cellWidth;
            this.cellHeight = cellHeight;

            ncols = (cols + 1) * nRooms + 1;
            nrows = rows;
            hits = new int[ncols, nrows];
        }

        public static void Expand(
            Panel panel,
            ViewContext vc,
            int cols,
            int rows,
            int cellRoomWidth,
            int nRooms,
            int cellWidth,
            int cellHeight)
        {
            LabelsExpander exp = new LabelsExpander
                (panel, vc, cols, rows, 
                 cellRoomWidth,  nRooms, cellWidth, cellHeight);

            //exp.CountHits();
            //exp.ExpandRight();
            //exp.ExpandLeft();
            exp.EqualizeWidths(vc);
        }

        private Label GetLabel(Control c)
        {
            Label l = c as Label;
            if (l == null)
                return null;
            if (l.Location.Y < vc.minY || l.Location.X < vc.minX)
                return null;

            x1 = (l.Location.X - vc.minX) / cellRoomWidth;
            y1 = (l.Location.Y - vc.minY) / cellHeight;
            x2 = (l.Location.X + l.Width - vc.minX) / cellRoomWidth - 1;
            y2 = (l.Location.Y + l.Height - vc.minY) / cellHeight - 1;

            if (x1 < 0 || x2 < 0 || x1 > ncols || x2 > ncols ||
                y1 < 0 || y2 < 0 || y1 > nrows || y2 > nrows)
                return null;

            x1min = x1 - (x1 % nRooms);  
            x1max = x1min + nRooms;

            x2min = x2 - (x2 % nRooms);  
            x2max = x2min + nRooms;


            return l;
        }

        private void CountHits()
        {
            foreach (Control c in panel.Controls)
            {
                Label l = GetLabel(c);
                if (l == null)
                    continue;

                for (int x = x1; x <= x2; x++)
                    for (int y = y1; y <= y2; y++)
                        hits[x, y] = hits[x, y] + 1;
            }
        }
        private void ExpandRight()
        {
            foreach (Control c in panel.Controls)
            {
                Label l = GetLabel(c);
                if (l == null)
                    continue;

                bool hit = false;
                for (int x = x2; x < x2max && !hit; x++)
                {
                    for (int y = y1; y < y2; y++)
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
        private void ExpandLeft()
        {
            foreach (Control c in panel.Controls)
            {
                Label l = GetLabel(c);
                if (l == null)
                    continue;

                bool hit = false;
                for (int x = x1; x > x1min && !hit; x--)
                {
                    for (int y = y1; y < y2; y++)
                    {
                        if (x - 1 < 0 || y < 0 || x - 1 >= ncols || y >= nrows)
                            return;
                        if (hits[x - 1, y] > 0)
                        {
                            hit = true;
                            break;
                        }
                    }
                    if (hit)
                        break;
                    l.Width += cellRoomWidth;
                    l.Location = new Point(l.Location.X - cellRoomWidth, l.Location.Y);
                    for (int y = y1; y < y2; y++)
                    {
                        if (x - 1 < 0 || y < 0 || x - 1 >= ncols || y >= nrows)
                            return;
                        hits[x - 1, y] = 1;
                    }
                }
            }
        }

        private void EqualizeWidths(ViewContext vc)
        {
            int[] labelsPerRow = new int[rows];
            HashSet<Control> moved = new HashSet<Control>();

            // Eah column (day) is totally separate
            for (int col = cols-1; col >= 0; col--)
            {
                for (int row = 0; row < rows; row++)
                {
                    int nLabels = 0;
                    foreach (Control c in panel.Controls)
                    {
                        Label l = GetLabel(c);
                        if (l == null)
                            continue;

                        if ((x1-1) / nRooms > col || (x2-1) / nRooms < col || y1 > row || y2 < row)
                            continue;

                        nLabels++;
                    }
                    labelsPerRow[row] = nLabels;
                }

                int maxInColumn = labelsPerRow.Max();
                if (maxInColumn == 0)
                    continue;
                int newWidth = cellWidth / maxInColumn;

                int[] used = new int[nrows];
                foreach (Control c in panel.Controls)
                {
                    Label l = GetLabel(c);
                    if (l == null)
                        continue;
                    if ( x1 / nRooms > col || x2 / nRooms < col)
                        continue;
                    if (moved.Contains(c))
                        continue;
                    moved.Add(c);

                    int newX = 0;
                    for (int yy = y1; yy < y2; yy++)
                    {
                        if (used[yy] > newX)
                            newX = used[yy];
                    }
                    newX++;
                    for (int yy = y1; yy < y2; yy++)
                        used[yy] = newX;

                    l.Width = newWidth;
                    l.Location = new Point(
                        vc.minX + cellWidth * col + (newX-1) * newWidth, 
                        l.Location.Y);
                }
            }
        }
    }



    public partial class FormGlob : Form
    {
        DateTime m_chosenDate = DateTime.Today;

        string m_view_chosen_state = "";
        string m_view_chosen_student = "";
        string m_view_chosen_teacher = "";
        string m_view_chosen_room = "";
        string m_view_chosen_program = "";

        bool m_view_selection_mode = false;
        Dictionary<int, Lesson> m_dvgViewTags = new Dictionary<int, Lesson>();
        Lesson m_slotLessonFromRightClick = null;
        const int lessonLabelVertMargin = 20;

        public void ViewShowMonth()
        {
            DisposeChildren(panelViewMonth);
            int days = DaysInMonth(m_chosenDate);
            int fullWidth = panelViewMonth.Width - 
                butViewNext.Width - butViewPrev.Width - 10;
            int fullheight = panelViewMonth.Height -
                butViewZoomIn.Height - butViewZoomOut.Height - 10;

            ViewContext vc = new ViewContext(
                days,
                fullWidth / days,
                fullheight / m_enumTimeSlot.Length,
                fullWidth / days / roomList.Count,
                60, 40, 20);

            DrawMonthDaysAsTopRow(m_chosenDate, panelViewMonth, vc);
            DrawTimeSlotsAsLeftColumn(panelViewMonth, vc);
            foreach (Lesson l in FindLessonsForView())
            {
                int x, ys, ye;
                l.GetLocationInMonth(out x, out ys, out ye);

                Label lb = new Label()
                {
                    Text = l.State.Substring(0, 1),
                    Width = vc.cellRoomWidth,
                    Height = vc.cellHight * (ye - ys + 1) - lessonLabelVertMargin,
                    Location = new Point(
                        vc.minX + vc.cellWidth * x + vc.cellRoomWidth * RoomIndex(l.Room),
                        vc.minY + vc.cellHight * ys + lessonLabelVertMargin/2),
                    Parent = panelViewMonth,
                    BackColor = LessonStateBackColor(l.State),
                    ForeColor = LessonStateForeColor(l.State),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = l
                };
                lb.ContextMenuStrip = ctxMenuLesson;
                lb.MouseHover += new System.EventHandler(this.butViewShowLesson_MouseHover);
                lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butViewShowLesson_MouseDown);
            }

            LabelsExpander.Expand(
                panelViewMonth, 
                vc,
                days, 
                m_enumTimeSlot.Length,
                vc.cellRoomWidth, 
                roomList.Count,
                vc.cellWidth,
                vc.cellHight);
        }

        public void ViewShowWeek()
        {
            DisposeChildren(panelViewWeek);
            int fullWidth = panelViewWeek.Width -
               butViewNext.Width - butViewPrev.Width - 10;
            int fullheight = panelViewWeek.Height -
                butViewZoomIn.Height- butViewZoomOut.Height - 10;

            ViewContext vc = new ViewContext(
                m_enumWeekdayNames.Length,
                fullWidth / m_enumWeekdayNames.Length,
                fullheight / m_enumTimeSlot.Length,
                fullWidth / m_enumWeekdayNames.Length / roomList.Count,
                60, 40, 20);

            DrawWeekDaysAsTopRow(m_chosenDate, panelViewWeek, vc);
            DrawTimeSlotsAsLeftColumn(panelViewWeek, vc);
            foreach (Lesson l in FindLessonsForView())
            {
                int x, ys, ye;
                l.GetLocationInWeek(out x, out ys, out ye);

                Label lb = new Label()
                {
                    Text = l.ShortDescription,
                    Width = vc.cellRoomWidth,
                    Height = vc.cellHight * (ye - ys + 1) - lessonLabelVertMargin,
                    Location = new Point(
                        vc.minX + vc.cellWidth * x + vc.cellRoomWidth * RoomIndex(l.Room),
                        vc.minY + vc.cellHight * ys + lessonLabelVertMargin/2),
                    Parent = panelViewWeek,
                    BackColor = LessonStateBackColor(l.State),
                    ForeColor = LessonStateForeColor(l.State),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Tag = l
                };
                lb.ContextMenuStrip = ctxMenuLesson;
                lb.MouseHover += new System.EventHandler(this.butViewShowLesson_MouseHover);
                lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butViewShowLesson_MouseDown);
            }

            LabelsExpander.Expand(
                panelViewWeek,
                vc,
                7, 
                m_enumTimeSlot.Length,
                vc.cellRoomWidth, 
                roomList.Count,
                vc.cellWidth, 
                vc.cellHight);
        }

        public void ViewShowDay()
        {
            if (roomList.Count == 0)
                return;
            DisposeChildren(panelViewDay);

            int fullWidth = panelViewDay.Width -
                   butViewNext.Width - butViewPrev.Width - 10;
            int fullheight = panelViewDay.Height -
                    butViewZoomIn.Height - butViewZoomOut.Height - 10;

            ViewContext vc = new ViewContext(
                1,
                fullWidth,
                (fullheight - 20) / m_enumTimeSlot.Length,
                (fullWidth - 60) / roomList.Count,
                60, 40, 20);


            DrawRoomsAsTopRow(panelViewDay, vc);
            DrawTimeSlotsAsLeftColumn(panelViewDay, vc);
            foreach (Lesson l in FindLessonsForView())
            {
                int x, ys, ye;
                Color roomColor;
                GetLocationInRooms(l, out x, out ys, out ye, out roomColor);

                Label lb = new Label()
                {
                    Text = l.Description,
                    Width = vc.cellRoomWidth - 10,
                    Height = vc.cellHight * (ye - ys + 1) - lessonLabelVertMargin,
                    Location = new Point(
                        vc.minX + vc.cellRoomWidth * x + 5, 
                        vc.minY + vc.cellHight * ys + lessonLabelVertMargin/2),
                    Parent = panelViewDay,
                    BackColor = LessonStateBackColor(l.State),
                    ForeColor = LessonStateForeColor(l.State),
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = l
                };
                lb.ContextMenuStrip = ctxMenuLesson;
                lb.MouseHover += new System.EventHandler(this.butViewShowLesson_MouseHover);
                lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butViewShowLesson_MouseDown);
            }
            panelViewDay.Refresh();
        }
        public void ViewShowSlots()
        {
            viewSlotList.Clear();
            m_dvgViewTags.Clear();
            for (int i = 0; i < m_enumTimeSlot.Length; i++)
                viewSlotList.Add(new RecordKeeper.ViewSlot(m_enumTimeSlot[i]));

            DateTime dts = WorkDayStart(m_chosenDate);
            foreach (Lesson l in FindLessonsForView())
            {
                int slot1Index = FormGlob.Slots(dts, l.DateTimeStart);
                if (slot1Index < 0 || slot1Index >= m_enumTimeSlot.Length)
                    continue;

                int slot2Index = FormGlob.Slots(dts, l.DateTimeEnd);
                if (slot2Index < 0 || slot2Index >= m_enumTimeSlot.Length)
                    continue;

                for (int slotIndex = slot1Index; slotIndex < slot2Index; slotIndex++)
                {
                    string text = GetSlotText(l, slotIndex - slot1Index);
                    ViewSlot slot = new RecordKeeper.ViewSlot(m_enumTimeSlot[slotIndex]);
                    int roomIndex = slot.SetViewSlot(l.Room, text);
                    if (roomIndex < 0 || roomIndex > 7)
                        continue;

                    viewSlotList.Add(slot);
                    Color c1 = LessonStateBackColor(l.State);
                    Color c2 = LessonStateForeColor(l.State);

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
                    dt1 = MonthStart(m_chosenDate);
                    dt2 = MonthEnd(m_chosenDate);
                    break;
                case TabScales.Week:
                    dt1 = WeekStart(m_chosenDate);
                    dt2 = WeekEnd(m_chosenDate);
                    break;
                case TabScales.Day:
                    dt1 = DayStart(m_chosenDate);
                    dt2 = DayEnd(m_chosenDate);
                    break;
                case TabScales.Slot:
                    dt1 = DayStart(m_chosenDate);
                    dt2 = DayEnd(m_chosenDate);
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

        private Color LessonStateBackColor(string state)
        {
            switch (state)
            {
                case "Done":   
                    return StateColors[(int)StatusColors.Irrelevant];
                case "Planned":   
                    return StateColors[(int)StatusColors.Good];
                case "Cancelled":   
                    return StateColors[(int)StatusColors.Bad];
                default:
                    return StateColors[(int)StatusColors.Unknown];
            }
        }

        private Color LessonStateForeColor(string state)
        {
            switch (state)
            {
                case "Done":   
                    return StateForeColors[(int)StatusColors.Irrelevant];
                case "Planned":  
                    return StateForeColors[(int)StatusColors.Good];
                case "Cancelled":
                    return StateForeColors[(int)StatusColors.Bad];
                default:
                    return StateForeColors[(int)StatusColors.Unknown];
            }
        }

        private void FollowFocusedDay()
        {
            DateTime dt;
            if (lbViewGbDate.Text != null && lbViewGbDate.Text.Length > 0 &&
                DateTime.TryParse(lbViewGbDate.Text, out dt))
            {
                dtpViewSlot.Value = dt;
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
            cbViewSelectProgram.Items.Add("");
            cbViewSelectProgram.Items.AddRange(programs.Keys.OrderBy(q => q).ToArray());

            cbViewSelectRoom.Items.Clear();
            cbViewSelectRoom.Items.Add("");
            cbViewSelectRoom.Items.AddRange(rooms.Keys.OrderBy(q => q).ToArray());

            cbViewSelectStudent.Items.Clear();
            cbViewSelectStudent.Items.Add("");
            cbViewSelectStudent.Items.AddRange(students.Keys.OrderBy(q => q).ToArray());

            cbViewSelectTeacher.Items.Clear();
            cbViewSelectTeacher.Items.Add("");
            cbViewSelectTeacher.Items.AddRange(teachers.Keys.OrderBy(q => q).ToArray());
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
            lbViewGbRoom.BackColor = RoomColor(l.Room);
            lbViewGbRoom.ForeColor = ComplementColor(RoomColor(l.Room));
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
                    color = LessonStateBackColor(l.State);
                }
                i++; 
            }

            if (col == -1)
                col = roomList.Count - 1;
        }

        private void DrawWeekDaysAsTopRow(DateTime dt, Panel panel, ViewContext vc)
        {
            int i = 0;
            foreach (string d in WeekOf(dt))
            {
                Label l = new Label()
                {
                    Text = d,
                    ForeColor = Color.Black,
                    BackColor = Color.White,
                    Width = vc.cellWidth,
                    Height = vc.labelHeight,
                    Location = new Point(
                        vc.minX + vc.cellWidth * i + 5,
                        5),
                    Parent = panel,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                i++;
            }
        }

        private void DrawMonthDaysAsTopRow(DateTime dt, Panel panel, ViewContext vc)
        {
            int i = 0;
            foreach (string d in MonthOf(dt))
            {
                Label l = new Label()
                {
                    Text = d,
                    ForeColor = Color.Black,
                    BackColor = Color.White,
                    Width = vc.cellWidth,
                    Height = vc.labelHeight,
                    Location = new Point(
                        vc.minX + vc.cellWidth * i + 5,
                        5),
                    Parent = panel,
                    TextAlign = ContentAlignment.TopLeft
                };
                i++;
            }
        }

        private void DrawRoomsAsTopRow(Panel panel, ViewContext vc)
        {
            int i = 0;
            foreach (Room r in roomList)
            {
                Label l = new Label()
                {
                    Text = r.Name,
                    Width = vc.cellRoomWidth,
                    Height = vc.labelHeight,
                    Location = new Point(
                        vc.minX + vc.cellRoomWidth * i + 5,
                        5),
                    Parent = panel,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = r.RoomColor,
                    ForeColor = ComplementColor(r.RoomColor)
                };
                i++;
            }
        }

        private void DrawTimeSlotsAsLeftColumn(Panel panel, ViewContext vc)
        {
            int prev = -1000;
            for (int j = 0; j < m_enumTimeSlot.Length; j++)
            {
                int y = vc.minY + vc.cellHight * j + 5;
                if (y - prev < 100)
                    continue;
                prev = y;
                Label l = new Label()
                {
                    Text = m_enumTimeSlot[j],
                    Width = 50,
                    Height = vc.labelHeight,
                    Location = new Point(5, y),
                    Parent = panel,
                    TextAlign = ContentAlignment.MiddleLeft
                };
            }
        }

        private void MoveLesson(object sender, int days)
        {
            InitializeMove();
            Lesson l = GetLessonFromSender(sender);

            dtpPlan.Value = l.DateTimeStart.AddDays(days);
            //cbPlanDuration.SelectedIndex = l.SlotsNumber;
            tbPlanComment.Text = l.Comments;
            SetComboByValue(cbPlanProgram, l.Program);
            SetComboByValue(cbPlanDuration, l.DurationString);
            SetComboByValue(cbPlanTeacher, l.Teacher1);
            SetComboByValue(cbPlanStud1, l.Student1);

            PopulateTeacherVacation(l.Teacher1, lbPlanTeachVacation);
            PopulateStudentPossibleSchedule(l.Student1, lbPlanStudSchedule1);
            if (!IsStringEmpty(l.Student2))
                PopulateStudentPossibleSchedule(l.Student2, lbPlanStudSchedule2);
            if (!IsStringEmpty(l.Student3))
                PopulateStudentPossibleSchedule(l.Student3, lbPlanStudSchedule3);
            if (!IsStringEmpty(l.Student4))
                PopulateStudentPossibleSchedule(l.Student4, lbPlanStudSchedule4);

            m_lessonInMove = l;
            tabControlOps.SelectedIndex = (int)TabControlOps.Plan;
            PlanShowDataIfReady();
        }

        private void MoveLesson(Lesson l, int days)
        {
            InitializeMove();
            dtpPlan.Value = l.DateTimeStart.AddDays(days);
            //cbPlanDuration.SelectedIndex = l.SlotsNumber;
            tbPlanComment.Text = l.Comments;
            SetComboByValue(cbPlanProgram, l.Program);
            SetComboByValue(cbPlanDuration, l.DurationString);
            SetComboByValue(cbPlanTeacher, l.Teacher1);
            SetComboByValue(cbPlanStud1, l.Student1);

            PopulateTeacherVacation(l.Teacher1, lbPlanTeachVacation);
            PopulateStudentPossibleSchedule(l.Student1, lbPlanStudSchedule1);
            if (!IsStringEmpty(l.Student2))
                PopulateStudentPossibleSchedule(l.Student2, lbPlanStudSchedule2);
            if (!IsStringEmpty(l.Student3))
                PopulateStudentPossibleSchedule(l.Student3, lbPlanStudSchedule3);
            if (!IsStringEmpty(l.Student4))
                PopulateStudentPossibleSchedule(l.Student4, lbPlanStudSchedule4);

            m_lessonInMove = l;
            tabControlOps.SelectedIndex = (int)TabControlOps.Plan;
            PlanShowDataIfReady();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Red";
            Modified = true;
            ShowView();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Green";
            Modified = true;
            ShowView();
        }

        private void tealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Teal";
            Modified = true;
            ShowView();
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Yellow";
            Modified = true;
            ShowView();
        }
        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.Room = "White";
            Modified = true;
            ShowView();
        }
        private void pinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.Room = "Pink";
            Modified = true;
            ShowView();
        }

        private void nAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.Room = "N/A";
            Modified = true;
            ShowView();
        }

        private void sameWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, 0);
        }

        private void weekForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, 7);
        }

        private void twoWeeksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, 14);
        }

        private void weekBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, -7);
        }

        private void twoWeeksBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            MoveLesson(l, -14);
        }


        private void plannedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.State = "Planned";
            Modified = true;
            ShowView();
        }

        private void doneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.State = "Done";
            Modified = true;
            ShowView();
        }

        private void cancelledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lesson l = GetLessonFromSender2(sender);
            l.State = "Cancelled";
            l.CancellationTime = DateTime.Now.ToString();
            Modified = true;
            ShowView();
        }

        private void weeklyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;

            using (RepeatForm rf = new RepeatForm())
            {
                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    Lesson l = GetLessonFromSender2(sender);
                    RepeatLesson(l, 
                        RepeatMode.Weekly, 
                        rf.UpToTheEOY, 
                        rf.Repeats);
                    Modified = true;
                    ShowView();
                }
            }
        }

        private void biweeklyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;

            using (RepeatForm rf = new RepeatForm())
            {
                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    Lesson l = GetLessonFromSender2(sender);
                    RepeatLesson(l,
                            RepeatMode.Biweekly,
                            rf.UpToTheEOY,
                            rf.Repeats);
                    Modified = true;
                    ShowView();
                }
            }
        }

        private void monthlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;

            using (RepeatForm rf = new RepeatForm())
            {
                if (rf.ShowDialog(this) == DialogResult.OK)
                {
                    Lesson l = GetLessonFromSender2(sender);
                    RepeatLesson(l,
                            RepeatMode.Monthly,
                            rf.UpToTheEOY,
                            rf.Repeats);

                    Modified = true;
                    ShowView();
                }
            }
        }

        private void deleteFuturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckSafety())
                return;

            Lesson l = GetLessonFromSender2(sender);
            DeleteFutures(l);
            Modified = true;
            ShowView();
        }

        private Lesson GetLessonFromSender(object sender)
        {
            Control c = ((ContextMenuStrip)((ToolStripItem)sender).Owner).SourceControl;
            Label lb = c as Label;
            if (lb != null)
                return lb.Tag as Lesson;

            Button bt = c as Button;
            if (bt != null)
                return bt.Tag as Lesson;

            DataGridView dgv = c as DataGridView;
            if (dgv != null)
                return m_slotLessonFromRightClick;

            return null;
        }

        private Lesson GetLessonFromSender2(object sender)
        {
            Control c = m_rightClickedControl;

            Label lb = c as Label;
            if (lb != null)
                return lb.Tag as Lesson;

            Button bt = c as Button;
            if (bt != null)
                return bt.Tag as Lesson;

            DataGridView dgv = c as DataGridView;
            if (dgv != null)
                return m_slotLessonFromRightClick;

            return null;
        }

    }
}