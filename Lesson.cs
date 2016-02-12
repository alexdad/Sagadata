using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class Lesson : Record
    {
        public string Name { get; set; }
        public override string Description
        {
            get { return Name; }
        }
        public override string Abbreviation
        {
            get
            {
                return (Name.Length > 0 ? Name.Substring(0, 1) : "X");
            }
        }

        public override bool Set(string field, string fieldValue)
        {
            string value = SetRecordFields(field, fieldValue);
            if (value.Length == 0)
                return true;
            switch (field)
            {
                case "Name":
                    this.Name = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public Lesson()
        {
            Name = "?";
        }

        public override string Get(string field)
        {
            string v = GetRecordFields(field);
            if (v != null)
                return v;

            switch (field)
            {
                case "Name":
                    return Name;
                default:
                    throw new Exception("unknown field " + field);

            }
        }

        #region "Comparers"
        public class ComparerByName : IComparer<Room>
        {
            public int Compare(Room y, Room x)
            {
                return y.Name.CompareTo(x.Name);
            }
        }
        #endregion
    }
}