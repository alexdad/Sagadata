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
        int m_plan_row = -1;
        int m_plan_col = -1;

        enum StatusColors
        {
            Good, Warning, Bad, Attention, Unknown
        };

        public Color[] StateColors = new Color[]
        {
                Color.FromArgb(179, 224, 158),      // light green
                Color.FromArgb(239, 237, 143),      // yellow
                Color.FromArgb(242, 223, 228),      // reddish
                Color.FromArgb(128, 0, 0),          // red-brown
                Color.FromArgb(192, 192, 192),      // gray
        };

        public static int StandardizeDayOfTheWeek(DayOfWeek dw)
        {
            int day= (int)dw;
            if (day == 0)
                day = 6;        // Sunday: .Net says Sunday, we want 6
            else
                day = day - 1; // We want to count from Monday as zero
            return day;
        }

        string[] WeekOf(DateTime dt, string hdr)
        {
            int day = StandardizeDayOfTheWeek(dt.DayOfWeek);
            DateTime d = dt;
            d = d.AddDays(-day);

            string[] w = new string[8];
            w[0] = hdr; 
            for (int i=0; i<7; i++)
            {
                w[i+1] = d.ToString("dd MMM");
                d = d.AddDays(1);
            }
            return w;
        }

        DateTime StartWeekOf(DateTime dt)
        {
            int day = StandardizeDayOfTheWeek(dt.DayOfWeek);
            DateTime d = dt;
            d = d.AddDays(-day);
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
        }

        void PopulateWeekChoices(ComboBox cb)
        {
            // Find closest Monday backward, and add 8
            int dayToday = StandardizeDayOfTheWeek(DateTime.Now.DayOfWeek);
            DateTime bow = DateTime.Now;
            bow = bow.AddDays(-dayToday);
            DateTime eow = bow;
            eow = eow.AddDays(6);

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
                case '0':   // light green - free
                    return StateColors[(int)StatusColors.Good];
                case '1':   // yellow - maybe
                    return StateColors[(int)StatusColors.Warning];
                case '2':   // reddish - unavailable
                    return StateColors[(int)StatusColors.Bad];
                case '4':   // red-brown - lesson    
                    return StateColors[(int)StatusColors.Attention];
                default:
                    return StateColors[(int)StatusColors.Unknown];
            }
        }

        private void PlanGetTeacherAvailability()
        {
            char[,] charSlots = GetTeacherAvailability(
                cbPlanTeacher.SelectedItem as string);
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

        private void PlanGetTeacherLessons()
        {
            DateTime ds = StartWeekOf(m_plan_chosenDate);
            DateTime de = ds;
            de =  de.AddDays(7);

            string t = cbPlanTeacher.SelectedItem as string;
            if (t != null)
                MarkLessons(LessonsByTeacher(t, ds, de) );

            t = cbPlanStud1.SelectedItem as string;
            if (t != null)
                MarkLessons(LessonsByStudent(t, ds, de));

            t = cbPlanStud2.SelectedItem as string;
            if (t != null)
                MarkLessons(LessonsByStudent(t, ds, de));

            t = cbPlanStud3.SelectedItem as string;
            if (t != null)
                MarkLessons(LessonsByStudent(t, ds, de));

            t = cbPlanStud4.SelectedItem as string;
            if (t != null)
                MarkLessons(LessonsByStudent(t, ds, de));
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
            foreach (Teacher t in ActiveTeachersByLanguage(lang))
                cb.Items.Add(t.Description);
        }
        void PopulateStudentChoices(string lang, ComboBox cb)
        {
            cb.Items.Clear();
            foreach (Student t in ActiveStudentsByLanguage(lang))
               cb.Items.Add(t.Description);
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
            if (m_plan_firstTimePlan)
            {
                Modes was = CurrentMode;
                if (studentList == null || studentList.Count == 0)
                {
                    CurrentMode = Modes.Students;
                    m_recordTypes[Modes.Students].ReadRecordsFile<Student>();
                }

                if (teacherList == null || teacherList.Count == 0)
                {
                    CurrentMode = Modes.Teachers;
                    m_recordTypes[Modes.Teachers].ReadRecordsFile<Teacher>();
                }

                if (lessonList == null || lessonList.Count == 0)
                {
                    CurrentMode = Modes.Lessons;
                    m_recordTypes[Modes.Lessons].ReadRecordsFile<Lesson>();
                }

                CurrentMode = was;
            }

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
            if (cbPlanTeacher.SelectedItem as string != null ||
                cbPlanStud1.SelectedItem as string != null)

                PlanShowData();
        }
        void PlanShowData()
        {
            // Form the grid
            planSlotList.Clear();
            planSlotList.Add(new RecordKeeper.Slot(WeekOf(m_plan_chosenDate, "")));
            for(int i=0; i < m_enumTimeSlot.Length; i++)
                planSlotList.Add(new RecordKeeper.Slot(m_enumTimeSlot[i]));

            // Assign colors based on teacher availability
            PlanGetTeacherAvailability();

            // Add info about teacher's lessons
            PlanGetTeacherLessons();

            // Add info about student's lessons

            // Add info about room's availability

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
            int len = cbPlanDuration.SelectedIndex + 1;

            for (int r = m_plan_row;
                r < m_plan_row + len && r < dgvPlan.RowCount;
                r++)
            {
                string v = dgvPlan.Rows[r].Cells[m_plan_col].Value as string;
                if (v == null)
                    v = "";
                dgvPlan.Rows[r].Cells[m_plan_col].Value = "New " + v; 
            }
            butPlanAccept.Visible = true; 
        }

        void AcceptNewLesson()
        {
            Modes was = CurrentMode;
            CurrentMode = Modes.Lessons;
            buttonAdd_Click(null, null);
            Lesson l = lessonList.Current as Lesson;
            CurrentMode = was;

            int weekdayPicked = StandardizeDayOfTheWeek(dtpPlan.Value.DayOfWeek);
            DateTime mondayPicked = dtpPlan.Value;
            mondayPicked = mondayPicked.AddDays(-weekdayPicked);

            DateTime dts = mondayPicked;
            dts = dts.AddDays(m_plan_col - 1);

            l.Day = dts.ToShortDateString();
            l.Start = m_enumTimeSlot[m_plan_row - 1];
            l.End = m_enumTimeSlot[m_plan_row + cbPlanDuration.SelectedIndex];
            l.Program = "";
            l.Room = "";
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
            l.Comments = tbPlanComment.Text;

            butPlanAccept.Visible = false;
            PlanShowDataIfReady();
        }
    }
}
