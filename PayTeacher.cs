using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class PayTeacher : Record
    {
        public string Teacher { get; set; }
        public string Day { get; set; }
        public string Sum { get; set; }
        public DateTime PaymentDate
        {
            get
            {
                if (FormGlob.IsStringEmpty(Day))
                    return DateTime.MinValue;

                DateTime dtd = DateTime.Parse(Day);
                return new DateTime(dtd.Year, dtd.Month, dtd.Day);
            }
            set
            {
                Day = value.ToShortDateString();
            }

        }

        public override Modes Mode { get { return Modes.PayTeachers; } }
        public override string Description
        {
            get { return Teacher + " : " + Sum; }
        }
        public override string Abbreviation
        {
            get { return Sum; }
        }

        public override bool Actual
        {
            get
            {
                return true;
            }
        }
        public override bool Set(string field, string fieldValue)
        {
            string value = SetRecordFields(field, fieldValue);
            if (value.Length == 0)
                return true;
            switch (field)
            {
                case "Teacher":
                    this.Teacher = value;
                    break;
                case "Day":
                    this.Day = value;
                    break;
                case "Sum":
                    this.Sum = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public PayTeacher()
        {
            Teacher = "";
            Day = "";
            Sum = "";
        }

        public override string Get(string field)
        {
            string v = GetRecordFields(field);
            if (v != null)
                return v;

            switch (field)
            {
                case "Teacher":
                    return Teacher;
                case "Day":
                    return Day;
                case "Sum":
                    return Sum;
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

                if (FormGlob.IsStringEmpty(Teacher))
                    return false;
                if (FormGlob.IsStringEmpty(Day))
                    return false;
                if (FormGlob.IsStringEmpty(Sum))
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string Validate2FirstProblem()
        {
            DateTime dt = DateTime.Now;
            if (FormGlob.IsStringEmpty(Day) || !DateTime.TryParse(Day, out dt))
                return "PayTeacher has no valid Day";
            double d = 0;
            if (FormGlob.IsStringEmpty(Day) || !double.TryParse(Sum, out d))
                return "PayTeacher has no valid Sum";
            if (FormGlob.IsStringEmpty(Teacher))
                return "PayTeacher has no Teacher";

            return null;
        }

        #region "Comparers"
        public class ComparerByTeacher: IComparer<PayTeacher>
        {
            public int Compare(PayTeacher y, PayTeacher x)
            {
                return y.Teacher.CompareTo(x.Teacher);
            }
        }
        public class ComparerByDay : IComparer<PayTeacher>
        {
            public int Compare(PayTeacher y, PayTeacher x)
            {
                return DateTime.Parse(y.Day).CompareTo(DateTime.Parse(x.Day));
            }
        }
        public class ComparerBySum : IComparer<PayTeacher>
        {
            public int Compare(PayTeacher y, PayTeacher x)
            {
                return double.Parse(y.Sum).CompareTo(double.Parse(x.Sum));
            }
        }
        #endregion
    }
}