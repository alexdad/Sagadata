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

        public override bool Actual
        {
            get
            {
                switch (Status)
                {
                    case "Active":
                        return true;
                    case "Prospect":
                        return false;
                    case "Inactive":
                        return false;
                    default:
                        return false;
                }
            }
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
        public override string Validate2FirstProblem(FormGlob glob)
        {
            if (FormGlob.IsStringEmpty(FirstName))
                return "Student has no first  name";
            if (FormGlob.IsStringEmpty(LastName))
                return "Student has no last  name";
            if (FormGlob.IsStringEmpty(Email))
                return "Student has no email";
            if (FormGlob.IsStringEmpty(Phone))
                return "Student has no phone";
            if (FormGlob.IsStringEmpty(Prog1))
                return "Student has no program";
            if (FormGlob.IsStringEmpty(LearningLanguage))
                return "Student has no language";
            if (FormGlob.IsStringEmpty(Source))
                return "Student has no source";
            return null;
        }

        public override string ConcatenateAll()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Background);
            sb.Append(Birthday);
            sb.Append(CellPhone);
            sb.Append(Email);
            sb.Append(FirstName);
            sb.Append(Goals);
            sb.Append(HomePhone);
            sb.Append(Interests);
            sb.Append(LastName);
            sb.Append(LearningLanguage);
            sb.Append(LanguageDetail);
            sb.Append(Level);
            sb.Append(MailingAddress);
            sb.Append(NativeLanguage);
            sb.Append(OtherLanguage);
            sb.Append(PossibleSchedule);
            sb.Append(Price1);
            sb.Append(Price2);
            sb.Append(Price3);
            sb.Append(Prog1);
            sb.Append(Prog2);
            sb.Append(Prog3);
            sb.Append(Source);
            sb.Append(SourceDetail);
            sb.Append(Status);
            sb.Append(Key);
            sb.Append(Comments);

            return sb.ToString();
        }

        public string Program(int i)
        {
            string[] progs = new string[] { this.Prog1, this.Prog2, this.Prog3 };
            if (i < 1 || i > 3)
                throw new Exception("Bad params");
            else
                return progs[i-1];
        }
        public string Price(int i)
        {
            string[] prices = new string[] { this.Price1, this.Price2, this.Price3 };
            if (i < 1 || i > 3)
                throw new Exception("Bad params");
            else
                return prices[i-1];
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