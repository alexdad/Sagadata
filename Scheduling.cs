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
        DateTime m_chosenDate;
        string m_chosenLanguage = "English";

        int StandardizeDayOfTheWeek(DayOfWeek dw)
        {
            int day= (int)dw;
            if (day == 0)
                day = 6;        // Sunday: .Net says Sunday, we want 6
            else
                day = day - 1; // We want to count from Monday as zero
            return day;
        }

        string[] WeekOf(DateTime dt)
        {
            int day = StandardizeDayOfTheWeek(dt.DayOfWeek);
            DateTime d = dt;
            d.AddDays(-day);

            string[] w = new string[7];
            for (int i=0; i<7; i++)
            {
                w[i] = d.ToShortDateString();
                d.AddDays(1);
            }
            return w;
        }
        void PopulateWeekChoices(ComboBox cb)
        {
            // Find closest Monday backward, and add 8
            int dayToday = StandardizeDayOfTheWeek(DateTime.Now.DayOfWeek);
            DateTime bow = DateTime.Now;
            bow.AddDays(-dayToday);
            DateTime eow = bow;
            eow.AddDays(6);

            cb.Items.Clear();
            for (int i=0; i < 9; i++)
            {
                cb.Items.Add(bow.ToShortDateString() + " - " + eow.ToShortDateString());
            }
        }

        void SchedNewLesson()
        {

        }
    }
}
