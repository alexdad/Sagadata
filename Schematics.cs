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
        }

        private void ShowCurrentStudent()
        {
            textBoxBirthday.Text = m_curStudent.Birthday;
            textBoxEmail.Text = m_curStudent.Email;
            textBoxAddress1.Text = m_curStudent.MailingAddress;
            textBoxFirstName.Text = m_curStudent.FirstName;
            textBoxLastName.Text = m_curStudent.LastName;
            textBoxSchedule.Text = m_curStudent.PossibleSchedule;
            textBoxHomePhone.Text = m_curStudent.HomePhone;
            textBoxCellPhone.Text = m_curStudent.CellPhone;
            textBoxComments.Text = m_curStudent.Comments;
            textBoxInterests.Text = m_curStudent.Interests;
            textBoxGoals.Text = m_curStudent.Goals;
            textBoxBackground.Text = m_curStudent.Background;
            textBoxSourceDetail.Text = m_curStudent.SourceDetail;
            textBoxBirthday.Text = m_curStudent.Birthday;
            textBoxLanguageDetail.Text = m_curStudent.LanguageDetail;
            comboBoxLearns.Text = m_curStudent.LearningLanguage;
            comboBoxLevel.Text = m_curStudent.Level;
            comboBoxOther.Text = m_curStudent.OtherLanguage;
            comboBoxSource.Text = m_curStudent.Source;
            comboBoxSpeaks.Text = m_curStudent.NativeLanguage;
            comboBoxStatus.Text = m_curStudent.Status;

            labelCount.Text = studentList.Count.ToString();
        }

    }
}