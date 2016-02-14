using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class Teacher : Record
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Birthday { get; set; }
        public String Language { get; set; }
        public String Language2 { get; set; }
        public String LanguageDetail { get; set; }
        public string MailingAddress { get; set; }
        public String Vacations { get; set; }
        public String Monday { get; set; }
        public String Tuesday { get; set; }
        public String Wednesday { get; set; }
        public String Thursday { get; set; }
        public String Friday { get; set; }
        public String Saturday { get; set; }
        public String Sunday { get; set; }

        public override string Description
        {
            get { return FirstName + " " + LastName; }
        }
        public override string Abbreviation
        {
            get
            {
                return ((FirstName.Length > 0 ? FirstName.Substring(0, 1) : "X") +
                        (LastName.Length  > 0 ? LastName.Substring(0, 1)  : "Y"));
            }
        }

        public override bool Set(string field, string fieldValue)
        {
            string value = SetRecordFields(field, fieldValue);
            if (value.Length == 0)
                return true;
            switch (field)
            {
                case "Birthday":
                    this.Birthday = value;
                    break;
                case "Email":
                    this.Email = value;
                    break;
                case "FirstName":
                    this.FirstName = value;
                    break;
                case "LastName":
                    this.LastName = value;
                    break;
                case "Language":
                    this.Language = value;
                    break;
                case "Language2":
                    this.Language2 = value;
                    break;
                case "LanguageDetail":
                    this.LanguageDetail = value;
                    break;
                case "MailingAddress":
                    this.MailingAddress = value;
                    break;
                case "Phone":
                    this.Phone = value;
                    break;
                case "Status":
                    this.Status = value;
                    break;
                case "Vacations":
                    this.Vacations = value;
                    break;
                case "Monday":
                    this.Monday = value;
                    break;
                case "Tuesday":
                    this.Tuesday = value;
                    break;
                case "Wednesday":
                    this.Wednesday = value;
                    break;
                case "Thursday":
                    this.Thursday = value;
                    break;
                case "Friday":
                    this.Friday = value;
                    break;
                case "Saturday":
                    this.Saturday = value;
                    break;
                case "Sunday":
                    this.Sunday = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public Teacher()
        {
            Birthday = "";
            Email = "";
            FirstName = "";
            LastName = "";
            Language = "?";
            Language2 = "?";
            LanguageDetail = "";
            MailingAddress = "";
            Phone = "";
            Status = "?";
            Vacations = "";
            Monday="";
            Tuesday="";
            Wednesday="";
            Thursday="";
            Friday="";
            Saturday="";
            Sunday="";
        }

        public override string Get(string field)
        {
            string v = GetRecordFields(field);
            if (v != null)
                return v;

            switch (field)
            {
                case "Birthday":
                    return this.Birthday;
                case "Email":
                    return this.Email;
                case "FirstName":
                    return this.FirstName;
                case "LastName":
                    return this.LastName;
                case "Language":
                    return this.Language;
                case "Language2":
                    return this.Language2;
                case "LanguageDetail":
                    return this.LanguageDetail;
                case "MailingAddress":
                    return this.MailingAddress;
                case "Phone":
                    return this.Phone;
                case "Status":
                    return this.Status;
                case "Vacations":
                    return this.Vacations;
                case "Monday":
                    return this.Monday;
                case "Tuesday":
                    return this.Tuesday;
                case "Wednesday":
                    return this.Wednesday;
                case "Thursday":
                    return this.Thursday;
                case "Friday":
                    return this.Friday;
                case "Saturday":
                    return this.Saturday;
                case "Sunday":
                    return this.Sunday;
                default:
                    throw new Exception("unknown field " + field);
            }
        }

        #region "Comparers"
        public class ComparerByBirthday : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.Birthday.CompareTo(x.Birthday);
            }
        }
        public class ComparerByEmail : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.Email.CompareTo(x.Email);
            }
        }
        public class ComparerByFirstName : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.FirstName.CompareTo(x.FirstName);
            }
        }
        public class ComparerByLastName : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.LastName.CompareTo(x.LastName);
            }
        }
        public class ComparerByLanguage : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.Language.CompareTo(x.Language);
            }
        }
        public class ComparerByLanguage2 : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.Language2.CompareTo(x.Language2);
            }
        }
        public class ComparerByLanguageDetail : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.LanguageDetail.CompareTo(x.LanguageDetail);
            }
        }
        public class ComparerByMailingAddress : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.MailingAddress.CompareTo(x.MailingAddress);
            }
        }
        public class ComparerByPhone : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.Phone.CompareTo(x.Phone);
            }
        }
        public class ComparerByStatus : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.Status.CompareTo(x.Status);
            }
        }
        public class ComparerByVacations : IComparer<Teacher>
        {
            public int Compare(Teacher y, Teacher x)
            {
                return y.Vacations.CompareTo(x.Vacations);
            }
        }
        #endregion
    }
}