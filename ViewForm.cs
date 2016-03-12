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
        public int cellWidth;
        public int cellHight;
        public int cellRoomWidth;
        public int minX;
        public int minY;
        public int labelHeight;
        public int elements;
        public int elemcols;
        public int elemrows;
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public int x2max;
        public int cols;
        public int rows;
        public HashSet<Label> movedAlready; 

        public ViewContext(
            int cellWidth,
            int cellHight,
            int cellRoomWidth,
            int elements,
            int rows,
            int cols,
            int minX,
            int minY,
            int labelHeight)
        {
            this.cellWidth = cellWidth;
            this.cellHight = cellHight;
            this.cellRoomWidth = cellRoomWidth;
            this.cols = cols;
            this.rows = rows;
            this.minX = minX;
            this.minY = minY;
            this.labelHeight = labelHeight;
            this.elements = elements;
            this.elemcols = 0;
            this.elemrows = 0;
            this.x1 = 0;
            this.y1 = 0;
            this.x2 = 0;
            this.y2 = 0;
            this.x2max = 0;
            this.movedAlready = new HashSet<Label>();
        }
    }

    public class Packer
    {
        private int m_rows;
        private int m_cols;
        private bool[,] m_used; 
        public Packer(int rows, int cols)
        {
            m_rows = rows;
            m_cols = cols;
            m_used = new bool[rows, cols];
        }

        private bool Check(int r1, int r2, int c)
        {
            for (int r = r1; r <= r2; r++)
            {
                if (m_used[r, c])
                    return false;
            }
            return true;
        }
        private void Use(int r1, int r2, int c)
        {
            for (int r = r1; r <= r2; r++)
                m_used[r, c] = true;
        }

        public int Place(int r1, int r2)
        {
            // We need to respect r1, r2, and choose only c
            for (int c=0; c < m_cols; c++)
            {
                if (Check(r1, r2, c))
                {
                    Use(r1, r2, c);
                    return c;
                }
            }
            return -1;
        }
    }

    public partial class FormGlob : Form
    {
        DateTime m_chosenDate = DateTime.Today;
        DateTime m_viewMinDate = DateTime.Today;
        DateTime m_viewMaxDate = DateTime.Today;

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

            m_viewMinDate = new DateTime(m_chosenDate.Year, m_chosenDate.Month, 1, 0, 0, 0);
            m_viewMaxDate = new DateTime(m_chosenDate.Year, m_chosenDate.Month, days, 23, 59, 59);

            ViewContext vc = new ViewContext(
                fullWidth / days,
                fullheight / m_enumTimeSlot.Length,
                fullWidth / days / roomList.Count,
                roomList.Count,
                m_enumTimeSlot.Length,
                days, 
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
                        vc.minY + vc.cellHight * ys + lessonLabelVertMargin / 2),
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
                lb.DoubleClick += new System.EventHandler(this.butViewShowLesson_DoubleClick);
            }

            PackRectangles(panelViewMonth, vc);
            ExpandRight(panelViewMonth, vc);
            MarkAllCollisions(panelViewMonth);
        }

        public void ViewShowWeek()
        {
            DisposeChildren(panelViewWeek);
            int fullWidth = panelViewWeek.Width -
                butViewNext.Width - butViewPrev.Width - 10;
            int fullheight = panelViewWeek.Height -
                butViewZoomIn.Height - butViewZoomOut.Height - 10;

            m_viewMinDate = WeekStart(m_chosenDate);
            m_viewMaxDate = WeekEnd(m_chosenDate);

            ViewContext vc = new ViewContext(
                fullWidth / m_enumWeekdayNames.Length,
                fullheight / m_enumTimeSlot.Length,
                fullWidth / m_enumWeekdayNames.Length / roomList.Count,
                roomList.Count,
                m_enumTimeSlot.Length,
                7,
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
                        vc.minY + vc.cellHight * ys + lessonLabelVertMargin / 2),
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
                lb.DoubleClick += new System.EventHandler(this.butViewShowLesson_DoubleClick);
            }

            PackRectangles(panelViewWeek, vc);
            ExpandRight(panelViewWeek, vc);
            MarkAllCollisions(panelViewWeek);
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

            m_viewMinDate = new DateTime(m_chosenDate.Year, m_chosenDate.Month, m_chosenDate.Day, 0, 0, 0);
            m_viewMaxDate = new DateTime(m_chosenDate.Year, m_chosenDate.Month, m_chosenDate.Day, 23, 59, 59);

            ViewContext vc = new ViewContext(
                fullWidth,
                (fullheight - 20) / m_enumTimeSlot.Length,
                (fullWidth - 60) / roomList.Count,
                roomList.Count,
                m_enumTimeSlot.Length,
                roomList.Count,
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
                        vc.minY + vc.cellHight * ys + lessonLabelVertMargin / 2),
                    Parent = panelViewDay,
                    BackColor = LessonStateBackColor(l.State),
                    ForeColor = LessonStateForeColor(l.State),
                    BorderStyle = BorderStyle.FixedSingle,
                    Tag = l
                };

                MarkCollisions(lb, panelViewDay);

                lb.ContextMenuStrip = ctxMenuLesson;
                lb.MouseHover += new System.EventHandler(this.butViewShowLesson_MouseHover);
                lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butViewShowLesson_MouseDown);
                lb.DoubleClick += new System.EventHandler(this.butViewShowLesson_DoubleClick);
            }
            panelViewDay.Refresh();
        }
        public void ViewShowSlots()
        {

            m_viewMinDate = new DateTime(m_chosenDate.Year, m_chosenDate.Month, m_chosenDate.Day, 0, 0, 0);
            m_viewMaxDate = new DateTime(m_chosenDate.Year, m_chosenDate.Month, m_chosenDate.Day, 23, 59, 59);

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
                dtpViewChosen.Value = dt;
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

            m_chosenDate = l.DateTimeStart;
            dtpViewChosen.Value = m_chosenDate;
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
            ChangeOperMode(Ops.Plan);
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
            ChangeOperMode(Ops.Plan);
            PlanShowDataIfReady();
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

        private void MarkAllCollisions(Panel panel)
        {
            foreach (Control c in panel.Controls)
            {
                Label lb = c as Label;
                if (lb == null)
                    continue;
                Lesson lsn = lb.Tag as Lesson;
                if (lsn == null)
                    continue;

                MarkCollisions(lb, panel);

            }
        }

        int IndexOnStringArray(string[] arr, string word)
        {
            for (int i=0; i < arr.Length; i++)
            {
                if (word == arr[i])
                    return i;
            }
            return -1;
        }

        private Label GetLabel(Control c, ref ViewContext vc)
        {
            Label l = c as Label;
            if (l == null)
                return null;
            if (l.Location.Y < vc.minY || l.Location.X < vc.minX)
                return null;
            Lesson lsn = l.Tag as Lesson;
            if (lsn == null)
                return null;

            //vc.y1 = IndexOnStringArray(m_enumTimeSlot, lsn.Start);
            //vc.y2 = IndexOnStringArray(m_enumTimeSlot, lsn.End);

            int roomIndex = RoomIndex(lsn.Room);
            vc.x1 = (l.Location.X - vc.minX - roomIndex * vc.cellRoomWidth)           / vc.cellRoomWidth;
            vc.x2 = (l.Location.X - vc.minX - roomIndex * vc.cellRoomWidth + l.Width) / vc.cellRoomWidth - 1;
            vc.x2max = (l.Location.X - vc.minX - (roomList.Count - 1) * vc.cellRoomWidth + l.Width) / vc.cellRoomWidth - 1;
            vc.y1 = (l.Location.Y - vc.minY - vc.labelHeight / 2)            / vc.cellHight;
            vc.y2 = (l.Location.Y - vc.minY - vc.labelHeight / 2 + l.Height) / vc.cellHight - 1;

            if (vc.x1 < 0 || vc.x2 < 0 || vc.x1 > vc.elemcols || vc.x2 > vc.elemcols ||
                vc.y1 < 0 || vc.y2 < 0 || vc.y1 > vc.elemrows || vc.y2 > vc.elemrows)
                return null;

            return l;
        }

        private int EstimateChannels(List<Label> labels, ViewContext vc, int col)
        {
            int[] perRow = new int[m_enumTimeSlot.Count() + 1];

            foreach (Label l in labels)
            {
                GetLabel(l, ref vc);
                for (int y = vc.y1; y <= vc.y2; y++)
                    perRow[y]++;
            }

            return perRow.Max() + 1;
        }
        private List<Label> CollectColumnLabels(Panel panel, ViewContext vc, int col)
        {
            List<Label> labels = new List<Label>();
            foreach (Control c in panel.Controls)
            {
                Label l = GetLabel(c, ref vc);
                if (l == null)
                    continue;
                if (vc.x1 / vc.elements > col || vc.x2 / vc.elements < col)
                    continue;
                if (vc.movedAlready.Contains(l))
                    continue;
                labels.Add(l);
            }

            labels.Sort((l1, l2) => l1.Height.CompareTo(l2.Height));
            return labels;
        }
        private void PackRectangles(Panel panel, ViewContext vc)
        {
            vc.elemcols = (vc.cols + 1) * vc.elements + 1;
            vc.elemrows = vc.rows;

            for (int col = vc.cols - 1; col >= 0; col--)
            {
                List<Label> labels = CollectColumnLabels(panel, vc, col);
                if (labels.Count == 0)
                    continue;

                int estChannels = EstimateChannels(labels, vc, col);
                if (estChannels == 0)
                    continue;

                for (int channels = estChannels; channels <= vc.elements; channels++)
                {
                    int channelWidth = vc.cellWidth / channels - 1;
                    Packer packer = new Packer(vc.elemrows, vc.elements);

                    bool failed = false;
                    foreach (Label l in labels)
                    {
                        GetLabel(l, ref vc);
                        int c = packer.Place(vc.y1, vc.y2);
                        if (c < 0)
                        {
                            failed = true;
                            break;
                        }

                        l.Width = channelWidth;
                        l.Location = new Point(
                            vc.minX + vc.cellWidth * col + c * channelWidth,
                            l.Location.Y);
                    }
                    if (!failed)
                        break;
                }

                foreach (Label l in labels)
                    vc.movedAlready.Add(l);
            }
        }

        private List<Label> CollectColumnLabels2(Panel panel, ViewContext vc, int col)
        {
            List<Label> labels = new List<Label>();
            foreach (Control c in panel.Controls)
            {
                Label l = c as Label;
                if (l == null)
                    continue;
                Lesson lsn = l.Tag as Lesson;
                if (lsn == null)
                    continue;

                if (l.Location.X >= vc.minX + (col + 1) * vc.cellWidth ||
                    l.Location.X + l.Width < vc.minX + col * vc.cellWidth)
                    continue;
                labels.Add(l);
            }
            return labels;
        }

        private void ExpandRight(Panel panel, ViewContext vc)
        {
            for (int col = vc.cols - 1; col >= 0; col--)
            {
                List<Label> labels = CollectColumnLabels2(panel, vc, col);
                if (labels.Count == 0)
                    continue;

                int[] rightmost = new int[panel.Height];
                foreach (Label l in labels)
                {
                    int rb = l.Location.X + l.Width;
                    for (int y = l.Location.Y; y <= l.Location.Y + l.Height; y++)
                    {
                        if (rb > rightmost[y])
                            rightmost[y] = rb;
                    }
                }
                foreach (Label l in labels)
                {
                    int rb = l.Location.X + l.Width;
                    bool hit = false;
                    for (int y = l.Location.Y; y <= l.Location.Y + l.Height; y++)
                    {
                        if (rb != rightmost[y])
                        {
                            hit = true;
                            break;
                        }
                    }
                    if (!hit)
                        l.Width = (vc.minX + (col + 1) * vc.cellWidth - l.Location.X);
                }
            }
        }

        private bool MarkCollisions(Label lb, Panel panel)
        {
            bool found = false;
            foreach (Control c in panel.Controls)
            {
                Label lc = c as Label;
                if (lc == null || lc == lb)
                    continue;
                Lesson lsn = lc.Tag as Lesson;
                if (lsn == null)
                    continue;

                if (!(lb.Location.X >= lc.Location.X + lc.Width ||
                        lb.Location.X + lb.Width  <= lc.Location.X ||
                        lb.Location.Y >= lc.Location.Y + lc.Height ||
                        lb.Location.Y + lb.Height  <= lc.Location.Y))
                {
                    found = true;
                    if (lc.BackColor != Color.Transparent)
                    {
                        lc.ForeColor = lc.BackColor;
                        lc.BackColor = Color.Transparent;
                    }
                }
            }

            if (found)
            {
                if (lb.BackColor != Color.Transparent)
                {
                    lb.ForeColor = lb.BackColor;
                    lb.BackColor = Color.Transparent;
                }
            }
            return found;
        }
    }
}
