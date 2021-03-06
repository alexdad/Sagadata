﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    public partial class FormGlob : Form
    {
        public static Color AttentionColor = Color.FromArgb(255, 127, 39);
        public static Color RelaxationColor = Color.FromArgb(212, 255, 236);
        private static string[] m_jokes = {
            "coffee?", "tea?", "lemonade?", "movie?", "sorry for the delay...", "patience please",
            "it takes time...", "slow?", "cheer", "hang tight", "next avaliable agent...",
            "is it rainy today?", "ask Siri about meaning of life",
            "boring", "arghhh", "disgusting...", "devil..."
        };

        private Label m_labelWorking;
        private Random m_rnd;

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
                string dtstr = d.ToString("dd MMM");
                if (d.Year == dt.Year && d.Month == dt.Month && d.Day == dt.Day)
                    dtstr = dtstr + "  ***";
                w[i + 1] = dtstr;
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

        public static int DaysInMonth(DateTime dt)
        {
            int days = s_DaysPerMonth[dt.Month - 1];
            if (dt.Month == 2 && dt.Year % 4 == 0)
                days++;
            return days;
        }

        public static string[] MonthOf(DateTime dt)
        {
            int days = DaysInMonth(dt);
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

        static int[] s_DaysPerMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public static DateTime MonthStart(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
        }

        public static DateTime MonthEnd(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DaysInMonth(dt), 23, 59, 59);
        }

        public static DateTime ProjectToNextMonth(DateTime dt)
        {
            int weekNumber = -1;
            DateTime dtp = dt;
            while (dtp.Month == dt.Month)
            {
                dtp = dtp.AddDays(-7);
                weekNumber++;
            }
            
            DateTime dtn = dt;
            while(dtn.Month == dt.Month)
                dtn = dtn.AddDays(7);

            for (int i=0; i < weekNumber; i++)
                dtn = dtn.AddDays(7);

            return dtn;
        }

        public static int Slots(DateTime dt1, DateTime dt2)
        {
            return (int)((dt2.Ticks - dt1.Ticks) / SlotInTicks);
        }

        public static int CalcSlot(DateTime dt)
        {
            return CalcSlot(dt, false);
        }
        public static int CalcSlot(DateTime dt, bool roundUp)
        {
            return dt.Hour * 4 + (dt.Minute + (roundUp ? 14 : 0)) / 15 - 7 * 4;
        }
        public static int SlotFromStringTime(string tm, bool roundUp)
        {
            DateTime dtm = DateTime.Now;
            if (!DateTime.TryParse(tm, out dtm))
                return 0;
            else
                return CalcSlot(dtm, roundUp);
        }
        #endregion

        #region "Schematics-related"
        public int LanguageIndex(string lang)
        {
            for (int i=0; i < m_enumLanguage.Length; i++)
            {
                if (lang == m_enumLanguage[i])
                    return i;
            }
            return -1;
        }

        public string SetComboByValue(ComboBox cb, string str)
        {
            if (IsStringEmpty(str))
                return null;
            string needed = str.Trim().ToLower();
            for (int i = 0; i < cb.Items.Count; i++)
            {
                string sv = (string)cb.Items[i];
                string svl = sv.Trim().ToLower();
                if (IsStringEmpty(svl))
                    continue;

                if (needed == svl)
                {
                    cb.SelectedIndex = i;
                    return sv;
                }
            }
            return null;
        }

        public string SetComboByInitial(ComboBox cb, string str)
        {
            if (IsStringEmpty(str))
                return null;
            string initial = str.Trim().Substring(0, 1).ToLower();
            for (int i = 0; i < cb.Items.Count; i++)
            {
                string sv = ((string)cb.Items[i]);
                if (IsStringEmpty(sv))
                    continue;

                if (initial == sv.ToLower().Substring(0, 1))
                {
                    cb.SelectedIndex = i;
                    return sv;
                }
            }
            return null; 
        }

        #endregion

        #region "location-related"
        public int RoomIndex(string room)
        {
            int i = 0;
            foreach(Room r in roomList)
            {
                if (!IsStringEmpty(room) &&
                    r.Name.Trim().ToLower() == room.Trim().ToLower())
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
                if (!IsStringEmpty(room) && 
                    r.Name.Trim().ToLower() == room.Trim().ToLower())
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
            m_labelWorking.Location = new System.Drawing.Point(100, 100);
            m_labelWorking.Name = "labelWorking";
            m_labelWorking.Size = new System.Drawing.Size(333, 42);
            m_labelWorking.TabIndex = 14;
            m_labelWorking.Text = "Working... " + m_jokes[ (int) ((m_jokes.Length-1) * m_rnd.NextDouble())];
            m_labelWorking.Parent = this;
            m_labelWorking.Visible = false;
        }
        #endregion

        #region "Detection-releated"
        // We assume following Google calendar event description syntax:
        // [title :] student,* / teacher,* [; comment]
        //    or
        // [title :] student,* - teacher,* [; comment]
        //      That is:
        //      If there is ";", anything after is a comment. Before that:
        //      If there is ":", anything before is a title. After that:
        //      If there is "/", anything before is a comma-separated list of students
        //                       anything after is a comma-separated list of teachers
        // 
        // Variations:
        //     we allow using "-" intead of "-;
        //     we allow using second "/" or "-" (but the same) instead of ";"

        public static void ParseEventDescription(
            string desc, 
            out string title,
            out string[] students, 
            out string[] teachers,
            out string comment)
        {
            title = ""; comment = "";
            if (desc == null)
                desc = "";

            // Comments after last ";"
            int semicolon = desc.LastIndexOf(";");
            if (semicolon >= 0 && semicolon < desc.Length - 1)
            {
                comment = desc.Substring(semicolon + 1);
                desc = desc.Substring(0, semicolon);
            }

            // Title before first ":"
            int colon = desc.IndexOf(":");
            if (colon >= 0)
            {
                title = desc.Substring(0, colon);
                desc = (colon + 1 < desc.Length - 1 ? desc.Substring(colon + 1) : "");
            }

            // Drop possible language. 
            // TODO - this needs to come from m_enumLanguage, but this is static...
            string[] languages = { "English", "French", "German", "Spanish", "Mandarin", 
                                   "Portuguese", "Italian", "Polish", "Russian",
                                   "Japanese", "Korean", "Chinese", "Aftrikaans"};
            foreach (string lang in languages)
            {
                int ll = desc.ToLower().IndexOf(lang.ToLower());
                if (ll >= 0)
                {
                    if (ll > 0 && Char.IsLetter(desc[ll - 1]))
                        continue;

                    int le = ll + lang.Length;
                    if (le >= desc.Length - 1)
                    {
                        desc = desc.Substring(0, ll);
                        continue;
                    }
                    if (Char.IsLetter(desc[le]))
                        continue;

                    while( le < desc.Length - 1 &&
                           (Char.IsWhiteSpace(desc[le]) ||
                            Char.IsPunctuation(desc[le])))
                        le++;

                    desc = desc.Substring(0, ll) + " " + desc.Substring(le);
                }
            }

            // Teachers after "/" or "-"
            int slash = desc.IndexOf("/");
            if (slash < 0)
                slash = desc.IndexOf("-");

            // If we have one more slash or minus, assume comment after 2nd
            int slash2 = desc.IndexOf("/", slash + 1);
            int slash2m = desc.IndexOf("-", slash + 1);
            if (slash2 < 0)
                slash2 = slash2m;
            else if (slash2m >= 0 && slash2m < slash2)
                slash2 = slash2m;

            if (slash2 >= 0)
            {
                comment = desc.Substring(slash2 + 1);
                desc = desc.Substring(0, slash2);
            }

            if (slash >= 0 && slash < desc.Length - 1)
            {
                string ts = desc.Substring(slash + 1);
                teachers = ts.Split(',');
                desc = (slash == 0 ? "" : desc.Substring(0, slash));
            }
            else
                teachers = new string[0];

            desc = desc.Replace(" and ", ",");

            students = desc.Split(',');
        }

        bool AreRoomsEquivalent(string gRoom, string lRoom)
        {
            if (IsStringEmpty(gRoom))
                return IsStringEmpty(lRoom);
            else if (IsStringEmpty(lRoom))
                return false;

            return gRoom.Substring(0, 1).ToLower() == lRoom.Substring(0, 1).ToLower();
        }

        bool AreStringsEquivalent(string gs, string ls)
        {
            if (IsStringEmpty(gs))
                return IsStringEmpty(ls);
            else if (IsStringEmpty(ls))
                return false;

            return gs.ToLower() == ls.ToLower();
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

        public static string ExtractFirstWord(string s)
        {
            if (s == null)
                return "";

            int space = s.Trim().IndexOf(" ");
            if (space > 0)
                return s.Trim().Substring(0, space);
            else
                return s.Trim();
        }

        public static string ExtractLastWord(string s)
        {
            if (s == null)
                return "";

            int space = s.Trim().LastIndexOf(" ");
            if (space > 0)
                return s.Trim().Substring(space);
            else
                return s.Trim();
        }

        public static string ExtractFirstNameAndInitial(string s)
        {
            if (s == null)
                return "";

            int space = s.Trim().IndexOf(" ");
            if (space < 0)
                return s.Trim();

            string res = s.Trim().Substring(0, space) + " "; 

            space = s.Trim().LastIndexOf(" ");
            res += s.Trim().Substring(space+1, 1);
            return res;
        }

        public static int LevensteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            if (n == 0)
                return m;
            if (m == 0)
                return n;

            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        public static bool IsDateTimeReasonable(DateTime dt)
        {
            return (dt.Year >= 2014 && dt.Year < 2020 &&
                    dt.Month >= 0 && dt.Month <= 12 &&
                    dt.Day >= 0 && dt.Day <= 31);
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

        public static string CalculateMD5(string data)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("X2"));

            return sb.ToString();
        }
        #endregion
    }
}