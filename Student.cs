using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class Student : Record
    {
        public string Background { get; set; }
        public String Birthday { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Goals { get; set; }
        public string HomePhone { get; set; }
        public string Interests { get; set; }
        public string LastName { get; set; }
        public String LearningLanguage { get; set; }
        public String LanguageDetail { get; set; }
        public String Level { get; set; }
        public string MailingAddress { get; set; }
        public String NativeLanguage { get; set; }
        public String OtherLanguage { get; set; }
        public string PossibleSchedule { get; set; }
        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public string Price3 { get; set; }
        public string Prog1 { get; set; }
        public string Prog2 { get; set; }
        public string Prog3 { get; set; }
        public string Source { get; set; }
        public string SourceDetail { get; set; }
        public string Status { get; set; }
        public string Phone
        {
            get { return (CellPhone.Length > 0 ? CellPhone : HomePhone); }
        }
        public override string Description
        {
            get { return FirstName + " " + LastName; }
        }
        public override string Abbreviation
        {
            get {
                return (
                    (FirstName.Length > 0 ? FirstName.Substring(0,1) : "X") +
                    (LastName.Length > 0 ? LastName.Substring(0, 1) : "X") ); }
        }

        public Student()
        {
            Background = "";
            Birthday = "";
            CellPhone = "";
            Email = "";
            FirstName = "";
            Goals = "";
            HomePhone = "";
            Interests = "";
            LastName = "";
            LearningLanguage = "";
            LanguageDetail = "";
            Level = "";
            MailingAddress = "";
            NativeLanguage = "";
            OtherLanguage = "";
            PossibleSchedule = "";
            Source = "";
            SourceDetail = "";
            Status = "Active";
            Prog1 = "";
            Price1 = "";
            Prog2 = "";
            Price2 = "";
            Prog3 = "";
            Price3 = "";
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
                case "Background":
                    this.Background = value;
                    break;
                case "CellPhone":
                    this.CellPhone = value;
                    break;
                case "EMail":
                    this.Email = value;
                    break;
                case "FirstName":
                    this.FirstName = value;
                    break;
                case "Goals":
                    this.Goals = value;
                    break;
                case "HomePhone":
                    this.HomePhone = value;
                    break;
                case "Interests":
                    this.Interests = value;
                    break;
                case "LanguageDetail":
                    this.LanguageDetail = value;
                    break;
                case "LastName":
                    this.LastName = value;
                    break;
                case "LearningLanguage":
                    this.LearningLanguage = value;
                    break;
                case "Level":
                    this.Level = value;
                    break;
                case "MailingAddress":
                    this.MailingAddress = value;
                    break;
                case "NativeLanguage":
                    this.NativeLanguage = value;
                    break;
                case "OtherLanguage":
                    this.OtherLanguage = value;
                    break;
                case "PossibleSchedule":
                    this.PossibleSchedule = value;
                    break;
                case "Price1":
                    this.Price1 = value;
                    break;
                case "Price2":
                    this.Price2 = value;
                    break;
                case "Price3":
                    this.Price3 = value;
                    break;
                case "Prog1":
                    this.Prog1 = value;
                    break;
                case "Prog2":
                    this.Prog2 = value;
                    break;
                case "Prog3":
                    this.Prog3 = value;
                    break;
                case "Source":
                    this.Source = value;
                    break;
                case "SourceDetail":
                    this.SourceDetail = value;
                    break;
                case "Status":
                    this.Status = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public override string Get(string field)
        {
            string v = GetRecordFields(field);
            if (v != null)
                return v;

            switch (field)
            {
                case "Background":
                    return Background;
                case "Birthday":
                    return Birthday;
                case "CellPhone":
                    return CellPhone;
                case "EMail":
                    return Email;
                case "FirstName":
                    return FirstName;
                case "Goals":
                    return Goals;
                case "HomePhone":
                    return HomePhone;
                case "Interests":
                    return Interests;
                case "LastName":
                    return LastName;
                case "LearningLanguage":
                    return LearningLanguage;
                case "LanguageDetail":
                    return LanguageDetail;
                case "Level":
                    return Level;
                case "MailingAddress":
                    return MailingAddress;
                case "NativeLanguage":
                    return NativeLanguage;
                case "OtherLanguage":
                    return OtherLanguage;
                case "PossibleSchedule":
                    return PossibleSchedule;
                case "Price1":
                    return Price1;
                case "Price2":
                    return Price2;
                case "Price3":
                    return Price3;
                case "Prog1":
                    return Prog1;
                case "Prog2":
                    return Prog2;
                case "Prog3":
                    return Prog3;
                case "Source":
                    return Source;
                case "SourceDetail":
                    return SourceDetail;
                case "Status":
                    return Status;
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

                if (FormGlob.IsStringEmpty(FirstName))
                    return false;
                if (FormGlob.IsStringEmpty(LearningLanguage))
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region "Comparers"
        public class ComparerByBirthday : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.Birthday.CompareTo(x.Birthday);
            }
        }
        public class ComparerByCellPhone : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.CellPhone.CompareTo(x.CellPhone);
            }
        }
        public class ComparerByChanged : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.Changed.CompareTo(x.Changed);
            }
        }
        public class ComparerByEmail : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.Email.CompareTo(x.Email);
            }
        }
        public class ComparerByFirstName : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.FirstName.CompareTo(x.FirstName);
            }
        }
        public class ComparerByLastName : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.LastName.CompareTo(x.LastName);
            }
        }
        public class ComparerByLearns : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.LearningLanguage.CompareTo(x.LearningLanguage);
            }
        }
        public class ComparerByLevel : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.Level.CompareTo(x.Level);
            }
        }
        public class ComparerByAddress : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.MailingAddress.CompareTo(x.MailingAddress);
            }
        }
        public class ComparerBySpeaks : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.NativeLanguage.CompareTo(x.NativeLanguage);
            }
        }
        public class ComparerByOther : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.OtherLanguage.CompareTo(x.OtherLanguage);
            }
        }
        public class ComparerBySource : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.Source.CompareTo(x.Source);
            }
        }
        public class ComparerByStatus : IComparer<Student>
        {
            public int Compare(Student y, Student x)
            {
                return y.Status.CompareTo(x.Status);
            }
        }
        #endregion
    }
}