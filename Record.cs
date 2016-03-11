using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{

    public abstract class Record
    {
        protected FormGlob m_glob;

        public static int LastColumnSorted = -1;
        public static bool NeedToReverse = false;

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public string Comments { get; set; }
        public string Key { get { return CreatedBy + Id.ToString(); } }

        private string m_hashAsRead;

        public bool ValidateBase
        {
            get
            {
                try
                {
                    if (FormGlob.IsStringEmpty(Key))
                        return false;
                    if (FormGlob.IsStringEmpty(Description))
                        return false;
                    if (!FormGlob.IsDateTimeReasonable(Created))
                        return false;
                    if (FormGlob.IsStringEmpty(CreatedBy))
                        return false;
                    if (!FormGlob.IsDateTimeReasonable(Changed))
                        return false;
                    if (FormGlob.IsStringEmpty(ChangedBy))
                        return false;

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public abstract Modes Mode { get; } 
        public abstract string Description { get; }
        public abstract string Abbreviation { get; }
        public abstract bool Set(string field, string value);
        public abstract string Get(string field);
        public abstract bool Actual { get; }
        public abstract bool Validate();
        public abstract string Validate2FirstProblem();

        public Record()
        {
            Changed = DateTime.Now;
            ChangedBy = FormGlob.ClientCode;
            Created = DateTime.Now;
            CreatedBy = FormGlob.ClientCode;
            Comments = "";
            Id = FormGlob.AllocateID();
        }

        public void SetGlob(FormGlob glob)
        {
            m_glob = glob;
        }


        public string SetRecordFields(string field, string value)
        {
            while (value.StartsWith("\""))
                value = value.Substring(1);
            while (value.EndsWith("\""))
                value = value.Substring(0, value.Length - 1);

            switch (field)
            {
                case "CreatedBy":
                    this.CreatedBy = value;
                    return "";
                case "ChangedBy":
                    this.ChangedBy = value;
                    return "";
                case "Comments":
                    this.Comments = value;
                    return "";
                case "DateTimeChanged":
                    this.Changed = DateTime.Parse(value);
                    return "";
                case "DateTimeCreated":
                    this.Created = DateTime.Parse(value);
                    return "";
                case "id":
                    this.Id = int.Parse(value);
                    FormGlob.AccumulateID(Id);
                    return "";
                default:
                    return value;
            }
        }

        public string GetRecordFields(string field)
        {
            switch (field)
            {
                case "ChangedBy":
                    return ChangedBy;
                case "CreatedBy":
                    return CreatedBy;
                case "Comments":
                    return Comments;
                case "DateTimeChanged":
                    return Changed.ToString();
                case "DateTimeCreated":
                    return Created.ToString();
                case "id":
                    return Id.ToString();
                default:
                    return null;
            }
        }

        public string GetHash()
        {
            return FormGlob.CalculateMD5( m_glob.PersistRecord(this));
        }

        public void SetHash()
        {
            m_hashAsRead = GetHash();
        }
        public bool IsHashAsRead()
        {
            return (m_hashAsRead == GetHash());
        }
    }
}