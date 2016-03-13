using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class PayExpense : Record
    {
        public string Day { get; set; }
        public string Sum { get; set; }
        public string Category { get; set; }
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

        public override Modes Mode { get { return Modes.PayExpenses; } }
        public override string Description
        {
            get { return Day + " : " + Sum; }
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
                case "Day":
                    this.Day = value;
                    break;
                case "Sum":
                    this.Sum = value;
                    break;
                case "Category":
                    this.Category = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public PayExpense()
        {
            Day = "";
            Sum = "";
            Category = "";
        }

        public override string Get(string field)
        {
            string v = GetRecordFields(field);
            if (v != null)
                return v;

            switch (field)
            {
                case "Day":
                    return Day;
                case "Sum":
                    return Sum;
                case "Category":
                    return Category;
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

                if (FormGlob.IsStringEmpty(Sum))
                    return false;
                if (FormGlob.IsStringEmpty(Day))
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
                return "PayExpense has no Day";
            double d = 0;
            if (FormGlob.IsStringEmpty(Day) || !double.TryParse(Sum, out d))
                return "PayExpense has no Sum";
            if (FormGlob.IsStringEmpty(Category))
                return "PayExpense has no Category";
            return null;
        }

        #region "Comparers"
        public class ComparerByDay : IComparer<PayExpense>
        {
            public int Compare(PayExpense y, PayExpense x)
            {
                return DateTime.Parse(y.Day).CompareTo(DateTime.Parse(x.Day));  
            }
        }
        public class ComparerBySum : IComparer<PayExpense>
        {
            public int Compare(PayExpense y, PayExpense x)
            {
                return double.Parse(y.Sum).CompareTo(double.Parse(x.Sum));  
            }
        }
        
        public class ComparerByCategory : IComparer<PayExpense>
        {
            public int Compare(PayExpense y, PayExpense x)
            {
                return y.Category.CompareTo(x.Category);
            }
        }
        #endregion
    }
}