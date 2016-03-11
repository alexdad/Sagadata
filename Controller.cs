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
    public enum Ops
    {
        Edit,
        Plan,
        View,
        SchedCancel,
        PayStud,
        PayTeach,
        PayExpense,
        page8
    };
    public enum Modes
    {
        Students = 0,
        Teachers = 1,
        Programs = 2,
        Rooms = 3,
        Lessons = 4,
        Clients = 5,
        MaxMode = 6
    }
    public enum Scales
    {
        Month = 0,
        Week = 1,
        Day = 2,
        Slots = 3
    };

    public partial class FormGlob : Form
    {
        #region "Datalist"

        string m_readDataHash;

        public System.Windows.Forms.BindingSource DataList
        {
            get
            {
                // Here are links between manually crafted per-record-type UI and modes
                switch (CurrentMode)
                {
                    case Modes.Programs:
                        return programList;
                    case Modes.Rooms:
                        return roomList;
                    case Modes.Students:
                        return studentList;
                    case Modes.Teachers:
                        return teacherList;
                    case Modes.Lessons:
                        return lessonList;
                    case Modes.Clients:
                        return clientList;

                    default:
                        return null;
                }
            }
        }

        public void Datalist_AddRecord()
        {
            Record st = (Record)DataList.AddNew();
            st.Id = FormGlob.AllocateID();
            st.ChangedBy = ClientCode;
            st.CreatedBy = ClientCode;
            st.SetHash();
        }

        public void Datalist_SetPosition(int i)
        {
            DataList.CurrencyManager.Position = i;
        }
        public bool Datalist_Find(string key)
        {
            for (int i = 0; i < DataList.Count; i++)
            {
                DataList.CurrencyManager.Position = i;
                Record r = (Record)DataList.Current;
                if (r.Key == key)
                    return true;
            }
            return false;
        }

        public void Datalist_StepForward()
        {
            if (DataList.CurrencyManager.Position < DataList.Count - 1)
                DataList.CurrencyManager.Position++;
        }
        public void Datalist_StepBack()
        {
            if (DataList.CurrencyManager.Position > 0)
                DataList.CurrencyManager.Position--;
        }
        public bool Datalist_Complete()
        {
            if (DataList.Count == 0)
                return true;

            if (OperMode() == Ops.Edit)
            {
                Record rec = DataList.Current as Record;
                string prob = rec.Validate2FirstProblem();
                if (!IsStringEmpty(prob))
                {
                    labelGlobEditValid.Text = prob;
                    return false;
                }
                labelGlobEditValid.Text = "";
            }

            if (!(DataList.Current as Record).IsHashAsRead())
            {
                Modified = true;
                if (CurrentMode != Modes.Lessons)
                    StaleComboLists = true;
            }

            if (DataList.CurrencyManager.Position < DataList.Count - 1)
            {
                DataList.CurrencyManager.Position++;
                DataList.CurrencyManager.Position--;
            }
            if (DataList.CurrencyManager.Position > 0)
            {
                DataList.CurrencyManager.Position--;
                DataList.CurrencyManager.Position++;
            }
            return true;
        }

        public string Datalist_GetHash()
        {
            StringBuilder sb = new StringBuilder();
            int poswas = DataList.CurrencyManager.Position;
            for (int i=0; i < DataList.Count; i++)
            {
                DataList.CurrencyManager.Position = i;
                Record r = (Record)DataList.Current;
                sb.Append(r.GetHash());
            }
            DataList.CurrencyManager.Position = poswas;
            return FormGlob.CalculateMD5(sb.ToString());
        }

        public string PersistRecord(Record r)
        {
            return m_recordTypes[r.Mode].Persist(r);
        }

        public bool HashAll()
        {
            string oldHash = m_readDataHash;

            StringBuilder sb = new StringBuilder();
            Modes modeWas = CurrentMode;
            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetEditMode(i);
                sb.Append(Datalist_GetHash());
            }
            SetEditMode(modeWas);

            m_readDataHash = CalculateMD5(sb.ToString());
            return (oldHash != m_readDataHash);
        }

        private void UploadAll()
        {
            HideWorkout = true;

            Modes modeWas = CurrentMode;
            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetEditMode(i);
                UploadCurrentFile();
            }
            CurrentMode = modeWas;
            Synced = !Modified;

            HideWorkout = false;
        }

        private bool DownloadAll()
        {
            HideWorkout = true;

            bool success = true;
            Modes modeWas = CurrentMode;

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetEditMode(i);
                if (!DownloadCurrentFile())
                    success = false;
            }
            CurrentMode = modeWas;
            Synced = !Modified;

            HideWorkout = false;
            return success;
        }

        private bool ReadAllFiles()
        {
            HideWorkout = true;

            bool success = true;
            Modes modeWas = CurrentMode;

            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetEditMode(i);
                if (!ReadCurrentFile())
                    success = false;
            }
            SetEditMode(modeWas);

            //HashAll();
            HideWorkout = false;
            return success;
        }

        private void SaveAll()
        {
            HideWorkout = true;

            Modes was = CurrentMode;
            for (Modes i = (Modes)0; i < Modes.MaxMode; i++)
            {
                SetEditMode(i);
                CurrentType.WriteRecordsFile();
            }
            Modified = false;
            //HashAll();
            UpdateComboLists();
            SetEditMode(was);
            m_editSavingTrap = false;
            HideWorkout = false;
        }

        #endregion

        #region "Operational mode"

        public Ops OperMode()
        {
            return (Ops)tabControlOps.SelectedIndex;
        }

        public void ChangeOperMode(Ops op)
        {
            CurrentType.EndSelectionMode();
            tabControlOps.SelectedIndex = (int)op;
        }
        public bool Operation_CompletePrevious()
        {
            switch(OperMode())
            {
                case Ops.Edit:
                    if (!Datalist_Complete())
                        return false;
                    UpdateComboLists();
                    break;
                case Ops.View:
                    if (!Datalist_Complete())
                        return false;
                    break;
                case Ops.Plan:
                    if (!Datalist_Complete())
                        return false;
                    break;
                case Ops.SchedCancel:
                    break;
                case Ops.PayStud:
                    break;
                case Ops.PayTeach:
                    break;
                case Ops.PayExpense:
                    break;
                case Ops.page8:
                    break;
                default:
                    break;
            }
            return true;
        }

        #endregion

        #region "Edit Mode"
        public Modes CurrentMode { get; set; }
        private void ChangeEditMode(Modes newMode)
        {
            if (!CheckSafety())
                return;

            this.splitContainerGlobDataControls.Panel1.Visible = false;

            CurrentType.EndSelectionMode();

            if (Modified)
                SaveAll();

            UpdateComboLists();

            SelectionMode = false;
            CurrentType.SavedFullListDuringSelection = null;
            SetEditMode(newMode);
            cbGlobMode.Text = newMode.ToString();
            ShowCurrentCount();
            ManageSearchWindow();

            this.splitContainerGlobDataControls.Panel1.Visible = true;
        }

        private void cbGlobType_SelectedIndexChanged_Actual(object sender, EventArgs e)
        {
            if (!Operation_CompletePrevious())
                return;

            ComboBox comboBox = (ComboBox)sender;
            Modes newMode = (Modes)comboBox.SelectedIndex;
            if (newMode == CurrentMode)
                return;
            ChangeEditMode(newMode);
        }

        private void SetEditMode(int mode)
        {
            SetEditMode((Modes)mode);
        }
        private void SetEditMode(Modes mode)
        {
            CurrentMode = mode;
            tabControlModesBottom.SelectedIndex = (int)CurrentMode;
            tabControlModesTop.SelectedIndex = (int)CurrentMode;
            tabControlSearch.SelectedIndex = (int)CurrentMode;

        }
        #endregion

        #region "View Scale"
        public void ChangeOperScale(Scales op)
        {
            tabControlViewScales.SelectedIndex = (int)op;
        }

        public Scales ScaleMode()
        {
            return (Scales)tabControlViewScales.SelectedIndex;
        }
        #endregion

        #region "Modification"
        bool m_modified;
        public bool Modified
        {
            get
            {
                return m_modified;
            }
            set
            {
                m_modified = value;
                buttonSave.Visible = m_modified;
                if (value)
                    Synced = false;
            }
        }

        #endregion

        #region "Synchronization"
        bool m_synced;
        public bool Synced
        {
            get 
            {
                return m_synced;
            }
            set
            {
                m_synced = value;
            }
        }

        #endregion

        #region "Edit Trap"

        bool m_editSavingTrap;

        public bool EditTrap
        {
            set
            {
                m_editSavingTrap = value;
                if (value)
                {
                    StaleComboLists = true;
                    buttonGlobEditAccept.Visible = true;
                }
                else
                {
                    buttonGlobEditAccept.Visible = false;
                }
            }
        }

        public bool CheckSafety()
        {
            if (m_bulkOperationInPlay)
                return true;

            if (m_unsavedAvailabilityChanges)
            {
                if (MessageBox.Show(
                    "Want to lose them?",
                    "You did not grab changes in availability form",
                    MessageBoxButtons.YesNo)
                                            != DialogResult.Yes)
                    return false;
                else
                    DropFlagUnsavedAvailabilityChanges();
            }
            return true;
            /*
            if (m_editSavingTrap)
            {
                if (MessageBox.Show(
                    "Want to lose them?",
                    "You did not Accept editing changes",
                    MessageBoxButtons.YesNo)
                                            != DialogResult.Yes)
                    return false;
                else
                    m_editSavingTrap = false;
            }
            return true;
            */
        }
        #endregion

        #region "ComboLists"

        public bool StaleComboLists { get; set; }

        public void UpdateComboLists()
        {
            if (StaleComboLists)
                AssignListsToComboBoxes();
        }

        #endregion
        #region "Bulk operations"
        bool m_bulkOperationInPlay;
        Modes m_modePreBulkOperation;

        public bool BulkOperation
        {
            set
            {
                if (value)
                {
                    m_bulkOperationInPlay = true;
                    m_modePreBulkOperation = CurrentMode;
                }
                else
                {
                    m_bulkOperationInPlay = false;
                    CurrentMode = m_modePreBulkOperation;
                }
            }
        }
         
        public void StartBulkEditOperation(Modes mode)
        {
            BulkOperation = true;
            CurrentMode = mode;
        }
        public void CompleteBulkEditOperation()
        {
            buttonGlobEditAccept_Click(null, null);
            BulkOperation = false;
            Modified = true;
        }
        #endregion

    }
}