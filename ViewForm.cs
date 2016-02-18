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
    public partial class FormGlob : Form
    {
        public void ShowWeek()
        {

        }
        public void ShowDay()
        {

            while (panelViewDay.Controls.Count > 0)
                panelViewDay.Controls[0].Dispose();

            // Now we can draw here buttons per lesson in right places 

            DateTime t = DateTime.Today;
            DateTime t1 = new DateTime(t.Year, t.Month, t.Day, 0, 0, 0);
            DateTime t2 = new DateTime(t.Year, t.Month, t.Day, 23, 59, 59);
            List<Lesson> lessons = LessonsByTime(t1, t2);

            int i = 1;
            foreach(Lesson l in lessons)
            {
                Button b = new Button()
                {
                    Text = i.ToString(),
                    Width = 100,
                    Height = 30,
                    Location = new Point(i * 30, i * 30 ),
                    Parent = panelViewDay,
                    Tag = i
                };
                // b.MouseHover
                i++;

            }





        }
        public void ShowSlots()
        {

        }
    }
}