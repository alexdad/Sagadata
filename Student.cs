using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students
{
    class Student
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MailingAddress { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Phone
        {
            get { return (CellPhone.Length > 0 ? CellPhone : HomePhone); }
        }
        public String Birthday { get; set; }
        public String NativeLanguage { get; set; }
        public String LearningLanguage { get; set; }
        public String OtherLanguage { get; set; }
        public String LanguageDetail { get; set; }
        public String Level { get; set; }
        public string Background { get; set; }
        public string Goals { get; set; }
        public string Interests { get; set; }
        public string PossibleSchedule { get; set; }
        public string Source { get; set; }
        public string SourceDetail { get; set; }
        public string Comments { get; set; }
        public DateTime Created { get; set; }
        public DateTime Changed { get; set; }

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
                    break;
                case "Status":
                    this.Status = value;
                    break;
                case "DateTimeCreated":
                    this.Created = DateTime.Parse(value);
                    break;
                case "DateTimeChanged":
                    this.Changed = DateTime.Parse(value);
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
                case "id":
                    return Id.ToString();
                case "Status":
                    return Status;
                case "DateTimeCreated":
                    return Created.ToString();
                case "DateTimeChanged":
                    return Changed.ToString();
                case "FirstName":
                    return FirstName;
                case "LastName":
                    return LastName;
                case "EMail":
                    return Email;
                case "MailingAddress":
                    return MailingAddress;
                case "HomePhone":
                    return HomePhone;
                case "CellPhone":
                    return CellPhone;
                case "Birthday":
                    return Birthday;
                case "NativeLanguage":
                    return NativeLanguage;
                case "LearningLanguage":
                    return LearningLanguage;
                case "OtherLanguage":
                    return OtherLanguage;
                case "LanguageDetail":
                    return LanguageDetail;
                case "Level":
                    return Level;
                case "Background":
                    return Background;
                case "Goals":
                    return Goals;
                case "Interests":
                    return Interests;
                case "PossibleSchedule":
                    return PossibleSchedule;
                case "Source":
                    return Source;
                case "SourceDetail":
                    return SourceDetail;
                case "Comments":
                    return Comments;
                default:
                    throw new Exception("unknown field " + field);
            }
        }

        public static Student Factory()
        {
            Student st = new Student();
            st.Background = "";
            st.Birthday = "";
            st.CellPhone = "?";
            st.Changed = DateTime.Now;
            st.Comments = "";
            st.Created = DateTime.Now;
            st.Email = "?";
            st.FirstName = "";
            st.Goals = "";
            st.HomePhone = "?";
            st.Id = -1;
            st.Interests = "";
            st.LanguageDetail = "";
            st.LastName = "";
            st.LearningLanguage = "?";
            st.Level = "?";
            st.MailingAddress = "";
            st.NativeLanguage = "?";
            st.OtherLanguage = "?";
            st.PossibleSchedule = "";
            st.Source = "?";
            st.SourceDetail = "";
            st.Status = "?";

            return st;
        }
    }
}
