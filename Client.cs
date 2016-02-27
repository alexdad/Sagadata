using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class Client : Record
    {
        public string Code { get; set; }
        public string MachineName { get; set; }
        public string LastTouch { get; set; }

        public override string Description
        {
            get { return MachineName; }
        }

        public override string Abbreviation
        {
            get
            {
                return Code;
            }
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
                case "MachineName":
                    this.MachineName = value;
                    break;
                case "LastTouch":
                    this.LastTouch = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public Client()
        {
            Code = "?";
            MachineName = "";
            LastTouch = "";
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
                case "MachineName":
                    return MachineName;
                case "LastTouch":
                    return LastTouch;
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

                if (FormGlob.IsStringEmpty(Code))
                    return false;
                if (FormGlob.IsStringEmpty(MachineName))
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region "Comparers"
        public class ComparerByCode : IComparer<Client>
        {
            public int Compare(Client y, Client x)
            {
                return y.Code.CompareTo(x.Code);
            }
        }
        public class ComparerByMachineName: IComparer<Client>
        {
            public int Compare(Client y, Client x)
            {
                return y.MachineName.CompareTo(x.MachineName);
            }
        }
        public class ComparerByLastTouch: IComparer<Client>
        {
            public int Compare(Client y, Client x)
            {
                return y.LastTouch.CompareTo(x.LastTouch);
            }
        }
        #endregion
    }
}