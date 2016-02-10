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
        public static int LastColumnSorted = -1;
        public static bool NeedToReverse = false;

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Changed { get; set; }
        public string ChangedBy { get; set; }
        public string Comments { get; set; }
        public string Key { get { return CreatedBy + Id.ToString(); } }

        public abstract string Description{ get; }
        public abstract bool Set(string field, string value);
        public abstract string Get(string field);
    }
}