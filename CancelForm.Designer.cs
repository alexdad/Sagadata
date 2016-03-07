namespace RecordKeeper
{
    partial class CancelForm
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
            this.groupBoxCancel = new System.Windows.Forms.GroupBox();
            this.buttonCancelCancel = new System.Windows.Forms.Button();
            this.buttonCancelOK = new System.Windows.Forms.Button();
            this.tbCancelComment = new System.Windows.Forms.TextBox();
            this.groupBoxCancelRadios = new System.Windows.Forms.GroupBox();
            this.radioButtonCancelOnTime = new System.Windows.Forms.RadioButton();
            this.radioButtonCancelLate = new System.Windows.Forms.RadioButton();
            this.radioButtonCancelNoShow = new System.Windows.Forms.RadioButton();
            this.radioButtonCancelExcused = new System.Windows.Forms.RadioButton();
            this.groupBoxCancel.SuspendLayout();
            this.groupBoxCancelRadios.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCancel
            // 
            this.groupBoxCancel.Controls.Add(this.groupBoxCancelRadios);
            this.groupBoxCancel.Controls.Add(this.tbCancelComment);
            this.groupBoxCancel.Controls.Add(this.buttonCancelOK);
            this.groupBoxCancel.Controls.Add(this.buttonCancelCancel);
            this.groupBoxCancel.Location = new System.Drawing.Point(12, 12);
            this.groupBoxCancel.Name = "groupBoxCancel";
            this.groupBoxCancel.Size = new System.Drawing.Size(260, 237);
            this.groupBoxCancel.TabIndex = 0;
            this.groupBoxCancel.TabStop = false;
            this.groupBoxCancel.Text = "Cancelling lesson:";
            // 
            // buttonCancelCancel
            // 
            this.buttonCancelCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancelCancel.Location = new System.Drawing.Point(27, 205);
            this.buttonCancelCancel.Name = "buttonCancelCancel";
            this.buttonCancelCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelCancel.TabIndex = 0;
            this.buttonCancelCancel.Text = "Cancel";
            this.buttonCancelCancel.UseVisualStyleBackColor = true;
            // 
            // buttonCancelOK
            // 
            this.buttonCancelOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonCancelOK.Location = new System.Drawing.Point(154, 205);
            this.buttonCancelOK.Name = "buttonCancelOK";
            this.buttonCancelOK.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelOK.TabIndex = 1;
            this.buttonCancelOK.Text = "OK";
            this.buttonCancelOK.UseVisualStyleBackColor = true;
            // 
            // tbCancelComment
            // 
            this.tbCancelComment.Location = new System.Drawing.Point(6, 168);
            this.tbCancelComment.Name = "tbCancelComment";
            this.tbCancelComment.Size = new System.Drawing.Size(235, 20);
            this.tbCancelComment.TabIndex = 2;
            this.tbCancelComment.TextChanged += new System.EventHandler(this.tbCancelComment_TextChanged);
            // 
            // groupBoxCancelRadios
            // 
            this.groupBoxCancelRadios.Controls.Add(this.radioButtonCancelExcused);
            this.groupBoxCancelRadios.Controls.Add(this.radioButtonCancelNoShow);
            this.groupBoxCancelRadios.Controls.Add(this.radioButtonCancelLate);
            this.groupBoxCancelRadios.Controls.Add(this.radioButtonCancelOnTime);
            this.groupBoxCancelRadios.Location = new System.Drawing.Point(16, 20);
            this.groupBoxCancelRadios.Name = "groupBoxCancelRadios";
            this.groupBoxCancelRadios.Size = new System.Drawing.Size(213, 123);
            this.groupBoxCancelRadios.TabIndex = 3;
            this.groupBoxCancelRadios.TabStop = false;
            // 
            // radioButtonCancelOnTime
            // 
            this.radioButtonCancelOnTime.AutoSize = true;
            this.radioButtonCancelOnTime.Location = new System.Drawing.Point(52, 20);
            this.radioButtonCancelOnTime.Name = "radioButtonCancelOnTime";
            this.radioButtonCancelOnTime.Size = new System.Drawing.Size(65, 17);
            this.radioButtonCancelOnTime.TabIndex = 0;
            this.radioButtonCancelOnTime.TabStop = true;
            this.radioButtonCancelOnTime.Text = "On Time";
            this.radioButtonCancelOnTime.UseVisualStyleBackColor = true;
            this.radioButtonCancelOnTime.CheckedChanged += new System.EventHandler(this.radioButtonCancelOnTime_CheckedChanged);
            // 
            // radioButtonCancelLate
            // 
            this.radioButtonCancelLate.AutoSize = true;
            this.radioButtonCancelLate.Location = new System.Drawing.Point(52, 43);
            this.radioButtonCancelLate.Name = "radioButtonCancelLate";
            this.radioButtonCancelLate.Size = new System.Drawing.Size(46, 17);
            this.radioButtonCancelLate.TabIndex = 1;
            this.radioButtonCancelLate.TabStop = true;
            this.radioButtonCancelLate.Text = "Late";
            this.radioButtonCancelLate.UseVisualStyleBackColor = true;
            this.radioButtonCancelLate.CheckedChanged += new System.EventHandler(this.radioButtonCancelLate_CheckedChanged);
            // 
            // radioButtonCancelNoShow
            // 
            this.radioButtonCancelNoShow.AutoSize = true;
            this.radioButtonCancelNoShow.Location = new System.Drawing.Point(52, 66);
            this.radioButtonCancelNoShow.Name = "radioButtonCancelNoShow";
            this.radioButtonCancelNoShow.Size = new System.Drawing.Size(69, 17);
            this.radioButtonCancelNoShow.TabIndex = 2;
            this.radioButtonCancelNoShow.TabStop = true;
            this.radioButtonCancelNoShow.Text = "No Show";
            this.radioButtonCancelNoShow.UseVisualStyleBackColor = true;
            this.radioButtonCancelNoShow.CheckedChanged += new System.EventHandler(this.radioButtonCancelNoShow_CheckedChanged);
            // 
            // radioButtonCancelExcused
            // 
            this.radioButtonCancelExcused.AutoSize = true;
            this.radioButtonCancelExcused.Location = new System.Drawing.Point(52, 89);
            this.radioButtonCancelExcused.Name = "radioButtonCancelExcused";
            this.radioButtonCancelExcused.Size = new System.Drawing.Size(66, 17);
            this.radioButtonCancelExcused.TabIndex = 3;
            this.radioButtonCancelExcused.TabStop = true;
            this.radioButtonCancelExcused.Text = "Excused";
            this.radioButtonCancelExcused.UseVisualStyleBackColor = true;
            this.radioButtonCancelExcused.CheckedChanged += new System.EventHandler(this.radioButtonCancelExcused_CheckedChanged);
            // 
            // CancelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.groupBoxCancel);
            this.Name = "CancelForm";
            this.Text = "CancelForm";
            this.groupBoxCancel.ResumeLayout(false);
            this.groupBoxCancel.PerformLayout();
            this.groupBoxCancelRadios.ResumeLayout(false);
            this.groupBoxCancelRadios.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCancel;
        private System.Windows.Forms.Button buttonCancelOK;
        private System.Windows.Forms.Button buttonCancelCancel;
        private System.Windows.Forms.TextBox tbCancelComment;
        private System.Windows.Forms.GroupBox groupBoxCancelRadios;
        private System.Windows.Forms.RadioButton radioButtonCancelExcused;
        private System.Windows.Forms.RadioButton radioButtonCancelNoShow;
        private System.Windows.Forms.RadioButton radioButtonCancelLate;
        private System.Windows.Forms.RadioButton radioButtonCancelOnTime;
    }
}