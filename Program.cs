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
        public string Name { get; set; }
        public string Price { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }

        public override Modes Mode { get { return Modes.Programs; } }
        public override string Description
        {
            get { return Name; }
        }
        public override string Abbreviation
        {
            get { return Code; }
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
                case "Code":
                    this.Code = value;
                    break;
                case "Language":
                    this.Language = value;
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
                case "Type":
                    this.Type = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public Program()
        {
            Code = "";
            Language = "";
            Name = "";
            Price = "0";
            Summary = "";
            Type = "";
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
                case "Name":
                    return Name;
                case "Price":
                    return Price;
                case "Summary":
                    return Summary;
                case "Type":
                    return Type;
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

                if (FormGlob.IsStringEmpty(Name))
                    return false;
                if (FormGlob.IsStringEmpty(Price))
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
            // TODO
            return null;
        }

        #region "Comparers"
        public class ComparerByCode : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Code.CompareTo(x.Code);
            }
        }
        public class ComparerByName : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Name.CompareTo(x.Name);
            }
        }
        
        public class ComparerByType : IComparer<Program>
        {
            public int Compare(Program y, Program x)
            {
                return y.Type.CompareTo(x.Type);
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