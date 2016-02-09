using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecordKeeper
{
    enum Clouds
    {
        None,
        Dir,
        Google,
        Azure
    }
    enum Validations
    {
        Ignore,
        Number,
        Text,
        Timing,
        Phone,
        Email,
        Status,
        Language,
        Level,
        Source
    }

    struct SchemaField
    {
        public SchemaField(string h, string v)
        {
            Header = h;
            switch (v)
            {
                case "number":
                    Validation = Validations.Number;
                    break;
                case "text":
                    Validation = Validations.Text;
                    break;
                case "timing":
                    Validation = Validations.Timing;
                    break;
                case "phone":
                    Validation = Validations.Phone;
                    break;
                case "email":
                    Validation = Validations.Email;
                    break;
                case "Status":
                    Validation = Validations.Status;
                    break;
                case "Language":
                    Validation = Validations.Language;
                    break;
                case "Level":
                    Validation = Validations.Level;
                    break;
                case "Source":
                    Validation = Validations.Source;
                    break;
                default:
                    throw new Exception("Unknown schema type " + v);
            }
        }
        public string Header;
        public Validations Validation;
    }

    public partial class FormGlob : Form
    {
        private void AssignEnums()
        {
            cbStudSelectLearns.Items.AddRange(m_enumLanguage);
            cbStudSelectSpeaks.Items.AddRange(m_enumLanguage);
            cbStudSelectStatus.Items.AddRange(m_enumStatus);
            cbStudSelectSource.Items.AddRange(m_enumSource);
            cbStudSelectLevel.Items.AddRange(m_enumLevel);

            cbStudLearns.Items.AddRange(m_enumLanguage);
            cbStudOther.Items.AddRange(m_enumLanguage);
            cbStudSpeaks.Items.AddRange(m_enumLanguage);
            cbStudLevel.Items.AddRange(m_enumLevel);
            cbStudSource.Items.AddRange(m_enumSource);
            cbStudStatus.Items.AddRange(m_enumStatus);

            cbGlobType.Items.Add("Welcome");
            cbGlobType.Items.Add("Students");
            cbGlobType.Items.Add("Teachers");
            cbGlobType.Items.Add("Programs");
            cbGlobType.Items.Add("Rooms");
            cbGlobType.Items.Add("Lessons");
            cbGlobType.Items.Add("Expenses");
            cbGlobType.Text = "Welcome";
        }

        private void ShowStudentCount()
        {
            labelGlobCount.Text = studentList.Count.ToString();
        }

    }
}