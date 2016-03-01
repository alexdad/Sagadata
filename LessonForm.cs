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
    public class OperEvent
    {
        public OperEvent(Lesson l, GCal.CalEvent ge)
        {
            SuspectedLesson = l;
            GoogleEvent = ge;
            Linked = false; 
            SimilarityPlace = -1;
            SimilarityStart = -1;
            SimilarityDuration = -1;
            SimilarityTeacher = -1;
            SimilarityStudent = -1;
            SimilarityStatus = -1;
        }
        public Lesson SuspectedLesson;
        public GCal.CalEvent GoogleEvent;
        public bool Linked;
        public double SimilarityPlace;
        public double SimilarityStart;
        public double SimilarityDuration;
        public double SimilarityTeacher;
        public double SimilarityStudent;
        public double SimilarityStatus;

        public double OverallSimularity;

        public void CalculateSimilarities()
        {
            Linked = !FormGlob.IsStringEmpty(SuspectedLesson.GoogleId);

            SimilarityDuration = SimilarityDurations(
                SuspectedLesson.DateTimeStart, 
                SuspectedLesson.DateTimeStart,
                GoogleEvent.Start, 
                GoogleEvent.End);
            SimilarityPlace = SimilarytyPlaces(
                SuspectedLesson.Room,
                GoogleEvent.Location);
            SimilarityStart = SimilatityTimes(
                SuspectedLesson.DateTimeStart,
                SuspectedLesson.DateTimeEnd,
                GoogleEvent.Start,
                GoogleEvent.End);
            SimilarityStatus = SimilarityStates(
                SuspectedLesson.State,
                GoogleEvent.Status);
            SimilarityStudent = SimilarityStudents(
                SuspectedLesson.Student1, 
                GoogleEvent.Summary);
            SimilarityTeacher = SimilarityTeachers(
                SuspectedLesson.Teacher1,
                GoogleEvent.Summary);

            OverallSimularity = CalculateOverallSimularity();
        }

        private double CalculateOverallSimularity()
        {
            if (Linked)
            {
                if (SuspectedLesson.GoogleId == GoogleEvent.Id)
                    return 1.0;
                else
                    return 0.0;
            }

            if (SimilarityStart > 0.5 &&
                SimilarityTeacher > 0.5 &&
                SimilarityStudent > 0.5)
                return 1.0;

            if (SimilarityStart > 0.5 &&
                SimilarityPlace > 0.5)
                return 0.9;

            int count = 0;
            if (SimilarityPlace > 0.5)
                count++;
            if (SimilarityStart > 0.5)
                count++;
            if (SimilarityDuration > 0.5)
                count++;
            if (SimilarityTeacher > 0.5)
                count++;
            if (SimilarityStudent > 0.5)
                count++;

            return (double)count / 5.0; 
        }

        private double SimilarytyPlaces(string lRoom, string gLocation)
        {
            if (FormGlob.IsStringEmpty(lRoom) || FormGlob.IsStringEmpty(gLocation))
                return -1;
            if (gLocation.Substring(0, 1).ToLower() == lRoom.Substring(0, 1).ToLower())
                return 1;
            return 0;
        }
        private double SimilatityTimes(
            DateTime lStart, DateTime lEnd, 
            DateTime gStart, DateTime gEnd)
        {
            if (lStart == gStart)
                return 1.0;

            DateTime cs = (lStart > gStart ? lStart : gStart);
            DateTime ce = (lEnd < gEnd ? lEnd : gEnd);
            DateTime us = (lStart < gStart ? lStart : gStart);
            DateTime ue = (lEnd > gEnd ? lEnd : gEnd);

            if (cs < ce)
                return (ce - cs).Ticks / (ue - us).Ticks;
            else
                return 0.0; ;
        }

        private double SimilarityDurations(DateTime lStart, DateTime lEnd, DateTime gStart, DateTime gEnd)
        {
            TimeSpan ls = lEnd - lStart;
            TimeSpan gs = gEnd - gStart;
            if (ls == gs)
                return 1;
            else
                return 0;
        }
        private double SimilarityStates(string lState, string gStatus)
        {
            // todo
            return -1;
        }
        private double SimilarityStudents(string lStudent, string gSummary)
        {
            if (FormGlob.IsStringEmpty(lStudent) || FormGlob.IsStringEmpty(gSummary))
                return -1;
            string gFirst= FormGlob.ExtractFirstWord(gSummary).ToLower();
            string lFirst = FormGlob.ExtractFirstWord(lStudent).ToLower();

            if (FormGlob.IsStringEmpty(gFirst) || FormGlob.IsStringEmpty(lFirst))
                return -1;

            return 1.0 - (double)FormGlob.LevensteinDistance(gFirst, lFirst) /
                         (double)Math.Max(gFirst.Length, lFirst.Length);
        }

        private double SimilarityTeachers(string lTeacher, string gSummary)
        {
            if (FormGlob.IsStringEmpty(lTeacher) || FormGlob.IsStringEmpty(gSummary))
                return -1;

            int slash = gSummary.IndexOf("/");
            if (slash < 0)
                slash = gSummary.IndexOf("-");
            if (slash < 0)
                return -1;
            string gTeacher = FormGlob.ExtractFirstWord(gSummary.Substring(slash+1)).ToLower();
            string lFirst = FormGlob.ExtractFirstWord(lTeacher).ToLower();

            return 1.0 - (double)FormGlob.LevensteinDistance(gTeacher, lFirst) /
                         (double)Math.Max(gTeacher.Length, lFirst.Length);
        }
    }

    public partial class FormGlob : Form
    {
        string m_LessonSelectionTeacher;
        string m_LessonSelectionStudent;
        DateTime m_LessonSelectionDay;
        bool m_use_LessonSelectionDay;
        string m_LessonSelectionProgram;
        string m_LessonSelectionRoom;
        string m_currentLessonKey;

        GCal.CalEvent[] m_OperationalEvents;
        int m_curOperationalevent;

        delegate bool EvaluateLesson(Lesson t);
        private void EditLessonDetailsChanged()
        {
            EditTrap = true;
        }

        private void DropLessonSelection()
        {
            m_LessonSelectionTeacher = null;
            m_LessonSelectionStudent = null;
            m_use_LessonSelectionDay = false;
            m_LessonSelectionProgram = null;
            m_LessonSelectionRoom = null;
        }
        private void LessonToFormConst1()
        {
            m_dataTypes.Add(Modes.Lessons, typeof(Lesson));
            m_recordTypes.Add(Modes.Lessons, new LessonType(this));
        }
        private void LessonToFormConst2()
        {
            cbGlobMode.Items.Add(Modes.Lessons.ToString());
        }

        public void DoLessonSelection()
        {
            if (!SelectionMode)
            {
                SelectionMode = true;
                CurrentType.StashRecordList();
            }

            DataList.Clear();
            foreach (Lesson s in m_recordTypes[Modes.Lessons].SavedFullListDuringSelection)
            {
                if (!CurrentType.Fit(m_LessonSelectionTeacher, s.Teacher1, false) &&
                    !CurrentType.Fit(m_LessonSelectionTeacher, s.Teacher2, false))
                    continue;
                if (!CurrentType.Fit(m_LessonSelectionStudent, s.Student1, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student2, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student3, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student4, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student5, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student6, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student7, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student8, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student9, false) &&
                    !CurrentType.Fit(m_LessonSelectionStudent, s.Student10, false)                    )
                    continue;
                if (m_use_LessonSelectionDay &&
                    !RecordType.FitDate(m_LessonSelectionDay, s.Day))
                    continue;
                if (!CurrentType.Fit(m_LessonSelectionProgram, s.Program, false))
                    continue;
                if (!CurrentType.Fit(m_LessonSelectionRoom, s.Room, false))
                    continue;

                DataList.Add(s);
            }
            ShowCurrentCount();
        }

        List<Lesson> LessonsByTime(DateTime t1, DateTime t2, bool onlyActual)
        {
            return FindLessons(l =>
                    (!onlyActual || l.Actual) &&
                    DateTime.Parse(l.Start) <= t2 && 
                    DateTime.Parse(l.End) >= t1);
        }
        List<Lesson> LessonsByTeacher(string desc, bool onlyActual)
        {
            if (IsStringEmpty(desc))
                return new List<Lesson>();

            return FindLessons(l => 
                (!onlyActual || l.Actual) &&
                (l.Teacher1 == desc || l.Teacher2 == desc));
        }
        List<Lesson> LessonsByStudent(string desc, bool onlyActual)
        {
            if (IsStringEmpty(desc))
                return new List<Lesson>();

            return FindLessons(l =>
                (!onlyActual || l.Actual) &&
                (l.Student1 == desc || l.Student2 == desc || l.Student3 == desc ||
                l.Student4 == desc || l.Student5 == desc || l.Student6 == desc ||
                l.Student7 == desc || l.Student8 == desc || l.Student9 == desc ||
                l.Student10 == desc));
        }

        List<Lesson> LessonsByTeacher(string desc, DateTime t1, DateTime t2, bool onlyActual)
        {
            if (IsStringEmpty(desc))
                return new List<Lesson>();

            return FindLessons(l =>
                    (!onlyActual || l.Actual) &&
                    (l.Teacher1 == desc || l.Teacher2 == desc) &&
                    l.DateTimeStart <= t2 && l.DateTimeEnd >= t1);
        }

        List<Lesson> LessonsByStudent(string desc, DateTime t1, DateTime t2, bool onlyActual)
        {
            if (IsStringEmpty(desc))
                return new List<Lesson>();

            return FindLessons(l =>
                   (!onlyActual || l.Actual) &&
                   (l.Student1 == desc || l.Student2 == desc || l.Student3 == desc ||
                    l.Student4 == desc || l.Student5 == desc || l.Student6 == desc ||
                    l.Student7 == desc || l.Student8 == desc || l.Student9 == desc ||
                    l.Student10 == desc) &&
                    l.DateTimeStart <= t2 &&
                    l.DateTimeEnd >= t1);
        }

        public bool SetLessonDetails(string key, string program, string room, string comment)
        {
            foreach (var tt in this.lessonList.List)
            {
                Lesson t = tt as Lesson;
                if (t.Key == key)
                {
                    t.Program = program;
                    t.Room = room;
                    t.Comments = comment;
                    return true;
                }
            }
            return false;
        }

        List<Lesson> FindLessons(EvaluateLesson comp)
        {
            List<Lesson> lessons = new List<Lesson>();
            foreach (var tt in this.lessonList.List)
            {
                Lesson t = tt as Lesson;
                if (comp(t))
                    lessons.Add(t);
            }
            return lessons;
        }

        List<Lesson> FindLessonSlots(
            string state,
            string student,
            string teacher,
            string room,
            string program,
            DateTime t1,
            DateTime t2)
        {
            List<Lesson> lessons = new List<Lesson>();
            foreach (var tt in this.lessonList.List)
            {
                Lesson t = tt as Lesson;
                if (t.DateTimeStart >= t2 || t.DateTimeEnd < t1)
                    continue;
                if (state != null && state.Trim().Length > 1 &&
                    state != t.State)
                    continue;
                if (room != null && room.Trim().Length > 1 &&
                    room != t.Room)
                    continue;
                if (program != null && program.Trim().Length > 1 &&
                    program != t.Program)
                    continue;
                if (teacher != null && teacher.Trim().Length > 1 &&
                    teacher != t.Teacher1 && teacher != t.Teacher2)
                    continue;
                if (student != null && student.Trim().Length > 1 &&
                    student != t.Student1 && student != t.Student2 &&
                    student != t.Student3 && student != t.Student4 &&
                    student != t.Student5 && student != t.Student6 &&
                    student != t.Student7 && student != t.Student8 &&
                    student != t.Student9 && student != t.Student10)
                    continue;

                lessons.Add(t);
            }
            return lessons;
        }

        List<GCal.CalEvent> GetEventsForPublishing(DateTime dtMin, DateTime dtMax)
        {
            List<GCal.CalEvent> evts = new List<GCal.CalEvent>();
            foreach (Lesson l in LessonsByTime(dtMin, dtMax, true))
            {
                evts.Add(new GCal.CalEvent(
                                    l.DateTimeStart,
                                    l.DateTimeEnd,
                                    l.Room,
                                    l.Details,
                                    l.Description));
            }

            return evts;
        }

        public bool SetCurrentOperationalEvent()
        {
            if (m_OperationalEvents == null ||
                m_curOperationalevent < 0 ||
                m_curOperationalevent >= m_OperationalEvents.Length)
                return false;

            GCal.CalEvent ce = m_OperationalEvents[m_curOperationalevent];

            labellbReconcileX.Text = (m_curOperationalevent+1).ToString();
            labellbReconcileY.Text = m_OperationalEvents.Length.ToString();

            lbReconcileDate.Text = ce.Start.ToShortDateString();
            lbReconcileFrom.Text = ce.Start.ToShortTimeString();
            lbReconcileTo.Text = ce.End.ToShortTimeString();
            lbReconcileLocation.Text = (ce.Location == null ? "" : ce.Location);
            lbReconcileDescription.Text = (ce.Summary == null ? "" : ce.Summary);
            lbReconcileCreated.Text = (ce.Creator == null ? "" : ce.Creator);
            lbReconcileGoogleCalId.Text = (ce.Id == null ? "" : ce.Id);
            lbReconcileTouched.Text = ce.LastTouched;

            double similarity = -1.0;
            bool found = FindClosestLesson(out similarity);
            Lesson l = lessonList.Current as Lesson;
            if (found && l != null)
            {
                lbReconcileLocationDiff.Visible = !AreRoomsEquivalent(
                                                    lbReconcileLocation.Text, l.Room);
                lbReconcileDateDiff.Visible = !AreStringsEquivalent(
                                                    lbReconcileDate.Text, l.Day);
                lbReconcileFromDiff.Visible = !AreStringsEquivalent(
                                                    lbReconcileFrom.Text, l.Start);
                lbReconcileToDiff.Visible = !AreStringsEquivalent(
                                                    lbReconcileTo.Text, l.End);
                lbReconcileDescDiff.Visible = !AreStringsEquivalent(
                                                    lbReconcileDescription.Text, l.Comments); ;
            }
            else
            {
                lbReconcileLocationDiff.Visible = false;
                lbReconcileDateDiff.Visible = false;
                lbReconcileFromDiff.Visible = false;
                lbReconcileToDiff.Visible = false;
                lbReconcileDescDiff.Visible = false;
            };

            MatchingState ms = MatchingState.NoMatch;
            if (found)
            {
                if (l.GoogleId == ce.Id)
                    ms = MatchingState.Linked;
                else if (similarity == -1.0)
                    ms = MatchingState.NoMatch;
                else if (similarity > 0.9)
                    ms = MatchingState.Match;
            }
            ShowMatching(ms, similarity);
            return found;
        }

        public void ShowMatching(MatchingState ms, double similarity)
        {
            switch(ms)
            {
                case MatchingState.Linked:
                    lbReconcileResult.Text = "Linked";
                    lbReconcileResult.BackColor = Color.Blue;
                    lbReconcileResult.ForeColor = Color.Yellow;
                    break;
                case MatchingState.Match:
                    lbReconcileResult.Text = "Match";
                    lbReconcileResult.BackColor = Color.DarkGreen;
                    lbReconcileResult.ForeColor = Color.Yellow;
                    break;
                case MatchingState.NoMatch:
                    lbReconcileResult.Text = "No match";
                    lbReconcileResult.BackColor = Color.Red;
                    lbReconcileResult.ForeColor = Color.Yellow;
                    break;
                case MatchingState.Similar:
                    lbReconcileResult.Text = "Similar";
                    lbReconcileResult.BackColor = Color.FromArgb(0, (int)(128 * (2.0 - similarity)), 0);
                    lbReconcileResult.ForeColor = ComplementColor(lbReconcileResult.BackColor);
                    break;
                default:
                    throw new Exception("Bad params");
            }

        }
        public bool FindClosestLesson(out double similarity)
        {
            similarity = -1.0;

            if (m_OperationalEvents == null ||
                m_curOperationalevent < 0 ||
                m_curOperationalevent >= m_OperationalEvents.Length)
                return false;

            GCal.CalEvent ge = m_OperationalEvents[m_curOperationalevent];
            List<OperEvent> candidates = new List<OperEvent>();

            foreach (Lesson l in lessonList)
            {
                OperEvent oe = new OperEvent(l, ge);
                oe.CalculateSimilarities();
                if (oe.OverallSimularity > 0.3)
                    candidates.Add(oe);
            }

            if (candidates.Count == 0)
                return false;

            OperEvent best = candidates.First();
            foreach (OperEvent oe in candidates)
            {
                if (oe.OverallSimularity > best.OverallSimularity)
                    best = oe;
            }

            int i = 0;
            foreach(Lesson l in lessonList)
            {
                if (l == best.SuspectedLesson)
                {
                    lessonList.Position = i;
                    similarity = best.OverallSimularity;
                    return true;
                }
                i++;
            }
            return false;
        }

        public ComboBox LessonTeacherComboBox(int i)
        {
            ComboBox[] boxes = new ComboBox[] { cbLessonTeacher1, cbLessonTeacher2 };
            if (i < 1 || i > 2)
                throw new Exception("Bad params");
            return boxes[i - 1];
        }
        public ComboBox LessonStudentComboBox(int i)
        {
            ComboBox[] boxes = new ComboBox[]
            { cbLessonStudent1, cbLessonStudent2, cbLessonStudent3,
              cbLessonStudent4, cbLessonStudent5, cbLessonStudent6,
              cbLessonStudent7, cbLessonStudent8, cbLessonStudent9, cbLessonStudent10 };

            if (i < 1 || i > 10) 
                throw new Exception("Bad params");
            return boxes[i - 1];
        }

        public void FillLessonFromCalendar(Lesson l)
        {
            SetComboByValue(cbLessonStart, lbReconcileFrom.Text);
            l.Start = lbReconcileFrom.Text;

            SetComboByValue(cbLessonEnd, lbReconcileTo.Text);
            l.End = lbReconcileTo.Text;

            DateTime dt = DateTime.Now;
            DateTime.TryParse(lbReconcileDate.Text, out dt);
            monthCalLessonDate.SetDate(dt);
            l.Day = dt.ToShortDateString();

            tbLessonComment.Text = lbReconcileDescription.Text;
            l.Comments = lbReconcileDescription.Text;

            l.Room = SetComboByInitial(cbLessonRoom, lbReconcileLocation.Text);

            l.GoogleId = lbReconcileGoogleCalId.Text;

            l.State = SetComboByValue(cbLessonState, 
                                      m_enumState[(int)LessonStates.Planned]);


            string geTitle, geComment;
            string[] geStudents, geTeachers;

            ParseEventDescription( lbReconcileDescription.Text,
                out geTitle, out geStudents, out geTeachers, out geComment);

            for (int i = 0; i < 2 && i < geTeachers.Length; i++)
            {
                string str = ExtractTeacher(geTeachers[i], i+1);
                if (!IsStringEmpty(str))
                    l.SetTeacher(SetComboByValue(LessonTeacherComboBox(i+1), str), i+1);
            }

            for (int i = 0; i < 10 && i < geStudents.Length; i++)
            {
                string str = ExtractStudent(geStudents[i], i+1);
                if (!IsStringEmpty(str))
                    l.SetStudent(SetComboByValue(LessonStudentComboBox(i+1), str), i+1);
            }

            if (!IsStringEmpty(l.Student1))
                PopulateLessonProgPrice(l);
        }

        void PopulateLessonProgPrice(Lesson l)
        {
            foreach (Student stud in SpecificStudent(l.Student1))
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (!IsStringEmpty(stud.Program(i)))
                    {
                        l.Program = SetComboByValue(cbLessonProg, stud.Program(i));
                        tbLessonPrice.Text = stud.Price(i);
                        l.Price = stud.Price(i);
                        return;
                    }
                }
            }
        }


        private void TakeDistance(int d, string str, ref int mindist, ref string bestval)
        {
            if (d >= 0 && d < mindist)
            {
                mindist = d;
                bestval = str;
            }
        }
        public string ExtractPerson(ComboBox cb, string desc)
        {
            if (IsStringEmpty(desc))
                return null;
            string first, last, initial;
            int space = desc.IndexOf(" ");
            if (space >= 0)
            {
                first = (space == 0 ? "" : desc.Substring(0, space));
                last = (space < desc.Length - 1 ? desc.Substring(space + 1) : "");
                initial = (last.Length > 0 ? last.Substring(0, 1) : "");
            }
            else
            {
                first = desc;
                last = "";
                initial = "";
            }

            // Find for the best edit distance across 3 choices: 
            //    "first", "first last", "first initial"
            int mindist = int.MaxValue;
            string bestval = null;
            foreach(string str in cb.Items)
            {
                if (IsStringEmpty(str))
                    continue;
                string[] strx = str.Split(' ');
                string str1 = strx[0];
                string str2 = (strx.Length > 1 ? strx[strx.Length - 1] : "");
                string stri = (str2.Length > 0 ? str2.Substring(0,1) : "");

                if (!IsStringEmpty(first))
                {
                    int d = LevensteinDistance(
                        str1.Trim().ToLower(), 
                        first.Trim().ToLower());

                    TakeDistance(d, str, ref mindist, ref bestval);

                    if (!IsStringEmpty(last))
                    {
                        d = LevensteinDistance(
                            (str1.Trim()  + " " + str2.Trim()).ToLower(), 
                            (first.Trim() + " " + last.Trim()).ToLower());

                        TakeDistance(d, str, ref mindist, ref bestval);

                        d = LevensteinDistance(
                            (str1.Trim()  + " " + stri).ToLower(),
                            (first.Trim() + " " + initial).ToLower());

                        TakeDistance(d, str, ref mindist, ref bestval);
                    }

                }
                else if (!IsStringEmpty(last))
                {
                    int d = LevensteinDistance(
                            str2.Trim().ToLower(),
                            last.Trim().ToLower());

                    TakeDistance(d, str, ref mindist, ref bestval);
                }
            }

            // we allow up to 2 typos 
            if (mindist < 0 || mindist > 2)
                return null;
            else
                return bestval;

        }
        

        public string ExtractStudent(string desc, int ind)
        {
            return ExtractPerson( LessonStudentComboBox(ind), desc );
        }
        public string ExtractTeacher(string desc, int ind)
        {
            return ExtractPerson(LessonTeacherComboBox(ind), desc);
        }


    }
}