using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class Program : Record
    {
        public string Code { get; set; }
        public string Language { get; set; }
        public string Level { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Summary { get; set; }

        public override string Description
        {
            get { return Name; }
        }
        public override string Abbreviation
        {
            get { return Code; }
        }

        public override bool Set(string field, string fieldValue)
        {
            string value = SetRecordFields(field, fieldValue);
            if (value.Length == 0)
                return true;
            switch (field)
            {
                case "Code":
                    this.Code = value;
                    break;
                case "Language":
                    this.Language = value;
                    break;
                case "Level":
                    this.Level = value;
                    break;
                case "Name":
                    this.Name = value;
                    break;
                case "Price":
                    this.Price = value;
                    break;
                case "Summary":
                    this.Summary = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public Program()
        {
            Code = "?";
            Name = "";
            Language = "?";
            Level = "?";
            Price = "50";
            Summary = "";
        }

        public override string Get(string field)
        {
            string v = GetRecordFields(field);
            if (v != null)
                return v;

            switch (field)
            {
                case "Code":
                    return Code;
                case "Language":
                    return Language;
                case "Level":
                    return Level;
                case "Name":
                    return Name;
                case "Price":
                    return Price;
                case "Summary":
                    return Summary;
                default:
                    throw new Exception("unknown field " + field);

            }
        }

        #region "Comparers"
        public class ComparerByCode : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Code.CompareTo(x.Code);
            }
        }
        public class ComparerByLanguage : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Language.CompareTo(x.Language);
            }
        }
        public class ComparerByLevel : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Level.CompareTo(x.Level);
            }
        }
        public class ComparerByName : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Name.CompareTo(x.Name);
            }
        }
        public class ComparerByPrice : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Price.CompareTo(x.Price);
            }
        }
        public class ComparerBySummary : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Summary.CompareTo(x.Summary);
            }
        }
        #endregion
    }
}