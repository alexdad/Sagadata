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
            Slot= "";
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
            switch(room)
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
                case "Ourside":
                    Outside = text;
                    return 7;
                default:
                    return -1;
            }
        }
    }


    public partial class FormGlob : Form
    {
        DateTime m_view_chosenDate = DateTime.Today;
        string m_view_chosen_status = "";
        string m_view_chosen_student = "";
        string m_view_chosen_teacher = "";
        string m_view_chosen_room= "";

        int m_viewslots_row = -1;
        int m_viewslots_col = -1;

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

        public void ShowWeek()
        {

        }
        public void ShowDay()
        {

            while (panelViewDay.Controls.Count > 0)
                panelViewDay.Controls[0].Dispose();

            panelViewDay.Controls.Clear();

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
        public void ViewShowSlots()
        {
            viewSlotList.Clear();
            for (int i = 0; i < m_enumTimeSlot.Length; i++)
                viewSlotList.Add(new RecordKeeper.ViewSlot(m_enumTimeSlot[i]));

            DateTime dt1 = new DateTime(
                m_view_chosenDate.Year,
                m_view_chosenDate.Month,
                m_view_chosenDate.Day,
                0, 0, 0);

            DateTime dt2 = new DateTime(
                m_view_chosenDate.Year,
                m_view_chosenDate.Month,
                m_view_chosenDate.Day,
                23, 59, 59);

            List<Lesson> lsn = FindLessonSlots(
                m_view_chosen_status,
                m_view_chosen_student,
                m_view_chosen_teacher,
                m_view_chosen_room,
                dt1, dt2);

            DateTime dts = new DateTime(
               m_view_chosenDate.Year,
               m_view_chosenDate.Month,
               m_view_chosenDate.Day,
               7, 0, 0);  // slots start at 7 am 

            DateTime dtn = new DateTime(
               m_view_chosenDate.Year,
               m_view_chosenDate.Month,
               m_view_chosenDate.Day,
               7, 15, 0);

            TimeSpan ts = dtn - dts;

            foreach (Lesson l in lsn)
            {
                int slotIndex = (int)((l.DateTimeStart.Ticks - dts.Ticks) / ts.Ticks);
                if (slotIndex < 0 || slotIndex >= m_enumTimeSlot.Length)
                    continue;

                ViewSlot slot = new RecordKeeper.ViewSlot(m_enumTimeSlot[slotIndex]);
                int roomIndex = slot.SetViewSlot(l.Room, l.Teacher1);
                if (roomIndex < 0 || roomIndex >= 7)
                    continue;

                viewSlotList.Add(slot);
                dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Style.BackColor =
                    m_viewSlotColors[roomIndex - 1];
                dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Style.Tag = l;
                dgvViewSlots.Rows[slotIndex].Cells[roomIndex].Value = l.Teacher1;
            }

            /*
            viewSlotList.Add(new RecordKeeper.ViewSlot());
            for (int i=0; i <7; i++)
                dgvViewSlots.Rows[0].Cells[i + 1].Style.BackColor = m_viewSlotColors[i];
            */



            // Assign colors based on teacher availability
            //PlanGetTeacherAvailability();


        }


    }
}