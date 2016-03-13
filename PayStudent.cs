using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class PayStudent : Record
    {
        public string Student { get; set; }
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

        public override Modes Mode { get { return Modes.PayStudents; } }
        public override string Description
        {
            get { return Student + " : " + Sum; }
        }
        public override string Abbreviation
        {
            get { return Student; }
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
                case "Student":
                    this.Student = value;
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

        public PayStudent()
        {
            Student = "";
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
                case "Student":
                    return Student;
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

                if (FormGlob.IsStringEmpty(Student))
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
                return "PayStudent has no valid Day";
            double d = 0;
            if (FormGlob.IsStringEmpty(Day) || !double.TryParse(Sum, out d))
                return "PayStudent has no valid Sum";
            if (FormGlob.IsStringEmpty(Student))
                return "PayStudent has no Student";

            return null;
        }

        #region "Comparers"
        public class ComparerByStudent: IComparer<PayStudent>
        {
            public int Compare(PayStudent y, PayStudent x)
            {
                return y.Student.CompareTo(x.Student);
            }
        }
        public class ComparerByDay : IComparer<PayStudent>
        {
            public int Compare(PayStudent y, PayStudent x)
            {
                return DateTime.Parse(y.Day).CompareTo(DateTime.Parse(x.Day));
            }
        }
        public class ComparerBySum : IComparer<PayStudent>
        {
            public int Compare(PayStudent y, PayStudent x)
            {
                return double.Parse(y.Sum).CompareTo(double.Parse(x.Sum));
            }
        }
        #endregion
    }
}