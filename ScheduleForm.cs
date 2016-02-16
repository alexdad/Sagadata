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
        DateTime m_schedNew_chosenDate = DateTime.Today;
        string m_schedNew_chosenLanguage = "English";
        bool m_schedNew_firstTimeSchedNew = true;
        int m_schedNew_row = -1;
        int m_schedNew_col = -1;


        int StandardizeDayOfTheWeek(DayOfWeek dw)
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

        private Color AvailabilityColor(char c)
        {

            // Todo - better array here. Also need color key on the screen.
            switch(c)
            {
                case '0':   // light green - free
                    return Color.FromArgb(179, 224, 158);
                case '1':   // yellow - maybe
                    return Color.FromArgb(239, 237, 143);
                case '2':   // reddish - unavailable
                    return Color.FromArgb(242, 223, 228);
                case '4':   // red-brown - lesson    
                    return Color.FromArgb(128, 0, 0);
                default:
                    return Color.FromArgb(192, 192, 192);
            }
        }

        private void SchedNewGetTeacherAvailability()
        {
            char[,] charSlots = GetTeacherAvailability(
                cbSchedNewTeacher.SelectedItem as string);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < m_enumTimeSlot.Length; j++)
                {
                    char c = charSlots[i, j];
                    dgvSchedNew.Rows[j + 1].Cells[i + 1].Style.BackColor = AvailabilityColor(c);
                    dgvSchedNew.Rows[j + 1].Cells[i + 1].Tag = c;
                }
            }
        }

        private void SchedNewGetTeacherLessons()
        {
            DateTime ds = StartWeekOf(m_schedNew_chosenDate);
            DateTime de = ds;
            de =  de.AddDays(7);

            List<Lesson> lsn = LessonsByTeacherAndTime(
                cbSchedNewTeacher.SelectedItem as string, 
                ds, de);

            foreach(Lesson l in lsn)
            {
                string stud = l.Student1 as string;
                int i = StandardizeDayOfTheWeek(l.DateTimeStart.DayOfWeek);
                int js = l.DateTimeStart.Hour * 4 + l.DateTimeStart.Minute / 15 - 7 * 4;
                int je = l.DateTimeEnd.Hour * 4 + l.DateTimeEnd.Minute / 15 - 7 * 4;
                char c = '4';
                for (int j = js; j < je; j++)
                {
                    Color bc = AvailabilityColor(c);
                    Color fc = Color.FromArgb(255 - bc.R, 255 - bc.G, 255 - bc.B);
                    dgvSchedNew.Rows[j + 1].Cells[i + 1].Style.BackColor = bc;
                    dgvSchedNew.Rows[j + 1].Cells[i + 1].Style.ForeColor = fc;

                    dgvSchedNew.Rows[j + 1].Cells[i + 1].Tag = c;
                    dgvSchedNew.Rows[j + 1].Cells[i + 1].Value = stud;
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
        

        void SchedNewLesson()
        {
            //cbSchedNewDuration
        }

        void InitializeSchedNew(bool force)
        {
            if (m_schedNew_firstTimeSchedNew)
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

            if (m_schedNew_firstTimeSchedNew || force)
            {
                PopulateTeacherChoices(m_schedNew_chosenLanguage, cbSchedNewTeacher);
                cbSchedNewTeacher.SelectedIndex = -1;
                cbSchedNewTeacher.Text = "";

                PopulateStudentChoices(m_schedNew_chosenLanguage, cbSchedNewStud1);
                cbSchedNewStud1.SelectedIndex = -1;
                cbSchedNewStud1.Text = "";

                PopulateStudentChoices(m_schedNew_chosenLanguage, cbSchedNewStud2);
                cbSchedNewStud2.SelectedIndex = -1;
                cbSchedNewStud2.Text = "";

                PopulateStudentChoices(m_schedNew_chosenLanguage, cbSchedNewStud3);
                cbSchedNewStud3.SelectedIndex = -1;
                cbSchedNewStud3.Text = "";

                PopulateStudentChoices(m_schedNew_chosenLanguage, cbSchedNewStud4);
                cbSchedNewStud4.SelectedIndex = -1;
                cbSchedNewStud4.Text = "";
            }

            if (m_schedNew_firstTimeSchedNew)
            {
                m_schedNew_firstTimeSchedNew = false;
                cbSchedNewLanguage.SelectedIndex = 1;   // Starting from English
                cbSchedNewDuration.SelectedIndex = 5;   // Starting from 1:30
            }

            butSchedNewAccept.Visible = false;
        }
        void SchedNewLessonLanguageChosen()
        {
            string lang = (string)cbSchedNewLanguage.SelectedItem;
            m_schedNew_chosenLanguage = lang;
            InitializeSchedNew(true);
        }

        void SchedNewShowDataIfReady()
        {
            if (cbSchedNewTeacher.SelectedItem as string != null &&
                cbSchedNewStud1.SelectedItem as string != null)

                SchedNewShowData();
        }
        void SchedNewShowData()
        {
            // Form the grid
            schedNewSlotList.Clear();
            schedNewSlotList.Add(new RecordKeeper.Slot(WeekOf(m_schedNew_chosenDate, "")));
            for(int i=0; i < m_enumTimeSlot.Length; i++)
                schedNewSlotList.Add(new RecordKeeper.Slot(m_enumTimeSlot[i]));

            // Assign colors based on teacher availability
            SchedNewGetTeacherAvailability();

            // Add info about teacher's lessons
            SchedNewGetTeacherLessons();

            // Add info about student's lessons

            // Add info about room's availability

            // Assign texts: for 
        }

        void ProposeNewLesson(DataGridView dgv, int row, int col)
        {
            if (row < 0 || row >= dgvSchedNew.RowCount ||
                col < 0 || col > dgvSchedNew.ColumnCount)
                return;

            if (m_schedNew_row >= 0 && m_schedNew_col >= 0 && 
                m_schedNew_col < dgvSchedNew.ColumnCount)
            {
                for (int r = m_schedNew_row; 
                    r < m_schedNew_row + 32 && r < dgvSchedNew.RowCount; 
                    r++)
                {
                    string v = dgvSchedNew.Rows[r].Cells[m_schedNew_col].Value as string;
                    if (v == null)
                        v = "";
                    if (v.Length >= 4 && v.Substring(0, 4) == "New ")
                        dgvSchedNew.Rows[r].Cells[m_schedNew_col].Value = v.Substring(4);
                    else
                        break;
                }
            }

            m_schedNew_row = row;
            m_schedNew_col = col;
            int len = cbSchedNewDuration.SelectedIndex + 1;

            for (int r = m_schedNew_row;
                r < m_schedNew_row + len && r < dgvSchedNew.RowCount;
                r++)
            {
                string v = dgvSchedNew.Rows[r].Cells[m_schedNew_col].Value as string;
                if (v == null)
                    v = "";
                dgvSchedNew.Rows[r].Cells[m_schedNew_col].Value = "New " + v; 
            }
            butSchedNewAccept.Visible = true; 
        }

        void AcceptNewLesson()
        {
            Modes was = CurrentMode;
            CurrentMode = Modes.Lessons;
            buttonAdd_Click(null, null);
            Lesson l = lessonList.Current as Lesson;
            CurrentMode = was;

            int weekdayPicked = StandardizeDayOfTheWeek(dtpSchedNew.Value.DayOfWeek);
            DateTime mondayPicked = dtpSchedNew.Value;
            mondayPicked = mondayPicked.AddDays(-weekdayPicked);

            DateTime dts = mondayPicked;
            dts = dts.AddDays(m_schedNew_col - 1);

            l.Day = dts.ToShortDateString();
            l.Start = m_enumTimeSlot[m_schedNew_row - 1];
            l.End = m_enumTimeSlot[m_schedNew_row + cbSchedNewDuration.SelectedIndex];
            l.Program = "";
            l.Room = "";
            l.State = m_enumState[1]; // Lesson state 1 = Planned
            l.Student1 = cbSchedNewStud1.SelectedItem as string;
            l.Student2 = cbSchedNewStud2.SelectedItem as string;
            l.Student3 = cbSchedNewStud3.SelectedItem as string;
            l.Student4 = cbSchedNewStud4.SelectedItem as string;
            l.Student5 = "";
            l.Student6 = "";
            l.Student7 = "";
            l.Student8 = "";
            l.Student9 = "";
            l.Student10 = "";
            l.Teacher1 = cbSchedNewTeacher.SelectedItem as string;
            l.Teacher2 = "";
            l.Comments = tbSchedNewComment.Text;

            butSchedNewAccept.Visible = false;
            SchedNewShowDataIfReady();
        }
    }
}
