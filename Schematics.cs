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

namespace Students
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

    public partial class Form1 : Form
    {
        private void AssignEnums()
        {
            comboBoxSelectLearns.Items.AddRange(m_enumLanguage);
            comboBoxSelectSpeaks.Items.AddRange(m_enumLanguage);
            comboBoxSelectStatus.Items.AddRange(m_enumStatus);
            comboBoxSelectSource.Items.AddRange(m_enumSource);
            comboBoxSelectLevel.Items.AddRange(m_enumLevel);

            comboBoxLearns.Items.AddRange(m_enumLanguage);
            comboBoxOther.Items.AddRange(m_enumLanguage);
            comboBoxSpeaks.Items.AddRange(m_enumLanguage);
            comboBoxLevel.Items.AddRange(m_enumLevel);
            comboBoxSource.Items.AddRange(m_enumSource);
            comboBoxStatus.Items.AddRange(m_enumStatus);

            comboBoxType.Items.Add("Students");
            comboBoxType.Items.Add("Teachers");
            comboBoxType.Items.Add("Expenses");
            comboBoxType.Text = "Students";
        }

        private void ShowStudentCount()
        {
            labelCount.Text = studentList.Count.ToString();
        }

    }
}