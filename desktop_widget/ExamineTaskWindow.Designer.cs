namespace desktop_widget
{
    partial class ExamineTaskWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamineTaskWindow));
            this.taskDescriptionBox = new System.Windows.Forms.RichTextBox();
            this.deadlineCalendar = new System.Windows.Forms.MonthCalendar();
            this.deadlineLabel = new System.Windows.Forms.Label();
            this.createDateLabel = new System.Windows.Forms.Label();
            this.modifyDateLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskDescriptionBox
            // 
            this.taskDescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDescriptionBox.Location = new System.Drawing.Point(12, 12);
            this.taskDescriptionBox.Name = "taskDescriptionBox";
            this.taskDescriptionBox.ReadOnly = true;
            this.taskDescriptionBox.Size = new System.Drawing.Size(430, 110);
            this.taskDescriptionBox.TabIndex = 4;
            this.taskDescriptionBox.Text = "";
            // 
            // deadlineCalendar
            // 
            this.deadlineCalendar.Location = new System.Drawing.Point(12, 19);
            this.deadlineCalendar.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.deadlineCalendar.Name = "deadlineCalendar";
            this.deadlineCalendar.TabIndex = 5;
            this.deadlineCalendar.Visible = false;
            // 
            // deadlineLabel
            // 
            this.deadlineLabel.AutoSize = true;
            this.deadlineLabel.Location = new System.Drawing.Point(6, 23);
            this.deadlineLabel.Name = "deadlineLabel";
            this.deadlineLabel.Size = new System.Drawing.Size(86, 15);
            this.deadlineLabel.TabIndex = 7;
            this.deadlineLabel.Text = "deadlineLabel";
            this.deadlineLabel.Visible = false;
            // 
            // createDateLabel
            // 
            this.createDateLabel.AutoSize = true;
            this.createDateLabel.Location = new System.Drawing.Point(85, 19);
            this.createDateLabel.Name = "createDateLabel";
            this.createDateLabel.Size = new System.Drawing.Size(101, 15);
            this.createDateLabel.TabIndex = 8;
            this.createDateLabel.Text = "createDateLabel";
            // 
            // modifyDateLabel
            // 
            this.modifyDateLabel.AutoSize = true;
            this.modifyDateLabel.Location = new System.Drawing.Point(85, 47);
            this.modifyDateLabel.Name = "modifyDateLabel";
            this.modifyDateLabel.Size = new System.Drawing.Size(104, 15);
            this.modifyDateLabel.TabIndex = 9;
            this.modifyDateLabel.Text = "modifyDateLabel";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Создана";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Изменена";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.createDateLabel);
            this.groupBox1.Controls.Add(this.modifyDateLabel);
            this.groupBox1.Location = new System.Drawing.Point(232, 129);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 74);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Мета";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deadlineCalendar);
            this.groupBox2.Controls.Add(this.deadlineLabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 193);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Дедлайн";
            // 
            // ExamineTaskWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(454, 334);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.taskDescriptionBox);
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximumSize = new System.Drawing.Size(470, 373);
            this.MinimumSize = new System.Drawing.Size(470, 373);
            this.Name = "ExamineTaskWindow";
            this.Text = "ExamineTaskWindow";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox taskDescriptionBox;
        private System.Windows.Forms.MonthCalendar deadlineCalendar;
        private System.Windows.Forms.Label deadlineLabel;
        private System.Windows.Forms.Label createDateLabel;
        private System.Windows.Forms.Label modifyDateLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}