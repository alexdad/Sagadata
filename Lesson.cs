using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class Lesson : Record
    {
        public string Day { get; set; }
        public string Program { get; set; }
        public string Room { get; set; }
        public string State { get; set; }
        public string Student1{ get; set; }
        public string Student2 { get; set; }
        public string Student3 { get; set; }
        public string Student4 { get; set; }
        public string Student5 { get; set; }
        public string Student6 { get; set; }
        public string Student7 { get; set; }
        public string Student8 { get; set; }
        public string Student9 { get; set; }
        public string Student10 { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Teacher1 { get; set; }
        public string Teacher2 { get; set; }
        public string CancellationTime { get; set; }
        public string Price { get; set; }

        public DateTime DateTimeStart
        {
            get
            {
                DateTime dtd = DateTime.Parse(Day);
                DateTime dts = DateTime.Parse(Start);
                return new DateTime(dtd.Year, dtd.Month, dtd.Day, dts.Hour, dts.Minute, 0);
            }
        }

        public DateTime DateTimeEnd
        {
            get
            {
                DateTime dtd = DateTime.Parse(Day);
                DateTime dte = DateTime.Parse(End);
                return new DateTime(dtd.Year, dtd.Month, dtd.Day, dte.Hour, dte.Minute, 0);
            }
        }

        public string DurationString
        {
            get
            {
                return FormGlob.GetDurationString(DateTimeStart, DateTimeEnd);
            }
        }

        public override string Description
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(FormGlob.ExtractFirstWord(this.Student1));
                sb.Append(" / ");
                sb.Append(FormGlob.ExtractFirstWord(this.Teacher1));
                return sb.ToString();
            }
        }

        public  string ShortDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(State.Substring(0, 1));
                
                if (this.Student1 != null)
                {
                    sb.Append(":");
                    sb.Append(FormGlob.ExtractFirstWord(this.Student1));
                }
                return sb.ToString();
            }
        }

        public int SlotsNumber
        {
            get
            {
                return FormGlob.Slots(DateTimeStart, DateTimeEnd);
            }
        }


        public void GetLocationInWeek(out int col, out int row1, out int row2)
        {
            col = FormGlob.StandardizeDayOfTheWeek(DateTimeStart.DayOfWeek);
            row1 = DateTimeStart.Hour * 4 + DateTimeStart.Minute / 15 - 7 * 4;
            row2 = DateTimeEnd.Hour * 4 + DateTimeEnd.Minute / 15 - 7 * 4;

        }
        public void GetLocationInMonth(out int col, out int row1, out int row2)
        {
            col = DateTimeStart.Day - 1;
            row1 = DateTimeStart.Hour * 4 + DateTimeStart.Minute / 15 - 7 * 4;
            row2 = DateTimeEnd.Hour * 4 + DateTimeEnd.Minute / 15 - 7 * 4;

        }

        public override string Abbreviation
        {
            get { return ("X"); }
        }

        public override bool Set(string field, string fieldValue)
        {
            string value = SetRecordFields(field, fieldValue);
            if (value.Length == 0)
                return true;
            switch (field)
            {
                case "Day":
                    this.Day = value;
                    break;
                case "End":
                    this.End = value;
                    break;
                case "Program":
                    this.Program = value;
                    break;
                case "Room":
                    this.Room = value;
                    break;
                case "Start":
                    this.Start = value;
                    break;
                case "State":
                    this.State = value;
                    break;
                case "Student1":
                    this.Student1 = value;
                    break;
                case "Student2":
                    this.Student2 = value;
                    break;
                case "Student3":
                    this.Student3 = value;
                    break;
                case "Student4":
                    this.Student4 = value;
                    break;
                case "Student5":
                    this.Student5 = value;
                    break;
                case "Student6":
                    this.Student6 = value;
                    break;
                case "Student7":
                    this.Student7 = value;
                    break;
                case "Student8":
                    this.Student8 = value;
                    break;
                case "Student9":
                    this.Student9 = value;
                    break;
                case "Student10":
                    this.Student10 = value;
                    break;
                case "Teacher1":
                    this.Teacher1 = value;
                    break;
                case "Teacher2":
                    this.Teacher2 = value;
                    break;
                case "CancellationTime":
                    this.CancellationTime = value;
                    break;
                case "Price":
                    this.Price = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public Lesson()
        {
             Day = "";
             End = "";
             Program = "";
             Room = "";
             State = "";
             Student1 = "";
             Student2 = "";
             Student3 = "";
             Student4 = "";
             Student5 = "";
             Student6 = "";
             Student7 = "";
             Student8 = "";
             Student9 = "";
             Student10 = "";
             Start = "";
             Teacher1 = "";
             Teacher2 = "";
             CancellationTime = "";
             Price = "";
    }

    public override string Get(string field)
        {
            string v = GetRecordFields(field);
            if (v != null)
                return v;

            switch (field)
            {
                case "Day":
                    return this.Day;
                case "End":
                    return this.End;
                case "Program":
                    return this.Program;
                case "Room":
                    return this.Room;
                case "Start":
                    return this.Start;
                case "State":
                    return this.State;
                case "Student1":
                    return this.Student1;
                case "Student2":
                    return this.Student2;
                case "Student3":
                    return this.Student3;
                case "Student4":
                    return this.Student4;
                case "Student5":
                    return this.Student5;
                case "Student6":
                    return this.Student6;
                case "Student7":
                    return this.Student7;
                case "Student8":
                    return this.Student8;
                case "Student9":
                    return this.Student9;
                case "Student10":
                    return this.Student10;
                case "Teacher1":
                    return this.Teacher1;
                case "Teacher2":
                    return this.Teacher2;
                case "CancellationTime":
                    return this.CancellationTime;
                case "Price":
                    return this.Price;

                default:
                    throw new Exception("unknown field " + field);
            }
        }

        public override bool Validate()
        {
            try
            {
                if (!ValidateBase)
                    return false;
                if (FormGlob.IsStringEmpty(Student1))
                    return false;
                if (FormGlob.IsStringEmpty(Teacher1))
                    return false;
                if (!FormGlob.IsDateTimeReasonable(DateTimeStart))
                    return false;
                if (!FormGlob.IsDateTimeReasonable(DateTimeEnd))
                    return false;

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }


        #region "Comparers"
        public class ComparerByDay : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Day.CompareTo(x.Day);
            }
        }
        public class ComparerByEnd : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.End.CompareTo(x.End);
            }
        }
        public class ComparerByProgram : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Program.CompareTo(x.Program);
            }
        }
        public class ComparerByRoom : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Room.CompareTo(x.Room);
            }
        }
        public class ComparerByState: IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.State.CompareTo(x.State);
            }
        }
        public class ComparerByStudent1 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student1.CompareTo(x.Student1);
            }
        }
        public class ComparerByStudent2 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student2.CompareTo(x.Student2);
            }
        }
        public class ComparerByStudent3 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student3.CompareTo(x.Student3);
            }
        }
        public class ComparerByStudent4 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student4.CompareTo(x.Student4);
            }
        }
        public class ComparerByStudent5 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student5.CompareTo(x.Student5);
            }
        }
        public class ComparerByStudent6 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student6.CompareTo(x.Student6);
            }
        }
        public class ComparerByStudent7 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student7.CompareTo(x.Student7);
            }
        }
        public class ComparerByStudent8 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student8.CompareTo(x.Student8);
            }
        }
        public class ComparerByStudent9 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student9.CompareTo(x.Student9);
            }
        }
        public class ComparerByStudent10 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Student10.CompareTo(x.Student10);
            }
        }
        public class ComparerByStart : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Start.CompareTo(x.Start);
            }
        }
        public class ComparerByTeacher1 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Teacher1.CompareTo(x.Teacher1);
            }
        }
        public class ComparerByTeacher2 : IComparer<Lesson>
        {
            public int Compare(Lesson y, Lesson x)
            {
                return y.Teacher2.CompareTo(x.Teacher2);
            }
        }
        #endregion
    }
}