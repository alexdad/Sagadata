using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    public class Student
    {
        public string Background { get; set; }
        public String Birthday { get; set; }
        public string CellPhone { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public string Comments { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Goals { get; set; }
        public string HomePhone { get; set; }
        public int Id { get; set; }
        public string Interests { get; set; }
        public string LastName { get; set; }
        public String LearningLanguage { get; set; }
        public String LanguageDetail { get; set; }
        public String Level { get; set; }
        public string MailingAddress { get; set; }
        public String NativeLanguage { get; set; }
        public String OtherLanguage { get; set; }
        public string PossibleSchedule { get; set; }
        public string Source { get; set; }
        public string SourceDetail { get; set; }
        public string Status { get; set; }

        public string Key { get { return CreatedBy + Id.ToString();  } }
        public string Phone { get { return (CellPhone.Length > 0 ? CellPhone : HomePhone); }
        }

        public Student()
        {
            Background = "";
            Birthday = "";
            CellPhone = "";
            ChangedBy = FormGlob.Client;
            CreatedBy = FormGlob.Client;
            Comments = "";
            Created = DateTime.Now;
            Changed = DateTime.Now;
            Email = "";
            FirstName = "";
            Goals = "";
            HomePhone = "";
            Id = 0;
            Interests = "";
            LastName = "";
            LearningLanguage = "?";
            LanguageDetail = "";
            Level = "?";
            MailingAddress = "";
            NativeLanguage = "?";
            OtherLanguage = "?";
            PossibleSchedule = "";
            Source = "?";
            SourceDetail = "";
            Status = "?";
        }

        public Student(Student st)
        {
            Background = st.Background;
            Birthday = st.Birthday;
            CellPhone = st.CellPhone;
            Changed = st.Changed;
            ChangedBy = st.ChangedBy;
            Comments = st.Comments;
            Created = st.Created;
            CreatedBy = st.CreatedBy;
            Email = st.Email;
            FirstName = st.FirstName;
            Goals = st.Goals;
            HomePhone = st.HomePhone;
            Id = st.Id;
            Interests = st.Interests;
            LastName = st.LastName;
            LearningLanguage = st.LearningLanguage;
            LanguageDetail = st.LanguageDetail;
            Level = st.Level;
            MailingAddress = st.MailingAddress;
            NativeLanguage = st.NativeLanguage;
            OtherLanguage = st.OtherLanguage;
            PossibleSchedule = st.PossibleSchedule;
            Source = st.Source;
            SourceDetail = st.SourceDetail;
            Status = st.Status;
        }

        public bool Set(string field, string value)
        {
            while (value.StartsWith("\""))
                value = value.Substring(1);
            while (value.EndsWith("\""))
                value = value.Substring(0, value.Length - 1);

            switch (field)
            {
                case "id":
                    this.Id = int.Parse(value);
                    FormGlob.AccumulateID(Id);
                    break;
                case "Status":
                    this.Status = value;
                    break;
                case "DateTimeCreated":
                    this.Created = DateTime.Parse(value);
                    break;
                case "CreatedBy":
                    this.CreatedBy = value;
                    break;
                case "DateTimeChanged":
                    this.Changed = DateTime.Parse(value);
                    break;
                case "ChangedBy":
                    this.ChangedBy = value;
                    break;
                case "FirstName":
                    this.FirstName = value;
                    break;
                case "LastName":
                    this.LastName = value;
                    break;
                case "EMail":
                    this.Email = value;
                    break;
                case "MailingAddress":
                    this.MailingAddress = value;
                    break;
                case "HomePhone":
                    this.HomePhone = value;
                    break;
                case "CellPhone":
                    this.CellPhone = value;
                    break;
                case "Birthday":
                    this.Birthday = value;
                    break;
                case "NativeLanguage":
                    this.NativeLanguage = value;
                    break;
                case "LearningLanguage":
                    this.LearningLanguage = value;
                    break;
                case "OtherLanguage":
                    this.OtherLanguage = value;
                    break;
                case "LanguageDetail":
                    this.LanguageDetail = value;
                    break;
                case "Level":
                    this.Level = value;
                    break;
                case "Background":
                    this.Background = value;
                    break;
                case "Goals":
                    this.Goals = value;
                    break;
                case "Interests":
                    this.Interests = value;
                    break;
                case "PossibleSchedule":
                    this.PossibleSchedule = value;
                    break;
                case "Source":
                    this.Source = value;
                    break;
                case "SourceDetail":
                    this.SourceDetail = value;
                    break;
                case "Comments":
                    this.Comments = value;
                    break;
                default:
                    throw new Exception("unknown field " + field);
            }
            return true;
        }

        public string Get(string field)
        {
            switch (field)
            {
                case "Background":
                    return Background;
                case "Birthday":
                    return Birthday;
                case "CellPhone":
                    return CellPhone;
                case "DateTimeChanged":
                    return Changed.ToString();
                case "ChangedBy":
                    return ChangedBy;
                case "Comments":
                    return Comments;
                case "CreatedBy":
                    return CreatedBy;
                case "DateTimeCreated":
                    return Created.ToString();
                case "EMail":
                    return Email;
                case "FirstName":
                    return FirstName;
                case "Goals":
                    return Goals;
                case "HomePhone":
                    return HomePhone;
                case "id":
                    return Id.ToString();
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
    }

    #region "Comparers"
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
    public class ComparerByStatus : IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.Status.CompareTo(x.Status);
        }
    }
    public class ComparerBySource : IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.Source.CompareTo(x.Source);
        }
    }
    public class ComparerByLearns : IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.LearningLanguage.CompareTo(x.LearningLanguage);
        }
    }
    public class ComparerByOther: IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.OtherLanguage.CompareTo(x.OtherLanguage);
        }
    }
    public class ComparerByLevel: IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.Level.CompareTo(x.Level);
        }
    }
    public class ComparerBySpeaks : IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.NativeLanguage.CompareTo(x.NativeLanguage);
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
    public class ComparerByCellPhone: IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.CellPhone.CompareTo(x.CellPhone);
        }
    }
    public class ComparerByBirthday : IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.Birthday.CompareTo(x.Birthday);
        }
    }

    public class ComparerByAddress : IComparer<Student>
    {
        public int Compare(Student y, Student x)
        {
            return y.MailingAddress.CompareTo(x.MailingAddress);
        }
    }
    #endregion
}
