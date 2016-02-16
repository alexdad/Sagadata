namespace RecordKeeper
{
    partial class FormGlob
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGlob));
            this.menuStripGlobalOps = new System.Windows.Forms.MenuStrip();
            this.menuItemGlobDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.planToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.payToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.studentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teachersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportExpenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGlobSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGlobUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemGlobExit = new System.Windows.Forms.ToolStripMenuItem();
            this.labelGlobLastDownload = new System.Windows.Forms.Label();
            this.labelGlobLastUpload = new System.Windows.Forms.Label();
            this.panelGlobIndicators = new System.Windows.Forms.Panel();
            this.tabControlOps = new RecordKeeper.HiddenTabControl();
            this.tabEdit = new System.Windows.Forms.TabPage();
            this.splitContainerGlobDataControls = new System.Windows.Forms.SplitContainer();
            this.splitContainerGlobMasterDetail = new System.Windows.Forms.SplitContainer();
            this.tabControlModesTop = new RecordKeeper.HiddenTabControl();
            this.tabTopPageStudents = new System.Windows.Forms.TabPage();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.dgvStudColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnLearningLanguage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnNativeLanguage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnOtherLanguage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnBirthday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStudColumnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.studentList = new System.Windows.Forms.BindingSource(this.components);
            this.tabTopPageTeachers = new System.Windows.Forms.TabPage();
            this.dgvTeachers = new System.Windows.Forms.DataGridView();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.languageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.language2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.languageDetailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vacationsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentsDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mailingAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthdayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacherList = new System.Windows.Forms.BindingSource(this.components);
            this.tabTopPagePrograms = new System.Windows.Forms.TabPage();
            this.dgvPrograms = new System.Windows.Forms.DataGridView();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.languageDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.programList = new System.Windows.Forms.BindingSource(this.components);
            this.tabTopPageRooms = new System.Windows.Forms.TabPage();
            this.dgvRooms = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomList = new System.Windows.Forms.BindingSource(this.components);
            this.tabTopPageLessons = new System.Windows.Forms.TabPage();
            this.dgvLesson = new System.Windows.Forms.DataGridView();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.programDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.student1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.student2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.student3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.student10DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Student4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.teacher2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentsDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lessonList = new System.Windows.Forms.BindingSource(this.components);
            this.panelGlobEdit = new System.Windows.Forms.Panel();
            this.tabControlModesBottom = new RecordKeeper.HiddenTabControl();
            this.tabBottomPageStudents = new System.Windows.Forms.TabPage();
            this.panelStudent = new System.Windows.Forms.Panel();
            this.panelStudPrimary = new System.Windows.Forms.Panel();
            this.groupBoxStudPrinaryRight = new System.Windows.Forms.GroupBox();
            this.labelStudDetailsSource = new System.Windows.Forms.Label();
            this.labelStudDetailsLanguage = new System.Windows.Forms.Label();
            this.labelStudAddress1 = new System.Windows.Forms.Label();
            this.tbStudAddress1 = new System.Windows.Forms.TextBox();
            this.tbStudLanguageDetail = new System.Windows.Forms.TextBox();
            this.tbStudSourceDetail = new System.Windows.Forms.TextBox();
            this.panelStudPrimaryLeft = new System.Windows.Forms.Panel();
            this.cbStudSpeaks = new System.Windows.Forms.ComboBox();
            this.cbStudOther = new System.Windows.Forms.ComboBox();
            this.labelStudBirthday = new System.Windows.Forms.Label();
            this.tbStudLastName = new System.Windows.Forms.TextBox();
            this.labelStudAlso = new System.Windows.Forms.Label();
            this.labelStudFirstname = new System.Windows.Forms.Label();
            this.labelStudStatus1 = new System.Windows.Forms.Label();
            this.tbStudFirstName = new System.Windows.Forms.TextBox();
            this.cbStudStatus = new System.Windows.Forms.ComboBox();
            this.labelStudLastName1 = new System.Windows.Forms.Label();
            this.cbStudLearns = new System.Windows.Forms.ComboBox();
            this.labelStudEmail1 = new System.Windows.Forms.Label();
            this.tbStudBirthday = new System.Windows.Forms.TextBox();
            this.tbStudEmail = new System.Windows.Forms.TextBox();
            this.labelStudSpeaks1 = new System.Windows.Forms.Label();
            this.labelStudCellPhone = new System.Windows.Forms.Label();
            this.labelStudLevel1 = new System.Windows.Forms.Label();
            this.labelStudHomePhone = new System.Windows.Forms.Label();
            this.cbStudLevel = new System.Windows.Forms.ComboBox();
            this.tbStudCellPhone = new System.Windows.Forms.TextBox();
            this.labelStudSource1 = new System.Windows.Forms.Label();
            this.tbStudHomePhone = new System.Windows.Forms.TextBox();
            this.labelStudSpeaks2 = new System.Windows.Forms.Label();
            this.cbStudSource = new System.Windows.Forms.ComboBox();
            this.panelStudSecondary = new System.Windows.Forms.Panel();
            this.groupBoxStudSecondaryRight = new System.Windows.Forms.GroupBox();
            this.tbStudComments = new System.Windows.Forms.TextBox();
            this.tbStudSchedule = new System.Windows.Forms.TextBox();
            this.tbStudInterests = new System.Windows.Forms.TextBox();
            this.tbStudGoals = new System.Windows.Forms.TextBox();
            this.tbStudBackground = new System.Windows.Forms.TextBox();
            this.panelStudSecondaryLeft = new System.Windows.Forms.Panel();
            this.labelStudGoals = new System.Windows.Forms.Label();
            this.labelStudBackground = new System.Windows.Forms.Label();
            this.labelStudComments = new System.Windows.Forms.Label();
            this.labelStudSchedule = new System.Windows.Forms.Label();
            this.labelStudInterests = new System.Windows.Forms.Label();
            this.tabBottomPageTeachers = new System.Windows.Forms.TabPage();
            this.panelTeacherSecondary = new System.Windows.Forms.Panel();
            this.panelTeachMatrix = new System.Windows.Forms.Panel();
            this.buttonTeach_GrabAvailChanges = new System.Windows.Forms.Button();
            this.panelTeacherPrimary = new System.Windows.Forms.Panel();
            this.groupBoxTeacherPrimaryRight = new System.Windows.Forms.GroupBox();
            this.tbTeachComment = new System.Windows.Forms.TextBox();
            this.tbTeachVacations = new System.Windows.Forms.TextBox();
            this.tbTeachAddress = new System.Windows.Forms.TextBox();
            this.labelTeachComment = new System.Windows.Forms.Label();
            this.labelTeachVacations = new System.Windows.Forms.Label();
            this.labelTeachAddress = new System.Windows.Forms.Label();
            this.panelTeacherPrimaryLeft = new System.Windows.Forms.Panel();
            this.tbTeachLanguageDetail = new System.Windows.Forms.TextBox();
            this.cbTeachLanguage2 = new System.Windows.Forms.ComboBox();
            this.cbTeachLanguage = new System.Windows.Forms.ComboBox();
            this.labelTeachLabguageDetail = new System.Windows.Forms.Label();
            this.labelTeachLanguage2 = new System.Windows.Forms.Label();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.cbTeachStatus = new System.Windows.Forms.ComboBox();
            this.tbTeachLastBirthday = new System.Windows.Forms.TextBox();
            this.tbTeachPhone = new System.Windows.Forms.TextBox();
            this.labelTeachBirthday = new System.Windows.Forms.Label();
            this.labelTeachPhone = new System.Windows.Forms.Label();
            this.labelTeachStatus = new System.Windows.Forms.Label();
            this.tbTeachEmail = new System.Windows.Forms.TextBox();
            this.tbTeachLastName = new System.Windows.Forms.TextBox();
            this.tbTeachFirstName = new System.Windows.Forms.TextBox();
            this.labelTeachEmail = new System.Windows.Forms.Label();
            this.labelTeachLastName = new System.Windows.Forms.Label();
            this.labelTeachFirstName = new System.Windows.Forms.Label();
            this.tabBottomPagePrograms = new System.Windows.Forms.TabPage();
            this.panelProgram = new System.Windows.Forms.Panel();
            this.groupBoxProgram = new System.Windows.Forms.GroupBox();
            this.tbProgComments = new System.Windows.Forms.TextBox();
            this.tbProgSummary = new System.Windows.Forms.TextBox();
            this.labelProgComments = new System.Windows.Forms.Label();
            this.labelProgSummary = new System.Windows.Forms.Label();
            this.panelProgramPrimaryLeft = new System.Windows.Forms.Panel();
            this.labelProgExplanation = new System.Windows.Forms.Label();
            this.tbProgProce = new System.Windows.Forms.TextBox();
            this.labelProgPrice = new System.Windows.Forms.Label();
            this.cbProgLevel = new System.Windows.Forms.ComboBox();
            this.labelProgLevel = new System.Windows.Forms.Label();
            this.cbProgLanguage = new System.Windows.Forms.ComboBox();
            this.labelProgLanguage = new System.Windows.Forms.Label();
            this.tbProgName = new System.Windows.Forms.TextBox();
            this.labelProgName = new System.Windows.Forms.Label();
            this.tbProgCode = new System.Windows.Forms.TextBox();
            this.labelProgCode = new System.Windows.Forms.Label();
            this.tabBottomPageRooms = new System.Windows.Forms.TabPage();
            this.panelRoom = new System.Windows.Forms.Panel();
            this.panelRoomPrimaryLeft = new System.Windows.Forms.Panel();
            this.labelRoomRank = new System.Windows.Forms.Label();
            this.labelRoomCapacity = new System.Windows.Forms.Label();
            this.labelRoomName = new System.Windows.Forms.Label();
            this.tbRoomPreferrability = new System.Windows.Forms.TextBox();
            this.tbRoomCapacity = new System.Windows.Forms.TextBox();
            this.tbRoomName = new System.Windows.Forms.TextBox();
            this.groupBoxRoom = new System.Windows.Forms.GroupBox();
            this.labelRoomComments = new System.Windows.Forms.Label();
            this.labelRoomTags = new System.Windows.Forms.Label();
            this.tbRoomComments = new System.Windows.Forms.TextBox();
            this.tbRoomTags = new System.Windows.Forms.TextBox();
            this.tabBottomPageLessons = new System.Windows.Forms.TabPage();
            this.panelLesson = new System.Windows.Forms.Panel();
            this.cbLessonStudent10 = new System.Windows.Forms.ComboBox();
            this.cbLessonStudent9 = new System.Windows.Forms.ComboBox();
            this.labelLessonStudent10 = new System.Windows.Forms.Label();
            this.labelLessonStudent9 = new System.Windows.Forms.Label();
            this.cbLessonStudent8 = new System.Windows.Forms.ComboBox();
            this.cbLessonStudent7 = new System.Windows.Forms.ComboBox();
            this.labelLessonStudent8 = new System.Windows.Forms.Label();
            this.labelLessonStudent7 = new System.Windows.Forms.Label();
            this.cbLessonStudent6 = new System.Windows.Forms.ComboBox();
            this.cbLessonStudent5 = new System.Windows.Forms.ComboBox();
            this.labelLessonStudent6 = new System.Windows.Forms.Label();
            this.labelLessonStudent5 = new System.Windows.Forms.Label();
            this.cbLessonStudent4 = new System.Windows.Forms.ComboBox();
            this.labelLessonStudent4 = new System.Windows.Forms.Label();
            this.cbLessonStudent3 = new System.Windows.Forms.ComboBox();
            this.labelLessonStudent3 = new System.Windows.Forms.Label();
            this.labelLessonComment = new System.Windows.Forms.Label();
            this.tbLEssonComment = new System.Windows.Forms.TextBox();
            this.cbLessonStudent2 = new System.Windows.Forms.ComboBox();
            this.labelLessonStudent2 = new System.Windows.Forms.Label();
            this.cbLessonStudent1 = new System.Windows.Forms.ComboBox();
            this.cbLessonTeacher2 = new System.Windows.Forms.ComboBox();
            this.cbLessonTeacher1 = new System.Windows.Forms.ComboBox();
            this.cbLessonProg = new System.Windows.Forms.ComboBox();
            this.labelLessonStudent1 = new System.Windows.Forms.Label();
            this.labelLessonTeacher2 = new System.Windows.Forms.Label();
            this.labelLessonTeacher1 = new System.Windows.Forms.Label();
            this.labelLessonProgram = new System.Windows.Forms.Label();
            this.cbLessonState = new System.Windows.Forms.ComboBox();
            this.labelLessonState = new System.Windows.Forms.Label();
            this.cbLessonEnd = new System.Windows.Forms.ComboBox();
            this.cbLessonStart = new System.Windows.Forms.ComboBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.cbLessonRoom = new System.Windows.Forms.ComboBox();
            this.labelLessonRoom = new System.Windows.Forms.Label();
            this.labelLessonEnd = new System.Windows.Forms.Label();
            this.labelLessonStart = new System.Windows.Forms.Label();
            this.labelLessonDate = new System.Windows.Forms.Label();
            this.panelGlobPrevDelete = new System.Windows.Forms.Panel();
            this.butGlobalDelete = new System.Windows.Forms.Button();
            this.butGlobalPrev = new System.Windows.Forms.Button();
            this.panelGlobNextNew = new System.Windows.Forms.Panel();
            this.butGlobalAdd = new System.Windows.Forms.Button();
            this.butGlobalNext = new System.Windows.Forms.Button();
            this.buttonGlobRefresh = new System.Windows.Forms.Button();
            this.panelGlobSearch = new System.Windows.Forms.Panel();
            this.panelSearchButtons = new System.Windows.Forms.Panel();
            this.butGlobalToExcel = new System.Windows.Forms.Button();
            this.buttGlobalShowAll = new System.Windows.Forms.Button();
            this.tabControlSearch = new RecordKeeper.HiddenTabControl();
            this.tabPageSearchStudent = new System.Windows.Forms.TabPage();
            this.panelStudSearch = new System.Windows.Forms.Panel();
            this.lbStudSearchStatus = new System.Windows.Forms.Label();
            this.lbStudSearchLearns = new System.Windows.Forms.Label();
            this.lbStudSearchSpeaks = new System.Windows.Forms.Label();
            this.cbStudSearchLevel = new System.Windows.Forms.ComboBox();
            this.cbStudSearchLearns = new System.Windows.Forms.ComboBox();
            this.lbStudSearchLevel = new System.Windows.Forms.Label();
            this.cbStudSearchSpeaks = new System.Windows.Forms.ComboBox();
            this.lbStudSearchFirstName1 = new System.Windows.Forms.Label();
            this.cbStudSearchSource = new System.Windows.Forms.ComboBox();
            this.tbStudSearchFirstName = new System.Windows.Forms.TextBox();
            this.lbStudSearchSource = new System.Windows.Forms.Label();
            this.cbStudSelectStatus = new System.Windows.Forms.ComboBox();
            this.lbStudSearchLastName = new System.Windows.Forms.Label();
            this.tbStudSearchLastName = new System.Windows.Forms.TextBox();
            this.tabPageSearchTeacher = new System.Windows.Forms.TabPage();
            this.panelSearchTeacher = new System.Windows.Forms.Panel();
            this.tbSearchTeachLastName = new System.Windows.Forms.TextBox();
            this.tbSearchTeachFirstName = new System.Windows.Forms.TextBox();
            this.cbSearchTeachLang1 = new System.Windows.Forms.ComboBox();
            this.cbSearchTeachStatus = new System.Windows.Forms.ComboBox();
            this.lbSerchTeachLastName = new System.Windows.Forms.Label();
            this.lbSerchTeachFirstName = new System.Windows.Forms.Label();
            this.lbSerchTeachLang1 = new System.Windows.Forms.Label();
            this.lbSerchTeachStatus = new System.Windows.Forms.Label();
            this.tabPageSearchProgram = new System.Windows.Forms.TabPage();
            this.panelSearchProgram = new System.Windows.Forms.Panel();
            this.cbSearchProgLevel = new System.Windows.Forms.ComboBox();
            this.cbSearchProgLanguage = new System.Windows.Forms.ComboBox();
            this.lbSearchProgLevel = new System.Windows.Forms.Label();
            this.lbSearchProgLanguage = new System.Windows.Forms.Label();
            this.tabPageSearchRoom = new System.Windows.Forms.TabPage();
            this.panelSearchRoom = new System.Windows.Forms.Panel();
            this.tabPageSearchLesson = new System.Windows.Forms.TabPage();
            this.panelSearchLesson = new System.Windows.Forms.Panel();
            this.tbSearchLessonDate = new System.Windows.Forms.TextBox();
            this.lbSearchLessonStudent = new System.Windows.Forms.Label();
            this.cbSearchLessonRoom = new System.Windows.Forms.ComboBox();
            this.lbSearchLessonRoom = new System.Windows.Forms.Label();
            this.cbSearchLessonProgram = new System.Windows.Forms.ComboBox();
            this.lbSearchLessonProgram = new System.Windows.Forms.Label();
            this.tbSearchLessonTeacher = new System.Windows.Forms.TextBox();
            this.lbSearchTeacher = new System.Windows.Forms.Label();
            this.tbSearchLessonStudent = new System.Windows.Forms.TextBox();
            this.lbSearchLessonDay = new System.Windows.Forms.Label();
            this.panelSearchLabels = new System.Windows.Forms.Panel();
            this.labelGlobSearch = new System.Windows.Forms.Label();
            this.panelGlobLogo = new System.Windows.Forms.Panel();
            this.cbGlobMode = new System.Windows.Forms.ComboBox();
            this.pictureBoxGlobIcon = new System.Windows.Forms.PictureBox();
            this.labelGlobSagalingua = new System.Windows.Forms.Label();
            this.labelGlobCount = new System.Windows.Forms.Label();
            this.tabSchedPlan = new System.Windows.Forms.TabPage();
            this.panelSchedNewMatrix = new System.Windows.Forms.Panel();
            this.dgvSchedNew = new System.Windows.Forms.DataGridView();
            this.Slot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mondayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tuesdayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wednesdayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thursdayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fridayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saturdayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sundayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schedNewSlotList = new System.Windows.Forms.BindingSource(this.components);
            this.panelSchedNewParams = new System.Windows.Forms.Panel();
            this.tbSchedNewComment = new System.Windows.Forms.TextBox();
            this.lbSchedNewComment = new System.Windows.Forms.Label();
            this.lbSchedNewTeachVacation = new System.Windows.Forms.Label();
            this.lbSchedNewStudSchedule4 = new System.Windows.Forms.Label();
            this.lbSchedNewStudSchedule3 = new System.Windows.Forms.Label();
            this.lbSchedNewStudSchedule2 = new System.Windows.Forms.Label();
            this.lbSchedNewStudSchedule1 = new System.Windows.Forms.Label();
            this.butSchedNewAccept = new System.Windows.Forms.Button();
            this.cbSchedNewDuration = new System.Windows.Forms.ComboBox();
            this.lbSchedNewDuration = new System.Windows.Forms.Label();
            this.dtpSchedNew = new System.Windows.Forms.DateTimePicker();
            this.cbSchedNewTeacher = new System.Windows.Forms.ComboBox();
            this.lbSchedNewTeacher = new System.Windows.Forms.Label();
            this.lbSchedPlanName = new System.Windows.Forms.Label();
            this.lbSchedNewWeek = new System.Windows.Forms.Label();
            this.cbSchedNewLanguage = new System.Windows.Forms.ComboBox();
            this.lbSchedNewLanguage = new System.Windows.Forms.Label();
            this.cbSchedNewStud4 = new System.Windows.Forms.ComboBox();
            this.lbSchedNewStud1 = new System.Windows.Forms.Label();
            this.cbSchedNewStud3 = new System.Windows.Forms.ComboBox();
            this.lbSchedNewStud2 = new System.Windows.Forms.Label();
            this.cbSchedNewStud2 = new System.Windows.Forms.ComboBox();
            this.lbSchedNewStud3 = new System.Windows.Forms.Label();
            this.cbSchedNewStud1 = new System.Windows.Forms.ComboBox();
            this.lbSchedNewStud4 = new System.Windows.Forms.Label();
            this.tabSchedShow = new System.Windows.Forms.TabPage();
            this.lbSchedShowName = new System.Windows.Forms.Label();
            this.tabSchedCancel = new System.Windows.Forms.TabPage();
            this.lbSchedCancelName = new System.Windows.Forms.Label();
            this.tabPayStud = new System.Windows.Forms.TabPage();
            this.lbPayStudName = new System.Windows.Forms.Label();
            this.tabPayTeach = new System.Windows.Forms.TabPage();
            this.lbPayTeachName = new System.Windows.Forms.Label();
            this.tabPayExpense = new System.Windows.Forms.TabPage();
            this.lbPayExpenseName = new System.Windows.Forms.Label();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.lbFutureOpName = new System.Windows.Forms.Label();
            this.menuStripGlobalOps.SuspendLayout();
            this.panelGlobIndicators.SuspendLayout();
            this.tabControlOps.SuspendLayout();
            this.tabEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGlobDataControls)).BeginInit();
            this.splitContainerGlobDataControls.Panel1.SuspendLayout();
            this.splitContainerGlobDataControls.Panel2.SuspendLayout();
            this.splitContainerGlobDataControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGlobMasterDetail)).BeginInit();
            this.splitContainerGlobMasterDetail.Panel1.SuspendLayout();
            this.splitContainerGlobMasterDetail.Panel2.SuspendLayout();
            this.splitContainerGlobMasterDetail.SuspendLayout();
            this.tabControlModesTop.SuspendLayout();
            this.tabTopPageStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentList)).BeginInit();
            this.tabTopPageTeachers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeachers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teacherList)).BeginInit();
            this.tabTopPagePrograms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrograms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.programList)).BeginInit();
            this.tabTopPageRooms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roomList)).BeginInit();
            this.tabTopPageLessons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLesson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lessonList)).BeginInit();
            this.panelGlobEdit.SuspendLayout();
            this.tabControlModesBottom.SuspendLayout();
            this.tabBottomPageStudents.SuspendLayout();
            this.panelStudent.SuspendLayout();
            this.panelStudPrimary.SuspendLayout();
            this.groupBoxStudPrinaryRight.SuspendLayout();
            this.panelStudPrimaryLeft.SuspendLayout();
            this.panelStudSecondary.SuspendLayout();
            this.groupBoxStudSecondaryRight.SuspendLayout();
            this.panelStudSecondaryLeft.SuspendLayout();
            this.tabBottomPageTeachers.SuspendLayout();
            this.panelTeacherSecondary.SuspendLayout();
            this.panelTeacherPrimary.SuspendLayout();
            this.groupBoxTeacherPrimaryRight.SuspendLayout();
            this.panelTeacherPrimaryLeft.SuspendLayout();
            this.tabBottomPagePrograms.SuspendLayout();
            this.panelProgram.SuspendLayout();
            this.groupBoxProgram.SuspendLayout();
            this.panelProgramPrimaryLeft.SuspendLayout();
            this.tabBottomPageRooms.SuspendLayout();
            this.panelRoom.SuspendLayout();
            this.panelRoomPrimaryLeft.SuspendLayout();
            this.groupBoxRoom.SuspendLayout();
            this.tabBottomPageLessons.SuspendLayout();
            this.panelLesson.SuspendLayout();
            this.panelGlobPrevDelete.SuspendLayout();
            this.panelGlobNextNew.SuspendLayout();
            this.panelGlobSearch.SuspendLayout();
            this.panelSearchButtons.SuspendLayout();
            this.tabControlSearch.SuspendLayout();
            this.tabPageSearchStudent.SuspendLayout();
            this.panelStudSearch.SuspendLayout();
            this.tabPageSearchTeacher.SuspendLayout();
            this.panelSearchTeacher.SuspendLayout();
            this.tabPageSearchProgram.SuspendLayout();
            this.panelSearchProgram.SuspendLayout();
            this.tabPageSearchRoom.SuspendLayout();
            this.tabPageSearchLesson.SuspendLayout();
            this.panelSearchLesson.SuspendLayout();
            this.panelSearchLabels.SuspendLayout();
            this.panelGlobLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGlobIcon)).BeginInit();
            this.tabSchedPlan.SuspendLayout();
            this.panelSchedNewMatrix.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedNewSlotList)).BeginInit();
            this.panelSchedNewParams.SuspendLayout();
            this.tabSchedShow.SuspendLayout();
            this.tabSchedCancel.SuspendLayout();
            this.tabPayStud.SuspendLayout();
            this.tabPayTeach.SuspendLayout();
            this.tabPayExpense.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripGlobalOps
            // 
            this.menuStripGlobalOps.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemGlobDownload,
            this.editToolStripMenuItem,
            this.scheduleToolStripMenuItem,
            this.payToolStripMenuItem,
            this.menuItemGlobSave,
            this.menuItemGlobUpload,
            this.menuItemGlobExit});
            this.menuStripGlobalOps.Location = new System.Drawing.Point(0, 0);
            this.menuStripGlobalOps.Name = "menuStripGlobalOps";
            this.menuStripGlobalOps.Size = new System.Drawing.Size(1291, 24);
            this.menuStripGlobalOps.TabIndex = 0;
            this.menuStripGlobalOps.Text = "menuStrip1";
            // 
            // menuItemGlobDownload
            // 
            this.menuItemGlobDownload.Name = "menuItemGlobDownload";
            this.menuItemGlobDownload.Size = new System.Drawing.Size(73, 20);
            this.menuItemGlobDownload.Text = "Download";
            this.menuItemGlobDownload.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // scheduleToolStripMenuItem
            // 
            this.scheduleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.planToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.scheduleToolStripMenuItem.Name = "scheduleToolStripMenuItem";
            this.scheduleToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.scheduleToolStripMenuItem.Text = "Scheduling";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // planToolStripMenuItem
            // 
            this.planToolStripMenuItem.Name = "planToolStripMenuItem";
            this.planToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.planToolStripMenuItem.Text = "Plan";
            this.planToolStripMenuItem.Click += new System.EventHandler(this.planToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // payToolStripMenuItem
            // 
            this.payToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.studentsToolStripMenuItem,
            this.teachersToolStripMenuItem,
            this.reportExpenseToolStripMenuItem});
            this.payToolStripMenuItem.Name = "payToolStripMenuItem";
            this.payToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.payToolStripMenuItem.Text = "Payments";
            // 
            // studentsToolStripMenuItem
            // 
            this.studentsToolStripMenuItem.Name = "studentsToolStripMenuItem";
            this.studentsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.studentsToolStripMenuItem.Text = "Students";
            this.studentsToolStripMenuItem.Click += new System.EventHandler(this.studentsToolStripMenuItem_Click);
            // 
            // teachersToolStripMenuItem
            // 
            this.teachersToolStripMenuItem.Name = "teachersToolStripMenuItem";
            this.teachersToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.teachersToolStripMenuItem.Text = "Teachers";
            this.teachersToolStripMenuItem.Click += new System.EventHandler(this.teachersToolStripMenuItem_Click);
            // 
            // reportExpenseToolStripMenuItem
            // 
            this.reportExpenseToolStripMenuItem.Name = "reportExpenseToolStripMenuItem";
            this.reportExpenseToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.reportExpenseToolStripMenuItem.Text = "Expenses";
            this.reportExpenseToolStripMenuItem.Click += new System.EventHandler(this.reportExpenseToolStripMenuItem_Click);
            // 
            // menuItemGlobSave
            // 
            this.menuItemGlobSave.Name = "menuItemGlobSave";
            this.menuItemGlobSave.Size = new System.Drawing.Size(43, 20);
            this.menuItemGlobSave.Text = "Save";
            this.menuItemGlobSave.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // menuItemGlobUpload
            // 
            this.menuItemGlobUpload.Name = "menuItemGlobUpload";
            this.menuItemGlobUpload.Size = new System.Drawing.Size(57, 20);
            this.menuItemGlobUpload.Text = "Upload";
            this.menuItemGlobUpload.Click += new System.EventHandler(this.uploadToolStripMenuItem_Click);
            // 
            // menuItemGlobExit
            // 
            this.menuItemGlobExit.Name = "menuItemGlobExit";
            this.menuItemGlobExit.Size = new System.Drawing.Size(37, 20);
            this.menuItemGlobExit.Text = "Exit";
            this.menuItemGlobExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // labelGlobLastDownload
            // 
            this.labelGlobLastDownload.AutoSize = true;
            this.labelGlobLastDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlobLastDownload.Location = new System.Drawing.Point(140, 9);
            this.labelGlobLastDownload.Name = "labelGlobLastDownload";
            this.labelGlobLastDownload.Size = new System.Drawing.Size(0, 13);
            this.labelGlobLastDownload.TabIndex = 21;
            // 
            // labelGlobLastUpload
            // 
            this.labelGlobLastUpload.AutoSize = true;
            this.labelGlobLastUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlobLastUpload.Location = new System.Drawing.Point(313, 9);
            this.labelGlobLastUpload.Name = "labelGlobLastUpload";
            this.labelGlobLastUpload.Size = new System.Drawing.Size(0, 13);
            this.labelGlobLastUpload.TabIndex = 22;
            // 
            // panelGlobIndicators
            // 
            this.panelGlobIndicators.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGlobIndicators.Controls.Add(this.labelGlobLastUpload);
            this.panelGlobIndicators.Controls.Add(this.labelGlobLastDownload);
            this.panelGlobIndicators.Location = new System.Drawing.Point(417, 0);
            this.panelGlobIndicators.Name = "panelGlobIndicators";
            this.panelGlobIndicators.Size = new System.Drawing.Size(871, 24);
            this.panelGlobIndicators.TabIndex = 27;
            // 
            // tabControlOps
            // 
            this.tabControlOps.Controls.Add(this.tabEdit);
            this.tabControlOps.Controls.Add(this.tabSchedPlan);
            this.tabControlOps.Controls.Add(this.tabSchedShow);
            this.tabControlOps.Controls.Add(this.tabSchedCancel);
            this.tabControlOps.Controls.Add(this.tabPayStud);
            this.tabControlOps.Controls.Add(this.tabPayTeach);
            this.tabControlOps.Controls.Add(this.tabPayExpense);
            this.tabControlOps.Controls.Add(this.tabPage8);
            this.tabControlOps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOps.Location = new System.Drawing.Point(0, 24);
            this.tabControlOps.Name = "tabControlOps";
            this.tabControlOps.SelectedIndex = 0;
            this.tabControlOps.Size = new System.Drawing.Size(1291, 807);
            this.tabControlOps.TabIndex = 14;
            // 
            // tabEdit
            // 
            this.tabEdit.Controls.Add(this.splitContainerGlobDataControls);
            this.tabEdit.Location = new System.Drawing.Point(4, 22);
            this.tabEdit.Name = "tabEdit";
            this.tabEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabEdit.Size = new System.Drawing.Size(1283, 781);
            this.tabEdit.TabIndex = 0;
            this.tabEdit.Text = "Edit";
            this.tabEdit.UseVisualStyleBackColor = true;
            // 
            // splitContainerGlobDataControls
            // 
            this.splitContainerGlobDataControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerGlobDataControls.Location = new System.Drawing.Point(3, 3);
            this.splitContainerGlobDataControls.Name = "splitContainerGlobDataControls";
            // 
            // splitContainerGlobDataControls.Panel1
            // 
            this.splitContainerGlobDataControls.Panel1.Controls.Add(this.splitContainerGlobMasterDetail);
            // 
            // splitContainerGlobDataControls.Panel2
            // 
            this.splitContainerGlobDataControls.Panel2.Controls.Add(this.buttonGlobRefresh);
            this.splitContainerGlobDataControls.Panel2.Controls.Add(this.panelGlobSearch);
            this.splitContainerGlobDataControls.Panel2.Controls.Add(this.panelGlobLogo);
            this.splitContainerGlobDataControls.Size = new System.Drawing.Size(1277, 775);
            this.splitContainerGlobDataControls.SplitterDistance = 1025;
            this.splitContainerGlobDataControls.TabIndex = 26;
            // 
            // splitContainerGlobMasterDetail
            // 
            this.splitContainerGlobMasterDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerGlobMasterDetail.Location = new System.Drawing.Point(0, 0);
            this.splitContainerGlobMasterDetail.Name = "splitContainerGlobMasterDetail";
            this.splitContainerGlobMasterDetail.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerGlobMasterDetail.Panel1
            // 
            this.splitContainerGlobMasterDetail.Panel1.Controls.Add(this.tabControlModesTop);
            // 
            // splitContainerGlobMasterDetail.Panel2
            // 
            this.splitContainerGlobMasterDetail.Panel2.Controls.Add(this.panelGlobEdit);
            this.splitContainerGlobMasterDetail.Size = new System.Drawing.Size(1025, 775);
            this.splitContainerGlobMasterDetail.SplitterDistance = 386;
            this.splitContainerGlobMasterDetail.TabIndex = 25;
            // 
            // tabControlModesTop
            // 
            this.tabControlModesTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlModesTop.Controls.Add(this.tabTopPageStudents);
            this.tabControlModesTop.Controls.Add(this.tabTopPageTeachers);
            this.tabControlModesTop.Controls.Add(this.tabTopPagePrograms);
            this.tabControlModesTop.Controls.Add(this.tabTopPageRooms);
            this.tabControlModesTop.Controls.Add(this.tabTopPageLessons);
            this.tabControlModesTop.Location = new System.Drawing.Point(3, 6);
            this.tabControlModesTop.Name = "tabControlModesTop";
            this.tabControlModesTop.SelectedIndex = 0;
            this.tabControlModesTop.Size = new System.Drawing.Size(1017, 377);
            this.tabControlModesTop.TabIndex = 14;
            // 
            // tabTopPageStudents
            // 
            this.tabTopPageStudents.Controls.Add(this.dgvStudents);
            this.tabTopPageStudents.Location = new System.Drawing.Point(4, 22);
            this.tabTopPageStudents.Name = "tabTopPageStudents";
            this.tabTopPageStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tabTopPageStudents.Size = new System.Drawing.Size(1009, 351);
            this.tabTopPageStudents.TabIndex = 0;
            this.tabTopPageStudents.Text = "Students";
            this.tabTopPageStudents.UseVisualStyleBackColor = true;
            // 
            // dgvStudents
            // 
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.AllowUserToOrderColumns = true;
            this.dgvStudents.AutoGenerateColumns = false;
            this.dgvStudents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvStudColumnStatus,
            this.dgvStudColumnFirstName,
            this.dgvStudColumnLastName,
            this.dgvStudColumnEmail,
            this.dgvStudColumnPhone,
            this.dgvStudColumnLearningLanguage,
            this.dgvStudColumnLevel,
            this.dgvStudColumnNativeLanguage,
            this.dgvStudColumnOtherLanguage,
            this.dgvStudColumnBirthday,
            this.dgvStudColumnSource,
            this.dgvStudColumnAddress});
            this.dgvStudents.DataSource = this.studentList;
            this.dgvStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudents.Location = new System.Drawing.Point(3, 3);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.Size = new System.Drawing.Size(1003, 345);
            this.dgvStudents.TabIndex = 13;
            this.dgvStudents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudents_CellContentClick);
            this.dgvStudents.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvStudents_ColumnHeaderMouseClick);
            // 
            // dgvStudColumnStatus
            // 
            this.dgvStudColumnStatus.DataPropertyName = "Status";
            this.dgvStudColumnStatus.HeaderText = "Status";
            this.dgvStudColumnStatus.Name = "dgvStudColumnStatus";
            this.dgvStudColumnStatus.ReadOnly = true;
            // 
            // dgvStudColumnFirstName
            // 
            this.dgvStudColumnFirstName.DataPropertyName = "FirstName";
            this.dgvStudColumnFirstName.HeaderText = "First Name";
            this.dgvStudColumnFirstName.Name = "dgvStudColumnFirstName";
            this.dgvStudColumnFirstName.ReadOnly = true;
            // 
            // dgvStudColumnLastName
            // 
            this.dgvStudColumnLastName.DataPropertyName = "LastName";
            this.dgvStudColumnLastName.HeaderText = "Last Name";
            this.dgvStudColumnLastName.Name = "dgvStudColumnLastName";
            this.dgvStudColumnLastName.ReadOnly = true;
            // 
            // dgvStudColumnEmail
            // 
            this.dgvStudColumnEmail.DataPropertyName = "Email";
            this.dgvStudColumnEmail.HeaderText = "Email";
            this.dgvStudColumnEmail.Name = "dgvStudColumnEmail";
            this.dgvStudColumnEmail.ReadOnly = true;
            // 
            // dgvStudColumnPhone
            // 
            this.dgvStudColumnPhone.DataPropertyName = "Phone";
            this.dgvStudColumnPhone.HeaderText = "Phone";
            this.dgvStudColumnPhone.Name = "dgvStudColumnPhone";
            this.dgvStudColumnPhone.ReadOnly = true;
            // 
            // dgvStudColumnLearningLanguage
            // 
            this.dgvStudColumnLearningLanguage.DataPropertyName = "LearningLanguage";
            this.dgvStudColumnLearningLanguage.HeaderText = "Learning";
            this.dgvStudColumnLearningLanguage.Name = "dgvStudColumnLearningLanguage";
            this.dgvStudColumnLearningLanguage.ReadOnly = true;
            // 
            // dgvStudColumnLevel
            // 
            this.dgvStudColumnLevel.DataPropertyName = "Level";
            this.dgvStudColumnLevel.HeaderText = "Level";
            this.dgvStudColumnLevel.Name = "dgvStudColumnLevel";
            this.dgvStudColumnLevel.ReadOnly = true;
            // 
            // dgvStudColumnNativeLanguage
            // 
            this.dgvStudColumnNativeLanguage.DataPropertyName = "NativeLanguage";
            this.dgvStudColumnNativeLanguage.HeaderText = "Native";
            this.dgvStudColumnNativeLanguage.Name = "dgvStudColumnNativeLanguage";
            this.dgvStudColumnNativeLanguage.ReadOnly = true;
            // 
            // dgvStudColumnOtherLanguage
            // 
            this.dgvStudColumnOtherLanguage.DataPropertyName = "OtherLanguage";
            this.dgvStudColumnOtherLanguage.HeaderText = "Other";
            this.dgvStudColumnOtherLanguage.Name = "dgvStudColumnOtherLanguage";
            this.dgvStudColumnOtherLanguage.ReadOnly = true;
            // 
            // dgvStudColumnBirthday
            // 
            this.dgvStudColumnBirthday.DataPropertyName = "Birthday";
            this.dgvStudColumnBirthday.HeaderText = "Birthday";
            this.dgvStudColumnBirthday.Name = "dgvStudColumnBirthday";
            this.dgvStudColumnBirthday.ReadOnly = true;
            // 
            // dgvStudColumnSource
            // 
            this.dgvStudColumnSource.DataPropertyName = "Source";
            this.dgvStudColumnSource.HeaderText = "Source";
            this.dgvStudColumnSource.Name = "dgvStudColumnSource";
            this.dgvStudColumnSource.ReadOnly = true;
            // 
            // dgvStudColumnAddress
            // 
            this.dgvStudColumnAddress.DataPropertyName = "MailingAddress";
            this.dgvStudColumnAddress.HeaderText = "Address";
            this.dgvStudColumnAddress.Name = "dgvStudColumnAddress";
            this.dgvStudColumnAddress.ReadOnly = true;
            // 
            // studentList
            // 
            this.studentList.DataSource = typeof(RecordKeeper.Student);
            // 
            // tabTopPageTeachers
            // 
            this.tabTopPageTeachers.Controls.Add(this.dgvTeachers);
            this.tabTopPageTeachers.Location = new System.Drawing.Point(4, 22);
            this.tabTopPageTeachers.Name = "tabTopPageTeachers";
            this.tabTopPageTeachers.Padding = new System.Windows.Forms.Padding(3);
            this.tabTopPageTeachers.Size = new System.Drawing.Size(1009, 351);
            this.tabTopPageTeachers.TabIndex = 1;
            this.tabTopPageTeachers.Text = "Teachers";
            this.tabTopPageTeachers.UseVisualStyleBackColor = true;
            // 
            // dgvTeachers
            // 
            this.dgvTeachers.AllowUserToAddRows = false;
            this.dgvTeachers.AllowUserToDeleteRows = false;
            this.dgvTeachers.AllowUserToOrderColumns = true;
            this.dgvTeachers.AutoGenerateColumns = false;
            this.dgvTeachers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTeachers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.statusDataGridViewTextBoxColumn,
            this.firstNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.phoneDataGridViewTextBoxColumn,
            this.languageDataGridViewTextBoxColumn,
            this.language2DataGridViewTextBoxColumn,
            this.languageDetailDataGridViewTextBoxColumn,
            this.vacationsDataGridViewTextBoxColumn,
            this.commentsDataGridViewTextBoxColumn1,
            this.mailingAddressDataGridViewTextBoxColumn,
            this.birthdayDataGridViewTextBoxColumn});
            this.dgvTeachers.DataSource = this.teacherList;
            this.dgvTeachers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTeachers.Location = new System.Drawing.Point(3, 3);
            this.dgvTeachers.Name = "dgvTeachers";
            this.dgvTeachers.ReadOnly = true;
            this.dgvTeachers.Size = new System.Drawing.Size(1003, 345);
            this.dgvTeachers.TabIndex = 1;
            this.dgvTeachers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTeachers_CellContentClick);
            this.dgvTeachers.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTeachers_ColumnHeaderMouseClick);
            this.dgvTeachers.SelectionChanged += new System.EventHandler(this.dgvTeachers_SelectionChanged);
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.HeaderText = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // phoneDataGridViewTextBoxColumn
            // 
            this.phoneDataGridViewTextBoxColumn.DataPropertyName = "Phone";
            this.phoneDataGridViewTextBoxColumn.HeaderText = "Phone";
            this.phoneDataGridViewTextBoxColumn.Name = "phoneDataGridViewTextBoxColumn";
            this.phoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // languageDataGridViewTextBoxColumn
            // 
            this.languageDataGridViewTextBoxColumn.DataPropertyName = "Language";
            this.languageDataGridViewTextBoxColumn.HeaderText = "Language";
            this.languageDataGridViewTextBoxColumn.Name = "languageDataGridViewTextBoxColumn";
            this.languageDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // language2DataGridViewTextBoxColumn
            // 
            this.language2DataGridViewTextBoxColumn.DataPropertyName = "Language2";
            this.language2DataGridViewTextBoxColumn.HeaderText = "Language2";
            this.language2DataGridViewTextBoxColumn.Name = "language2DataGridViewTextBoxColumn";
            this.language2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // languageDetailDataGridViewTextBoxColumn
            // 
            this.languageDetailDataGridViewTextBoxColumn.DataPropertyName = "LanguageDetail";
            this.languageDetailDataGridViewTextBoxColumn.HeaderText = "LanguageDetail";
            this.languageDetailDataGridViewTextBoxColumn.Name = "languageDetailDataGridViewTextBoxColumn";
            this.languageDetailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vacationsDataGridViewTextBoxColumn
            // 
            this.vacationsDataGridViewTextBoxColumn.DataPropertyName = "Vacations";
            this.vacationsDataGridViewTextBoxColumn.HeaderText = "Vacations";
            this.vacationsDataGridViewTextBoxColumn.Name = "vacationsDataGridViewTextBoxColumn";
            this.vacationsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // commentsDataGridViewTextBoxColumn1
            // 
            this.commentsDataGridViewTextBoxColumn1.DataPropertyName = "Comments";
            this.commentsDataGridViewTextBoxColumn1.HeaderText = "Comments";
            this.commentsDataGridViewTextBoxColumn1.Name = "commentsDataGridViewTextBoxColumn1";
            this.commentsDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // mailingAddressDataGridViewTextBoxColumn
            // 
            this.mailingAddressDataGridViewTextBoxColumn.DataPropertyName = "MailingAddress";
            this.mailingAddressDataGridViewTextBoxColumn.HeaderText = "MailingAddress";
            this.mailingAddressDataGridViewTextBoxColumn.Name = "mailingAddressDataGridViewTextBoxColumn";
            this.mailingAddressDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // birthdayDataGridViewTextBoxColumn
            // 
            this.birthdayDataGridViewTextBoxColumn.DataPropertyName = "Birthday";
            this.birthdayDataGridViewTextBoxColumn.HeaderText = "Birthday";
            this.birthdayDataGridViewTextBoxColumn.Name = "birthdayDataGridViewTextBoxColumn";
            this.birthdayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // teacherList
            // 
            this.teacherList.DataSource = typeof(RecordKeeper.Teacher);
            this.teacherList.CurrentChanged += new System.EventHandler(this.dgvTeachers__CurrentChanged);
            // 
            // tabTopPagePrograms
            // 
            this.tabTopPagePrograms.Controls.Add(this.dgvPrograms);
            this.tabTopPagePrograms.Location = new System.Drawing.Point(4, 22);
            this.tabTopPagePrograms.Name = "tabTopPagePrograms";
            this.tabTopPagePrograms.Padding = new System.Windows.Forms.Padding(3);
            this.tabTopPagePrograms.Size = new System.Drawing.Size(1009, 351);
            this.tabTopPagePrograms.TabIndex = 2;
            this.tabTopPagePrograms.Text = "Programs";
            this.tabTopPagePrograms.UseVisualStyleBackColor = true;
            // 
            // dgvPrograms
            // 
            this.dgvPrograms.AllowUserToAddRows = false;
            this.dgvPrograms.AllowUserToDeleteRows = false;
            this.dgvPrograms.AllowUserToOrderColumns = true;
            this.dgvPrograms.AutoGenerateColumns = false;
            this.dgvPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrograms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codeDataGridViewTextBoxColumn,
            this.languageDataGridViewTextBoxColumn1,
            this.levelDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn1,
            this.priceDataGridViewTextBoxColumn,
            this.Summary});
            this.dgvPrograms.DataSource = this.programList;
            this.dgvPrograms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPrograms.Location = new System.Drawing.Point(3, 3);
            this.dgvPrograms.Name = "dgvPrograms";
            this.dgvPrograms.ReadOnly = true;
            this.dgvPrograms.Size = new System.Drawing.Size(1003, 345);
            this.dgvPrograms.TabIndex = 0;
            this.dgvPrograms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPrograms_CellContentClick);
            this.dgvPrograms.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPrograms_ColumnHeaderMouseClick);
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // languageDataGridViewTextBoxColumn1
            // 
            this.languageDataGridViewTextBoxColumn1.DataPropertyName = "Language";
            this.languageDataGridViewTextBoxColumn1.HeaderText = "Language";
            this.languageDataGridViewTextBoxColumn1.Name = "languageDataGridViewTextBoxColumn1";
            this.languageDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            this.levelDataGridViewTextBoxColumn.HeaderText = "Level";
            this.levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            this.levelDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Summary
            // 
            this.Summary.DataPropertyName = "Summary";
            this.Summary.HeaderText = "Summary";
            this.Summary.Name = "Summary";
            this.Summary.ReadOnly = true;
            // 
            // programList
            // 
            this.programList.DataSource = typeof(RecordKeeper.Program);
            // 
            // tabTopPageRooms
            // 
            this.tabTopPageRooms.Controls.Add(this.dgvRooms);
            this.tabTopPageRooms.Location = new System.Drawing.Point(4, 22);
            this.tabTopPageRooms.Name = "tabTopPageRooms";
            this.tabTopPageRooms.Padding = new System.Windows.Forms.Padding(3);
            this.tabTopPageRooms.Size = new System.Drawing.Size(1009, 351);
            this.tabTopPageRooms.TabIndex = 3;
            this.tabTopPageRooms.Text = "Rooms";
            this.tabTopPageRooms.UseVisualStyleBackColor = true;
            // 
            // dgvRooms
            // 
            this.dgvRooms.AllowUserToAddRows = false;
            this.dgvRooms.AllowUserToDeleteRows = false;
            this.dgvRooms.AllowUserToOrderColumns = true;
            this.dgvRooms.AutoGenerateColumns = false;
            this.dgvRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRooms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.capacityDataGridViewTextBoxColumn,
            this.rankDataGridViewTextBoxColumn,
            this.tagsDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.commentsDataGridViewTextBoxColumn});
            this.dgvRooms.DataSource = this.roomList;
            this.dgvRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRooms.Location = new System.Drawing.Point(3, 3);
            this.dgvRooms.Name = "dgvRooms";
            this.dgvRooms.ReadOnly = true;
            this.dgvRooms.Size = new System.Drawing.Size(1003, 345);
            this.dgvRooms.TabIndex = 0;
            this.dgvRooms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRooms_CellContentClick);
            this.dgvRooms.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRooms_ColumnHeaderMouseClick);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // capacityDataGridViewTextBoxColumn
            // 
            this.capacityDataGridViewTextBoxColumn.DataPropertyName = "Capacity";
            this.capacityDataGridViewTextBoxColumn.HeaderText = "Capacity";
            this.capacityDataGridViewTextBoxColumn.Name = "capacityDataGridViewTextBoxColumn";
            this.capacityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rankDataGridViewTextBoxColumn
            // 
            this.rankDataGridViewTextBoxColumn.DataPropertyName = "Rank";
            this.rankDataGridViewTextBoxColumn.HeaderText = "Rank";
            this.rankDataGridViewTextBoxColumn.Name = "rankDataGridViewTextBoxColumn";
            this.rankDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tagsDataGridViewTextBoxColumn
            // 
            this.tagsDataGridViewTextBoxColumn.DataPropertyName = "Tags";
            this.tagsDataGridViewTextBoxColumn.HeaderText = "Tags";
            this.tagsDataGridViewTextBoxColumn.Name = "tagsDataGridViewTextBoxColumn";
            this.tagsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // commentsDataGridViewTextBoxColumn
            // 
            this.commentsDataGridViewTextBoxColumn.DataPropertyName = "Comments";
            this.commentsDataGridViewTextBoxColumn.HeaderText = "Comments";
            this.commentsDataGridViewTextBoxColumn.Name = "commentsDataGridViewTextBoxColumn";
            this.commentsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // roomList
            // 
            this.roomList.DataSource = typeof(RecordKeeper.Room);
            // 
            // tabTopPageLessons
            // 
            this.tabTopPageLessons.Controls.Add(this.dgvLesson);
            this.tabTopPageLessons.Location = new System.Drawing.Point(4, 22);
            this.tabTopPageLessons.Name = "tabTopPageLessons";
            this.tabTopPageLessons.Padding = new System.Windows.Forms.Padding(3);
            this.tabTopPageLessons.Size = new System.Drawing.Size(1009, 351);
            this.tabTopPageLessons.TabIndex = 4;
            this.tabTopPageLessons.Text = "Lessons";
            this.tabTopPageLessons.UseVisualStyleBackColor = true;
            // 
            // dgvLesson
            // 
            this.dgvLesson.AllowUserToAddRows = false;
            this.dgvLesson.AllowUserToDeleteRows = false;
            this.dgvLesson.AllowUserToOrderColumns = true;
            this.dgvLesson.AutoGenerateColumns = false;
            this.dgvLesson.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLesson.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.State,
            this.dayDataGridViewTextBoxColumn,
            this.Start,
            this.End,
            this.programDataGridViewTextBoxColumn,
            this.roomDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.student1DataGridViewTextBoxColumn,
            this.student2DataGridViewTextBoxColumn,
            this.student3DataGridViewTextBoxColumn,
            this.student10DataGridViewTextBoxColumn,
            this.Student4,
            this.teacher1DataGridViewTextBoxColumn,
            this.teacher2DataGridViewTextBoxColumn,
            this.commentsDataGridViewTextBoxColumn2});
            this.dgvLesson.DataSource = this.lessonList;
            this.dgvLesson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLesson.Location = new System.Drawing.Point(3, 3);
            this.dgvLesson.Name = "dgvLesson";
            this.dgvLesson.ReadOnly = true;
            this.dgvLesson.Size = new System.Drawing.Size(1003, 345);
            this.dgvLesson.TabIndex = 0;
            this.dgvLesson.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLesson_CellContentClick);
            this.dgvLesson.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLesson_ColumnHeaderMouseClick);
            // 
            // State
            // 
            this.State.DataPropertyName = "State";
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // dayDataGridViewTextBoxColumn
            // 
            this.dayDataGridViewTextBoxColumn.DataPropertyName = "Day";
            this.dayDataGridViewTextBoxColumn.HeaderText = "Day";
            this.dayDataGridViewTextBoxColumn.Name = "dayDataGridViewTextBoxColumn";
            this.dayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Start
            // 
            this.Start.DataPropertyName = "Start";
            this.Start.HeaderText = "Start";
            this.Start.Name = "Start";
            this.Start.ReadOnly = true;
            // 
            // End
            // 
            this.End.DataPropertyName = "End";
            this.End.HeaderText = "End";
            this.End.Name = "End";
            this.End.ReadOnly = true;
            // 
            // programDataGridViewTextBoxColumn
            // 
            this.programDataGridViewTextBoxColumn.DataPropertyName = "Program";
            this.programDataGridViewTextBoxColumn.HeaderText = "Program";
            this.programDataGridViewTextBoxColumn.Name = "programDataGridViewTextBoxColumn";
            this.programDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // roomDataGridViewTextBoxColumn
            // 
            this.roomDataGridViewTextBoxColumn.DataPropertyName = "Room";
            this.roomDataGridViewTextBoxColumn.HeaderText = "Room";
            this.roomDataGridViewTextBoxColumn.Name = "roomDataGridViewTextBoxColumn";
            this.roomDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // student1DataGridViewTextBoxColumn
            // 
            this.student1DataGridViewTextBoxColumn.DataPropertyName = "Student1";
            this.student1DataGridViewTextBoxColumn.HeaderText = "Student1";
            this.student1DataGridViewTextBoxColumn.Name = "student1DataGridViewTextBoxColumn";
            this.student1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // student2DataGridViewTextBoxColumn
            // 
            this.student2DataGridViewTextBoxColumn.DataPropertyName = "Student2";
            this.student2DataGridViewTextBoxColumn.HeaderText = "Student2";
            this.student2DataGridViewTextBoxColumn.Name = "student2DataGridViewTextBoxColumn";
            this.student2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // student3DataGridViewTextBoxColumn
            // 
            this.student3DataGridViewTextBoxColumn.DataPropertyName = "Student3";
            this.student3DataGridViewTextBoxColumn.HeaderText = "Student3";
            this.student3DataGridViewTextBoxColumn.Name = "student3DataGridViewTextBoxColumn";
            this.student3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // student10DataGridViewTextBoxColumn
            // 
            this.student10DataGridViewTextBoxColumn.DataPropertyName = "Student10";
            this.student10DataGridViewTextBoxColumn.HeaderText = "Student10";
            this.student10DataGridViewTextBoxColumn.Name = "student10DataGridViewTextBoxColumn";
            this.student10DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Student4
            // 
            this.Student4.DataPropertyName = "Student4";
            this.Student4.HeaderText = "Student4";
            this.Student4.Name = "Student4";
            this.Student4.ReadOnly = true;
            // 
            // teacher1DataGridViewTextBoxColumn
            // 
            this.teacher1DataGridViewTextBoxColumn.DataPropertyName = "Teacher1";
            this.teacher1DataGridViewTextBoxColumn.HeaderText = "Teacher1";
            this.teacher1DataGridViewTextBoxColumn.Name = "teacher1DataGridViewTextBoxColumn";
            this.teacher1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // teacher2DataGridViewTextBoxColumn
            // 
            this.teacher2DataGridViewTextBoxColumn.DataPropertyName = "Teacher2";
            this.teacher2DataGridViewTextBoxColumn.HeaderText = "Teacher2";
            this.teacher2DataGridViewTextBoxColumn.Name = "teacher2DataGridViewTextBoxColumn";
            this.teacher2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // commentsDataGridViewTextBoxColumn2
            // 
            this.commentsDataGridViewTextBoxColumn2.DataPropertyName = "Comments";
            this.commentsDataGridViewTextBoxColumn2.HeaderText = "Comments";
            this.commentsDataGridViewTextBoxColumn2.Name = "commentsDataGridViewTextBoxColumn2";
            this.commentsDataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // lessonList
            // 
            this.lessonList.DataSource = typeof(RecordKeeper.Lesson);
            // 
            // panelGlobEdit
            // 
            this.panelGlobEdit.Controls.Add(this.tabControlModesBottom);
            this.panelGlobEdit.Controls.Add(this.panelGlobPrevDelete);
            this.panelGlobEdit.Controls.Add(this.panelGlobNextNew);
            this.panelGlobEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGlobEdit.Location = new System.Drawing.Point(0, 0);
            this.panelGlobEdit.Name = "panelGlobEdit";
            this.panelGlobEdit.Size = new System.Drawing.Size(1025, 385);
            this.panelGlobEdit.TabIndex = 23;
            // 
            // tabControlModesBottom
            // 
            this.tabControlModesBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlModesBottom.Controls.Add(this.tabBottomPageStudents);
            this.tabControlModesBottom.Controls.Add(this.tabBottomPageTeachers);
            this.tabControlModesBottom.Controls.Add(this.tabBottomPagePrograms);
            this.tabControlModesBottom.Controls.Add(this.tabBottomPageRooms);
            this.tabControlModesBottom.Controls.Add(this.tabBottomPageLessons);
            this.tabControlModesBottom.Location = new System.Drawing.Point(46, 3);
            this.tabControlModesBottom.Name = "tabControlModesBottom";
            this.tabControlModesBottom.SelectedIndex = 0;
            this.tabControlModesBottom.Size = new System.Drawing.Size(930, 382);
            this.tabControlModesBottom.TabIndex = 14;
            // 
            // tabBottomPageStudents
            // 
            this.tabBottomPageStudents.Controls.Add(this.panelStudent);
            this.tabBottomPageStudents.Location = new System.Drawing.Point(4, 22);
            this.tabBottomPageStudents.Name = "tabBottomPageStudents";
            this.tabBottomPageStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tabBottomPageStudents.Size = new System.Drawing.Size(922, 356);
            this.tabBottomPageStudents.TabIndex = 0;
            this.tabBottomPageStudents.Text = "Students";
            this.tabBottomPageStudents.UseVisualStyleBackColor = true;
            // 
            // panelStudent
            // 
            this.panelStudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.panelStudent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStudent.Controls.Add(this.panelStudPrimary);
            this.panelStudent.Controls.Add(this.panelStudSecondary);
            this.panelStudent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStudent.Location = new System.Drawing.Point(3, 3);
            this.panelStudent.Name = "panelStudent";
            this.panelStudent.Size = new System.Drawing.Size(916, 350);
            this.panelStudent.TabIndex = 5;
            // 
            // panelStudPrimary
            // 
            this.panelStudPrimary.Controls.Add(this.groupBoxStudPrinaryRight);
            this.panelStudPrimary.Controls.Add(this.panelStudPrimaryLeft);
            this.panelStudPrimary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStudPrimary.Location = new System.Drawing.Point(0, 0);
            this.panelStudPrimary.Name = "panelStudPrimary";
            this.panelStudPrimary.Size = new System.Drawing.Size(914, 158);
            this.panelStudPrimary.TabIndex = 40;
            // 
            // groupBoxStudPrinaryRight
            // 
            this.groupBoxStudPrinaryRight.Controls.Add(this.labelStudDetailsSource);
            this.groupBoxStudPrinaryRight.Controls.Add(this.labelStudDetailsLanguage);
            this.groupBoxStudPrinaryRight.Controls.Add(this.labelStudAddress1);
            this.groupBoxStudPrinaryRight.Controls.Add(this.tbStudAddress1);
            this.groupBoxStudPrinaryRight.Controls.Add(this.tbStudLanguageDetail);
            this.groupBoxStudPrinaryRight.Controls.Add(this.tbStudSourceDetail);
            this.groupBoxStudPrinaryRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxStudPrinaryRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.groupBoxStudPrinaryRight.Location = new System.Drawing.Point(490, 0);
            this.groupBoxStudPrinaryRight.Name = "groupBoxStudPrinaryRight";
            this.groupBoxStudPrinaryRight.Size = new System.Drawing.Size(424, 158);
            this.groupBoxStudPrinaryRight.TabIndex = 2;
            this.groupBoxStudPrinaryRight.TabStop = false;
            // 
            // labelStudDetailsSource
            // 
            this.labelStudDetailsSource.AutoSize = true;
            this.labelStudDetailsSource.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelStudDetailsSource.Location = new System.Drawing.Point(17, 114);
            this.labelStudDetailsSource.Name = "labelStudDetailsSource";
            this.labelStudDetailsSource.Size = new System.Drawing.Size(80, 13);
            this.labelStudDetailsSource.TabIndex = 16;
            this.labelStudDetailsSource.Text = "Details (source)";
            // 
            // labelStudDetailsLanguage
            // 
            this.labelStudDetailsLanguage.AutoSize = true;
            this.labelStudDetailsLanguage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelStudDetailsLanguage.Location = new System.Drawing.Point(17, 79);
            this.labelStudDetailsLanguage.Name = "labelStudDetailsLanguage";
            this.labelStudDetailsLanguage.Size = new System.Drawing.Size(92, 13);
            this.labelStudDetailsLanguage.TabIndex = 15;
            this.labelStudDetailsLanguage.Text = "Details (language)";
            // 
            // labelStudAddress1
            // 
            this.labelStudAddress1.AutoSize = true;
            this.labelStudAddress1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelStudAddress1.Location = new System.Drawing.Point(17, 41);
            this.labelStudAddress1.Name = "labelStudAddress1";
            this.labelStudAddress1.Size = new System.Drawing.Size(48, 13);
            this.labelStudAddress1.TabIndex = 10;
            this.labelStudAddress1.Text = "Address:";
            // 
            // tbStudAddress1
            // 
            this.tbStudAddress1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStudAddress1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "MailingAddress", true));
            this.tbStudAddress1.Location = new System.Drawing.Point(6, 58);
            this.tbStudAddress1.Name = "tbStudAddress1";
            this.tbStudAddress1.Size = new System.Drawing.Size(412, 20);
            this.tbStudAddress1.TabIndex = 7;
            this.tbStudAddress1.TextChanged += new System.EventHandler(this.tbStudAddress_TextChanged);
            // 
            // tbStudLanguageDetail
            // 
            this.tbStudLanguageDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStudLanguageDetail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "LanguageDetail", true));
            this.tbStudLanguageDetail.Location = new System.Drawing.Point(6, 93);
            this.tbStudLanguageDetail.Name = "tbStudLanguageDetail";
            this.tbStudLanguageDetail.Size = new System.Drawing.Size(412, 20);
            this.tbStudLanguageDetail.TabIndex = 11;
            this.tbStudLanguageDetail.TextChanged += new System.EventHandler(this.tbStudLanguageDetail_TextChanged);
            // 
            // tbStudSourceDetail
            // 
            this.tbStudSourceDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStudSourceDetail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "SourceDetail", true));
            this.tbStudSourceDetail.Location = new System.Drawing.Point(6, 130);
            this.tbStudSourceDetail.Name = "tbStudSourceDetail";
            this.tbStudSourceDetail.Size = new System.Drawing.Size(412, 20);
            this.tbStudSourceDetail.TabIndex = 15;
            this.tbStudSourceDetail.TextChanged += new System.EventHandler(this.tbStudxSourceDetail_TextChanged);
            // 
            // panelStudPrimaryLeft
            // 
            this.panelStudPrimaryLeft.Controls.Add(this.cbStudSpeaks);
            this.panelStudPrimaryLeft.Controls.Add(this.cbStudOther);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudBirthday);
            this.panelStudPrimaryLeft.Controls.Add(this.tbStudLastName);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudAlso);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudFirstname);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudStatus1);
            this.panelStudPrimaryLeft.Controls.Add(this.tbStudFirstName);
            this.panelStudPrimaryLeft.Controls.Add(this.cbStudStatus);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudLastName1);
            this.panelStudPrimaryLeft.Controls.Add(this.cbStudLearns);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudEmail1);
            this.panelStudPrimaryLeft.Controls.Add(this.tbStudBirthday);
            this.panelStudPrimaryLeft.Controls.Add(this.tbStudEmail);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudSpeaks1);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudCellPhone);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudLevel1);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudHomePhone);
            this.panelStudPrimaryLeft.Controls.Add(this.cbStudLevel);
            this.panelStudPrimaryLeft.Controls.Add(this.tbStudCellPhone);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudSource1);
            this.panelStudPrimaryLeft.Controls.Add(this.tbStudHomePhone);
            this.panelStudPrimaryLeft.Controls.Add(this.labelStudSpeaks2);
            this.panelStudPrimaryLeft.Controls.Add(this.cbStudSource);
            this.panelStudPrimaryLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStudPrimaryLeft.Location = new System.Drawing.Point(0, 0);
            this.panelStudPrimaryLeft.Name = "panelStudPrimaryLeft";
            this.panelStudPrimaryLeft.Size = new System.Drawing.Size(490, 158);
            this.panelStudPrimaryLeft.TabIndex = 1;
            // 
            // cbStudSpeaks
            // 
            this.cbStudSpeaks.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "NativeLanguage", true));
            this.cbStudSpeaks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudSpeaks.FormattingEnabled = true;
            this.cbStudSpeaks.Location = new System.Drawing.Point(148, 92);
            this.cbStudSpeaks.Name = "cbStudSpeaks";
            this.cbStudSpeaks.Size = new System.Drawing.Size(162, 21);
            this.cbStudSpeaks.TabIndex = 9;
            // 
            // cbStudOther
            // 
            this.cbStudOther.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "OtherLanguage", true));
            this.cbStudOther.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudOther.FormattingEnabled = true;
            this.cbStudOther.Location = new System.Drawing.Point(328, 92);
            this.cbStudOther.Name = "cbStudOther";
            this.cbStudOther.Size = new System.Drawing.Size(152, 21);
            this.cbStudOther.TabIndex = 10;
            // 
            // labelStudBirthday
            // 
            this.labelStudBirthday.AutoSize = true;
            this.labelStudBirthday.Location = new System.Drawing.Point(155, 114);
            this.labelStudBirthday.Name = "labelStudBirthday";
            this.labelStudBirthday.Size = new System.Drawing.Size(48, 13);
            this.labelStudBirthday.TabIndex = 18;
            this.labelStudBirthday.Text = "Birthday:";
            // 
            // tbStudLastName
            // 
            this.tbStudLastName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "LastName", true));
            this.tbStudLastName.Location = new System.Drawing.Point(148, 19);
            this.tbStudLastName.Name = "tbStudLastName";
            this.tbStudLastName.Size = new System.Drawing.Size(162, 20);
            this.tbStudLastName.TabIndex = 2;
            this.tbStudLastName.TextChanged += new System.EventHandler(this.tbStudLastName_TextChanged);
            // 
            // labelStudAlso
            // 
            this.labelStudAlso.AutoSize = true;
            this.labelStudAlso.Location = new System.Drawing.Point(334, 79);
            this.labelStudAlso.Name = "labelStudAlso";
            this.labelStudAlso.Size = new System.Drawing.Size(27, 13);
            this.labelStudAlso.TabIndex = 17;
            this.labelStudAlso.Text = "Also";
            // 
            // labelStudFirstname
            // 
            this.labelStudFirstname.AutoSize = true;
            this.labelStudFirstname.Location = new System.Drawing.Point(5, 6);
            this.labelStudFirstname.Name = "labelStudFirstname";
            this.labelStudFirstname.Size = new System.Drawing.Size(58, 13);
            this.labelStudFirstname.TabIndex = 0;
            this.labelStudFirstname.Text = "First name:";
            // 
            // labelStudStatus1
            // 
            this.labelStudStatus1.AutoSize = true;
            this.labelStudStatus1.Location = new System.Drawing.Point(5, 41);
            this.labelStudStatus1.Name = "labelStudStatus1";
            this.labelStudStatus1.Size = new System.Drawing.Size(40, 13);
            this.labelStudStatus1.TabIndex = 21;
            this.labelStudStatus1.Text = "Status:";
            // 
            // tbStudFirstName
            // 
            this.tbStudFirstName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "FirstName", true));
            this.tbStudFirstName.Location = new System.Drawing.Point(5, 19);
            this.tbStudFirstName.Name = "tbStudFirstName";
            this.tbStudFirstName.Size = new System.Drawing.Size(130, 20);
            this.tbStudFirstName.TabIndex = 1;
            this.tbStudFirstName.TextChanged += new System.EventHandler(this.tbStudFirstName_TextChanged);
            // 
            // cbStudStatus
            // 
            this.cbStudStatus.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Status", true));
            this.cbStudStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudStatus.FormattingEnabled = true;
            this.cbStudStatus.Location = new System.Drawing.Point(5, 57);
            this.cbStudStatus.Name = "cbStudStatus";
            this.cbStudStatus.Size = new System.Drawing.Size(130, 21);
            this.cbStudStatus.TabIndex = 4;
            // 
            // labelStudLastName1
            // 
            this.labelStudLastName1.AutoSize = true;
            this.labelStudLastName1.Location = new System.Drawing.Point(155, 4);
            this.labelStudLastName1.Name = "labelStudLastName1";
            this.labelStudLastName1.Size = new System.Drawing.Size(59, 13);
            this.labelStudLastName1.TabIndex = 2;
            this.labelStudLastName1.Text = "Last name:";
            // 
            // cbStudLearns
            // 
            this.cbStudLearns.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "LearningLanguage", true));
            this.cbStudLearns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudLearns.FormattingEnabled = true;
            this.cbStudLearns.Location = new System.Drawing.Point(5, 91);
            this.cbStudLearns.Name = "cbStudLearns";
            this.cbStudLearns.Size = new System.Drawing.Size(130, 21);
            this.cbStudLearns.TabIndex = 8;
            // 
            // labelStudEmail1
            // 
            this.labelStudEmail1.AutoSize = true;
            this.labelStudEmail1.Location = new System.Drawing.Point(334, 6);
            this.labelStudEmail1.Name = "labelStudEmail1";
            this.labelStudEmail1.Size = new System.Drawing.Size(35, 13);
            this.labelStudEmail1.TabIndex = 4;
            this.labelStudEmail1.Text = "Email:";
            // 
            // tbStudBirthday
            // 
            this.tbStudBirthday.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Birthday", true));
            this.tbStudBirthday.Location = new System.Drawing.Point(148, 129);
            this.tbStudBirthday.Name = "tbStudBirthday";
            this.tbStudBirthday.Size = new System.Drawing.Size(124, 20);
            this.tbStudBirthday.TabIndex = 13;
            this.tbStudBirthday.TextChanged += new System.EventHandler(this.tbStudBirthday_TextChanged);
            // 
            // tbStudEmail
            // 
            this.tbStudEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Email", true));
            this.tbStudEmail.Location = new System.Drawing.Point(328, 19);
            this.tbStudEmail.Name = "tbStudEmail";
            this.tbStudEmail.Size = new System.Drawing.Size(152, 20);
            this.tbStudEmail.TabIndex = 3;
            this.tbStudEmail.TextChanged += new System.EventHandler(this.tbStudEmail_TextChanged);
            // 
            // labelStudSpeaks1
            // 
            this.labelStudSpeaks1.AutoSize = true;
            this.labelStudSpeaks1.Location = new System.Drawing.Point(155, 76);
            this.labelStudSpeaks1.Name = "labelStudSpeaks1";
            this.labelStudSpeaks1.Size = new System.Drawing.Size(46, 13);
            this.labelStudSpeaks1.TabIndex = 14;
            this.labelStudSpeaks1.Text = "Speaks:";
            // 
            // labelStudCellPhone
            // 
            this.labelStudCellPhone.AutoSize = true;
            this.labelStudCellPhone.Location = new System.Drawing.Point(155, 41);
            this.labelStudCellPhone.Name = "labelStudCellPhone";
            this.labelStudCellPhone.Size = new System.Drawing.Size(60, 13);
            this.labelStudCellPhone.TabIndex = 6;
            this.labelStudCellPhone.Text = "Cell phone:";
            // 
            // labelStudLevel1
            // 
            this.labelStudLevel1.AutoSize = true;
            this.labelStudLevel1.Location = new System.Drawing.Point(6, 114);
            this.labelStudLevel1.Name = "labelStudLevel1";
            this.labelStudLevel1.Size = new System.Drawing.Size(36, 13);
            this.labelStudLevel1.TabIndex = 24;
            this.labelStudLevel1.Text = "Level:";
            // 
            // labelStudHomePhone
            // 
            this.labelStudHomePhone.AutoSize = true;
            this.labelStudHomePhone.Location = new System.Drawing.Point(334, 41);
            this.labelStudHomePhone.Name = "labelStudHomePhone";
            this.labelStudHomePhone.Size = new System.Drawing.Size(71, 13);
            this.labelStudHomePhone.TabIndex = 7;
            this.labelStudHomePhone.Text = "Home phone:";
            // 
            // cbStudLevel
            // 
            this.cbStudLevel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Level", true));
            this.cbStudLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudLevel.FormattingEnabled = true;
            this.cbStudLevel.Location = new System.Drawing.Point(5, 129);
            this.cbStudLevel.Name = "cbStudLevel";
            this.cbStudLevel.Size = new System.Drawing.Size(130, 21);
            this.cbStudLevel.TabIndex = 12;
            // 
            // tbStudCellPhone
            // 
            this.tbStudCellPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "CellPhone", true));
            this.tbStudCellPhone.Location = new System.Drawing.Point(148, 55);
            this.tbStudCellPhone.Name = "tbStudCellPhone";
            this.tbStudCellPhone.Size = new System.Drawing.Size(130, 20);
            this.tbStudCellPhone.TabIndex = 5;
            this.tbStudCellPhone.TextChanged += new System.EventHandler(this.tbStudCellPhone_TextChanged);
            // 
            // labelStudSource1
            // 
            this.labelStudSource1.AutoSize = true;
            this.labelStudSource1.Location = new System.Drawing.Point(334, 114);
            this.labelStudSource1.Name = "labelStudSource1";
            this.labelStudSource1.Size = new System.Drawing.Size(44, 13);
            this.labelStudSource1.TabIndex = 26;
            this.labelStudSource1.Text = "Source:";
            // 
            // tbStudHomePhone
            // 
            this.tbStudHomePhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "HomePhone", true));
            this.tbStudHomePhone.Location = new System.Drawing.Point(328, 57);
            this.tbStudHomePhone.Name = "tbStudHomePhone";
            this.tbStudHomePhone.Size = new System.Drawing.Size(155, 20);
            this.tbStudHomePhone.TabIndex = 6;
            this.tbStudHomePhone.TextChanged += new System.EventHandler(this.tbStudHomePhone_TextChanged);
            // 
            // labelStudSpeaks2
            // 
            this.labelStudSpeaks2.AutoSize = true;
            this.labelStudSpeaks2.Location = new System.Drawing.Point(5, 78);
            this.labelStudSpeaks2.Name = "labelStudSpeaks2";
            this.labelStudSpeaks2.Size = new System.Drawing.Size(42, 13);
            this.labelStudSpeaks2.TabIndex = 11;
            this.labelStudSpeaks2.Text = "Learns:";
            // 
            // cbStudSource
            // 
            this.cbStudSource.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Source", true));
            this.cbStudSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudSource.FormattingEnabled = true;
            this.cbStudSource.Location = new System.Drawing.Point(325, 129);
            this.cbStudSource.Name = "cbStudSource";
            this.cbStudSource.Size = new System.Drawing.Size(155, 21);
            this.cbStudSource.TabIndex = 14;
            // 
            // panelStudSecondary
            // 
            this.panelStudSecondary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStudSecondary.Controls.Add(this.groupBoxStudSecondaryRight);
            this.panelStudSecondary.Controls.Add(this.panelStudSecondaryLeft);
            this.panelStudSecondary.Location = new System.Drawing.Point(0, 161);
            this.panelStudSecondary.Name = "panelStudSecondary";
            this.panelStudSecondary.Size = new System.Drawing.Size(934, 328);
            this.panelStudSecondary.TabIndex = 36;
            // 
            // groupBoxStudSecondaryRight
            // 
            this.groupBoxStudSecondaryRight.Controls.Add(this.tbStudComments);
            this.groupBoxStudSecondaryRight.Controls.Add(this.tbStudSchedule);
            this.groupBoxStudSecondaryRight.Controls.Add(this.tbStudInterests);
            this.groupBoxStudSecondaryRight.Controls.Add(this.tbStudGoals);
            this.groupBoxStudSecondaryRight.Controls.Add(this.tbStudBackground);
            this.groupBoxStudSecondaryRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxStudSecondaryRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.groupBoxStudSecondaryRight.Location = new System.Drawing.Point(80, 0);
            this.groupBoxStudSecondaryRight.Name = "groupBoxStudSecondaryRight";
            this.groupBoxStudSecondaryRight.Size = new System.Drawing.Size(854, 328);
            this.groupBoxStudSecondaryRight.TabIndex = 36;
            this.groupBoxStudSecondaryRight.TabStop = false;
            // 
            // tbStudComments
            // 
            this.tbStudComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStudComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Comments", true));
            this.tbStudComments.Location = new System.Drawing.Point(6, 114);
            this.tbStudComments.Name = "tbStudComments";
            this.tbStudComments.Size = new System.Drawing.Size(840, 20);
            this.tbStudComments.TabIndex = 20;
            this.tbStudComments.TextChanged += new System.EventHandler(this.tbStudComments_TextChanged);
            // 
            // tbStudSchedule
            // 
            this.tbStudSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStudSchedule.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "PossibleSchedule", true));
            this.tbStudSchedule.Location = new System.Drawing.Point(6, 88);
            this.tbStudSchedule.Name = "tbStudSchedule";
            this.tbStudSchedule.Size = new System.Drawing.Size(840, 20);
            this.tbStudSchedule.TabIndex = 19;
            this.tbStudSchedule.TextChanged += new System.EventHandler(this.tbStudSchedule_TextChanged);
            // 
            // tbStudInterests
            // 
            this.tbStudInterests.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStudInterests.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Interests", true));
            this.tbStudInterests.Location = new System.Drawing.Point(6, 63);
            this.tbStudInterests.Name = "tbStudInterests";
            this.tbStudInterests.Size = new System.Drawing.Size(840, 20);
            this.tbStudInterests.TabIndex = 18;
            this.tbStudInterests.TextChanged += new System.EventHandler(this.tbStudInterests_TextChanged);
            // 
            // tbStudGoals
            // 
            this.tbStudGoals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStudGoals.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Goals", true));
            this.tbStudGoals.Location = new System.Drawing.Point(6, 39);
            this.tbStudGoals.Name = "tbStudGoals";
            this.tbStudGoals.Size = new System.Drawing.Size(840, 20);
            this.tbStudGoals.TabIndex = 17;
            this.tbStudGoals.TextChanged += new System.EventHandler(this.tbStudGoals_TextChanged);
            // 
            // tbStudBackground
            // 
            this.tbStudBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStudBackground.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.studentList, "Background", true));
            this.tbStudBackground.Location = new System.Drawing.Point(6, 16);
            this.tbStudBackground.Name = "tbStudBackground";
            this.tbStudBackground.Size = new System.Drawing.Size(840, 20);
            this.tbStudBackground.TabIndex = 16;
            this.tbStudBackground.TextChanged += new System.EventHandler(this.tbStudBackground_TextChanged);
            // 
            // panelStudSecondaryLeft
            // 
            this.panelStudSecondaryLeft.Controls.Add(this.labelStudGoals);
            this.panelStudSecondaryLeft.Controls.Add(this.labelStudBackground);
            this.panelStudSecondaryLeft.Controls.Add(this.labelStudComments);
            this.panelStudSecondaryLeft.Controls.Add(this.labelStudSchedule);
            this.panelStudSecondaryLeft.Controls.Add(this.labelStudInterests);
            this.panelStudSecondaryLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelStudSecondaryLeft.Location = new System.Drawing.Point(0, 0);
            this.panelStudSecondaryLeft.Name = "panelStudSecondaryLeft";
            this.panelStudSecondaryLeft.Size = new System.Drawing.Size(80, 328);
            this.panelStudSecondaryLeft.TabIndex = 35;
            // 
            // labelStudGoals
            // 
            this.labelStudGoals.AutoSize = true;
            this.labelStudGoals.Location = new System.Drawing.Point(9, 42);
            this.labelStudGoals.Name = "labelStudGoals";
            this.labelStudGoals.Size = new System.Drawing.Size(37, 13);
            this.labelStudGoals.TabIndex = 30;
            this.labelStudGoals.Text = "Goals:";
            // 
            // labelStudBackground
            // 
            this.labelStudBackground.AutoSize = true;
            this.labelStudBackground.Location = new System.Drawing.Point(11, 19);
            this.labelStudBackground.Name = "labelStudBackground";
            this.labelStudBackground.Size = new System.Drawing.Size(68, 13);
            this.labelStudBackground.TabIndex = 29;
            this.labelStudBackground.Text = "Background:";
            // 
            // labelStudComments
            // 
            this.labelStudComments.AutoSize = true;
            this.labelStudComments.Location = new System.Drawing.Point(6, 116);
            this.labelStudComments.Name = "labelStudComments";
            this.labelStudComments.Size = new System.Drawing.Size(59, 13);
            this.labelStudComments.TabIndex = 33;
            this.labelStudComments.Text = "Comments:";
            // 
            // labelStudSchedule
            // 
            this.labelStudSchedule.AutoSize = true;
            this.labelStudSchedule.Location = new System.Drawing.Point(9, 88);
            this.labelStudSchedule.Name = "labelStudSchedule";
            this.labelStudSchedule.Size = new System.Drawing.Size(55, 13);
            this.labelStudSchedule.TabIndex = 32;
            this.labelStudSchedule.Text = "Schedule:";
            // 
            // labelStudInterests
            // 
            this.labelStudInterests.AutoSize = true;
            this.labelStudInterests.Location = new System.Drawing.Point(11, 64);
            this.labelStudInterests.Name = "labelStudInterests";
            this.labelStudInterests.Size = new System.Drawing.Size(50, 13);
            this.labelStudInterests.TabIndex = 31;
            this.labelStudInterests.Text = "Interests:";
            // 
            // tabBottomPageTeachers
            // 
            this.tabBottomPageTeachers.Controls.Add(this.panelTeacherSecondary);
            this.tabBottomPageTeachers.Controls.Add(this.panelTeacherPrimary);
            this.tabBottomPageTeachers.Location = new System.Drawing.Point(4, 22);
            this.tabBottomPageTeachers.Name = "tabBottomPageTeachers";
            this.tabBottomPageTeachers.Padding = new System.Windows.Forms.Padding(3);
            this.tabBottomPageTeachers.Size = new System.Drawing.Size(922, 356);
            this.tabBottomPageTeachers.TabIndex = 1;
            this.tabBottomPageTeachers.Text = "Teachers";
            this.tabBottomPageTeachers.UseVisualStyleBackColor = true;
            // 
            // panelTeacherSecondary
            // 
            this.panelTeacherSecondary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTeacherSecondary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.panelTeacherSecondary.Controls.Add(this.panelTeachMatrix);
            this.panelTeacherSecondary.Controls.Add(this.buttonTeach_GrabAvailChanges);
            this.panelTeacherSecondary.Location = new System.Drawing.Point(3, 135);
            this.panelTeacherSecondary.Name = "panelTeacherSecondary";
            this.panelTeacherSecondary.Size = new System.Drawing.Size(916, 222);
            this.panelTeacherSecondary.TabIndex = 1;
            // 
            // panelTeachMatrix
            // 
            this.panelTeachMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTeachMatrix.AutoScroll = true;
            this.panelTeachMatrix.Location = new System.Drawing.Point(7, 53);
            this.panelTeachMatrix.Name = "panelTeachMatrix";
            this.panelTeachMatrix.Size = new System.Drawing.Size(906, 166);
            this.panelTeachMatrix.TabIndex = 1;
            // 
            // buttonTeach_GrabAvailChanges
            // 
            this.buttonTeach_GrabAvailChanges.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(39)))));
            this.buttonTeach_GrabAvailChanges.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonTeach_GrabAvailChanges.Location = new System.Drawing.Point(195, 7);
            this.buttonTeach_GrabAvailChanges.Name = "buttonTeach_GrabAvailChanges";
            this.buttonTeach_GrabAvailChanges.Size = new System.Drawing.Size(200, 40);
            this.buttonTeach_GrabAvailChanges.TabIndex = 0;
            this.buttonTeach_GrabAvailChanges.Text = "Accept Availability Changes";
            this.buttonTeach_GrabAvailChanges.UseVisualStyleBackColor = false;
            this.buttonTeach_GrabAvailChanges.Visible = false;
            this.buttonTeach_GrabAvailChanges.Click += new System.EventHandler(this.availAcceptButton_Click);
            // 
            // panelTeacherPrimary
            // 
            this.panelTeacherPrimary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.panelTeacherPrimary.Controls.Add(this.groupBoxTeacherPrimaryRight);
            this.panelTeacherPrimary.Controls.Add(this.panelTeacherPrimaryLeft);
            this.panelTeacherPrimary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTeacherPrimary.Location = new System.Drawing.Point(3, 3);
            this.panelTeacherPrimary.Name = "panelTeacherPrimary";
            this.panelTeacherPrimary.Size = new System.Drawing.Size(916, 131);
            this.panelTeacherPrimary.TabIndex = 0;
            // 
            // groupBoxTeacherPrimaryRight
            // 
            this.groupBoxTeacherPrimaryRight.Controls.Add(this.tbTeachComment);
            this.groupBoxTeacherPrimaryRight.Controls.Add(this.tbTeachVacations);
            this.groupBoxTeacherPrimaryRight.Controls.Add(this.tbTeachAddress);
            this.groupBoxTeacherPrimaryRight.Controls.Add(this.labelTeachComment);
            this.groupBoxTeacherPrimaryRight.Controls.Add(this.labelTeachVacations);
            this.groupBoxTeacherPrimaryRight.Controls.Add(this.labelTeachAddress);
            this.groupBoxTeacherPrimaryRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTeacherPrimaryRight.Location = new System.Drawing.Point(470, 0);
            this.groupBoxTeacherPrimaryRight.Name = "groupBoxTeacherPrimaryRight";
            this.groupBoxTeacherPrimaryRight.Size = new System.Drawing.Size(446, 131);
            this.groupBoxTeacherPrimaryRight.TabIndex = 1;
            this.groupBoxTeacherPrimaryRight.TabStop = false;
            // 
            // tbTeachComment
            // 
            this.tbTeachComment.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "Comments", true));
            this.tbTeachComment.Location = new System.Drawing.Point(15, 92);
            this.tbTeachComment.Name = "tbTeachComment";
            this.tbTeachComment.Size = new System.Drawing.Size(437, 20);
            this.tbTeachComment.TabIndex = 12;
            this.tbTeachComment.TextChanged += new System.EventHandler(this.tbTeachComment_TextChanged);
            // 
            // tbTeachVacations
            // 
            this.tbTeachVacations.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "Vacations", true));
            this.tbTeachVacations.Location = new System.Drawing.Point(15, 55);
            this.tbTeachVacations.Name = "tbTeachVacations";
            this.tbTeachVacations.Size = new System.Drawing.Size(437, 20);
            this.tbTeachVacations.TabIndex = 8;
            this.tbTeachVacations.TextChanged += new System.EventHandler(this.tbTeachVacations_TextChanged);
            // 
            // tbTeachAddress
            // 
            this.tbTeachAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "MailingAddress", true));
            this.tbTeachAddress.Location = new System.Drawing.Point(15, 18);
            this.tbTeachAddress.Name = "tbTeachAddress";
            this.tbTeachAddress.Size = new System.Drawing.Size(437, 20);
            this.tbTeachAddress.TabIndex = 4;
            this.tbTeachAddress.TextChanged += new System.EventHandler(this.tbTeachAddress_TextChanged);
            // 
            // labelTeachComment
            // 
            this.labelTeachComment.AutoSize = true;
            this.labelTeachComment.Location = new System.Drawing.Point(39, 76);
            this.labelTeachComment.Name = "labelTeachComment";
            this.labelTeachComment.Size = new System.Drawing.Size(54, 13);
            this.labelTeachComment.TabIndex = 2;
            this.labelTeachComment.Text = "Comment:";
            // 
            // labelTeachVacations
            // 
            this.labelTeachVacations.AutoSize = true;
            this.labelTeachVacations.Location = new System.Drawing.Point(39, 38);
            this.labelTeachVacations.Name = "labelTeachVacations";
            this.labelTeachVacations.Size = new System.Drawing.Size(57, 13);
            this.labelTeachVacations.TabIndex = 1;
            this.labelTeachVacations.Text = "Vacations:";
            // 
            // labelTeachAddress
            // 
            this.labelTeachAddress.AutoSize = true;
            this.labelTeachAddress.Location = new System.Drawing.Point(39, 3);
            this.labelTeachAddress.Name = "labelTeachAddress";
            this.labelTeachAddress.Size = new System.Drawing.Size(48, 13);
            this.labelTeachAddress.TabIndex = 0;
            this.labelTeachAddress.Text = "Address:";
            // 
            // panelTeacherPrimaryLeft
            // 
            this.panelTeacherPrimaryLeft.Controls.Add(this.tbTeachLanguageDetail);
            this.panelTeacherPrimaryLeft.Controls.Add(this.cbTeachLanguage2);
            this.panelTeacherPrimaryLeft.Controls.Add(this.cbTeachLanguage);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelTeachLabguageDetail);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelTeachLanguage2);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelLanguage);
            this.panelTeacherPrimaryLeft.Controls.Add(this.cbTeachStatus);
            this.panelTeacherPrimaryLeft.Controls.Add(this.tbTeachLastBirthday);
            this.panelTeacherPrimaryLeft.Controls.Add(this.tbTeachPhone);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelTeachBirthday);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelTeachPhone);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelTeachStatus);
            this.panelTeacherPrimaryLeft.Controls.Add(this.tbTeachEmail);
            this.panelTeacherPrimaryLeft.Controls.Add(this.tbTeachLastName);
            this.panelTeacherPrimaryLeft.Controls.Add(this.tbTeachFirstName);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelTeachEmail);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelTeachLastName);
            this.panelTeacherPrimaryLeft.Controls.Add(this.labelTeachFirstName);
            this.panelTeacherPrimaryLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTeacherPrimaryLeft.Location = new System.Drawing.Point(0, 0);
            this.panelTeacherPrimaryLeft.Name = "panelTeacherPrimaryLeft";
            this.panelTeacherPrimaryLeft.Size = new System.Drawing.Size(470, 131);
            this.panelTeacherPrimaryLeft.TabIndex = 0;
            // 
            // tbTeachLanguageDetail
            // 
            this.tbTeachLanguageDetail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "LanguageDetail", true));
            this.tbTeachLanguageDetail.Location = new System.Drawing.Point(301, 92);
            this.tbTeachLanguageDetail.Name = "tbTeachLanguageDetail";
            this.tbTeachLanguageDetail.Size = new System.Drawing.Size(163, 20);
            this.tbTeachLanguageDetail.TabIndex = 11;
            this.tbTeachLanguageDetail.TextChanged += new System.EventHandler(this.tbTeachLanguageDetail_TextChanged);
            // 
            // cbTeachLanguage2
            // 
            this.cbTeachLanguage2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "Language2", true));
            this.cbTeachLanguage2.FormattingEnabled = true;
            this.cbTeachLanguage2.Location = new System.Drawing.Point(144, 92);
            this.cbTeachLanguage2.Name = "cbTeachLanguage2";
            this.cbTeachLanguage2.Size = new System.Drawing.Size(121, 21);
            this.cbTeachLanguage2.TabIndex = 10;
            this.cbTeachLanguage2.SelectedIndexChanged += new System.EventHandler(this.cbTeachLanguage2_SelectedIndexChanged);
            // 
            // cbTeachLanguage
            // 
            this.cbTeachLanguage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "Language", true));
            this.cbTeachLanguage.FormattingEnabled = true;
            this.cbTeachLanguage.Location = new System.Drawing.Point(7, 91);
            this.cbTeachLanguage.Name = "cbTeachLanguage";
            this.cbTeachLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbTeachLanguage.TabIndex = 9;
            this.cbTeachLanguage.SelectedIndexChanged += new System.EventHandler(this.cbTeachLanguage_SelectedIndexChanged);
            // 
            // labelTeachLabguageDetail
            // 
            this.labelTeachLabguageDetail.AutoSize = true;
            this.labelTeachLabguageDetail.Location = new System.Drawing.Point(309, 76);
            this.labelTeachLabguageDetail.Name = "labelTeachLabguageDetail";
            this.labelTeachLabguageDetail.Size = new System.Drawing.Size(86, 13);
            this.labelTeachLabguageDetail.TabIndex = 15;
            this.labelTeachLabguageDetail.Text = "Language detail:";
            // 
            // labelTeachLanguage2
            // 
            this.labelTeachLanguage2.AutoSize = true;
            this.labelTeachLanguage2.Location = new System.Drawing.Point(151, 76);
            this.labelTeachLanguage2.Name = "labelTeachLanguage2";
            this.labelTeachLanguage2.Size = new System.Drawing.Size(67, 13);
            this.labelTeachLanguage2.TabIndex = 14;
            this.labelTeachLanguage2.Text = "Language 2:";
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(17, 76);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(58, 13);
            this.labelLanguage.TabIndex = 13;
            this.labelLanguage.Text = "Language:";
            // 
            // cbTeachStatus
            // 
            this.cbTeachStatus.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "Status", true));
            this.cbTeachStatus.FormattingEnabled = true;
            this.cbTeachStatus.Location = new System.Drawing.Point(7, 52);
            this.cbTeachStatus.Name = "cbTeachStatus";
            this.cbTeachStatus.Size = new System.Drawing.Size(131, 21);
            this.cbTeachStatus.TabIndex = 5;
            this.cbTeachStatus.SelectedIndexChanged += new System.EventHandler(this.cbTeachStatus_SelectedIndexChanged);
            // 
            // tbTeachLastBirthday
            // 
            this.tbTeachLastBirthday.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "Birthday", true));
            this.tbTeachLastBirthday.Location = new System.Drawing.Point(301, 53);
            this.tbTeachLastBirthday.Name = "tbTeachLastBirthday";
            this.tbTeachLastBirthday.Size = new System.Drawing.Size(163, 20);
            this.tbTeachLastBirthday.TabIndex = 7;
            this.tbTeachLastBirthday.TextChanged += new System.EventHandler(this.tbTeachLastBirthday_TextChanged);
            // 
            // tbTeachPhone
            // 
            this.tbTeachPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "Phone", true));
            this.tbTeachPhone.Location = new System.Drawing.Point(144, 53);
            this.tbTeachPhone.Name = "tbTeachPhone";
            this.tbTeachPhone.Size = new System.Drawing.Size(151, 20);
            this.tbTeachPhone.TabIndex = 6;
            this.tbTeachPhone.TextChanged += new System.EventHandler(this.tbTeachPhone_TextChanged);
            // 
            // labelTeachBirthday
            // 
            this.labelTeachBirthday.AutoSize = true;
            this.labelTeachBirthday.Location = new System.Drawing.Point(309, 41);
            this.labelTeachBirthday.Name = "labelTeachBirthday";
            this.labelTeachBirthday.Size = new System.Drawing.Size(48, 13);
            this.labelTeachBirthday.TabIndex = 8;
            this.labelTeachBirthday.Text = "Birthday:";
            // 
            // labelTeachPhone
            // 
            this.labelTeachPhone.AutoSize = true;
            this.labelTeachPhone.Location = new System.Drawing.Point(151, 41);
            this.labelTeachPhone.Name = "labelTeachPhone";
            this.labelTeachPhone.Size = new System.Drawing.Size(41, 13);
            this.labelTeachPhone.TabIndex = 7;
            this.labelTeachPhone.Text = "Phone:";
            // 
            // labelTeachStatus
            // 
            this.labelTeachStatus.AutoSize = true;
            this.labelTeachStatus.Location = new System.Drawing.Point(17, 38);
            this.labelTeachStatus.Name = "labelTeachStatus";
            this.labelTeachStatus.Size = new System.Drawing.Size(40, 13);
            this.labelTeachStatus.TabIndex = 6;
            this.labelTeachStatus.Text = "Status:";
            // 
            // tbTeachEmail
            // 
            this.tbTeachEmail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "Email", true));
            this.tbTeachEmail.Location = new System.Drawing.Point(301, 19);
            this.tbTeachEmail.Name = "tbTeachEmail";
            this.tbTeachEmail.Size = new System.Drawing.Size(163, 20);
            this.tbTeachEmail.TabIndex = 3;
            this.tbTeachEmail.TextChanged += new System.EventHandler(this.tbTeachEmail_TextChanged);
            // 
            // tbTeachLastName
            // 
            this.tbTeachLastName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "LastName", true));
            this.tbTeachLastName.Location = new System.Drawing.Point(144, 19);
            this.tbTeachLastName.Name = "tbTeachLastName";
            this.tbTeachLastName.Size = new System.Drawing.Size(151, 20);
            this.tbTeachLastName.TabIndex = 2;
            this.tbTeachLastName.TextChanged += new System.EventHandler(this.tbTeachLastName_TextChanged);
            // 
            // tbTeachFirstName
            // 
            this.tbTeachFirstName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.teacherList, "FirstName", true));
            this.tbTeachFirstName.Location = new System.Drawing.Point(7, 18);
            this.tbTeachFirstName.Name = "tbTeachFirstName";
            this.tbTeachFirstName.Size = new System.Drawing.Size(131, 20);
            this.tbTeachFirstName.TabIndex = 1;
            this.tbTeachFirstName.TextChanged += new System.EventHandler(this.tbTeachFirstName_TextChanged);
            // 
            // labelTeachEmail
            // 
            this.labelTeachEmail.AutoSize = true;
            this.labelTeachEmail.Location = new System.Drawing.Point(309, 3);
            this.labelTeachEmail.Name = "labelTeachEmail";
            this.labelTeachEmail.Size = new System.Drawing.Size(35, 13);
            this.labelTeachEmail.TabIndex = 2;
            this.labelTeachEmail.Text = "Email:";
            // 
            // labelTeachLastName
            // 
            this.labelTeachLastName.AutoSize = true;
            this.labelTeachLastName.Location = new System.Drawing.Point(151, 3);
            this.labelTeachLastName.Name = "labelTeachLastName";
            this.labelTeachLastName.Size = new System.Drawing.Size(59, 13);
            this.labelTeachLastName.TabIndex = 1;
            this.labelTeachLastName.Text = "Last name:";
            // 
            // labelTeachFirstName
            // 
            this.labelTeachFirstName.AutoSize = true;
            this.labelTeachFirstName.Location = new System.Drawing.Point(17, 2);
            this.labelTeachFirstName.Name = "labelTeachFirstName";
            this.labelTeachFirstName.Size = new System.Drawing.Size(58, 13);
            this.labelTeachFirstName.TabIndex = 0;
            this.labelTeachFirstName.Text = "First name:";
            // 
            // tabBottomPagePrograms
            // 
            this.tabBottomPagePrograms.Controls.Add(this.panelProgram);
            this.tabBottomPagePrograms.Location = new System.Drawing.Point(4, 22);
            this.tabBottomPagePrograms.Name = "tabBottomPagePrograms";
            this.tabBottomPagePrograms.Padding = new System.Windows.Forms.Padding(3);
            this.tabBottomPagePrograms.Size = new System.Drawing.Size(922, 356);
            this.tabBottomPagePrograms.TabIndex = 2;
            this.tabBottomPagePrograms.Text = "Programs";
            this.tabBottomPagePrograms.UseVisualStyleBackColor = true;
            // 
            // panelProgram
            // 
            this.panelProgram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.panelProgram.Controls.Add(this.groupBoxProgram);
            this.panelProgram.Controls.Add(this.panelProgramPrimaryLeft);
            this.panelProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProgram.Location = new System.Drawing.Point(3, 3);
            this.panelProgram.Name = "panelProgram";
            this.panelProgram.Size = new System.Drawing.Size(916, 350);
            this.panelProgram.TabIndex = 0;
            // 
            // groupBoxProgram
            // 
            this.groupBoxProgram.Controls.Add(this.tbProgComments);
            this.groupBoxProgram.Controls.Add(this.tbProgSummary);
            this.groupBoxProgram.Controls.Add(this.labelProgComments);
            this.groupBoxProgram.Controls.Add(this.labelProgSummary);
            this.groupBoxProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxProgram.Location = new System.Drawing.Point(241, 0);
            this.groupBoxProgram.Name = "groupBoxProgram";
            this.groupBoxProgram.Size = new System.Drawing.Size(675, 350);
            this.groupBoxProgram.TabIndex = 1;
            this.groupBoxProgram.TabStop = false;
            // 
            // tbProgComments
            // 
            this.tbProgComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProgComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.programList, "Comments", true));
            this.tbProgComments.Location = new System.Drawing.Point(88, 88);
            this.tbProgComments.Name = "tbProgComments";
            this.tbProgComments.Size = new System.Drawing.Size(581, 20);
            this.tbProgComments.TabIndex = 7;
            this.tbProgComments.TextChanged += new System.EventHandler(this.tbProgComments_TextChanged);
            // 
            // tbProgSummary
            // 
            this.tbProgSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbProgSummary.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.programList, "Summary", true));
            this.tbProgSummary.Location = new System.Drawing.Point(88, 22);
            this.tbProgSummary.Name = "tbProgSummary";
            this.tbProgSummary.Size = new System.Drawing.Size(581, 20);
            this.tbProgSummary.TabIndex = 6;
            this.tbProgSummary.TextChanged += new System.EventHandler(this.tbProgSummary_TextChanged);
            // 
            // labelProgComments
            // 
            this.labelProgComments.AutoSize = true;
            this.labelProgComments.Location = new System.Drawing.Point(20, 91);
            this.labelProgComments.Name = "labelProgComments";
            this.labelProgComments.Size = new System.Drawing.Size(59, 13);
            this.labelProgComments.TabIndex = 1;
            this.labelProgComments.Text = "Comments:";
            // 
            // labelProgSummary
            // 
            this.labelProgSummary.AutoSize = true;
            this.labelProgSummary.Location = new System.Drawing.Point(20, 25);
            this.labelProgSummary.Name = "labelProgSummary";
            this.labelProgSummary.Size = new System.Drawing.Size(53, 13);
            this.labelProgSummary.TabIndex = 0;
            this.labelProgSummary.Text = "Summary:";
            // 
            // panelProgramPrimaryLeft
            // 
            this.panelProgramPrimaryLeft.Controls.Add(this.labelProgExplanation);
            this.panelProgramPrimaryLeft.Controls.Add(this.tbProgProce);
            this.panelProgramPrimaryLeft.Controls.Add(this.labelProgPrice);
            this.panelProgramPrimaryLeft.Controls.Add(this.cbProgLevel);
            this.panelProgramPrimaryLeft.Controls.Add(this.labelProgLevel);
            this.panelProgramPrimaryLeft.Controls.Add(this.cbProgLanguage);
            this.panelProgramPrimaryLeft.Controls.Add(this.labelProgLanguage);
            this.panelProgramPrimaryLeft.Controls.Add(this.tbProgName);
            this.panelProgramPrimaryLeft.Controls.Add(this.labelProgName);
            this.panelProgramPrimaryLeft.Controls.Add(this.tbProgCode);
            this.panelProgramPrimaryLeft.Controls.Add(this.labelProgCode);
            this.panelProgramPrimaryLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProgramPrimaryLeft.Location = new System.Drawing.Point(0, 0);
            this.panelProgramPrimaryLeft.Name = "panelProgramPrimaryLeft";
            this.panelProgramPrimaryLeft.Size = new System.Drawing.Size(241, 350);
            this.panelProgramPrimaryLeft.TabIndex = 0;
            // 
            // labelProgExplanation
            // 
            this.labelProgExplanation.AutoSize = true;
            this.labelProgExplanation.Location = new System.Drawing.Point(117, 190);
            this.labelProgExplanation.Name = "labelProgExplanation";
            this.labelProgExplanation.Size = new System.Drawing.Size(103, 13);
            this.labelProgExplanation.TabIndex = 4;
            this.labelProgExplanation.Text = "Per hour, per person";
            // 
            // tbProgProce
            // 
            this.tbProgProce.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.programList, "Price", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C0"));
            this.tbProgProce.Location = new System.Drawing.Point(74, 167);
            this.tbProgProce.Name = "tbProgProce";
            this.tbProgProce.Size = new System.Drawing.Size(146, 20);
            this.tbProgProce.TabIndex = 5;
            this.tbProgProce.TextChanged += new System.EventHandler(this.tbProgPrice_TextChanged);
            // 
            // labelProgPrice
            // 
            this.labelProgPrice.AutoSize = true;
            this.labelProgPrice.Location = new System.Drawing.Point(10, 170);
            this.labelProgPrice.Name = "labelProgPrice";
            this.labelProgPrice.Size = new System.Drawing.Size(31, 13);
            this.labelProgPrice.TabIndex = 8;
            this.labelProgPrice.Text = "Price";
            // 
            // cbProgLevel
            // 
            this.cbProgLevel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.programList, "Level", true));
            this.cbProgLevel.FormattingEnabled = true;
            this.cbProgLevel.Location = new System.Drawing.Point(74, 130);
            this.cbProgLevel.Name = "cbProgLevel";
            this.cbProgLevel.Size = new System.Drawing.Size(146, 21);
            this.cbProgLevel.TabIndex = 4;
            this.cbProgLevel.SelectedIndexChanged += new System.EventHandler(this.cbProgLevel_SelectedIndexChanged);
            // 
            // labelProgLevel
            // 
            this.labelProgLevel.AutoSize = true;
            this.labelProgLevel.Location = new System.Drawing.Point(10, 130);
            this.labelProgLevel.Name = "labelProgLevel";
            this.labelProgLevel.Size = new System.Drawing.Size(36, 13);
            this.labelProgLevel.TabIndex = 6;
            this.labelProgLevel.Text = "Level:";
            // 
            // cbProgLanguage
            // 
            this.cbProgLanguage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.programList, "Language", true));
            this.cbProgLanguage.FormattingEnabled = true;
            this.cbProgLanguage.Location = new System.Drawing.Point(74, 91);
            this.cbProgLanguage.Name = "cbProgLanguage";
            this.cbProgLanguage.Size = new System.Drawing.Size(146, 21);
            this.cbProgLanguage.TabIndex = 3;
            this.cbProgLanguage.SelectedIndexChanged += new System.EventHandler(this.cbProgLanguage_SelectedIndexChanged);
            // 
            // labelProgLanguage
            // 
            this.labelProgLanguage.AutoSize = true;
            this.labelProgLanguage.Location = new System.Drawing.Point(10, 94);
            this.labelProgLanguage.Name = "labelProgLanguage";
            this.labelProgLanguage.Size = new System.Drawing.Size(58, 13);
            this.labelProgLanguage.TabIndex = 4;
            this.labelProgLanguage.Text = "Language:";
            // 
            // tbProgName
            // 
            this.tbProgName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.programList, "Name", true));
            this.tbProgName.Location = new System.Drawing.Point(74, 56);
            this.tbProgName.Name = "tbProgName";
            this.tbProgName.Size = new System.Drawing.Size(146, 20);
            this.tbProgName.TabIndex = 2;
            this.tbProgName.TextChanged += new System.EventHandler(this.tbProgName_TextChanged);
            // 
            // labelProgName
            // 
            this.labelProgName.AutoSize = true;
            this.labelProgName.Location = new System.Drawing.Point(10, 56);
            this.labelProgName.Name = "labelProgName";
            this.labelProgName.Size = new System.Drawing.Size(38, 13);
            this.labelProgName.TabIndex = 2;
            this.labelProgName.Text = "Name:";
            // 
            // tbProgCode
            // 
            this.tbProgCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.programList, "Code", true));
            this.tbProgCode.Location = new System.Drawing.Point(74, 22);
            this.tbProgCode.Name = "tbProgCode";
            this.tbProgCode.Size = new System.Drawing.Size(146, 20);
            this.tbProgCode.TabIndex = 1;
            this.tbProgCode.TextChanged += new System.EventHandler(this.tbProgCode_TextChanged);
            // 
            // labelProgCode
            // 
            this.labelProgCode.AutoSize = true;
            this.labelProgCode.Location = new System.Drawing.Point(10, 25);
            this.labelProgCode.Name = "labelProgCode";
            this.labelProgCode.Size = new System.Drawing.Size(35, 13);
            this.labelProgCode.TabIndex = 0;
            this.labelProgCode.Text = "Code:";
            // 
            // tabBottomPageRooms
            // 
            this.tabBottomPageRooms.Controls.Add(this.panelRoom);
            this.tabBottomPageRooms.Location = new System.Drawing.Point(4, 22);
            this.tabBottomPageRooms.Name = "tabBottomPageRooms";
            this.tabBottomPageRooms.Padding = new System.Windows.Forms.Padding(3);
            this.tabBottomPageRooms.Size = new System.Drawing.Size(922, 356);
            this.tabBottomPageRooms.TabIndex = 3;
            this.tabBottomPageRooms.Text = "Rooms";
            this.tabBottomPageRooms.UseVisualStyleBackColor = true;
            // 
            // panelRoom
            // 
            this.panelRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.panelRoom.Controls.Add(this.panelRoomPrimaryLeft);
            this.panelRoom.Controls.Add(this.groupBoxRoom);
            this.panelRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRoom.Location = new System.Drawing.Point(3, 3);
            this.panelRoom.Name = "panelRoom";
            this.panelRoom.Size = new System.Drawing.Size(916, 350);
            this.panelRoom.TabIndex = 0;
            // 
            // panelRoomPrimaryLeft
            // 
            this.panelRoomPrimaryLeft.Controls.Add(this.labelRoomRank);
            this.panelRoomPrimaryLeft.Controls.Add(this.labelRoomCapacity);
            this.panelRoomPrimaryLeft.Controls.Add(this.labelRoomName);
            this.panelRoomPrimaryLeft.Controls.Add(this.tbRoomPreferrability);
            this.panelRoomPrimaryLeft.Controls.Add(this.tbRoomCapacity);
            this.panelRoomPrimaryLeft.Controls.Add(this.tbRoomName);
            this.panelRoomPrimaryLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelRoomPrimaryLeft.Location = new System.Drawing.Point(0, 0);
            this.panelRoomPrimaryLeft.Name = "panelRoomPrimaryLeft";
            this.panelRoomPrimaryLeft.Size = new System.Drawing.Size(200, 350);
            this.panelRoomPrimaryLeft.TabIndex = 1;
            // 
            // labelRoomRank
            // 
            this.labelRoomRank.AutoSize = true;
            this.labelRoomRank.Location = new System.Drawing.Point(6, 71);
            this.labelRoomRank.Name = "labelRoomRank";
            this.labelRoomRank.Size = new System.Drawing.Size(36, 13);
            this.labelRoomRank.TabIndex = 5;
            this.labelRoomRank.Text = "Rank:";
            // 
            // labelRoomCapacity
            // 
            this.labelRoomCapacity.AutoSize = true;
            this.labelRoomCapacity.Location = new System.Drawing.Point(6, 42);
            this.labelRoomCapacity.Name = "labelRoomCapacity";
            this.labelRoomCapacity.Size = new System.Drawing.Size(51, 13);
            this.labelRoomCapacity.TabIndex = 4;
            this.labelRoomCapacity.Text = "Capacity:";
            // 
            // labelRoomName
            // 
            this.labelRoomName.AutoSize = true;
            this.labelRoomName.Location = new System.Drawing.Point(6, 13);
            this.labelRoomName.Name = "labelRoomName";
            this.labelRoomName.Size = new System.Drawing.Size(38, 13);
            this.labelRoomName.TabIndex = 3;
            this.labelRoomName.Text = "Name:";
            // 
            // tbRoomPreferrability
            // 
            this.tbRoomPreferrability.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.roomList, "Rank", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.tbRoomPreferrability.Location = new System.Drawing.Point(61, 68);
            this.tbRoomPreferrability.Name = "tbRoomPreferrability";
            this.tbRoomPreferrability.Size = new System.Drawing.Size(87, 20);
            this.tbRoomPreferrability.TabIndex = 3;
            this.tbRoomPreferrability.TextChanged += new System.EventHandler(this.tbRoomPreferrability_TextChanged);
            // 
            // tbRoomCapacity
            // 
            this.tbRoomCapacity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.roomList, "Capacity", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N0"));
            this.tbRoomCapacity.Location = new System.Drawing.Point(61, 39);
            this.tbRoomCapacity.Name = "tbRoomCapacity";
            this.tbRoomCapacity.Size = new System.Drawing.Size(87, 20);
            this.tbRoomCapacity.TabIndex = 2;
            this.tbRoomCapacity.TextChanged += new System.EventHandler(this.tbRoomCapacity_TextChanged);
            // 
            // tbRoomName
            // 
            this.tbRoomName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.roomList, "Name", true));
            this.tbRoomName.Location = new System.Drawing.Point(61, 10);
            this.tbRoomName.Name = "tbRoomName";
            this.tbRoomName.Size = new System.Drawing.Size(87, 20);
            this.tbRoomName.TabIndex = 1;
            this.tbRoomName.TextChanged += new System.EventHandler(this.tbRoomName_TextChanged);
            // 
            // groupBoxRoom
            // 
            this.groupBoxRoom.Controls.Add(this.labelRoomComments);
            this.groupBoxRoom.Controls.Add(this.labelRoomTags);
            this.groupBoxRoom.Controls.Add(this.tbRoomComments);
            this.groupBoxRoom.Controls.Add(this.tbRoomTags);
            this.groupBoxRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRoom.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRoom.Name = "groupBoxRoom";
            this.groupBoxRoom.Size = new System.Drawing.Size(916, 350);
            this.groupBoxRoom.TabIndex = 2;
            this.groupBoxRoom.TabStop = false;
            // 
            // labelRoomComments
            // 
            this.labelRoomComments.AutoSize = true;
            this.labelRoomComments.Location = new System.Drawing.Point(210, 59);
            this.labelRoomComments.Name = "labelRoomComments";
            this.labelRoomComments.Size = new System.Drawing.Size(59, 13);
            this.labelRoomComments.TabIndex = 3;
            this.labelRoomComments.Text = "Comments:";
            // 
            // labelRoomTags
            // 
            this.labelRoomTags.AutoSize = true;
            this.labelRoomTags.Location = new System.Drawing.Point(207, 13);
            this.labelRoomTags.Name = "labelRoomTags";
            this.labelRoomTags.Size = new System.Drawing.Size(34, 13);
            this.labelRoomTags.TabIndex = 2;
            this.labelRoomTags.Text = "Tags:";
            // 
            // tbRoomComments
            // 
            this.tbRoomComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRoomComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.roomList, "Comments", true));
            this.tbRoomComments.Location = new System.Drawing.Point(206, 78);
            this.tbRoomComments.Name = "tbRoomComments";
            this.tbRoomComments.Size = new System.Drawing.Size(704, 20);
            this.tbRoomComments.TabIndex = 5;
            this.tbRoomComments.TextChanged += new System.EventHandler(this.tbRoomComments_TextChanged);
            // 
            // tbRoomTags
            // 
            this.tbRoomTags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRoomTags.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.roomList, "Tags", true));
            this.tbRoomTags.Location = new System.Drawing.Point(206, 32);
            this.tbRoomTags.Name = "tbRoomTags";
            this.tbRoomTags.Size = new System.Drawing.Size(704, 20);
            this.tbRoomTags.TabIndex = 4;
            this.tbRoomTags.TextChanged += new System.EventHandler(this.tbRoomTags_TextChanged);
            // 
            // tabBottomPageLessons
            // 
            this.tabBottomPageLessons.Controls.Add(this.panelLesson);
            this.tabBottomPageLessons.Location = new System.Drawing.Point(4, 22);
            this.tabBottomPageLessons.Name = "tabBottomPageLessons";
            this.tabBottomPageLessons.Padding = new System.Windows.Forms.Padding(3);
            this.tabBottomPageLessons.Size = new System.Drawing.Size(922, 356);
            this.tabBottomPageLessons.TabIndex = 4;
            this.tabBottomPageLessons.Text = "Lessons";
            this.tabBottomPageLessons.UseVisualStyleBackColor = true;
            // 
            // panelLesson
            // 
            this.panelLesson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.panelLesson.Controls.Add(this.cbLessonStudent10);
            this.panelLesson.Controls.Add(this.cbLessonStudent9);
            this.panelLesson.Controls.Add(this.labelLessonStudent10);
            this.panelLesson.Controls.Add(this.labelLessonStudent9);
            this.panelLesson.Controls.Add(this.cbLessonStudent8);
            this.panelLesson.Controls.Add(this.cbLessonStudent7);
            this.panelLesson.Controls.Add(this.labelLessonStudent8);
            this.panelLesson.Controls.Add(this.labelLessonStudent7);
            this.panelLesson.Controls.Add(this.cbLessonStudent6);
            this.panelLesson.Controls.Add(this.cbLessonStudent5);
            this.panelLesson.Controls.Add(this.labelLessonStudent6);
            this.panelLesson.Controls.Add(this.labelLessonStudent5);
            this.panelLesson.Controls.Add(this.cbLessonStudent4);
            this.panelLesson.Controls.Add(this.labelLessonStudent4);
            this.panelLesson.Controls.Add(this.cbLessonStudent3);
            this.panelLesson.Controls.Add(this.labelLessonStudent3);
            this.panelLesson.Controls.Add(this.labelLessonComment);
            this.panelLesson.Controls.Add(this.tbLEssonComment);
            this.panelLesson.Controls.Add(this.cbLessonStudent2);
            this.panelLesson.Controls.Add(this.labelLessonStudent2);
            this.panelLesson.Controls.Add(this.cbLessonStudent1);
            this.panelLesson.Controls.Add(this.cbLessonTeacher2);
            this.panelLesson.Controls.Add(this.cbLessonTeacher1);
            this.panelLesson.Controls.Add(this.cbLessonProg);
            this.panelLesson.Controls.Add(this.labelLessonStudent1);
            this.panelLesson.Controls.Add(this.labelLessonTeacher2);
            this.panelLesson.Controls.Add(this.labelLessonTeacher1);
            this.panelLesson.Controls.Add(this.labelLessonProgram);
            this.panelLesson.Controls.Add(this.cbLessonState);
            this.panelLesson.Controls.Add(this.labelLessonState);
            this.panelLesson.Controls.Add(this.cbLessonEnd);
            this.panelLesson.Controls.Add(this.cbLessonStart);
            this.panelLesson.Controls.Add(this.monthCalendar1);
            this.panelLesson.Controls.Add(this.cbLessonRoom);
            this.panelLesson.Controls.Add(this.labelLessonRoom);
            this.panelLesson.Controls.Add(this.labelLessonEnd);
            this.panelLesson.Controls.Add(this.labelLessonStart);
            this.panelLesson.Controls.Add(this.labelLessonDate);
            this.panelLesson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLesson.Location = new System.Drawing.Point(3, 3);
            this.panelLesson.Name = "panelLesson";
            this.panelLesson.Size = new System.Drawing.Size(916, 350);
            this.panelLesson.TabIndex = 0;
            // 
            // cbLessonStudent10
            // 
            this.cbLessonStudent10.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student10", true));
            this.cbLessonStudent10.FormattingEnabled = true;
            this.cbLessonStudent10.Location = new System.Drawing.Point(688, 150);
            this.cbLessonStudent10.Name = "cbLessonStudent10";
            this.cbLessonStudent10.Size = new System.Drawing.Size(93, 21);
            this.cbLessonStudent10.TabIndex = 18;
            this.cbLessonStudent10.SelectedIndexChanged += new System.EventHandler(this.cbLessonStudent10_SelectedIndexChanged);
            // 
            // cbLessonStudent9
            // 
            this.cbLessonStudent9.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student9", true));
            this.cbLessonStudent9.FormattingEnabled = true;
            this.cbLessonStudent9.Location = new System.Drawing.Point(582, 150);
            this.cbLessonStudent9.Name = "cbLessonStudent9";
            this.cbLessonStudent9.Size = new System.Drawing.Size(82, 21);
            this.cbLessonStudent9.TabIndex = 17;
            this.cbLessonStudent9.SelectedIndexChanged += new System.EventHandler(this.cbLessonStudent9_SelectedIndexChanged);
            // 
            // labelLessonStudent10
            // 
            this.labelLessonStudent10.AutoSize = true;
            this.labelLessonStudent10.Location = new System.Drawing.Point(685, 129);
            this.labelLessonStudent10.Name = "labelLessonStudent10";
            this.labelLessonStudent10.Size = new System.Drawing.Size(62, 13);
            this.labelLessonStudent10.TabIndex = 41;
            this.labelLessonStudent10.Text = "Student 10:";
            // 
            // labelLessonStudent9
            // 
            this.labelLessonStudent9.AutoSize = true;
            this.labelLessonStudent9.Location = new System.Drawing.Point(579, 133);
            this.labelLessonStudent9.Name = "labelLessonStudent9";
            this.labelLessonStudent9.Size = new System.Drawing.Size(59, 13);
            this.labelLessonStudent9.TabIndex = 40;
            this.labelLessonStudent9.Text = "Student  9:";
            // 
            // cbLessonStudent8
            // 
            this.cbLessonStudent8.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student8", true));
            this.cbLessonStudent8.FormattingEnabled = true;
            this.cbLessonStudent8.Location = new System.Drawing.Point(688, 105);
            this.cbLessonStudent8.Name = "cbLessonStudent8";
            this.cbLessonStudent8.Size = new System.Drawing.Size(93, 21);
            this.cbLessonStudent8.TabIndex = 16;
            this.cbLessonStudent8.SelectedIndexChanged += new System.EventHandler(this.cbLessonStudent8_SelectedIndexChanged);
            // 
            // cbLessonStudent7
            // 
            this.cbLessonStudent7.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student7", true));
            this.cbLessonStudent7.FormattingEnabled = true;
            this.cbLessonStudent7.Location = new System.Drawing.Point(579, 105);
            this.cbLessonStudent7.Name = "cbLessonStudent7";
            this.cbLessonStudent7.Size = new System.Drawing.Size(85, 21);
            this.cbLessonStudent7.TabIndex = 15;
            this.cbLessonStudent7.SelectedIndexChanged += new System.EventHandler(this.cbLessonStudent7_SelectedIndexChanged);
            // 
            // labelLessonStudent8
            // 
            this.labelLessonStudent8.AutoSize = true;
            this.labelLessonStudent8.Location = new System.Drawing.Point(685, 90);
            this.labelLessonStudent8.Name = "labelLessonStudent8";
            this.labelLessonStudent8.Size = new System.Drawing.Size(59, 13);
            this.labelLessonStudent8.TabIndex = 37;
            this.labelLessonStudent8.Text = "Student  8:";
            // 
            // labelLessonStudent7
            // 
            this.labelLessonStudent7.AutoSize = true;
            this.labelLessonStudent7.Location = new System.Drawing.Point(576, 90);
            this.labelLessonStudent7.Name = "labelLessonStudent7";
            this.labelLessonStudent7.Size = new System.Drawing.Size(56, 13);
            this.labelLessonStudent7.TabIndex = 36;
            this.labelLessonStudent7.Text = "Student 7:";
            // 
            // cbLessonStudent6
            // 
            this.cbLessonStudent6.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student6", true));
            this.cbLessonStudent6.FormattingEnabled = true;
            this.cbLessonStudent6.Location = new System.Drawing.Point(688, 63);
            this.cbLessonStudent6.Name = "cbLessonStudent6";
            this.cbLessonStudent6.Size = new System.Drawing.Size(93, 21);
            this.cbLessonStudent6.TabIndex = 14;
            this.cbLessonStudent6.SelectedIndexChanged += new System.EventHandler(this.cbLessonStudent6_SelectedIndexChanged);
            // 
            // cbLessonStudent5
            // 
            this.cbLessonStudent5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student5", true));
            this.cbLessonStudent5.FormattingEnabled = true;
            this.cbLessonStudent5.Location = new System.Drawing.Point(579, 63);
            this.cbLessonStudent5.Name = "cbLessonStudent5";
            this.cbLessonStudent5.Size = new System.Drawing.Size(85, 21);
            this.cbLessonStudent5.TabIndex = 13;
            this.cbLessonStudent5.SelectedIndexChanged += new System.EventHandler(this.cbLessonStudent5_SelectedIndexChanged);
            // 
            // labelLessonStudent6
            // 
            this.labelLessonStudent6.AutoSize = true;
            this.labelLessonStudent6.Location = new System.Drawing.Point(685, 49);
            this.labelLessonStudent6.Name = "labelLessonStudent6";
            this.labelLessonStudent6.Size = new System.Drawing.Size(59, 13);
            this.labelLessonStudent6.TabIndex = 33;
            this.labelLessonStudent6.Text = "Student  6:";
            // 
            // labelLessonStudent5
            // 
            this.labelLessonStudent5.AutoSize = true;
            this.labelLessonStudent5.Location = new System.Drawing.Point(576, 49);
            this.labelLessonStudent5.Name = "labelLessonStudent5";
            this.labelLessonStudent5.Size = new System.Drawing.Size(56, 13);
            this.labelLessonStudent5.TabIndex = 32;
            this.labelLessonStudent5.Text = "Student 5:";
            // 
            // cbLessonStudent4
            // 
            this.cbLessonStudent4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student4", true));
            this.cbLessonStudent4.FormattingEnabled = true;
            this.cbLessonStudent4.Location = new System.Drawing.Point(688, 24);
            this.cbLessonStudent4.Name = "cbLessonStudent4";
            this.cbLessonStudent4.Size = new System.Drawing.Size(93, 21);
            this.cbLessonStudent4.TabIndex = 12;
            this.cbLessonStudent4.SelectedIndexChanged += new System.EventHandler(this.cbLessonStudent4_SelectedIndexChanged);
            // 
            // labelLessonStudent4
            // 
            this.labelLessonStudent4.AutoSize = true;
            this.labelLessonStudent4.Location = new System.Drawing.Point(685, 6);
            this.labelLessonStudent4.Name = "labelLessonStudent4";
            this.labelLessonStudent4.Size = new System.Drawing.Size(56, 13);
            this.labelLessonStudent4.TabIndex = 30;
            this.labelLessonStudent4.Text = "Student 4:";
            // 
            // cbLessonStudent3
            // 
            this.cbLessonStudent3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student3", true));
            this.cbLessonStudent3.FormattingEnabled = true;
            this.cbLessonStudent3.Location = new System.Drawing.Point(579, 24);
            this.cbLessonStudent3.Name = "cbLessonStudent3";
            this.cbLessonStudent3.Size = new System.Drawing.Size(85, 21);
            this.cbLessonStudent3.TabIndex = 11;
            this.cbLessonStudent3.SelectedIndexChanged += new System.EventHandler(this.cbLessonStudent3_SelectedIndexChanged);
            // 
            // labelLessonStudent3
            // 
            this.labelLessonStudent3.AutoSize = true;
            this.labelLessonStudent3.Location = new System.Drawing.Point(576, 6);
            this.labelLessonStudent3.Name = "labelLessonStudent3";
            this.labelLessonStudent3.Size = new System.Drawing.Size(56, 13);
            this.labelLessonStudent3.TabIndex = 28;
            this.labelLessonStudent3.Text = "Student 3:";
            // 
            // labelLessonComment
            // 
            this.labelLessonComment.AutoSize = true;
            this.labelLessonComment.Location = new System.Drawing.Point(266, 200);
            this.labelLessonComment.Name = "labelLessonComment";
            this.labelLessonComment.Size = new System.Drawing.Size(59, 13);
            this.labelLessonComment.TabIndex = 27;
            this.labelLessonComment.Text = "Comments:";
            // 
            // tbLEssonComment
            // 
            this.tbLEssonComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLEssonComment.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Comments", true));
            this.tbLEssonComment.Location = new System.Drawing.Point(9, 217);
            this.tbLEssonComment.Name = "tbLEssonComment";
            this.tbLEssonComment.Size = new System.Drawing.Size(891, 20);
            this.tbLEssonComment.TabIndex = 26;
            this.tbLEssonComment.TextChanged += new System.EventHandler(this.tbLEssonComment_TextChanged);
            // 
            // cbLessonStudent2
            // 
            this.cbLessonStudent2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student2", true));
            this.cbLessonStudent2.FormattingEnabled = true;
            this.cbLessonStudent2.Location = new System.Drawing.Point(377, 150);
            this.cbLessonStudent2.Name = "cbLessonStudent2";
            this.cbLessonStudent2.Size = new System.Drawing.Size(92, 21);
            this.cbLessonStudent2.TabIndex = 10;
            this.cbLessonStudent2.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // labelLessonStudent2
            // 
            this.labelLessonStudent2.AutoSize = true;
            this.labelLessonStudent2.Location = new System.Drawing.Point(303, 150);
            this.labelLessonStudent2.Name = "labelLessonStudent2";
            this.labelLessonStudent2.Size = new System.Drawing.Size(56, 13);
            this.labelLessonStudent2.TabIndex = 24;
            this.labelLessonStudent2.Text = "Student 2:";
            // 
            // cbLessonStudent1
            // 
            this.cbLessonStudent1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Student1", true));
            this.cbLessonStudent1.FormattingEnabled = true;
            this.cbLessonStudent1.Location = new System.Drawing.Point(373, 107);
            this.cbLessonStudent1.Name = "cbLessonStudent1";
            this.cbLessonStudent1.Size = new System.Drawing.Size(96, 21);
            this.cbLessonStudent1.TabIndex = 9;
            this.cbLessonStudent1.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // cbLessonTeacher2
            // 
            this.cbLessonTeacher2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Teacher2", true));
            this.cbLessonTeacher2.FormattingEnabled = true;
            this.cbLessonTeacher2.Location = new System.Drawing.Point(582, 188);
            this.cbLessonTeacher2.Name = "cbLessonTeacher2";
            this.cbLessonTeacher2.Size = new System.Drawing.Size(92, 21);
            this.cbLessonTeacher2.TabIndex = 8;
            this.cbLessonTeacher2.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // cbLessonTeacher1
            // 
            this.cbLessonTeacher1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Teacher1", true));
            this.cbLessonTeacher1.FormattingEnabled = true;
            this.cbLessonTeacher1.Location = new System.Drawing.Point(263, 107);
            this.cbLessonTeacher1.Name = "cbLessonTeacher1";
            this.cbLessonTeacher1.Size = new System.Drawing.Size(96, 21);
            this.cbLessonTeacher1.TabIndex = 7;
            this.cbLessonTeacher1.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // cbLessonProg
            // 
            this.cbLessonProg.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Program", true));
            this.cbLessonProg.FormattingEnabled = true;
            this.cbLessonProg.Location = new System.Drawing.Point(263, 23);
            this.cbLessonProg.Name = "cbLessonProg";
            this.cbLessonProg.Size = new System.Drawing.Size(96, 21);
            this.cbLessonProg.TabIndex = 3;
            this.cbLessonProg.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelLessonStudent1
            // 
            this.labelLessonStudent1.AutoSize = true;
            this.labelLessonStudent1.Location = new System.Drawing.Point(377, 89);
            this.labelLessonStudent1.Name = "labelLessonStudent1";
            this.labelLessonStudent1.Size = new System.Drawing.Size(56, 13);
            this.labelLessonStudent1.TabIndex = 19;
            this.labelLessonStudent1.Text = "Student 1:";
            // 
            // labelLessonTeacher2
            // 
            this.labelLessonTeacher2.AutoSize = true;
            this.labelLessonTeacher2.Location = new System.Drawing.Point(579, 174);
            this.labelLessonTeacher2.Name = "labelLessonTeacher2";
            this.labelLessonTeacher2.Size = new System.Drawing.Size(59, 13);
            this.labelLessonTeacher2.TabIndex = 17;
            this.labelLessonTeacher2.Text = "Teacher 2:";
            // 
            // labelLessonTeacher1
            // 
            this.labelLessonTeacher1.AutoSize = true;
            this.labelLessonTeacher1.Location = new System.Drawing.Point(260, 90);
            this.labelLessonTeacher1.Name = "labelLessonTeacher1";
            this.labelLessonTeacher1.Size = new System.Drawing.Size(50, 13);
            this.labelLessonTeacher1.TabIndex = 16;
            this.labelLessonTeacher1.Text = "Teacher:";
            // 
            // labelLessonProgram
            // 
            this.labelLessonProgram.AutoSize = true;
            this.labelLessonProgram.Location = new System.Drawing.Point(260, 6);
            this.labelLessonProgram.Name = "labelLessonProgram";
            this.labelLessonProgram.Size = new System.Drawing.Size(49, 13);
            this.labelLessonProgram.TabIndex = 13;
            this.labelLessonProgram.Text = "Program:";
            // 
            // cbLessonState
            // 
            this.cbLessonState.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "State", true));
            this.cbLessonState.FormattingEnabled = true;
            this.cbLessonState.Location = new System.Drawing.Point(145, 22);
            this.cbLessonState.Name = "cbLessonState";
            this.cbLessonState.Size = new System.Drawing.Size(91, 21);
            this.cbLessonState.TabIndex = 1;
            this.cbLessonState.SelectedIndexChanged += new System.EventHandler(this.cbLessonState_SelectedIndexChanged);
            // 
            // labelLessonState
            // 
            this.labelLessonState.AutoSize = true;
            this.labelLessonState.Location = new System.Drawing.Point(152, 6);
            this.labelLessonState.Name = "labelLessonState";
            this.labelLessonState.Size = new System.Drawing.Size(35, 13);
            this.labelLessonState.TabIndex = 11;
            this.labelLessonState.Text = "State:";
            // 
            // cbLessonEnd
            // 
            this.cbLessonEnd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "End", true));
            this.cbLessonEnd.FormattingEnabled = true;
            this.cbLessonEnd.Location = new System.Drawing.Point(377, 65);
            this.cbLessonEnd.Name = "cbLessonEnd";
            this.cbLessonEnd.Size = new System.Drawing.Size(92, 21);
            this.cbLessonEnd.TabIndex = 6;
            this.cbLessonEnd.SelectedIndexChanged += new System.EventHandler(this.cbLessonEnd_SelectedIndexChanged);
            // 
            // cbLessonStart
            // 
            this.cbLessonStart.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Start", true));
            this.cbLessonStart.FormattingEnabled = true;
            this.cbLessonStart.Location = new System.Drawing.Point(263, 63);
            this.cbLessonStart.Name = "cbLessonStart";
            this.cbLessonStart.Size = new System.Drawing.Size(96, 21);
            this.cbLessonStart.TabIndex = 5;
            this.cbLessonStart.SelectedIndexChanged += new System.EventHandler(this.cbLessonStart_SelectedIndexChanged);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(9, 47);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 2;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // cbLessonRoom
            // 
            this.cbLessonRoom.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.lessonList, "Room", true));
            this.cbLessonRoom.FormattingEnabled = true;
            this.cbLessonRoom.Location = new System.Drawing.Point(377, 24);
            this.cbLessonRoom.Name = "cbLessonRoom";
            this.cbLessonRoom.Size = new System.Drawing.Size(92, 21);
            this.cbLessonRoom.TabIndex = 4;
            this.cbLessonRoom.SelectedIndexChanged += new System.EventHandler(this.cbLessonRoom_SelectedIndexChanged);
            // 
            // labelLessonRoom
            // 
            this.labelLessonRoom.AutoSize = true;
            this.labelLessonRoom.Location = new System.Drawing.Point(383, 6);
            this.labelLessonRoom.Name = "labelLessonRoom";
            this.labelLessonRoom.Size = new System.Drawing.Size(38, 13);
            this.labelLessonRoom.TabIndex = 6;
            this.labelLessonRoom.Text = "Room:";
            // 
            // labelLessonEnd
            // 
            this.labelLessonEnd.AutoSize = true;
            this.labelLessonEnd.Location = new System.Drawing.Point(383, 49);
            this.labelLessonEnd.Name = "labelLessonEnd";
            this.labelLessonEnd.Size = new System.Drawing.Size(29, 13);
            this.labelLessonEnd.TabIndex = 4;
            this.labelLessonEnd.Text = "End:";
            // 
            // labelLessonStart
            // 
            this.labelLessonStart.AutoSize = true;
            this.labelLessonStart.Location = new System.Drawing.Point(260, 47);
            this.labelLessonStart.Name = "labelLessonStart";
            this.labelLessonStart.Size = new System.Drawing.Size(32, 13);
            this.labelLessonStart.TabIndex = 2;
            this.labelLessonStart.Text = "Start:";
            // 
            // labelLessonDate
            // 
            this.labelLessonDate.AutoSize = true;
            this.labelLessonDate.Location = new System.Drawing.Point(18, 30);
            this.labelLessonDate.Name = "labelLessonDate";
            this.labelLessonDate.Size = new System.Drawing.Size(33, 13);
            this.labelLessonDate.TabIndex = 0;
            this.labelLessonDate.Text = "Date:";
            // 
            // panelGlobPrevDelete
            // 
            this.panelGlobPrevDelete.Controls.Add(this.butGlobalDelete);
            this.panelGlobPrevDelete.Controls.Add(this.butGlobalPrev);
            this.panelGlobPrevDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelGlobPrevDelete.Location = new System.Drawing.Point(0, 0);
            this.panelGlobPrevDelete.Name = "panelGlobPrevDelete";
            this.panelGlobPrevDelete.Size = new System.Drawing.Size(44, 385);
            this.panelGlobPrevDelete.TabIndex = 17;
            // 
            // butGlobalDelete
            // 
            this.butGlobalDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(0)))), ((int)(((byte)(20)))));
            this.butGlobalDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butGlobalDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butGlobalDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.butGlobalDelete.Location = new System.Drawing.Point(4, 181);
            this.butGlobalDelete.Name = "butGlobalDelete";
            this.butGlobalDelete.Size = new System.Drawing.Size(40, 102);
            this.butGlobalDelete.TabIndex = 32;
            this.butGlobalDelete.Text = "X";
            this.butGlobalDelete.UseVisualStyleBackColor = false;
            this.butGlobalDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // butGlobalPrev
            // 
            this.butGlobalPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.butGlobalPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butGlobalPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butGlobalPrev.Location = new System.Drawing.Point(3, 0);
            this.butGlobalPrev.Name = "butGlobalPrev";
            this.butGlobalPrev.Size = new System.Drawing.Size(37, 159);
            this.butGlobalPrev.TabIndex = 30;
            this.butGlobalPrev.Text = "<<";
            this.butGlobalPrev.UseVisualStyleBackColor = false;
            this.butGlobalPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // panelGlobNextNew
            // 
            this.panelGlobNextNew.Controls.Add(this.butGlobalAdd);
            this.panelGlobNextNew.Controls.Add(this.butGlobalNext);
            this.panelGlobNextNew.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelGlobNextNew.Location = new System.Drawing.Point(977, 0);
            this.panelGlobNextNew.Name = "panelGlobNextNew";
            this.panelGlobNextNew.Size = new System.Drawing.Size(48, 385);
            this.panelGlobNextNew.TabIndex = 20;
            // 
            // butGlobalAdd
            // 
            this.butGlobalAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butGlobalAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(162)))), ((int)(((byte)(0)))));
            this.butGlobalAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butGlobalAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butGlobalAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.butGlobalAdd.Location = new System.Drawing.Point(3, 181);
            this.butGlobalAdd.Name = "butGlobalAdd";
            this.butGlobalAdd.Size = new System.Drawing.Size(40, 102);
            this.butGlobalAdd.TabIndex = 33;
            this.butGlobalAdd.Text = "+";
            this.butGlobalAdd.UseVisualStyleBackColor = false;
            this.butGlobalAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // butGlobalNext
            // 
            this.butGlobalNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butGlobalNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(255)))), ((int)(((byte)(236)))));
            this.butGlobalNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butGlobalNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butGlobalNext.Location = new System.Drawing.Point(5, 1);
            this.butGlobalNext.Name = "butGlobalNext";
            this.butGlobalNext.Size = new System.Drawing.Size(40, 158);
            this.butGlobalNext.TabIndex = 31;
            this.butGlobalNext.Text = ">>";
            this.butGlobalNext.UseVisualStyleBackColor = false;
            this.butGlobalNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonGlobRefresh
            // 
            this.buttonGlobRefresh.Location = new System.Drawing.Point(63, 334);
            this.buttonGlobRefresh.Name = "buttonGlobRefresh";
            this.buttonGlobRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonGlobRefresh.TabIndex = 14;
            this.buttonGlobRefresh.Text = "Refresh";
            this.buttonGlobRefresh.UseVisualStyleBackColor = true;
            this.buttonGlobRefresh.Click += new System.EventHandler(this.buttonGlobRefresh_Click);
            // 
            // panelGlobSearch
            // 
            this.panelGlobSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(195)))), ((int)(((byte)(178)))));
            this.panelGlobSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGlobSearch.Controls.Add(this.panelSearchButtons);
            this.panelGlobSearch.Controls.Add(this.tabControlSearch);
            this.panelGlobSearch.Controls.Add(this.panelSearchLabels);
            this.panelGlobSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGlobSearch.Location = new System.Drawing.Point(0, 408);
            this.panelGlobSearch.Name = "panelGlobSearch";
            this.panelGlobSearch.Size = new System.Drawing.Size(248, 367);
            this.panelGlobSearch.TabIndex = 11;
            // 
            // panelSearchButtons
            // 
            this.panelSearchButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchButtons.Controls.Add(this.butGlobalToExcel);
            this.panelSearchButtons.Controls.Add(this.buttGlobalShowAll);
            this.panelSearchButtons.Location = new System.Drawing.Point(28, 332);
            this.panelSearchButtons.Name = "panelSearchButtons";
            this.panelSearchButtons.Size = new System.Drawing.Size(213, 34);
            this.panelSearchButtons.TabIndex = 51;
            // 
            // butGlobalToExcel
            // 
            this.butGlobalToExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(210)))), ((int)(((byte)(47)))));
            this.butGlobalToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butGlobalToExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butGlobalToExcel.Location = new System.Drawing.Point(3, 8);
            this.butGlobalToExcel.Name = "butGlobalToExcel";
            this.butGlobalToExcel.Size = new System.Drawing.Size(82, 23);
            this.butGlobalToExcel.TabIndex = 47;
            this.butGlobalToExcel.Text = "To Excel";
            this.butGlobalToExcel.UseVisualStyleBackColor = false;
            this.butGlobalToExcel.Click += new System.EventHandler(this.buttonToExcel_Click);
            // 
            // buttGlobalShowAll
            // 
            this.buttGlobalShowAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(210)))), ((int)(((byte)(47)))));
            this.buttGlobalShowAll.Enabled = false;
            this.buttGlobalShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttGlobalShowAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttGlobalShowAll.Location = new System.Drawing.Point(128, 8);
            this.buttGlobalShowAll.Name = "buttGlobalShowAll";
            this.buttGlobalShowAll.Size = new System.Drawing.Size(82, 23);
            this.buttGlobalShowAll.TabIndex = 48;
            this.buttGlobalShowAll.Text = "Show all";
            this.buttGlobalShowAll.UseVisualStyleBackColor = false;
            this.buttGlobalShowAll.Click += new System.EventHandler(this.buttonShowAll_Click);
            // 
            // tabControlSearch
            // 
            this.tabControlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSearch.Controls.Add(this.tabPageSearchStudent);
            this.tabControlSearch.Controls.Add(this.tabPageSearchTeacher);
            this.tabControlSearch.Controls.Add(this.tabPageSearchProgram);
            this.tabControlSearch.Controls.Add(this.tabPageSearchRoom);
            this.tabControlSearch.Controls.Add(this.tabPageSearchLesson);
            this.tabControlSearch.Location = new System.Drawing.Point(24, 36);
            this.tabControlSearch.Name = "tabControlSearch";
            this.tabControlSearch.SelectedIndex = 0;
            this.tabControlSearch.Size = new System.Drawing.Size(219, 297);
            this.tabControlSearch.TabIndex = 49;
            // 
            // tabPageSearchStudent
            // 
            this.tabPageSearchStudent.Controls.Add(this.panelStudSearch);
            this.tabPageSearchStudent.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearchStudent.Name = "tabPageSearchStudent";
            this.tabPageSearchStudent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearchStudent.Size = new System.Drawing.Size(211, 271);
            this.tabPageSearchStudent.TabIndex = 0;
            this.tabPageSearchStudent.Text = "t1";
            this.tabPageSearchStudent.UseVisualStyleBackColor = true;
            // 
            // panelStudSearch
            // 
            this.panelStudSearch.Controls.Add(this.lbStudSearchStatus);
            this.panelStudSearch.Controls.Add(this.lbStudSearchLearns);
            this.panelStudSearch.Controls.Add(this.lbStudSearchSpeaks);
            this.panelStudSearch.Controls.Add(this.cbStudSearchLevel);
            this.panelStudSearch.Controls.Add(this.cbStudSearchLearns);
            this.panelStudSearch.Controls.Add(this.lbStudSearchLevel);
            this.panelStudSearch.Controls.Add(this.cbStudSearchSpeaks);
            this.panelStudSearch.Controls.Add(this.lbStudSearchFirstName1);
            this.panelStudSearch.Controls.Add(this.cbStudSearchSource);
            this.panelStudSearch.Controls.Add(this.tbStudSearchFirstName);
            this.panelStudSearch.Controls.Add(this.lbStudSearchSource);
            this.panelStudSearch.Controls.Add(this.cbStudSelectStatus);
            this.panelStudSearch.Controls.Add(this.lbStudSearchLastName);
            this.panelStudSearch.Controls.Add(this.tbStudSearchLastName);
            this.panelStudSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStudSearch.Location = new System.Drawing.Point(3, 3);
            this.panelStudSearch.Name = "panelStudSearch";
            this.panelStudSearch.Size = new System.Drawing.Size(205, 265);
            this.panelStudSearch.TabIndex = 19;
            // 
            // lbStudSearchStatus
            // 
            this.lbStudSearchStatus.AutoSize = true;
            this.lbStudSearchStatus.Location = new System.Drawing.Point(14, 7);
            this.lbStudSearchStatus.Name = "lbStudSearchStatus";
            this.lbStudSearchStatus.Size = new System.Drawing.Size(40, 13);
            this.lbStudSearchStatus.TabIndex = 0;
            this.lbStudSearchStatus.Text = "Status:";
            // 
            // lbStudSearchLearns
            // 
            this.lbStudSearchLearns.AutoSize = true;
            this.lbStudSearchLearns.Location = new System.Drawing.Point(16, 45);
            this.lbStudSearchLearns.Name = "lbStudSearchLearns";
            this.lbStudSearchLearns.Size = new System.Drawing.Size(42, 13);
            this.lbStudSearchLearns.TabIndex = 4;
            this.lbStudSearchLearns.Text = "Learns:";
            // 
            // lbStudSearchSpeaks
            // 
            this.lbStudSearchSpeaks.AutoSize = true;
            this.lbStudSearchSpeaks.Location = new System.Drawing.Point(14, 73);
            this.lbStudSearchSpeaks.Name = "lbStudSearchSpeaks";
            this.lbStudSearchSpeaks.Size = new System.Drawing.Size(46, 13);
            this.lbStudSearchSpeaks.TabIndex = 6;
            this.lbStudSearchSpeaks.Text = "Speaks:";
            // 
            // cbStudSearchLevel
            // 
            this.cbStudSearchLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudSearchLevel.FormattingEnabled = true;
            this.cbStudSearchLevel.Location = new System.Drawing.Point(69, 205);
            this.cbStudSearchLevel.Name = "cbStudSearchLevel";
            this.cbStudSearchLevel.Size = new System.Drawing.Size(121, 21);
            this.cbStudSearchLevel.TabIndex = 45;
            this.cbStudSearchLevel.SelectedIndexChanged += new System.EventHandler(this.cbSearchStudLevel_SelectedIndexChanged);
            // 
            // cbStudSearchLearns
            // 
            this.cbStudSearchLearns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudSearchLearns.FormattingEnabled = true;
            this.cbStudSearchLearns.Location = new System.Drawing.Point(71, 57);
            this.cbStudSearchLearns.Name = "cbStudSearchLearns";
            this.cbStudSearchLearns.Size = new System.Drawing.Size(121, 21);
            this.cbStudSearchLearns.TabIndex = 41;
            this.cbStudSearchLearns.SelectedIndexChanged += new System.EventHandler(this.cbSearchStudLearns_SelectedIndexChanged);
            // 
            // lbStudSearchLevel
            // 
            this.lbStudSearchLevel.AutoSize = true;
            this.lbStudSearchLevel.Location = new System.Drawing.Point(16, 196);
            this.lbStudSearchLevel.Name = "lbStudSearchLevel";
            this.lbStudSearchLevel.Size = new System.Drawing.Size(36, 13);
            this.lbStudSearchLevel.TabIndex = 15;
            this.lbStudSearchLevel.Text = "Level:";
            // 
            // cbStudSearchSpeaks
            // 
            this.cbStudSearchSpeaks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudSearchSpeaks.FormattingEnabled = true;
            this.cbStudSearchSpeaks.Location = new System.Drawing.Point(71, 90);
            this.cbStudSearchSpeaks.Name = "cbStudSearchSpeaks";
            this.cbStudSearchSpeaks.Size = new System.Drawing.Size(121, 21);
            this.cbStudSearchSpeaks.TabIndex = 42;
            this.cbStudSearchSpeaks.SelectedIndexChanged += new System.EventHandler(this.cbSearchStudSpeaks_SelectedIndexChanged);
            // 
            // lbStudSearchFirstName1
            // 
            this.lbStudSearchFirstName1.AutoSize = true;
            this.lbStudSearchFirstName1.Location = new System.Drawing.Point(16, 114);
            this.lbStudSearchFirstName1.Name = "lbStudSearchFirstName1";
            this.lbStudSearchFirstName1.Size = new System.Drawing.Size(60, 13);
            this.lbStudSearchFirstName1.TabIndex = 8;
            this.lbStudSearchFirstName1.Text = "First Name:";
            // 
            // cbStudSearchSource
            // 
            this.cbStudSearchSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudSearchSource.FormattingEnabled = true;
            this.cbStudSearchSource.Location = new System.Drawing.Point(69, 239);
            this.cbStudSearchSource.Name = "cbStudSearchSource";
            this.cbStudSearchSource.Size = new System.Drawing.Size(121, 21);
            this.cbStudSearchSource.TabIndex = 46;
            this.cbStudSearchSource.SelectedIndexChanged += new System.EventHandler(this.cbSearchStudSource_SelectedIndexChanged);
            // 
            // tbStudSearchFirstName
            // 
            this.tbStudSearchFirstName.Location = new System.Drawing.Point(69, 130);
            this.tbStudSearchFirstName.Name = "tbStudSearchFirstName";
            this.tbStudSearchFirstName.Size = new System.Drawing.Size(121, 20);
            this.tbStudSearchFirstName.TabIndex = 43;
            this.tbStudSearchFirstName.TextChanged += new System.EventHandler(this.tbSearchStudFirstName_TextChanged);
            // 
            // lbStudSearchSource
            // 
            this.lbStudSearchSource.AutoSize = true;
            this.lbStudSearchSource.Location = new System.Drawing.Point(16, 230);
            this.lbStudSearchSource.Name = "lbStudSearchSource";
            this.lbStudSearchSource.Size = new System.Drawing.Size(44, 13);
            this.lbStudSearchSource.TabIndex = 12;
            this.lbStudSearchSource.Text = "Source:";
            // 
            // cbStudSelectStatus
            // 
            this.cbStudSelectStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStudSelectStatus.FormattingEnabled = true;
            this.cbStudSelectStatus.Location = new System.Drawing.Point(69, 15);
            this.cbStudSelectStatus.Name = "cbStudSelectStatus";
            this.cbStudSelectStatus.Size = new System.Drawing.Size(120, 21);
            this.cbStudSelectStatus.TabIndex = 40;
            this.cbStudSelectStatus.SelectedIndexChanged += new System.EventHandler(this.cbSearchStudStatus_SelectedIndexChanged);
            // 
            // lbStudSearchLastName
            // 
            this.lbStudSearchLastName.AutoSize = true;
            this.lbStudSearchLastName.Location = new System.Drawing.Point(16, 151);
            this.lbStudSearchLastName.Name = "lbStudSearchLastName";
            this.lbStudSearchLastName.Size = new System.Drawing.Size(61, 13);
            this.lbStudSearchLastName.TabIndex = 10;
            this.lbStudSearchLastName.Text = "Last Name:";
            // 
            // tbStudSearchLastName
            // 
            this.tbStudSearchLastName.Location = new System.Drawing.Point(69, 168);
            this.tbStudSearchLastName.Name = "tbStudSearchLastName";
            this.tbStudSearchLastName.Size = new System.Drawing.Size(120, 20);
            this.tbStudSearchLastName.TabIndex = 44;
            this.tbStudSearchLastName.TextChanged += new System.EventHandler(this.tbSearchStudLastName_TextChanged);
            // 
            // tabPageSearchTeacher
            // 
            this.tabPageSearchTeacher.Controls.Add(this.panelSearchTeacher);
            this.tabPageSearchTeacher.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearchTeacher.Name = "tabPageSearchTeacher";
            this.tabPageSearchTeacher.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearchTeacher.Size = new System.Drawing.Size(211, 271);
            this.tabPageSearchTeacher.TabIndex = 1;
            this.tabPageSearchTeacher.Text = "t2";
            this.tabPageSearchTeacher.UseVisualStyleBackColor = true;
            // 
            // panelSearchTeacher
            // 
            this.panelSearchTeacher.Controls.Add(this.tbSearchTeachLastName);
            this.panelSearchTeacher.Controls.Add(this.tbSearchTeachFirstName);
            this.panelSearchTeacher.Controls.Add(this.cbSearchTeachLang1);
            this.panelSearchTeacher.Controls.Add(this.cbSearchTeachStatus);
            this.panelSearchTeacher.Controls.Add(this.lbSerchTeachLastName);
            this.panelSearchTeacher.Controls.Add(this.lbSerchTeachFirstName);
            this.panelSearchTeacher.Controls.Add(this.lbSerchTeachLang1);
            this.panelSearchTeacher.Controls.Add(this.lbSerchTeachStatus);
            this.panelSearchTeacher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearchTeacher.Location = new System.Drawing.Point(3, 3);
            this.panelSearchTeacher.Name = "panelSearchTeacher";
            this.panelSearchTeacher.Size = new System.Drawing.Size(205, 265);
            this.panelSearchTeacher.TabIndex = 52;
            // 
            // tbSearchTeachLastName
            // 
            this.tbSearchTeachLastName.Location = new System.Drawing.Point(30, 158);
            this.tbSearchTeachLastName.Name = "tbSearchTeachLastName";
            this.tbSearchTeachLastName.Size = new System.Drawing.Size(148, 20);
            this.tbSearchTeachLastName.TabIndex = 9;
            this.tbSearchTeachLastName.TextChanged += new System.EventHandler(this.tbSearchTeachLastName_TextChanged);
            // 
            // tbSearchTeachFirstName
            // 
            this.tbSearchTeachFirstName.Location = new System.Drawing.Point(30, 113);
            this.tbSearchTeachFirstName.Name = "tbSearchTeachFirstName";
            this.tbSearchTeachFirstName.Size = new System.Drawing.Size(148, 20);
            this.tbSearchTeachFirstName.TabIndex = 8;
            this.tbSearchTeachFirstName.TextChanged += new System.EventHandler(this.tbSearchTeachFirstName_TextChanged);
            // 
            // cbSearchTeachLang1
            // 
            this.cbSearchTeachLang1.FormattingEnabled = true;
            this.cbSearchTeachLang1.Location = new System.Drawing.Point(30, 64);
            this.cbSearchTeachLang1.Name = "cbSearchTeachLang1";
            this.cbSearchTeachLang1.Size = new System.Drawing.Size(148, 21);
            this.cbSearchTeachLang1.TabIndex = 6;
            this.cbSearchTeachLang1.SelectedIndexChanged += new System.EventHandler(this.cbSearchTeachLang1_SelectedIndexChanged);
            // 
            // cbSearchTeachStatus
            // 
            this.cbSearchTeachStatus.FormattingEnabled = true;
            this.cbSearchTeachStatus.Location = new System.Drawing.Point(30, 24);
            this.cbSearchTeachStatus.Name = "cbSearchTeachStatus";
            this.cbSearchTeachStatus.Size = new System.Drawing.Size(148, 21);
            this.cbSearchTeachStatus.TabIndex = 5;
            this.cbSearchTeachStatus.SelectedIndexChanged += new System.EventHandler(this.cbSearchTeachStatus_SelectedIndexChanged);
            // 
            // lbSerchTeachLastName
            // 
            this.lbSerchTeachLastName.AutoSize = true;
            this.lbSerchTeachLastName.Location = new System.Drawing.Point(15, 140);
            this.lbSerchTeachLastName.Name = "lbSerchTeachLastName";
            this.lbSerchTeachLastName.Size = new System.Drawing.Size(59, 13);
            this.lbSerchTeachLastName.TabIndex = 4;
            this.lbSerchTeachLastName.Text = "Last name:";
            // 
            // lbSerchTeachFirstName
            // 
            this.lbSerchTeachFirstName.AutoSize = true;
            this.lbSerchTeachFirstName.Location = new System.Drawing.Point(15, 94);
            this.lbSerchTeachFirstName.Name = "lbSerchTeachFirstName";
            this.lbSerchTeachFirstName.Size = new System.Drawing.Size(58, 13);
            this.lbSerchTeachFirstName.TabIndex = 3;
            this.lbSerchTeachFirstName.Text = "First name:";
            // 
            // lbSerchTeachLang1
            // 
            this.lbSerchTeachLang1.AutoSize = true;
            this.lbSerchTeachLang1.Location = new System.Drawing.Point(15, 48);
            this.lbSerchTeachLang1.Name = "lbSerchTeachLang1";
            this.lbSerchTeachLang1.Size = new System.Drawing.Size(58, 13);
            this.lbSerchTeachLang1.TabIndex = 1;
            this.lbSerchTeachLang1.Text = "Language:";
            // 
            // lbSerchTeachStatus
            // 
            this.lbSerchTeachStatus.AutoSize = true;
            this.lbSerchTeachStatus.Location = new System.Drawing.Point(15, 9);
            this.lbSerchTeachStatus.Name = "lbSerchTeachStatus";
            this.lbSerchTeachStatus.Size = new System.Drawing.Size(40, 13);
            this.lbSerchTeachStatus.TabIndex = 0;
            this.lbSerchTeachStatus.Text = "Status:";
            // 
            // tabPageSearchProgram
            // 
            this.tabPageSearchProgram.Controls.Add(this.panelSearchProgram);
            this.tabPageSearchProgram.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearchProgram.Name = "tabPageSearchProgram";
            this.tabPageSearchProgram.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearchProgram.Size = new System.Drawing.Size(211, 271);
            this.tabPageSearchProgram.TabIndex = 2;
            this.tabPageSearchProgram.Text = "t3";
            this.tabPageSearchProgram.UseVisualStyleBackColor = true;
            // 
            // panelSearchProgram
            // 
            this.panelSearchProgram.Controls.Add(this.cbSearchProgLevel);
            this.panelSearchProgram.Controls.Add(this.cbSearchProgLanguage);
            this.panelSearchProgram.Controls.Add(this.lbSearchProgLevel);
            this.panelSearchProgram.Controls.Add(this.lbSearchProgLanguage);
            this.panelSearchProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearchProgram.Location = new System.Drawing.Point(3, 3);
            this.panelSearchProgram.Name = "panelSearchProgram";
            this.panelSearchProgram.Size = new System.Drawing.Size(205, 265);
            this.panelSearchProgram.TabIndex = 53;
            // 
            // cbSearchProgLevel
            // 
            this.cbSearchProgLevel.FormattingEnabled = true;
            this.cbSearchProgLevel.Location = new System.Drawing.Point(54, 63);
            this.cbSearchProgLevel.Name = "cbSearchProgLevel";
            this.cbSearchProgLevel.Size = new System.Drawing.Size(121, 21);
            this.cbSearchProgLevel.TabIndex = 3;
            this.cbSearchProgLevel.SelectedIndexChanged += new System.EventHandler(this.cbSearchProgLevel_SelectedIndexChanged);
            // 
            // cbSearchProgLanguage
            // 
            this.cbSearchProgLanguage.FormattingEnabled = true;
            this.cbSearchProgLanguage.Location = new System.Drawing.Point(54, 23);
            this.cbSearchProgLanguage.Name = "cbSearchProgLanguage";
            this.cbSearchProgLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbSearchProgLanguage.TabIndex = 2;
            this.cbSearchProgLanguage.SelectedIndexChanged += new System.EventHandler(this.cbSearchProgLanguage_SelectedIndexChanged);
            // 
            // lbSearchProgLevel
            // 
            this.lbSearchProgLevel.AutoSize = true;
            this.lbSearchProgLevel.Location = new System.Drawing.Point(13, 50);
            this.lbSearchProgLevel.Name = "lbSearchProgLevel";
            this.lbSearchProgLevel.Size = new System.Drawing.Size(36, 13);
            this.lbSearchProgLevel.TabIndex = 1;
            this.lbSearchProgLevel.Text = "Level:";
            // 
            // lbSearchProgLanguage
            // 
            this.lbSearchProgLanguage.AutoSize = true;
            this.lbSearchProgLanguage.Location = new System.Drawing.Point(13, 8);
            this.lbSearchProgLanguage.Name = "lbSearchProgLanguage";
            this.lbSearchProgLanguage.Size = new System.Drawing.Size(58, 13);
            this.lbSearchProgLanguage.TabIndex = 0;
            this.lbSearchProgLanguage.Text = "Language:";
            // 
            // tabPageSearchRoom
            // 
            this.tabPageSearchRoom.Controls.Add(this.panelSearchRoom);
            this.tabPageSearchRoom.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearchRoom.Name = "tabPageSearchRoom";
            this.tabPageSearchRoom.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearchRoom.Size = new System.Drawing.Size(211, 271);
            this.tabPageSearchRoom.TabIndex = 3;
            this.tabPageSearchRoom.Text = "t4";
            this.tabPageSearchRoom.UseVisualStyleBackColor = true;
            // 
            // panelSearchRoom
            // 
            this.panelSearchRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearchRoom.Location = new System.Drawing.Point(3, 3);
            this.panelSearchRoom.Name = "panelSearchRoom";
            this.panelSearchRoom.Size = new System.Drawing.Size(205, 265);
            this.panelSearchRoom.TabIndex = 52;
            // 
            // tabPageSearchLesson
            // 
            this.tabPageSearchLesson.Controls.Add(this.panelSearchLesson);
            this.tabPageSearchLesson.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearchLesson.Name = "tabPageSearchLesson";
            this.tabPageSearchLesson.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearchLesson.Size = new System.Drawing.Size(211, 271);
            this.tabPageSearchLesson.TabIndex = 4;
            this.tabPageSearchLesson.Text = "t5";
            this.tabPageSearchLesson.UseVisualStyleBackColor = true;
            // 
            // panelSearchLesson
            // 
            this.panelSearchLesson.Controls.Add(this.tbSearchLessonDate);
            this.panelSearchLesson.Controls.Add(this.lbSearchLessonStudent);
            this.panelSearchLesson.Controls.Add(this.cbSearchLessonRoom);
            this.panelSearchLesson.Controls.Add(this.lbSearchLessonRoom);
            this.panelSearchLesson.Controls.Add(this.cbSearchLessonProgram);
            this.panelSearchLesson.Controls.Add(this.lbSearchLessonProgram);
            this.panelSearchLesson.Controls.Add(this.tbSearchLessonTeacher);
            this.panelSearchLesson.Controls.Add(this.lbSearchTeacher);
            this.panelSearchLesson.Controls.Add(this.tbSearchLessonStudent);
            this.panelSearchLesson.Controls.Add(this.lbSearchLessonDay);
            this.panelSearchLesson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearchLesson.Location = new System.Drawing.Point(3, 3);
            this.panelSearchLesson.Name = "panelSearchLesson";
            this.panelSearchLesson.Size = new System.Drawing.Size(205, 265);
            this.panelSearchLesson.TabIndex = 53;
            // 
            // tbSearchLessonDate
            // 
            this.tbSearchLessonDate.Location = new System.Drawing.Point(70, 210);
            this.tbSearchLessonDate.Name = "tbSearchLessonDate";
            this.tbSearchLessonDate.Size = new System.Drawing.Size(121, 20);
            this.tbSearchLessonDate.TabIndex = 10;
            this.tbSearchLessonDate.TextChanged += new System.EventHandler(this.tbSearchLessonDate_TextChanged);
            // 
            // lbSearchLessonStudent
            // 
            this.lbSearchLessonStudent.AutoSize = true;
            this.lbSearchLessonStudent.Location = new System.Drawing.Point(15, 9);
            this.lbSearchLessonStudent.Name = "lbSearchLessonStudent";
            this.lbSearchLessonStudent.Size = new System.Drawing.Size(47, 13);
            this.lbSearchLessonStudent.TabIndex = 9;
            this.lbSearchLessonStudent.Text = "Student:";
            // 
            // cbSearchLessonRoom
            // 
            this.cbSearchLessonRoom.FormattingEnabled = true;
            this.cbSearchLessonRoom.Location = new System.Drawing.Point(70, 158);
            this.cbSearchLessonRoom.Name = "cbSearchLessonRoom";
            this.cbSearchLessonRoom.Size = new System.Drawing.Size(121, 21);
            this.cbSearchLessonRoom.TabIndex = 8;
            this.cbSearchLessonRoom.TextChanged += new System.EventHandler(this.cbSearchLessonRoom_TextChanged);
            // 
            // lbSearchLessonRoom
            // 
            this.lbSearchLessonRoom.AutoSize = true;
            this.lbSearchLessonRoom.Location = new System.Drawing.Point(13, 136);
            this.lbSearchLessonRoom.Name = "lbSearchLessonRoom";
            this.lbSearchLessonRoom.Size = new System.Drawing.Size(38, 13);
            this.lbSearchLessonRoom.TabIndex = 6;
            this.lbSearchLessonRoom.Text = "Room:";
            // 
            // cbSearchLessonProgram
            // 
            this.cbSearchLessonProgram.FormattingEnabled = true;
            this.cbSearchLessonProgram.Location = new System.Drawing.Point(70, 105);
            this.cbSearchLessonProgram.Name = "cbSearchLessonProgram";
            this.cbSearchLessonProgram.Size = new System.Drawing.Size(121, 21);
            this.cbSearchLessonProgram.TabIndex = 5;
            this.cbSearchLessonProgram.TextChanged += new System.EventHandler(this.cbSearchLessonProgram_TextChanged);
            // 
            // lbSearchLessonProgram
            // 
            this.lbSearchLessonProgram.AutoSize = true;
            this.lbSearchLessonProgram.Location = new System.Drawing.Point(13, 94);
            this.lbSearchLessonProgram.Name = "lbSearchLessonProgram";
            this.lbSearchLessonProgram.Size = new System.Drawing.Size(49, 13);
            this.lbSearchLessonProgram.TabIndex = 4;
            this.lbSearchLessonProgram.Text = "Program:";
            // 
            // tbSearchLessonTeacher
            // 
            this.tbSearchLessonTeacher.Location = new System.Drawing.Point(70, 62);
            this.tbSearchLessonTeacher.Name = "tbSearchLessonTeacher";
            this.tbSearchLessonTeacher.Size = new System.Drawing.Size(121, 20);
            this.tbSearchLessonTeacher.TabIndex = 3;
            this.tbSearchLessonTeacher.TextChanged += new System.EventHandler(this.tbSearchLessonTeacher_TextChanged);
            // 
            // lbSearchTeacher
            // 
            this.lbSearchTeacher.AutoSize = true;
            this.lbSearchTeacher.Location = new System.Drawing.Point(13, 47);
            this.lbSearchTeacher.Name = "lbSearchTeacher";
            this.lbSearchTeacher.Size = new System.Drawing.Size(50, 13);
            this.lbSearchTeacher.TabIndex = 2;
            this.lbSearchTeacher.Text = "Teacher:";
            // 
            // tbSearchLessonStudent
            // 
            this.tbSearchLessonStudent.Location = new System.Drawing.Point(70, 12);
            this.tbSearchLessonStudent.Name = "tbSearchLessonStudent";
            this.tbSearchLessonStudent.Size = new System.Drawing.Size(121, 20);
            this.tbSearchLessonStudent.TabIndex = 1;
            this.tbSearchLessonStudent.TextChanged += new System.EventHandler(this.tbSearchLessonStudent_TextChanged);
            // 
            // lbSearchLessonDay
            // 
            this.lbSearchLessonDay.AutoSize = true;
            this.lbSearchLessonDay.Location = new System.Drawing.Point(15, 189);
            this.lbSearchLessonDay.Name = "lbSearchLessonDay";
            this.lbSearchLessonDay.Size = new System.Drawing.Size(33, 13);
            this.lbSearchLessonDay.TabIndex = 0;
            this.lbSearchLessonDay.Text = "Date:";
            // 
            // panelSearchLabels
            // 
            this.panelSearchLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchLabels.Controls.Add(this.labelGlobSearch);
            this.panelSearchLabels.Location = new System.Drawing.Point(22, 3);
            this.panelSearchLabels.Name = "panelSearchLabels";
            this.panelSearchLabels.Size = new System.Drawing.Size(200, 30);
            this.panelSearchLabels.TabIndex = 50;
            // 
            // labelGlobSearch
            // 
            this.labelGlobSearch.AutoSize = true;
            this.labelGlobSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(210)))), ((int)(((byte)(47)))));
            this.labelGlobSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlobSearch.Location = new System.Drawing.Point(68, 9);
            this.labelGlobSearch.Name = "labelGlobSearch";
            this.labelGlobSearch.Size = new System.Drawing.Size(47, 15);
            this.labelGlobSearch.TabIndex = 17;
            this.labelGlobSearch.Text = "Select";
            // 
            // panelGlobLogo
            // 
            this.panelGlobLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGlobLogo.Controls.Add(this.cbGlobMode);
            this.panelGlobLogo.Controls.Add(this.pictureBoxGlobIcon);
            this.panelGlobLogo.Controls.Add(this.labelGlobSagalingua);
            this.panelGlobLogo.Controls.Add(this.labelGlobCount);
            this.panelGlobLogo.Location = new System.Drawing.Point(59, 3);
            this.panelGlobLogo.Name = "panelGlobLogo";
            this.panelGlobLogo.Size = new System.Drawing.Size(183, 325);
            this.panelGlobLogo.TabIndex = 24;
            // 
            // cbGlobMode
            // 
            this.cbGlobMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbGlobMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(119)))), ((int)(((byte)(237)))));
            this.cbGlobMode.FormattingEnabled = true;
            this.cbGlobMode.Location = new System.Drawing.Point(3, 28);
            this.cbGlobMode.Name = "cbGlobMode";
            this.cbGlobMode.Size = new System.Drawing.Size(145, 33);
            this.cbGlobMode.TabIndex = 21;
            this.cbGlobMode.SelectedIndexChanged += new System.EventHandler(this.cbGlobType_SelectedIndexChanged);
            // 
            // pictureBoxGlobIcon
            // 
            this.pictureBoxGlobIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxGlobIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxGlobIcon.BackgroundImage")));
            this.pictureBoxGlobIcon.Location = new System.Drawing.Point(13, 123);
            this.pictureBoxGlobIcon.Name = "pictureBoxGlobIcon";
            this.pictureBoxGlobIcon.Size = new System.Drawing.Size(151, 199);
            this.pictureBoxGlobIcon.TabIndex = 7;
            this.pictureBoxGlobIcon.TabStop = false;
            // 
            // labelGlobSagalingua
            // 
            this.labelGlobSagalingua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGlobSagalingua.AutoSize = true;
            this.labelGlobSagalingua.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlobSagalingua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(119)))), ((int)(((byte)(237)))));
            this.labelGlobSagalingua.Location = new System.Drawing.Point(-2, 0);
            this.labelGlobSagalingua.Name = "labelGlobSagalingua";
            this.labelGlobSagalingua.Size = new System.Drawing.Size(137, 25);
            this.labelGlobSagalingua.TabIndex = 8;
            this.labelGlobSagalingua.Text = "Sagalingua ";
            // 
            // labelGlobCount
            // 
            this.labelGlobCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGlobCount.AutoSize = true;
            this.labelGlobCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGlobCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(119)))), ((int)(((byte)(237)))));
            this.labelGlobCount.Location = new System.Drawing.Point(59, 78);
            this.labelGlobCount.MinimumSize = new System.Drawing.Size(50, 20);
            this.labelGlobCount.Name = "labelGlobCount";
            this.labelGlobCount.Size = new System.Drawing.Size(50, 24);
            this.labelGlobCount.TabIndex = 20;
            // 
            // tabSchedPlan
            // 
            this.tabSchedPlan.Controls.Add(this.panelSchedNewMatrix);
            this.tabSchedPlan.Controls.Add(this.panelSchedNewParams);
            this.tabSchedPlan.Location = new System.Drawing.Point(4, 22);
            this.tabSchedPlan.Name = "tabSchedPlan";
            this.tabSchedPlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchedPlan.Size = new System.Drawing.Size(1283, 781);
            this.tabSchedPlan.TabIndex = 1;
            this.tabSchedPlan.Text = "SchedPlan";
            this.tabSchedPlan.UseVisualStyleBackColor = true;
            // 
            // panelSchedNewMatrix
            // 
            this.panelSchedNewMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSchedNewMatrix.AutoScroll = true;
            this.panelSchedNewMatrix.Controls.Add(this.dgvSchedNew);
            this.panelSchedNewMatrix.Location = new System.Drawing.Point(3, 241);
            this.panelSchedNewMatrix.Name = "panelSchedNewMatrix";
            this.panelSchedNewMatrix.Size = new System.Drawing.Size(1269, 344);
            this.panelSchedNewMatrix.TabIndex = 18;
            // 
            // dgvSchedNew
            // 
            this.dgvSchedNew.AllowUserToAddRows = false;
            this.dgvSchedNew.AllowUserToDeleteRows = false;
            this.dgvSchedNew.AutoGenerateColumns = false;
            this.dgvSchedNew.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedNew.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slot,
            this.mondayDataGridViewTextBoxColumn,
            this.tuesdayDataGridViewTextBoxColumn,
            this.wednesdayDataGridViewTextBoxColumn,
            this.thursdayDataGridViewTextBoxColumn,
            this.fridayDataGridViewTextBoxColumn,
            this.saturdayDataGridViewTextBoxColumn,
            this.sundayDataGridViewTextBoxColumn});
            this.dgvSchedNew.DataSource = this.schedNewSlotList;
            this.dgvSchedNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchedNew.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSchedNew.Location = new System.Drawing.Point(0, 0);
            this.dgvSchedNew.MultiSelect = false;
            this.dgvSchedNew.Name = "dgvSchedNew";
            this.dgvSchedNew.ReadOnly = true;
            this.dgvSchedNew.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvSchedNew.Size = new System.Drawing.Size(1269, 344);
            this.dgvSchedNew.TabIndex = 0;
            this.dgvSchedNew.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedNew_CellClick);
            // 
            // Slot
            // 
            this.Slot.DataPropertyName = "SlotName";
            this.Slot.HeaderText = "Time";
            this.Slot.Name = "Slot";
            this.Slot.ReadOnly = true;
            // 
            // mondayDataGridViewTextBoxColumn
            // 
            this.mondayDataGridViewTextBoxColumn.DataPropertyName = "Monday";
            this.mondayDataGridViewTextBoxColumn.HeaderText = "Monday";
            this.mondayDataGridViewTextBoxColumn.Name = "mondayDataGridViewTextBoxColumn";
            this.mondayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tuesdayDataGridViewTextBoxColumn
            // 
            this.tuesdayDataGridViewTextBoxColumn.DataPropertyName = "Tuesday";
            this.tuesdayDataGridViewTextBoxColumn.HeaderText = "Tuesday";
            this.tuesdayDataGridViewTextBoxColumn.Name = "tuesdayDataGridViewTextBoxColumn";
            this.tuesdayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // wednesdayDataGridViewTextBoxColumn
            // 
            this.wednesdayDataGridViewTextBoxColumn.DataPropertyName = "Wednesday";
            this.wednesdayDataGridViewTextBoxColumn.HeaderText = "Wednesday";
            this.wednesdayDataGridViewTextBoxColumn.Name = "wednesdayDataGridViewTextBoxColumn";
            this.wednesdayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // thursdayDataGridViewTextBoxColumn
            // 
            this.thursdayDataGridViewTextBoxColumn.DataPropertyName = "Thursday";
            this.thursdayDataGridViewTextBoxColumn.HeaderText = "Thursday";
            this.thursdayDataGridViewTextBoxColumn.Name = "thursdayDataGridViewTextBoxColumn";
            this.thursdayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fridayDataGridViewTextBoxColumn
            // 
            this.fridayDataGridViewTextBoxColumn.DataPropertyName = "Friday";
            this.fridayDataGridViewTextBoxColumn.HeaderText = "Friday";
            this.fridayDataGridViewTextBoxColumn.Name = "fridayDataGridViewTextBoxColumn";
            this.fridayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // saturdayDataGridViewTextBoxColumn
            // 
            this.saturdayDataGridViewTextBoxColumn.DataPropertyName = "Saturday";
            this.saturdayDataGridViewTextBoxColumn.HeaderText = "Saturday";
            this.saturdayDataGridViewTextBoxColumn.Name = "saturdayDataGridViewTextBoxColumn";
            this.saturdayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sundayDataGridViewTextBoxColumn
            // 
            this.sundayDataGridViewTextBoxColumn.DataPropertyName = "Sunday";
            this.sundayDataGridViewTextBoxColumn.HeaderText = "Sunday";
            this.sundayDataGridViewTextBoxColumn.Name = "sundayDataGridViewTextBoxColumn";
            this.sundayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // schedNewSlotList
            // 
            this.schedNewSlotList.DataSource = typeof(RecordKeeper.Slot);
            // 
            // panelSchedNewParams
            // 
            this.panelSchedNewParams.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSchedNewParams.Controls.Add(this.tbSchedNewComment);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewComment);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewTeachVacation);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewStudSchedule4);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewStudSchedule3);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewStudSchedule2);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewStudSchedule1);
            this.panelSchedNewParams.Controls.Add(this.butSchedNewAccept);
            this.panelSchedNewParams.Controls.Add(this.cbSchedNewDuration);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewDuration);
            this.panelSchedNewParams.Controls.Add(this.dtpSchedNew);
            this.panelSchedNewParams.Controls.Add(this.cbSchedNewTeacher);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewTeacher);
            this.panelSchedNewParams.Controls.Add(this.lbSchedPlanName);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewWeek);
            this.panelSchedNewParams.Controls.Add(this.cbSchedNewLanguage);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewLanguage);
            this.panelSchedNewParams.Controls.Add(this.cbSchedNewStud4);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewStud1);
            this.panelSchedNewParams.Controls.Add(this.cbSchedNewStud3);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewStud2);
            this.panelSchedNewParams.Controls.Add(this.cbSchedNewStud2);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewStud3);
            this.panelSchedNewParams.Controls.Add(this.cbSchedNewStud1);
            this.panelSchedNewParams.Controls.Add(this.lbSchedNewStud4);
            this.panelSchedNewParams.Location = new System.Drawing.Point(3, 6);
            this.panelSchedNewParams.Name = "panelSchedNewParams";
            this.panelSchedNewParams.Size = new System.Drawing.Size(1269, 229);
            this.panelSchedNewParams.TabIndex = 17;
            // 
            // tbSchedNewComment
            // 
            this.tbSchedNewComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSchedNewComment.Location = new System.Drawing.Point(352, 177);
            this.tbSchedNewComment.Name = "tbSchedNewComment";
            this.tbSchedNewComment.Size = new System.Drawing.Size(901, 20);
            this.tbSchedNewComment.TabIndex = 29;
            // 
            // lbSchedNewComment
            // 
            this.lbSchedNewComment.AutoSize = true;
            this.lbSchedNewComment.Location = new System.Drawing.Point(276, 177);
            this.lbSchedNewComment.Name = "lbSchedNewComment";
            this.lbSchedNewComment.Size = new System.Drawing.Size(54, 13);
            this.lbSchedNewComment.TabIndex = 28;
            this.lbSchedNewComment.Text = "Comment:";
            // 
            // lbSchedNewTeachVacation
            // 
            this.lbSchedNewTeachVacation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSchedNewTeachVacation.AutoSize = true;
            this.lbSchedNewTeachVacation.Location = new System.Drawing.Point(498, 11);
            this.lbSchedNewTeachVacation.Name = "lbSchedNewTeachVacation";
            this.lbSchedNewTeachVacation.Size = new System.Drawing.Size(0, 13);
            this.lbSchedNewTeachVacation.TabIndex = 27;
            // 
            // lbSchedNewStudSchedule4
            // 
            this.lbSchedNewStudSchedule4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSchedNewStudSchedule4.AutoSize = true;
            this.lbSchedNewStudSchedule4.Location = new System.Drawing.Point(498, 148);
            this.lbSchedNewStudSchedule4.Name = "lbSchedNewStudSchedule4";
            this.lbSchedNewStudSchedule4.Size = new System.Drawing.Size(0, 13);
            this.lbSchedNewStudSchedule4.TabIndex = 26;
            // 
            // lbSchedNewStudSchedule3
            // 
            this.lbSchedNewStudSchedule3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSchedNewStudSchedule3.AutoSize = true;
            this.lbSchedNewStudSchedule3.Location = new System.Drawing.Point(498, 112);
            this.lbSchedNewStudSchedule3.Name = "lbSchedNewStudSchedule3";
            this.lbSchedNewStudSchedule3.Size = new System.Drawing.Size(0, 13);
            this.lbSchedNewStudSchedule3.TabIndex = 25;
            // 
            // lbSchedNewStudSchedule2
            // 
            this.lbSchedNewStudSchedule2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSchedNewStudSchedule2.AutoSize = true;
            this.lbSchedNewStudSchedule2.Location = new System.Drawing.Point(498, 79);
            this.lbSchedNewStudSchedule2.Name = "lbSchedNewStudSchedule2";
            this.lbSchedNewStudSchedule2.Size = new System.Drawing.Size(0, 13);
            this.lbSchedNewStudSchedule2.TabIndex = 24;
            // 
            // lbSchedNewStudSchedule1
            // 
            this.lbSchedNewStudSchedule1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSchedNewStudSchedule1.AutoSize = true;
            this.lbSchedNewStudSchedule1.Location = new System.Drawing.Point(498, 43);
            this.lbSchedNewStudSchedule1.Name = "lbSchedNewStudSchedule1";
            this.lbSchedNewStudSchedule1.Size = new System.Drawing.Size(0, 13);
            this.lbSchedNewStudSchedule1.TabIndex = 23;
            // 
            // butSchedNewAccept
            // 
            this.butSchedNewAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(39)))));
            this.butSchedNewAccept.Cursor = System.Windows.Forms.Cursors.Default;
            this.butSchedNewAccept.Location = new System.Drawing.Point(352, 203);
            this.butSchedNewAccept.Name = "butSchedNewAccept";
            this.butSchedNewAccept.Size = new System.Drawing.Size(75, 23);
            this.butSchedNewAccept.TabIndex = 22;
            this.butSchedNewAccept.Text = "Accept";
            this.butSchedNewAccept.UseVisualStyleBackColor = false;
            this.butSchedNewAccept.Visible = false;
            this.butSchedNewAccept.Click += new System.EventHandler(this.butSchedNewAccept_Click);
            // 
            // cbSchedNewDuration
            // 
            this.cbSchedNewDuration.FormattingEnabled = true;
            this.cbSchedNewDuration.Location = new System.Drawing.Point(126, 112);
            this.cbSchedNewDuration.Name = "cbSchedNewDuration";
            this.cbSchedNewDuration.Size = new System.Drawing.Size(121, 21);
            this.cbSchedNewDuration.TabIndex = 20;
            // 
            // lbSchedNewDuration
            // 
            this.lbSchedNewDuration.AutoSize = true;
            this.lbSchedNewDuration.Location = new System.Drawing.Point(57, 112);
            this.lbSchedNewDuration.Name = "lbSchedNewDuration";
            this.lbSchedNewDuration.Size = new System.Drawing.Size(50, 13);
            this.lbSchedNewDuration.TabIndex = 19;
            this.lbSchedNewDuration.Text = "Duration:";
            // 
            // dtpSchedNew
            // 
            this.dtpSchedNew.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSchedNew.Location = new System.Drawing.Point(126, 42);
            this.dtpSchedNew.Name = "dtpSchedNew";
            this.dtpSchedNew.Size = new System.Drawing.Size(121, 20);
            this.dtpSchedNew.TabIndex = 0;
            this.dtpSchedNew.ValueChanged += new System.EventHandler(this.dtpSchedNew_ValueChanged);
            // 
            // cbSchedNewTeacher
            // 
            this.cbSchedNewTeacher.FormattingEnabled = true;
            this.cbSchedNewTeacher.Location = new System.Drawing.Point(352, 8);
            this.cbSchedNewTeacher.Name = "cbSchedNewTeacher";
            this.cbSchedNewTeacher.Size = new System.Drawing.Size(121, 21);
            this.cbSchedNewTeacher.TabIndex = 18;
            this.cbSchedNewTeacher.SelectedIndexChanged += new System.EventHandler(this.cbSchedNewTeacher_SelectedIndexChanged);
            // 
            // lbSchedNewTeacher
            // 
            this.lbSchedNewTeacher.AutoSize = true;
            this.lbSchedNewTeacher.Location = new System.Drawing.Point(276, 11);
            this.lbSchedNewTeacher.Name = "lbSchedNewTeacher";
            this.lbSchedNewTeacher.Size = new System.Drawing.Size(50, 13);
            this.lbSchedNewTeacher.TabIndex = 17;
            this.lbSchedNewTeacher.Text = "Teacher:";
            // 
            // lbSchedPlanName
            // 
            this.lbSchedPlanName.AutoSize = true;
            this.lbSchedPlanName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSchedPlanName.Location = new System.Drawing.Point(8, 11);
            this.lbSchedPlanName.Name = "lbSchedPlanName";
            this.lbSchedPlanName.Size = this.lbSchedPlanName.Size;
            this.lbSchedPlanName.TabIndex = 0;
            this.lbSchedPlanName.Text = "Planning New Lesson";
            // 
            // lbSchedNewWeek
            // 
            this.lbSchedNewWeek.AutoSize = true;
            this.lbSchedNewWeek.Location = new System.Drawing.Point(57, 48);
            this.lbSchedNewWeek.Name = "lbSchedNewWeek";
            this.lbSchedNewWeek.Size = new System.Drawing.Size(39, 13);
            this.lbSchedNewWeek.TabIndex = 1;
            this.lbSchedNewWeek.Text = "Week:";
            // 
            // cbSchedNewLanguage
            // 
            this.cbSchedNewLanguage.FormattingEnabled = true;
            this.cbSchedNewLanguage.Location = new System.Drawing.Point(126, 76);
            this.cbSchedNewLanguage.Name = "cbSchedNewLanguage";
            this.cbSchedNewLanguage.Size = new System.Drawing.Size(121, 21);
            this.cbSchedNewLanguage.TabIndex = 3;
            this.cbSchedNewLanguage.SelectedIndexChanged += new System.EventHandler(this.cbSchedNewLanguage_SelectedIndexChanged);
            // 
            // lbSchedNewLanguage
            // 
            this.lbSchedNewLanguage.AutoSize = true;
            this.lbSchedNewLanguage.Location = new System.Drawing.Point(57, 76);
            this.lbSchedNewLanguage.Name = "lbSchedNewLanguage";
            this.lbSchedNewLanguage.Size = new System.Drawing.Size(58, 13);
            this.lbSchedNewLanguage.TabIndex = 4;
            this.lbSchedNewLanguage.Text = "Language:";
            // 
            // cbSchedNewStud4
            // 
            this.cbSchedNewStud4.FormattingEnabled = true;
            this.cbSchedNewStud4.Location = new System.Drawing.Point(352, 145);
            this.cbSchedNewStud4.Name = "cbSchedNewStud4";
            this.cbSchedNewStud4.Size = new System.Drawing.Size(121, 21);
            this.cbSchedNewStud4.TabIndex = 12;
            this.cbSchedNewStud4.SelectedIndexChanged += new System.EventHandler(this.cbSchedNewStud4_SelectedIndexChanged);
            // 
            // lbSchedNewStud1
            // 
            this.lbSchedNewStud1.AutoSize = true;
            this.lbSchedNewStud1.Location = new System.Drawing.Point(276, 47);
            this.lbSchedNewStud1.Name = "lbSchedNewStud1";
            this.lbSchedNewStud1.Size = new System.Drawing.Size(47, 13);
            this.lbSchedNewStud1.TabIndex = 5;
            this.lbSchedNewStud1.Text = "Student:";
            // 
            // cbSchedNewStud3
            // 
            this.cbSchedNewStud3.FormattingEnabled = true;
            this.cbSchedNewStud3.Location = new System.Drawing.Point(352, 109);
            this.cbSchedNewStud3.Name = "cbSchedNewStud3";
            this.cbSchedNewStud3.Size = new System.Drawing.Size(121, 21);
            this.cbSchedNewStud3.TabIndex = 11;
            this.cbSchedNewStud3.SelectedIndexChanged += new System.EventHandler(this.cbSchedNewStud3_SelectedIndexChanged);
            // 
            // lbSchedNewStud2
            // 
            this.lbSchedNewStud2.AutoSize = true;
            this.lbSchedNewStud2.Location = new System.Drawing.Point(276, 76);
            this.lbSchedNewStud2.Name = "lbSchedNewStud2";
            this.lbSchedNewStud2.Size = new System.Drawing.Size(47, 13);
            this.lbSchedNewStud2.TabIndex = 6;
            this.lbSchedNewStud2.Text = "Student:";
            // 
            // cbSchedNewStud2
            // 
            this.cbSchedNewStud2.FormattingEnabled = true;
            this.cbSchedNewStud2.Location = new System.Drawing.Point(352, 73);
            this.cbSchedNewStud2.Name = "cbSchedNewStud2";
            this.cbSchedNewStud2.Size = new System.Drawing.Size(121, 21);
            this.cbSchedNewStud2.TabIndex = 10;
            this.cbSchedNewStud2.SelectedIndexChanged += new System.EventHandler(this.cbSchedNewStud2_SelectedIndexChanged);
            // 
            // lbSchedNewStud3
            // 
            this.lbSchedNewStud3.AutoSize = true;
            this.lbSchedNewStud3.Location = new System.Drawing.Point(276, 112);
            this.lbSchedNewStud3.Name = "lbSchedNewStud3";
            this.lbSchedNewStud3.Size = new System.Drawing.Size(47, 13);
            this.lbSchedNewStud3.TabIndex = 7;
            this.lbSchedNewStud3.Text = "Student:";
            // 
            // cbSchedNewStud1
            // 
            this.cbSchedNewStud1.FormattingEnabled = true;
            this.cbSchedNewStud1.Location = new System.Drawing.Point(352, 40);
            this.cbSchedNewStud1.Name = "cbSchedNewStud1";
            this.cbSchedNewStud1.Size = new System.Drawing.Size(121, 21);
            this.cbSchedNewStud1.TabIndex = 9;
            this.cbSchedNewStud1.SelectedIndexChanged += new System.EventHandler(this.cbSchedNewStud1_SelectedIndexChanged);
            // 
            // lbSchedNewStud4
            // 
            this.lbSchedNewStud4.AutoSize = true;
            this.lbSchedNewStud4.Location = new System.Drawing.Point(276, 145);
            this.lbSchedNewStud4.Name = "lbSchedNewStud4";
            this.lbSchedNewStud4.Size = new System.Drawing.Size(44, 13);
            this.lbSchedNewStud4.TabIndex = 8;
            this.lbSchedNewStud4.Text = "Student";
            // 
            // tabSchedShow
            // 
            this.tabSchedShow.Controls.Add(this.lbSchedShowName);
            this.tabSchedShow.Location = new System.Drawing.Point(4, 22);
            this.tabSchedShow.Name = "tabSchedShow";
            this.tabSchedShow.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchedShow.Size = new System.Drawing.Size(1283, 781);
            this.tabSchedShow.TabIndex = 2;
            this.tabSchedShow.Text = "SchedShow";
            this.tabSchedShow.UseVisualStyleBackColor = true;
            // 
            // lbSchedShowName
            // 
            this.lbSchedShowName.AutoSize = true;
            this.lbSchedShowName.Font = this.lbSchedPlanName.Font;
            this.lbSchedShowName.Location = this.lbSchedPlanName.Location;
            this.lbSchedShowName.Name = "lbSchedShowName";
            this.lbSchedShowName.Size = new System.Drawing.Size(195, 25);
            this.lbSchedShowName.TabIndex = 0;
            this.lbSchedShowName.Text = "Current Schedule";
            // 
            // tabSchedCancel
            // 
            this.tabSchedCancel.Controls.Add(this.lbSchedCancelName);
            this.tabSchedCancel.Location = new System.Drawing.Point(4, 22);
            this.tabSchedCancel.Name = "tabSchedCancel";
            this.tabSchedCancel.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchedCancel.Size = new System.Drawing.Size(1283, 781);
            this.tabSchedCancel.TabIndex = 3;
            this.tabSchedCancel.Text = "SchedCancel";
            this.tabSchedCancel.UseVisualStyleBackColor = true;
            // 
            // lbSchedCancelName
            // 
            this.lbSchedCancelName.AutoSize = true;
            this.lbSchedCancelName.Font = this.lbSchedPlanName.Font;
            this.lbSchedCancelName.Location = this.lbSchedPlanName.Location;
            this.lbSchedCancelName.Name = "lbSchedCancelName";
            this.lbSchedCancelName.Size = this.lbSchedPlanName.Size;
            this.lbSchedCancelName.TabIndex = 0;
            this.lbSchedCancelName.Text = "Cancel a Lesson";
            // 
            // tabPayStud
            // 
            this.tabPayStud.Controls.Add(this.lbPayStudName);
            this.tabPayStud.Location = new System.Drawing.Point(4, 22);
            this.tabPayStud.Name = "tabPayStud";
            this.tabPayStud.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayStud.Size = new System.Drawing.Size(1283, 781);
            this.tabPayStud.TabIndex = 4;
            this.tabPayStud.Text = "PayStud";
            this.tabPayStud.UseVisualStyleBackColor = true;
            // 
            // lbPayStudName
            // 
            this.lbPayStudName.AutoSize = true;
            this.lbPayStudName.Font = this.lbSchedPlanName.Font;
            this.lbPayStudName.Location = this.lbSchedPlanName.Location;
            this.lbPayStudName.Name = "lbPayStudName";
            this.lbPayStudName.Size = this.lbSchedPlanName.Size;
            this.lbPayStudName.TabIndex = 0;
            this.lbPayStudName.Text = "Enter Student Payment";
            // 
            // tabPayTeach
            // 
            this.tabPayTeach.Controls.Add(this.lbPayTeachName);
            this.tabPayTeach.Location = new System.Drawing.Point(4, 22);
            this.tabPayTeach.Name = "tabPayTeach";
            this.tabPayTeach.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayTeach.Size = new System.Drawing.Size(1283, 781);
            this.tabPayTeach.TabIndex = 5;
            this.tabPayTeach.Text = "PayTeach";
            this.tabPayTeach.UseVisualStyleBackColor = true;
            // 
            // lbPayTeachName
            // 
            this.lbPayTeachName.AutoSize = true;
            this.lbPayTeachName.Font = this.lbSchedPlanName.Font;
            this.lbPayTeachName.Location = this.lbSchedPlanName.Location;
            this.lbPayTeachName.Name = "lbPayTeachName";
            this.lbPayTeachName.Size = this.lbSchedPlanName.Size;
            this.lbPayTeachName.TabIndex = 0;
            this.lbPayTeachName.Text = "Enter Teacher Payment";
            // 
            // tabPayExpense
            // 
            this.tabPayExpense.Controls.Add(this.lbPayExpenseName);
            this.tabPayExpense.Location = new System.Drawing.Point(4, 22);
            this.tabPayExpense.Name = "tabPayExpense";
            this.tabPayExpense.Padding = new System.Windows.Forms.Padding(3);
            this.tabPayExpense.Size = new System.Drawing.Size(1283, 781);
            this.tabPayExpense.TabIndex = 6;
            this.tabPayExpense.Text = "PayExpense";
            this.tabPayExpense.UseVisualStyleBackColor = true;
            // 
            // lbPayExpenseName
            // 
            this.lbPayExpenseName.AutoSize = true;
            this.lbPayExpenseName.Font = this.lbSchedPlanName.Font;
            this.lbPayExpenseName.Location = this.lbSchedPlanName.Location;
            this.lbPayExpenseName.Name = "lbPayExpenseName";
            this.lbPayExpenseName.Size = this.lbSchedPlanName.Size;
            this.lbPayExpenseName.TabIndex = 0;
            this.lbPayExpenseName.Text = "Report New Expense";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.lbFutureOpName);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(1283, 781);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "tabPage8";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // lbFutureOpName
            // 
            this.lbFutureOpName.AutoSize = true;
            this.lbFutureOpName.Font = this.lbSchedPlanName.Font;
            this.lbFutureOpName.Location = this.lbSchedPlanName.Location;
            this.lbFutureOpName.Name = "lbFutureOpName";
            this.lbFutureOpName.Size = this.lbSchedPlanName.Size;
            this.lbFutureOpName.TabIndex = 0;
            this.lbFutureOpName.Text = "Future Operation";
            // 
            // FormGlob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 831);
            this.Controls.Add(this.tabControlOps);
            this.Controls.Add(this.panelGlobIndicators);
            this.Controls.Add(this.menuStripGlobalOps);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripGlobalOps;
            this.Name = "FormGlob";
            this.Text = "RecordKeeper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStripGlobalOps.ResumeLayout(false);
            this.menuStripGlobalOps.PerformLayout();
            this.panelGlobIndicators.ResumeLayout(false);
            this.panelGlobIndicators.PerformLayout();
            this.tabControlOps.ResumeLayout(false);
            this.tabEdit.ResumeLayout(false);
            this.splitContainerGlobDataControls.Panel1.ResumeLayout(false);
            this.splitContainerGlobDataControls.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGlobDataControls)).EndInit();
            this.splitContainerGlobDataControls.ResumeLayout(false);
            this.splitContainerGlobMasterDetail.Panel1.ResumeLayout(false);
            this.splitContainerGlobMasterDetail.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGlobMasterDetail)).EndInit();
            this.splitContainerGlobMasterDetail.ResumeLayout(false);
            this.tabControlModesTop.ResumeLayout(false);
            this.tabTopPageStudents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentList)).EndInit();
            this.tabTopPageTeachers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeachers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teacherList)).EndInit();
            this.tabTopPagePrograms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrograms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.programList)).EndInit();
            this.tabTopPageRooms.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roomList)).EndInit();
            this.tabTopPageLessons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLesson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lessonList)).EndInit();
            this.panelGlobEdit.ResumeLayout(false);
            this.tabControlModesBottom.ResumeLayout(false);
            this.tabBottomPageStudents.ResumeLayout(false);
            this.panelStudent.ResumeLayout(false);
            this.panelStudPrimary.ResumeLayout(false);
            this.groupBoxStudPrinaryRight.ResumeLayout(false);
            this.groupBoxStudPrinaryRight.PerformLayout();
            this.panelStudPrimaryLeft.ResumeLayout(false);
            this.panelStudPrimaryLeft.PerformLayout();
            this.panelStudSecondary.ResumeLayout(false);
            this.groupBoxStudSecondaryRight.ResumeLayout(false);
            this.groupBoxStudSecondaryRight.PerformLayout();
            this.panelStudSecondaryLeft.ResumeLayout(false);
            this.panelStudSecondaryLeft.PerformLayout();
            this.tabBottomPageTeachers.ResumeLayout(false);
            this.panelTeacherSecondary.ResumeLayout(false);
            this.panelTeacherPrimary.ResumeLayout(false);
            this.groupBoxTeacherPrimaryRight.ResumeLayout(false);
            this.groupBoxTeacherPrimaryRight.PerformLayout();
            this.panelTeacherPrimaryLeft.ResumeLayout(false);
            this.panelTeacherPrimaryLeft.PerformLayout();
            this.tabBottomPagePrograms.ResumeLayout(false);
            this.panelProgram.ResumeLayout(false);
            this.groupBoxProgram.ResumeLayout(false);
            this.groupBoxProgram.PerformLayout();
            this.panelProgramPrimaryLeft.ResumeLayout(false);
            this.panelProgramPrimaryLeft.PerformLayout();
            this.tabBottomPageRooms.ResumeLayout(false);
            this.panelRoom.ResumeLayout(false);
            this.panelRoomPrimaryLeft.ResumeLayout(false);
            this.panelRoomPrimaryLeft.PerformLayout();
            this.groupBoxRoom.ResumeLayout(false);
            this.groupBoxRoom.PerformLayout();
            this.tabBottomPageLessons.ResumeLayout(false);
            this.panelLesson.ResumeLayout(false);
            this.panelLesson.PerformLayout();
            this.panelGlobPrevDelete.ResumeLayout(false);
            this.panelGlobNextNew.ResumeLayout(false);
            this.panelGlobSearch.ResumeLayout(false);
            this.panelSearchButtons.ResumeLayout(false);
            this.tabControlSearch.ResumeLayout(false);
            this.tabPageSearchStudent.ResumeLayout(false);
            this.panelStudSearch.ResumeLayout(false);
            this.panelStudSearch.PerformLayout();
            this.tabPageSearchTeacher.ResumeLayout(false);
            this.panelSearchTeacher.ResumeLayout(false);
            this.panelSearchTeacher.PerformLayout();
            this.tabPageSearchProgram.ResumeLayout(false);
            this.panelSearchProgram.ResumeLayout(false);
            this.panelSearchProgram.PerformLayout();
            this.tabPageSearchRoom.ResumeLayout(false);
            this.tabPageSearchLesson.ResumeLayout(false);
            this.panelSearchLesson.ResumeLayout(false);
            this.panelSearchLesson.PerformLayout();
            this.panelSearchLabels.ResumeLayout(false);
            this.panelSearchLabels.PerformLayout();
            this.panelGlobLogo.ResumeLayout(false);
            this.panelGlobLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGlobIcon)).EndInit();
            this.tabSchedPlan.ResumeLayout(false);
            this.panelSchedNewMatrix.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedNewSlotList)).EndInit();
            this.panelSchedNewParams.ResumeLayout(false);
            this.panelSchedNewParams.PerformLayout();
            this.tabSchedShow.ResumeLayout(false);
            this.tabSchedShow.PerformLayout();
            this.tabSchedCancel.ResumeLayout(false);
            this.tabSchedCancel.PerformLayout();
            this.tabPayStud.ResumeLayout(false);
            this.tabPayStud.PerformLayout();
            this.tabPayTeach.ResumeLayout(false);
            this.tabPayTeach.PerformLayout();
            this.tabPayExpense.ResumeLayout(false);
            this.tabPayExpense.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripGlobalOps;
        private System.Windows.Forms.ToolStripMenuItem menuItemGlobSave;
        private System.Windows.Forms.Label lbStudSearchStatus;
        private System.Windows.Forms.ComboBox cbStudSelectStatus;
        private System.Windows.Forms.ComboBox cbStudSearchSpeaks;
        private System.Windows.Forms.Label lbStudSearchSpeaks;
        private System.Windows.Forms.ComboBox cbStudSearchLearns;
        private System.Windows.Forms.Label lbStudSearchLearns;
        private System.Windows.Forms.ComboBox cbStudSearchSource;
        private System.Windows.Forms.Label lbStudSearchSource;
        private System.Windows.Forms.TextBox tbStudSearchLastName;
        private System.Windows.Forms.Label lbStudSearchLastName;
        private System.Windows.Forms.TextBox tbStudSearchFirstName;
        private System.Windows.Forms.Label lbStudSearchFirstName1;
        private System.Windows.Forms.PictureBox pictureBoxGlobIcon;
        private System.Windows.Forms.Label labelGlobSagalingua;
        private System.Windows.Forms.Panel panelGlobSearch;
        private System.Windows.Forms.ToolStripMenuItem menuItemGlobExit;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.BindingSource studentList;
        private System.Windows.Forms.Button butGlobalPrev;
        private System.Windows.Forms.Button butGlobalAdd;
        private System.Windows.Forms.Button butGlobalDelete;
        private System.Windows.Forms.Button butGlobalNext;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnLearningLanguage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnNativeLanguage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnOtherLanguage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnBirthday;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStudColumnAddress;
        private System.Windows.Forms.Button buttGlobalShowAll;
        private System.Windows.Forms.ComboBox cbStudSearchLevel;
        private System.Windows.Forms.Label lbStudSearchLevel;
        private System.Windows.Forms.Label labelGlobSearch;
        private System.Windows.Forms.Label labelGlobCount;
        private System.Windows.Forms.ToolStripMenuItem menuItemGlobDownload;
        private System.Windows.Forms.ToolStripMenuItem menuItemGlobUpload;
        private System.Windows.Forms.Label labelGlobLastDownload;
        private System.Windows.Forms.Label labelGlobLastUpload;
        private System.Windows.Forms.Panel panelStudent;
        private System.Windows.Forms.TextBox tbStudComments;
        private System.Windows.Forms.TextBox tbStudLastName;
        private System.Windows.Forms.TextBox tbStudSchedule;
        private System.Windows.Forms.Label labelStudFirstname;
        private System.Windows.Forms.TextBox tbStudInterests;
        private System.Windows.Forms.TextBox tbStudFirstName;
        private System.Windows.Forms.TextBox tbStudGoals;
        private System.Windows.Forms.Label labelStudLastName1;
        private System.Windows.Forms.TextBox tbStudBackground;
        private System.Windows.Forms.Label labelStudEmail1;
        private System.Windows.Forms.Label labelStudComments;
        private System.Windows.Forms.TextBox tbStudEmail;
        private System.Windows.Forms.Label labelStudSchedule;
        private System.Windows.Forms.Label labelStudCellPhone;
        private System.Windows.Forms.Label labelStudInterests;
        private System.Windows.Forms.Label labelStudHomePhone;
        private System.Windows.Forms.Label labelStudGoals;
        private System.Windows.Forms.TextBox tbStudCellPhone;
        private System.Windows.Forms.Label labelStudBackground;
        private System.Windows.Forms.TextBox tbStudHomePhone;
        private System.Windows.Forms.TextBox tbStudSourceDetail;
        private System.Windows.Forms.Label labelStudAddress1;
        private System.Windows.Forms.ComboBox cbStudSource;
        private System.Windows.Forms.Label labelStudSpeaks2;
        private System.Windows.Forms.Label labelStudSource1;
        private System.Windows.Forms.TextBox tbStudAddress1;
        private System.Windows.Forms.ComboBox cbStudLevel;
        private System.Windows.Forms.Label labelStudLevel1;
        private System.Windows.Forms.Label labelStudSpeaks1;
        private System.Windows.Forms.TextBox tbStudBirthday;
        private System.Windows.Forms.ComboBox cbStudLearns;
        private System.Windows.Forms.ComboBox cbStudStatus;
        private System.Windows.Forms.ComboBox cbStudSpeaks;
        private System.Windows.Forms.Label labelStudStatus1;
        private System.Windows.Forms.Label labelStudAlso;
        private System.Windows.Forms.TextBox tbStudLanguageDetail;
        private System.Windows.Forms.Label labelStudBirthday;
        private System.Windows.Forms.ComboBox cbStudOther;
        private System.Windows.Forms.Panel panelGlobEdit;
        private System.Windows.Forms.Panel panelGlobLogo;
        private System.Windows.Forms.SplitContainer splitContainerGlobMasterDetail;
        private System.Windows.Forms.SplitContainer splitContainerGlobDataControls;
        private System.Windows.Forms.Panel panelGlobIndicators;
        private System.Windows.Forms.Panel panelGlobNextNew;
        private System.Windows.Forms.Button butGlobalToExcel;
        private System.Windows.Forms.Panel panelStudSecondary;
        private System.Windows.Forms.Panel panelStudSecondaryLeft;
        private System.Windows.Forms.GroupBox groupBoxStudSecondaryRight;
        private System.Windows.Forms.GroupBox groupBoxStudPrinaryRight;
        private System.Windows.Forms.Label labelStudDetailsSource;
        private System.Windows.Forms.Label labelStudDetailsLanguage;
        private System.Windows.Forms.Panel panelStudPrimaryLeft;
        private System.Windows.Forms.Panel panelStudPrimary;
        private System.Windows.Forms.ComboBox cbGlobMode;
        private System.Windows.Forms.Panel panelStudSearch;
        private HiddenTabControl tabControlModesBottom;
        private System.Windows.Forms.TabPage tabBottomPageStudents;
        private System.Windows.Forms.TabPage tabBottomPageTeachers;
        private System.Windows.Forms.TabPage tabBottomPagePrograms;
        private System.Windows.Forms.Panel panelGlobPrevDelete;
        private System.Windows.Forms.TabPage tabBottomPageRooms;
        private System.Windows.Forms.TabPage tabBottomPageLessons;
        private HiddenTabControl tabControlModesTop;
        private System.Windows.Forms.TabPage tabTopPageStudents;
        private System.Windows.Forms.TabPage tabTopPageTeachers;
        private System.Windows.Forms.TabPage tabTopPagePrograms;
        private System.Windows.Forms.TabPage tabTopPageRooms;
        private System.Windows.Forms.TabPage tabTopPageLessons;
        private System.Windows.Forms.Panel panelRoom;
        private System.Windows.Forms.Panel panelRoomPrimaryLeft;
        private System.Windows.Forms.GroupBox groupBoxRoom;
        private System.Windows.Forms.TextBox tbRoomComments;
        private System.Windows.Forms.TextBox tbRoomTags;
        private System.Windows.Forms.Label labelRoomRank;
        private System.Windows.Forms.Label labelRoomCapacity;
        private System.Windows.Forms.Label labelRoomName;
        private System.Windows.Forms.TextBox tbRoomPreferrability;
        private System.Windows.Forms.TextBox tbRoomCapacity;
        private System.Windows.Forms.TextBox tbRoomName;
        private System.Windows.Forms.Label labelRoomComments;
        private System.Windows.Forms.Label labelRoomTags;
        private System.Windows.Forms.DataGridView dgvRooms;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rankDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentsDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource roomList;
        private System.Windows.Forms.DataGridView dgvTeachers;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn language2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDetailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vacationsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentsDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn mailingAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthdayDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource teacherList;
        private System.Windows.Forms.Panel panelTeacherSecondary;
        private System.Windows.Forms.Panel panelTeacherPrimary;
        private System.Windows.Forms.GroupBox groupBoxTeacherPrimaryRight;
        private System.Windows.Forms.Panel panelTeacherPrimaryLeft;
        private System.Windows.Forms.TextBox tbTeachLanguageDetail;
        private System.Windows.Forms.ComboBox cbTeachLanguage2;
        private System.Windows.Forms.ComboBox cbTeachLanguage;
        private System.Windows.Forms.Label labelTeachLabguageDetail;
        private System.Windows.Forms.Label labelTeachLanguage2;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.ComboBox cbTeachStatus;
        private System.Windows.Forms.TextBox tbTeachLastBirthday;
        private System.Windows.Forms.TextBox tbTeachPhone;
        private System.Windows.Forms.Label labelTeachBirthday;
        private System.Windows.Forms.Label labelTeachPhone;
        private System.Windows.Forms.Label labelTeachStatus;
        private System.Windows.Forms.TextBox tbTeachEmail;
        private System.Windows.Forms.TextBox tbTeachLastName;
        private System.Windows.Forms.TextBox tbTeachFirstName;
        private System.Windows.Forms.Label labelTeachEmail;
        private System.Windows.Forms.Label labelTeachLastName;
        private System.Windows.Forms.Label labelTeachFirstName;
        private System.Windows.Forms.TextBox tbTeachComment;
        private System.Windows.Forms.TextBox tbTeachVacations;
        private System.Windows.Forms.TextBox tbTeachAddress;
        private System.Windows.Forms.Label labelTeachComment;
        private System.Windows.Forms.Label labelTeachVacations;
        private System.Windows.Forms.Label labelTeachAddress;
        private System.Windows.Forms.DataGridView dgvPrograms;
        private System.Windows.Forms.BindingSource programList;
        private System.Windows.Forms.Panel panelProgram;
        private System.Windows.Forms.GroupBox groupBoxProgram;
        private System.Windows.Forms.Panel panelProgramPrimaryLeft;
        private System.Windows.Forms.TextBox tbProgComments;
        private System.Windows.Forms.TextBox tbProgSummary;
        private System.Windows.Forms.Label labelProgComments;
        private System.Windows.Forms.Label labelProgSummary;
        private System.Windows.Forms.TextBox tbProgProce;
        private System.Windows.Forms.Label labelProgPrice;
        private System.Windows.Forms.ComboBox cbProgLevel;
        private System.Windows.Forms.Label labelProgLevel;
        private System.Windows.Forms.ComboBox cbProgLanguage;
        private System.Windows.Forms.Label labelProgLanguage;
        private System.Windows.Forms.TextBox tbProgName;
        private System.Windows.Forms.Label labelProgName;
        private System.Windows.Forms.TextBox tbProgCode;
        private System.Windows.Forms.Label labelProgCode;
        private System.Windows.Forms.Label labelProgExplanation;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Summary;
        private System.Windows.Forms.DataGridView dgvLesson;
        private System.Windows.Forms.Panel panelLesson;
        private System.Windows.Forms.Label labelLessonProgram;
        private System.Windows.Forms.ComboBox cbLessonState;
        private System.Windows.Forms.Label labelLessonState;
        private System.Windows.Forms.ComboBox cbLessonEnd;
        private System.Windows.Forms.ComboBox cbLessonStart;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ComboBox cbLessonRoom;
        private System.Windows.Forms.Label labelLessonRoom;
        private System.Windows.Forms.Label labelLessonEnd;
        private System.Windows.Forms.Label labelLessonStart;
        private System.Windows.Forms.Label labelLessonDate;
        private System.Windows.Forms.ComboBox cbLessonStudent10;
        private System.Windows.Forms.ComboBox cbLessonStudent9;
        private System.Windows.Forms.Label labelLessonStudent10;
        private System.Windows.Forms.Label labelLessonStudent9;
        private System.Windows.Forms.ComboBox cbLessonStudent8;
        private System.Windows.Forms.ComboBox cbLessonStudent7;
        private System.Windows.Forms.Label labelLessonStudent8;
        private System.Windows.Forms.Label labelLessonStudent7;
        private System.Windows.Forms.ComboBox cbLessonStudent6;
        private System.Windows.Forms.ComboBox cbLessonStudent5;
        private System.Windows.Forms.Label labelLessonStudent6;
        private System.Windows.Forms.Label labelLessonStudent5;
        private System.Windows.Forms.ComboBox cbLessonStudent4;
        private System.Windows.Forms.Label labelLessonStudent4;
        private System.Windows.Forms.ComboBox cbLessonStudent3;
        private System.Windows.Forms.Label labelLessonStudent3;
        private System.Windows.Forms.Label labelLessonComment;
        private System.Windows.Forms.TextBox tbLEssonComment;
        private System.Windows.Forms.ComboBox cbLessonStudent2;
        private System.Windows.Forms.Label labelLessonStudent2;
        private System.Windows.Forms.ComboBox cbLessonStudent1;
        private System.Windows.Forms.ComboBox cbLessonTeacher2;
        private System.Windows.Forms.ComboBox cbLessonTeacher1;
        private System.Windows.Forms.ComboBox cbLessonProg;
        private System.Windows.Forms.Label labelLessonStudent1;
        private System.Windows.Forms.Label labelLessonTeacher2;
        private System.Windows.Forms.Label labelLessonTeacher1;
        private HiddenTabControl tabControlSearch;
        private System.Windows.Forms.TabPage tabPageSearchStudent;
        private System.Windows.Forms.TabPage tabPageSearchTeacher;
        private System.Windows.Forms.TabPage tabPageSearchProgram;
        private System.Windows.Forms.TabPage tabPageSearchRoom;
        private System.Windows.Forms.TabPage tabPageSearchLesson;
        private System.Windows.Forms.Panel panelSearchButtons;
        private System.Windows.Forms.Panel panelSearchLabels;
        private System.Windows.Forms.Panel panelSearchTeacher;
        private System.Windows.Forms.Panel panelSearchProgram;
        private System.Windows.Forms.Panel panelSearchRoom;
        private System.Windows.Forms.Panel panelSearchLesson;
        private System.Windows.Forms.TextBox tbSearchTeachLastName;
        private System.Windows.Forms.TextBox tbSearchTeachFirstName;
        private System.Windows.Forms.ComboBox cbSearchTeachLang1;
        private System.Windows.Forms.ComboBox cbSearchTeachStatus;
        private System.Windows.Forms.Label lbSerchTeachLastName;
        private System.Windows.Forms.Label lbSerchTeachFirstName;
        private System.Windows.Forms.Label lbSerchTeachLang1;
        private System.Windows.Forms.Label lbSerchTeachStatus;
        private System.Windows.Forms.ComboBox cbSearchProgLevel;
        private System.Windows.Forms.ComboBox cbSearchProgLanguage;
        private System.Windows.Forms.Label lbSearchProgLevel;
        private System.Windows.Forms.Label lbSearchProgLanguage;
        private System.Windows.Forms.TextBox tbSearchLessonDate;
        private System.Windows.Forms.Label lbSearchLessonStudent;
        private System.Windows.Forms.ComboBox cbSearchLessonRoom;
        private System.Windows.Forms.Label lbSearchLessonRoom;
        private System.Windows.Forms.ComboBox cbSearchLessonProgram;
        private System.Windows.Forms.Label lbSearchLessonProgram;
        private System.Windows.Forms.TextBox tbSearchLessonTeacher;
        private System.Windows.Forms.Label lbSearchTeacher;
        private System.Windows.Forms.TextBox tbSearchLessonStudent;
        private System.Windows.Forms.Label lbSearchLessonDay;
        private System.Windows.Forms.Button buttonTeach_GrabAvailChanges;
        private System.Windows.Forms.ToolStripMenuItem scheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem planToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem payToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem studentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teachersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportExpenseToolStripMenuItem;
        private System.Windows.Forms.TabPage tabEdit;
        private System.Windows.Forms.TabPage tabSchedPlan;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private HiddenTabControl tabControlOps;
        private System.Windows.Forms.TabPage tabSchedShow;
        private System.Windows.Forms.TabPage tabSchedCancel;
        private System.Windows.Forms.TabPage tabPayStud;
        private System.Windows.Forms.TabPage tabPayTeach;
        private System.Windows.Forms.TabPage tabPayExpense;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Label lbSchedPlanName;
        private System.Windows.Forms.Label lbSchedShowName;
        private System.Windows.Forms.Label lbSchedCancelName;
        private System.Windows.Forms.Label lbPayStudName;
        private System.Windows.Forms.Label lbPayTeachName;
        private System.Windows.Forms.Label lbPayExpenseName;
        private System.Windows.Forms.Label lbFutureOpName;
        private System.Windows.Forms.Panel panelSchedNewMatrix;
        private System.Windows.Forms.Panel panelSchedNewParams;
        private System.Windows.Forms.ComboBox cbSchedNewTeacher;
        private System.Windows.Forms.Label lbSchedNewTeacher;
        private System.Windows.Forms.Label lbSchedNewWeek;
        private System.Windows.Forms.ComboBox cbSchedNewLanguage;
        private System.Windows.Forms.Label lbSchedNewLanguage;
        private System.Windows.Forms.ComboBox cbSchedNewStud4;
        private System.Windows.Forms.Label lbSchedNewStud1;
        private System.Windows.Forms.ComboBox cbSchedNewStud3;
        private System.Windows.Forms.Label lbSchedNewStud2;
        private System.Windows.Forms.ComboBox cbSchedNewStud2;
        private System.Windows.Forms.Label lbSchedNewStud3;
        private System.Windows.Forms.ComboBox cbSchedNewStud1;
        private System.Windows.Forms.Label lbSchedNewStud4;
        private System.Windows.Forms.DateTimePicker dtpSchedNew;
        private System.Windows.Forms.Panel panelTeachMatrix;
        private System.Windows.Forms.ComboBox cbSchedNewDuration;
        private System.Windows.Forms.Label lbSchedNewDuration;
        private System.Windows.Forms.Button butSchedNewAccept;
        private System.Windows.Forms.DataGridView dgvSchedNew;
        private System.Windows.Forms.BindingSource schedNewSlotList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slot;
        private System.Windows.Forms.DataGridViewTextBoxColumn mondayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tuesdayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wednesdayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn thursdayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fridayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn saturdayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sundayDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label lbSchedNewStudSchedule4;
        private System.Windows.Forms.Label lbSchedNewStudSchedule3;
        private System.Windows.Forms.Label lbSchedNewStudSchedule2;
        private System.Windows.Forms.Label lbSchedNewStudSchedule1;
        private System.Windows.Forms.TextBox tbSchedNewComment;
        private System.Windows.Forms.Label lbSchedNewComment;
        private System.Windows.Forms.Label lbSchedNewTeachVacation;
        private System.Windows.Forms.Button buttonGlobRefresh;
        private System.Windows.Forms.BindingSource lessonList;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.DataGridViewTextBoxColumn programDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn student1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn student2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn student3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn student10DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Student4;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentsDataGridViewTextBoxColumn2;
    }
}

