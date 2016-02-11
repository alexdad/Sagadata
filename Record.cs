﻿using System;
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
        public static int LastColumnSorted = -1;
        public static bool NeedToReverse = false;

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public string Comments { get; set; }
        public string Key { get { return CreatedBy + Id.ToString(); } }

        public abstract string Description { get; }
        public abstract string Abbreviation { get; }
        public abstract bool Set(string field, string value);
        public abstract string Get(string field);

        public Record()
        {
            Changed = DateTime.Now;
            ChangedBy = "";
            Created = DateTime.Now;
            CreatedBy = "";
            Comments = "";
            Id = 0;
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
    }
}