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
    public class Slot
    {
        public Slot()
        {
            Monday = "";
            Tuesday = "";
            Wednesday = "";
            Thursday = "";
            Friday = "";
            Saturday = "";
            Sunday = "";
            SlotName = "";
        }
        public Slot(string hdr)
        {
            Monday = "";
            Tuesday = "";
            Wednesday = "";
            Thursday = "";
            Friday = "";
            Saturday = "";
            Sunday = "";
            SlotName = hdr;
        }
        public Slot(string[] hdrs)
        {
            Monday = hdrs[1];
            Tuesday = hdrs[2];
            Wednesday = hdrs[3];
            Thursday = hdrs[4];
            Friday = hdrs[5];
            Saturday = hdrs[6];
            Sunday = hdrs[7];
            SlotName = hdrs[0];
        }
        public string SlotName { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }

    public partial class FormGlob : Form
    {
        DateTime m_plan_chosenDate = DateTime.Today;
        string m_plan_chosenLanguage = "English";
        bool m_plan_firstTimePlan = true;
        Lesson m_lessonInMove = null;
        int m_plan_row = -1;
        int m_plan_col = -1;

        void PopulateWeekChoices(ComboBox cb)
        {
            DateTime bow = WeekStart(DateTime.Now);
            DateTime eow = WeekEnd(DateTime.Now);

            cb.Items.Clear();
            for (int i=0; i < 9; i++)
            {
                cb.Items.Add(bow.ToShortDateString() + " - " + eow.ToShortDateString());
            }
        }

        private Color TeacherAvailabilityColor(char c)
        {
            switch(c)
            {
                case '0':   // free
                    return StateColors[(int)StatusColors.Good];
                case '1':   // maybe
                    return StateColors[(int)StatusColors.Warning];
                case '2':   // unavailable
                    return StateColors[(int)StatusColors.Bad];
                case '4':   // lesson    
                    return StateColors[(int)StatusColors.Irrelevant];
                default:
                    return StateColors[(int)StatusColors.Unknown];
            }
        }

        private void PlanGetTeacherAvailability()
        {
            char[,] charSlots = GetTeacherAvailability(
                (m_lessonInMove == null ? 
                    cbPlanTeacher.SelectedItem as string :
                    m_lessonInMove.Teacher1));

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < m_enumTimeSlot.Length; j++)
                {
                    char c = charSlots[i, j];
                    dgvPlan.Rows[j + 1].Cells[i + 1].Style.BackColor = TeacherAvailabilityColor(c);
                    dgvPlan.Rows[j + 1].Cells[i + 1].Tag = c;
                }
            }
        }

        private void MarkLessons(List<Lesson> lsn)
        {
            foreach (Lesson l in lsn)
            {
                string stud = l.Student1 as string;
                int i, js, je;
                l.GetLocationInWeek(out i, out js, out je);
                char c = '4';
                for (int j = js; j < je; j++)
                {
                    Color bc = TeacherAvailabilityColor(c);
                    Color fc = Color.FromArgb(255 - bc.R, 255 - bc.G, 255 - bc.B);
                    dgvPlan.Rows[j + 1].Cells[i + 1].Style.BackColor = bc;
                    dgvPlan.Rows[j + 1].Cells[i + 1].Style.ForeColor = fc;

                    dgvPlan.Rows[j + 1].Cells[i + 1].Tag = c;
                    dgvPlan.Rows[j + 1].Cells[i + 1].Value = stud;
                }
            }
        }

        private void PlanGetRelevantLessons()
        {
            DateTime ds = WeekStart(m_plan_chosenDate);
            DateTime de = ds;
            de = de.AddDays(7);

            string t = PlanSelectedTeacher1();
            if (t != null)
                MarkLessons(LessonsByTeacher(t, ds, de));

            t = PlanSelectedStudent1();
            if (t != null)
                MarkLessons(LessonsByStudent(t, ds, de));

            t = PlanSelectedStudent2();
            if (t != null)
                MarkLessons(LessonsByStudent(t, ds, de));

            t = PlanSelectedStudent3();
            if (t != null)
                MarkLessons(LessonsByStudent(t, ds, de));

            t = PlanSelectedStudent4();
            if (t != null)
                MarkLessons(LessonsByStudent(t, ds, de));
        }

        public void PlanGetRoomAvailability()
        {
            DateTime ds = WeekStart(m_plan_chosenDate);
            DateTime de = ds;
            de = de.AddDays(7);

            foreach(Lesson l in LessonsByTime(ds, de))
            {
                if (l.Room == null || l.Room.Length == 0 || l.Room == "N/A")
                    continue;
                string roomLetter = l.Room.Substring(0, 1);
                int i, js, je;
                l.GetLocationInWeek(out i, out js, out je);
                for (int j = js; j < je; j++)
                {
                    string val = dgvPlan.Rows[j + 1].Cells[i + 1].Value as string;
                    if (IsStringEmpty(val))
                        val = "*";
                    dgvPlan.Rows[j + 1].Cells[i + 1].Value = val + roomLetter;
                }
            }
        }

        public char[,] GetTeacherAvailability(string description)
        {
            char[,] res = new char[7, m_enumTimeSlot.Length];
            for (int i = 0; i < 7; i++)
                for (int j = 0; j < m_enumTimeSlot.Length; j++)
                    res[i, j] = '9';

            foreach(var tt in teacherList)
            {
                Teacher t = tt as Teacher;
                if (t.Description == description)
                {
                    for (int j=0; j < t.Monday.Length; j++)
                        res[0, j] = t.Monday[j];
                    for (int j = 0; j < t.Tuesday.Length; j++)
                        res[1, j] = t.Tuesday[j];
                    for (int j = 0; j < t.Wednesday.Length; j++)
                        res[2, j] = t.Wednesday[j];
                    for (int j = 0; j < t.Thursday.Length; j++)
                        res[3, j] = t.Thursday[j];
                    for (int j = 0; j < t.Friday.Length; j++)
                        res[4, j] = t.Friday[j];
                    for (int j = 0; j < t.Saturday.Length; j++)
                        res[5, j] = t.Saturday[j];
                    for (int j = 0; j < t.Sunday.Length; j++)
                        res[6, j] = t.Sunday[j];
                    break;
                }
            }
            return res;
        }

        void PopulateTeacherChoices(string lang, ComboBox cb)
        {
            cb.Items.Clear();
            List<string> ts = new List<string>();
            foreach (Teacher t in ActiveTeachersByLanguage(lang))
                ts.Add(t.Description);
            ts.Sort();
            cb.Items.AddRange(ts.ToArray()); 
        }
        void PopulateStudentChoices(string lang, ComboBox cb)
        {
            cb.Items.Clear();
            List<string> ts = new List<string>();
            foreach (Student t in ActiveStudentsByLanguage(lang))
                ts.Add(t.Description);
            ts.Sort();
            cb.Items.AddRange(ts.ToArray());
        }

        void PopulateStudentPossibleSchedule(string description, Label tb)
        {
            foreach (Student t in SpecificStudent(description))
               tb.Text = t.PossibleSchedule;
        }

        void PopulateTeacherVacation(string description, Label lb)
        {
            foreach (Teacher t in SpecificTeacher(description))
                lb.Text = t.Vacations;
        }
        

        void PlanLesson()
        {
            //cbPlanDuration
        }

        void InitializePlan(bool force)
        {
            if (m_plan_firstTimePlan || force)
            {
                PopulateTeacherChoices(m_plan_chosenLanguage, cbPlanTeacher);
                cbPlanTeacher.SelectedIndex = -1;
                cbPlanTeacher.Text = "";

                PopulateStudentChoices(m_plan_chosenLanguage, cbPlanStud1);
                cbPlanStud1.SelectedIndex = -1;
                cbPlanStud1.Text = "";

                PopulateStudentChoices(m_plan_chosenLanguage, cbPlanStud2);
                cbPlanStud2.SelectedIndex = -1;
                cbPlanStud2.Text = "";

                PopulateStudentChoices(m_plan_chosenLanguage, cbPlanStud3);
                cbPlanStud3.SelectedIndex = -1;
                cbPlanStud3.Text = "";

                PopulateStudentChoices(m_plan_chosenLanguage, cbPlanStud4);
                cbPlanStud4.SelectedIndex = -1;
                cbPlanStud4.Text = "";
            }

            if (m_plan_firstTimePlan)
            {
                m_plan_firstTimePlan = false;
                cbPlanLanguage.SelectedIndex = 1;   // Starting from English
                cbPlanDuration.SelectedIndex = 5;   // Starting from 1:30
            }

            butPlanAccept.Visible = false;
        }
        void PlanLessonLanguageChosen()
        {
            string lang = (string)cbPlanLanguage.SelectedItem;
            m_plan_chosenLanguage = lang;
            InitializePlan(true);
        }

        void PlanShowDataIfReady()
        {
            if (cbPlanTeacher.SelectedItem as string != null &&
                cbPlanStud1.SelectedItem as string != null ||
                m_lessonInMove != null)

                PlanShowData();
        }
        void PlanShowData()
        {
            // Form the grid
            planSlotList.Clear();
            DateTime weekStart, weekEnd;
            planSlotList.Add(
                new RecordKeeper.Slot(
                    WeekOf(m_plan_chosenDate, "", out weekStart, out weekEnd)));
            for(int i=0; i < m_enumTimeSlot.Length; i++)
                planSlotList.Add(new RecordKeeper.Slot(m_enumTimeSlot[i]));

            // Add info about room's availability
            PlanGetRoomAvailability();

            // Assign colors based on teacher availability
            PlanGetTeacherAvailability();

            // Add info about teacher's lessons
            PlanGetRelevantLessons();

            // Assign texts: for 
        }

        void ProposeNewLesson(DataGridView dgv, int row, int col)
        {
            if (row < 0 || row >= dgvPlan.RowCount ||
                col < 0 || col > dgvPlan.ColumnCount)
                return;

            if (m_plan_row >= 0 && m_plan_col >= 0 && 
                m_plan_col < dgvPlan.ColumnCount)
            {
                for (int r = m_plan_row; 
                    r < m_plan_row + 32 && r < dgvPlan.RowCount; 
                    r++)
                {
                    string v = dgvPlan.Rows[r].Cells[m_plan_col].Value as string;
                    if (v == null)
                        v = "";
                    if (v.Length >= 4 && v.Substring(0, 4) == "New ")
                        dgvPlan.Rows[r].Cells[m_plan_col].Value = v.Substring(4);
                    else
                        break;
                }
            }

            m_plan_row = row;
            m_plan_col = col;
            int len = (m_lessonInMove == null ?
                        cbPlanDuration.SelectedIndex + 1 :
                        m_lessonInMove.SlotsNumber);

            List<string> busyRooms = new List<string>();
            for (int r = m_plan_row;
                r < m_plan_row + len && r < dgvPlan.RowCount;
                r++)
            {
                string val = dgvPlan.Rows[r].Cells[m_plan_col].Value as string;
                if (val != null && val.Length > 0 && val.Substring(0,1) == "*")
                    busyRooms.Add(val.Substring(1));
                dgvPlan.Rows[r].Cells[m_plan_col].Value = "New "; 
            }

            StringBuilder sb = new StringBuilder();
            foreach(string s in busyRooms)
            {
                if (s != null && s.Length > 0)
                    sb.Append(s);
            }
            string brms = sb.ToString();
            cbPlanRoom.Items.Clear();
            foreach (Room rm in ActiveRooms())
            {
                string roomLetter = rm.Name.Substring(0, 1);
                if (brms.IndexOf(roomLetter) < 0 || roomLetter == "N")
                    cbPlanRoom.Items.Add(rm.Name);
            }
            cbPlanRoom.Text = "Choose room:";
            cbPlanRoom.Visible = true;
        }

        void AcceptNewLesson()
        {
            Modes was = CurrentMode;
            CurrentMode = Modes.Lessons;
            Lesson l;
            if (m_lessonInMove == null)
            {
                buttonAdd_Click(null, null);
                l = lessonList.Current as Lesson;
            }
            else
            {
                l = m_lessonInMove;
            }
            CurrentMode = was;
            Modified = true;

            l.Room = cbPlanRoom.SelectedItem as string;

            int weekdayPicked = StandardizeDayOfTheWeek(dtpPlan.Value.DayOfWeek);
            DateTime mondayPicked = dtpPlan.Value;
            mondayPicked = mondayPicked.AddDays(-weekdayPicked);

            DateTime dts = mondayPicked;
            dts = dts.AddDays(m_plan_col - 1);

            l.Day = dts.ToShortDateString();
            l.End = m_enumTimeSlot[m_plan_row +
                (m_lessonInMove == null ?
                        cbPlanDuration.SelectedIndex :
                        l.SlotsNumber - 1)];
            l.Start = m_enumTimeSlot[m_plan_row - 1];

            if (m_lessonInMove == null)
            {
                l.Program = cbPlanProgram.SelectedItem as string; 
                l.State = m_enumState[1]; // Lesson state 1 = Planned
                l.Student1 = cbPlanStud1.SelectedItem as string;
                l.Student2 = cbPlanStud2.SelectedItem as string;
                l.Student3 = cbPlanStud3.SelectedItem as string;
                l.Student4 = cbPlanStud4.SelectedItem as string;
                l.Student5 = "";
                l.Student6 = "";
                l.Student7 = "";
                l.Student8 = "";
                l.Student9 = "";
                l.Student10 = "";
                l.Teacher1 = cbPlanTeacher.SelectedItem as string;
                l.Teacher2 = "";
            }
            l.Comments = tbPlanComment.Text;

            cbPlanRoom.SelectedValue = "";
            cbPlanRoom.SelectedIndex = -1;
            cbPlanRoom.Items.Clear();
            cbPlanRoom.Visible = false;

            butPlanAccept.Visible = false;
            PlanShowDataIfReady();
        }
        public string PlanSelectedTeacher1()
        {
            return (m_lessonInMove == null ?
                            cbPlanTeacher.SelectedItem as string :
                            m_lessonInMove.Teacher1);
        }
        public string PlanSelectedStudent1()
        {
            return (m_lessonInMove == null ?
                        cbPlanStud1.SelectedItem as string :
                        m_lessonInMove.Student1);
        }
        public string PlanSelectedStudent2()
        {
            return (m_lessonInMove == null ?
                        cbPlanStud2.SelectedItem as string :
                        m_lessonInMove.Student2);
        }
        public string PlanSelectedStudent3()
        {
            return (m_lessonInMove == null ?
                        cbPlanStud3.SelectedItem as string :
                        m_lessonInMove.Student3);
        }
        public string PlanSelectedStudent4()
        {
            return (m_lessonInMove == null ?
                        cbPlanStud4.SelectedItem as string :
                        m_lessonInMove.Student4);
        }

    }
}
