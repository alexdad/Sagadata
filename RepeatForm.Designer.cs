namespace RecordKeeper
{
    partial class RepeatForm
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
            this.tbRepeatTimes = new System.Windows.Forms.TextBox();
            this.gbRepeat = new System.Windows.Forms.GroupBox();
            this.butRepeatOK = new System.Windows.Forms.Button();
            this.butRepeatCancel = new System.Windows.Forms.Button();
            this.rbRepeatTimes = new System.Windows.Forms.RadioButton();
            this.rbRepeatEOY = new System.Windows.Forms.RadioButton();
            this.gbRepeat.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbRepeatTimes
            // 
            this.tbRepeatTimes.Location = new System.Drawing.Point(116, 51);
            this.tbRepeatTimes.Name = "tbRepeatTimes";
            this.tbRepeatTimes.Size = new System.Drawing.Size(35, 20);
            this.tbRepeatTimes.TabIndex = 2;
            this.tbRepeatTimes.Click += new System.EventHandler(this.tbRepeatTimes_Click);
            this.tbRepeatTimes.TextChanged += new System.EventHandler(this.tbRepeatTimes_TextChanged);
            // 
            // gbRepeat
            // 
            this.gbRepeat.Controls.Add(this.butRepeatOK);
            this.gbRepeat.Controls.Add(this.butRepeatCancel);
            this.gbRepeat.Controls.Add(this.rbRepeatTimes);
            this.gbRepeat.Controls.Add(this.tbRepeatTimes);
            this.gbRepeat.Controls.Add(this.rbRepeatEOY);
            this.gbRepeat.Location = new System.Drawing.Point(2, 2);
            this.gbRepeat.Name = "gbRepeat";
            this.gbRepeat.Size = new System.Drawing.Size(243, 140);
            this.gbRepeat.TabIndex = 3;
            this.gbRepeat.TabStop = false;
            this.gbRepeat.Text = "Repeat xxx";
            // 
            // butRepeatOK
            // 
            this.butRepeatOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.butRepeatOK.Location = new System.Drawing.Point(95, 88);
            this.butRepeatOK.Name = "butRepeatOK";
            this.butRepeatOK.Size = new System.Drawing.Size(64, 23);
            this.butRepeatOK.TabIndex = 4;
            this.butRepeatOK.Text = "OK";
            this.butRepeatOK.UseVisualStyleBackColor = true;
            // 
            // butRepeatCancel
            // 
            this.butRepeatCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butRepeatCancel.Location = new System.Drawing.Point(10, 88);
            this.butRepeatCancel.Name = "butRepeatCancel";
            this.butRepeatCancel.Size = new System.Drawing.Size(66, 23);
            this.butRepeatCancel.TabIndex = 3;
            this.butRepeatCancel.Text = "Cancel";
            this.butRepeatCancel.UseVisualStyleBackColor = true;
            // 
            // rbRepeatTimes
            // 
            this.rbRepeatTimes.AutoSize = true;
            this.rbRepeatTimes.Location = new System.Drawing.Point(16, 51);
            this.rbRepeatTimes.Name = "rbRepeatTimes";
            this.rbRepeatTimes.Size = new System.Drawing.Size(94, 17);
            this.rbRepeatTimes.TabIndex = 1;
            this.rbRepeatTimes.TabStop = true;
            this.rbRepeatTimes.Text = "Several times: ";
            this.rbRepeatTimes.UseVisualStyleBackColor = true;
            // 
            // rbRepeatEOY
            // 
            this.rbRepeatEOY.AutoSize = true;
            this.rbRepeatEOY.Location = new System.Drawing.Point(16, 28);
            this.rbRepeatEOY.Name = "rbRepeatEOY";
            this.rbRepeatEOY.Size = new System.Drawing.Size(143, 17);
            this.rbRepeatEOY.TabIndex = 0;
            this.rbRepeatEOY.TabStop = true;
            this.rbRepeatEOY.Text = "Up to the end of the year";
            this.rbRepeatEOY.UseVisualStyleBackColor = true;
            this.rbRepeatEOY.CheckedChanged += new System.EventHandler(this.rbRepeatEOY_CheckedChanged);
            // 
            // RepeatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 125);
            this.Controls.Add(this.gbRepeat);
            this.Name = "RepeatForm";
            this.Text = "RepeatForm";
            this.gbRepeat.ResumeLayout(false);
            this.gbRepeat.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbRepeatTimes;
        private System.Windows.Forms.GroupBox gbRepeat;
        private System.Windows.Forms.RadioButton rbRepeatTimes;
        private System.Windows.Forms.RadioButton rbRepeatEOY;
        private System.Windows.Forms.Button butRepeatOK;
        private System.Windows.Forms.Button butRepeatCancel;
    }
}