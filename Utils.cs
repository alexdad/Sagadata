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
    public partial class FormGlob : Form
    {
        public static Color AttentionColor = Color.FromArgb(255, 127, 39);
        public static Color RelaxationColor = Color.FromArgb(212, 255, 236);

        private Label m_labelWorking;

        #region "Time-related"
        public static DateTime DayStart(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
        }
        public static DateTime WorkDayStart(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 7, 0, 0);
        }
        public static DateTime DayEnd(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
        }
        public static DateTime WorkDayEnd(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 23, 0, 0);
        }
        public static string[] WeekOf(DateTime dt, string hdr, out DateTime weekStart, out DateTime weekEnd)
        {
            weekStart = WeekStart(dt);
            weekEnd = WeekEnd(dt);

            string[] w = new string[8];
            w[0] = hdr;
            DateTime d = weekStart;
            for (int i = 0; i < 7; i++)
            {
                w[i + 1] = d.ToString("dd MMM");
                d = d.AddDays(1);
            }

            return w;
        }
        public static string[] WeekOf(DateTime dt)
        {
            DateTime weekStart = WeekStart(dt);
            DateTime weekEnd = WeekEnd(dt);

            string[] w = new string[7];
            DateTime d = weekStart;
            for (int i = 0; i < 7; i++)
            {
                w[i] = d.ToString("ddd,MMM dd");
                d = d.AddDays(1);
            }

            return w;
        }

        public static string[] MonthOf(DateTime dt)
        {
            int days = s_DaysPerMonth[dt.Month - 1];
            string[] w = new string[days];
            for (int i = 0; i < days; i++)
                w[i] = (i+1).ToString();
            return w;
        }

        public static int StandardizeDayOfTheWeek(DayOfWeek dw)
        {
            int day = (int)dw;
            if (day == 0)
                day = 6;        // Sunday: .Net says Sunday, we want 6
            else
                day = day - 1; // We want to count from Monday as zero
            return day;
        }

        public static DateTime WeekStart(DateTime dt)
        {
            int day = StandardizeDayOfTheWeek(dt.DayOfWeek);
            DateTime d = dt;
            d = d.AddDays(-day);
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
        }
        public static DateTime WeekEnd(DateTime dt)
        {
            int day = StandardizeDayOfTheWeek(dt.DayOfWeek);
            DateTime d = dt;
            d = d.AddDays(-day + 6);
            return new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
        }

        public static DateTime MonthStart(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
        }

        static int[] s_DaysPerMonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public static DateTime MonthEnd(DateTime dt)
        {
            int day = s_DaysPerMonth[dt.Month - 1];
            return new DateTime(dt.Year, dt.Month, s_DaysPerMonth[dt.Month - 1], 23, 59, 59);
        }

        public static string[] WeekdayNames()
        {
            string[] days = {"Monday", "Tuesday", "Wednesday",
                "Thursday", "Friday", "Saturday", "Sunday" };
            return days;
        }

        public static int Slots(DateTime dt1, DateTime dt2)
        {
            return (int)((dt2.Ticks - dt1.Ticks) / SlotInTicks);
        }

        public int LanguageIndex(string lang)
        {
            for (int i=0; i < m_enumLanguage.Length; i++)
            {
                if (lang == m_enumLanguage[i])
                    return i;
            }
            return -1;
        }

        public bool SetComboBoxIndexByValue(ComboBox cb, string str)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                if (str == (string)cb.Items[i])
                {
                    cb.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region "location-related"
        public int RoomIndex(string room)
        {
            int i = 0;
            foreach(Room r in roomList)
            {
                if (r.Name.Trim().ToLower() == room.Trim().ToLower())
                    return i;
                else
                    i++;
            }
            return roomList.Count - 1;
        }
        public Color RoomColor(string room)
        {
            Color color = Color.Gray;
            foreach (Room r in roomList)
            {
                color = r.RoomColor;
                if (r.Name.Trim().ToLower() == room.Trim().ToLower())
                    return color;
            }
            return color;
        }
        #endregion

        #region "Color-related"

        public static Color ComplementColor(Color c)
        {
            int maxdiff = Math.Max(Math.Abs(c.R - c.G), Math.Abs(c.R - c.B));
            maxdiff = Math.Max(Math.Abs(c.B - c.G), maxdiff);
            if (maxdiff > 60)
                return Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B);
            else if ((c.R + c.G + c.B) / 3 > 128)
                return Color.Black;
            else
                return Color.White;
        }
        enum StatusColors
        {
            Good, Warning, Bad, Irrelevant, Unknown
        };

        public Color[] StateColors = new Color[]
        {
                Color.FromArgb(0, 132, 0),          // Good = green
                Color.FromArgb(255, 255, 128),      // Warning = yellow
                Color.FromArgb(196, 0, 0),          // Bad = red
                Color.FromArgb(0, 0, 196),          // Irrelevant = blue
                Color.FromArgb(220, 220, 220),      // Unknown = gray
        };

        public Color[] StateForeColors = new Color[]
        {
                Color.Yellow,           // Good = yellow on green
                Color.Black,            // Warning = black on yellow
                Color.Yellow,           // Bad = yellow on red
                Color.Yellow,           // Irrelevant = yellow on blue
                Color.Yellow,           // Unknown = yellow on gray
        };
        #endregion

        #region "Scale-related"

        public enum TabScales
        {
            Month,
            Week,
            Day,
            Slot
        }
        public int StepPerScale()
        {
            TabScales scales = (TabScales)tabControlViewScales.SelectedIndex;

            switch (scales)
            {
                case TabScales.Month:
                    return 30;
                case TabScales.Week:
                    return 7;
                case TabScales.Day:
                    return 1;
                case TabScales.Slot:
                    return 1;
            }

            return -1;
        }
        #endregion

        #region "UI-related"
        private void CreateLabelWorking()
        {
            m_labelWorking = new System.Windows.Forms.Label();

            m_labelWorking.AutoSize = true;
            m_labelWorking.BackColor = System.Drawing.Color.Transparent;
            m_labelWorking.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            m_labelWorking.Location = new System.Drawing.Point(298, 263);
            m_labelWorking.Name = "labelWorking";
            m_labelWorking.Size = new System.Drawing.Size(533, 42);
            m_labelWorking.TabIndex = 14;
            m_labelWorking.Text = "Working...  it takes some time...";
            m_labelWorking.Parent = this;
            m_labelWorking.Visible = false;
        }
        #endregion

        #region "General"
        private static void CountString(string s, SortedDictionary<string, int> dict)
        {
            if (s != null && s.Trim().Length > 1)
            {
                if (dict.ContainsKey(s))
                    dict[s] = dict[s] + 1;
                else
                    dict[s] = 1;
            }
        }
        public void DisposeChildren(Control c)
        {
            s_lastHoveredLabelOrButton = null;
            while (c.Controls.Count > 0)
                c.Controls[0].Dispose();
            c.Controls.Clear();
        }

        public static bool IsStringEmpty(string s)
        {
            return (s == null || s.Trim().Length == 0);
        }

        const int highlightSize = 2;
        static Control s_lastHoveredLabelOrButton = null;

        public static void SetHighlight(Label lb)
        {
            if (lb == null)
                return;

            lb.BorderStyle = BorderStyle.Fixed3D;
            lb.FlatStyle = FlatStyle.Popup;
            lb.Font = new Font(lb.Font, FontStyle.Bold);
            s_lastHoveredLabelOrButton = lb;
        }
        public static void DropHighlight()
        {
            if (s_lastHoveredLabelOrButton == null)
                return;
            Label lb = s_lastHoveredLabelOrButton as Label;
            s_lastHoveredLabelOrButton = null;
            if (lb == null)
                return;

            lb.BorderStyle = BorderStyle.FixedSingle;
            lb.FlatStyle = FlatStyle.Standard;
            lb.Font = new Font(lb.Font, FontStyle.Regular);
        }
        #endregion
    }
}