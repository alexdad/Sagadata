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
        #region "Operational mode"
        public Ops OperMode()
        {
            return (Ops)tabControlOps.SelectedIndex;
        }

        public void ChangeOperMode(Ops op)
        {
            tabControlOps.SelectedIndex = (int)op;
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

            if (m_assignedListsChanged)
                AssignListsToComboBoxes();

            SelectionMode = false;
            CurrentType.SavedFullListDuringSelection = null;
            SetEditMode(newMode);
            ShowCurrentCount();
            ManageSearchWindow();

            this.splitContainerGlobDataControls.Panel1.Visible = true;
        }

        private void cbGlobType_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                    m_assignedListsChanged = true;
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