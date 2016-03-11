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
        public string GoogleId { get; set; }
        public string RepeaterKey { get; set; }
        public DateTime DateTimeStart
        {
            get
            {
                if (FormGlob.IsStringEmpty(Day) ||
                    FormGlob.IsStringEmpty(Start))
                    return DateTime.MinValue;

                DateTime dtd = DateTime.Parse(Day);
                DateTime dts = DateTime.Parse(Start);
                return new DateTime(dtd.Year, dtd.Month, dtd.Day, dts.Hour, dts.Minute, 0);
            }
        }

        public DateTime DateTimeEnd
        {
            get
            {
                if (FormGlob.IsStringEmpty(Day) ||
                    FormGlob.IsStringEmpty(End))
                    return DateTime.MinValue;

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
                string st1 = FormGlob.ExtractFirstNameAndInitial(this.Student1);
                string st2 = FormGlob.ExtractFirstNameAndInitial(this.Student2);
                string st3 = FormGlob.ExtractFirstNameAndInitial(this.Student3);
                string tc1 = FormGlob.ExtractFirstNameAndInitial(this.Teacher1);
                string tc2 = FormGlob.ExtractFirstNameAndInitial(this.Teacher1);

                int count = 0;
                StringBuilder sb = new StringBuilder();
                if (!FormGlob.IsStringEmpty(st1))
                {
                    sb.Append(st1);
                    count++;
                }
                if (!FormGlob.IsStringEmpty(st2))
                {
                    if (count > 0)
                        sb.Append(",");
                    sb.Append(st2);
                    count++;
                }
                if (!FormGlob.IsStringEmpty(st3))
                {
                    if (count > 0)
                        sb.Append(",");
                    sb.Append(st3);
                    count++;
                }
                sb.Append(" / ");
                sb.Append(FormGlob.ExtractFirstNameAndInitial(this.Teacher1));

                if (sb.ToString().Length < 24 && !FormGlob.IsStringEmpty(this.Comments))
                {
                    sb.Append("; ");
                    string cm = (this.Comments.Length < 50 ?
                                 this.Comments :
                                 this.Comments.Substring(0, 49));
                    sb.Append(cm);
                }
                return sb.ToString();
            }
        }

        public string ShortDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!FormGlob.IsStringEmpty(State))
                    sb.Append(State.Substring(0, 1));
                
                if (this.Student1 != null)
                {
                    sb.Append(":");
                    sb.Append(FormGlob.ExtractFirstNameAndInitial(this.Student1));
                }
                return sb.ToString();
            }
        }

        public string Details
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(Program);
                sb.Append("  /  ");
                sb.Append(Comments);

                if (!FormGlob.IsStringEmpty(Teacher2))
                {
                    sb.Append(" / Also with ");
                    sb.Append(Teacher2);
                }

                sb.Append(";  Students: ");
                sb.Append(Student1);
                if (!FormGlob.IsStringEmpty(Student2))
                {
                    sb.Append(" + ");
                    sb.Append(Student2);
                }
                if (!FormGlob.IsStringEmpty(Student3))
                {
                    sb.Append(" + ");
                    sb.Append(Student3);
                }
                if (!FormGlob.IsStringEmpty(Student4))
                {
                    sb.Append(" + ");
                    sb.Append(Student4);
                }
                if (!FormGlob.IsStringEmpty(Student5))
                {
                    sb.Append(" + ");
                    sb.Append(Student5);
                }
                if (!FormGlob.IsStringEmpty(Student6))
                {
                    sb.Append(" + ");
                    sb.Append(Student6);
                }
                if (!FormGlob.IsStringEmpty(Student7))
                {
                    sb.Append(" + ");
                    sb.Append(Student7);
                }
                if (!FormGlob.IsStringEmpty(Student8))
                {
                    sb.Append(" + ");
                    sb.Append(Student8);
                }
                if (!FormGlob.IsStringEmpty(Student9))
                {
                    sb.Append(" + ");
                    sb.Append(Student9);
                }
                if (!FormGlob.IsStringEmpty(Student10))
                {
                    sb.Append(" + ");
                    sb.Append(Student10);
                }
                sb.Append(" # REF: ");
                sb.Append(this.Key);

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

        public bool Linked
        {
            get
            {
                return (!FormGlob.IsStringEmpty(GoogleId));
            }
            set
            {
                if (value == false)
                    GoogleId = "";
                // Cannot set it from bool! 
            }
        }

        public string RepeaterSequenceKey
        {
            get
            {
                if (FormGlob.IsStringEmpty(RepeaterKey))
                    return Key;
                else
                    return RepeaterKey;
            }
        } 

        public void GetLocationInWeek(out int col, out int row1, out int row2)
        {
            col = FormGlob.StandardizeDayOfTheWeek(DateTimeStart.DayOfWeek);
            row1 = FormGlob.CalcSlot(DateTimeStart);
            row2 = FormGlob.CalcSlot(DateTimeEnd);
        }
        public void GetLocationInMonth(out int col, out int row1, out int row2)
        {
            col = DateTimeStart.Day - 1;
            row1 = FormGlob.CalcSlot(DateTimeStart);
            row2 = FormGlob.CalcSlot(DateTimeEnd);
        }

        public override string Abbreviation
        {
            get { return ("X"); }
        }

        public override bool Actual
        {
            get
            {
                switch (State)
                {
                    case "Planned":
                        return true;
                    case "Cancelled":
                        return false;
                    case "Done":
                        return false;
                    default:
                        return false;
                }
            }
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
                case "GoogleId":
                    this.GoogleId = value;
                    break;
                case "RepeaterKey":
                    this.RepeaterKey = value;
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
             GoogleId = "";
             RepeaterKey = "";
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
                case "GoogleId":
                    return this.GoogleId;
                case "RepeaterKey":
                    return this.RepeaterKey;

                default:
                    throw new Exception("unknown field " + field);
            }
        }

        public void CloneValuesTo(Lesson l)
        {
            l.Day = this.Day;
            l.End = this.End;
            l.Program = this.Program;
            l.Room = this.Room;
            l.State = this.State;
            l.Student1 = this.Student1;
            l.Student2 = this.Student2;
            l.Student3 = this.Student3;
            l.Student4 = this.Student4;
            l.Student5 = this.Student5;
            l.Student6 = this.Student6;
            l.Student7 = this.Student7;
            l.Student8 = this.Student8;
            l.Student9 = this.Student9;
            l.Student10 = this.Student10;
            l.Start = this.Start;
            l.Teacher1 = this.Teacher1;
            l.Teacher2 = this.Teacher2;
            l.Price = this.Price;
            l.CancellationTime = this.CancellationTime;
            l.GoogleId = "";
            l.RepeaterKey = this.RepeaterSequenceKey;
        }

        public override bool Validate()
        {
            try
            {
                if (!ValidateBase)
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

        public override string Validate2FirstProblem(FormGlob glob)
        {
            if (FormGlob.IsStringEmpty(Program))
                return "Lesson has no program"; ;
            string programType = glob.GetProgramType(Program);
            if (FormGlob.IsStringEmpty(Student1) && programType != "PriceFree") 
                return "Lesson has no first student";
            if (FormGlob.IsStringEmpty(Teacher1))
                return "Lesson has no teacher";
            if (FormGlob.IsStringEmpty(Day))
                return "Lesson has no date";
            DateTime start = DateTime.Now, end = DateTime.Now;
            if (FormGlob.IsStringEmpty(Start) || !DateTime.TryParse(Start, out start))
                return "Lesson has no start";
            if (FormGlob.IsStringEmpty(End) || !DateTime.TryParse(End, out end))
                return "Lesson has no end";
            if (start >= end)
                return "Lesson starts after its end";

            return null;
        }

        public override string ConcatenateAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Day);
            sb.Append(Program);
            sb.Append(Room);
            sb.Append(State);
            sb.Append(Student1);
            sb.Append(Student2);
            sb.Append(Student3);
            sb.Append(Student4);
            sb.Append(Student5);
            sb.Append(Student6);
            sb.Append(Student7);
            sb.Append(Student8);
            sb.Append(Student9);
            sb.Append(Student10);
            sb.Append(Teacher1);
            sb.Append(Teacher2);
            sb.Append(Start);
            sb.Append(End);
            sb.Append(CancellationTime);
            sb.Append(Price);
            sb.Append(GoogleId);
            sb.Append(RepeaterKey);
            sb.Append(Key);
            sb.Append(Comments);

            return sb.ToString();
        }

        public void SetStudent(string stud, int index)
        {
            switch(index)
            {
                case 1:
                    this.Student1 = stud;
                    return;
                case 2:
                    this.Student2 = stud;
                    return;
                case 3:
                    this.Student3 = stud;
                    return;
                case 4:
                    this.Student4 = stud;
                    return;
                case 5:
                    this.Student5 = stud;
                    return;
                case 6:
                    this.Student6 = stud;
                    return;
                case 7:
                    this.Student7 = stud;
                    return;
                case 8:
                    this.Student8 = stud;
                    return;
                case 9:
                    this.Student9 = stud;
                    return;
                case 10:
                    this.Student10 = stud;
                    return;
                default:
                    throw new Exception("Bad param");
            }
        }

        public void SetTeacher(string stud, int index)
        {
            switch (index)
            {
                case 1:
                    this.Teacher1 = stud;
                    return;
                case 2:
                    this.Teacher2 = stud;
                    return;
                default:
                    throw new Exception("Bad param");
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