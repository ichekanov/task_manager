using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace desktop_widget
{
    public partial class MainWindow : Form
    {
        public DataTable taskTable;
        public MainWindow(DataTable data)
        {
            InitializeComponent();
            taskTable = data;
            CreateButtons(data);
        }

        // https://metanit.com/sharp/windowsforms/3.1.php
        private void CreateButtons(DataTable data)
        {
            int buttonWidth = 350;
            const int buttonHeight = 30;

            foreach (DataRow row in data.Rows)
            {
                var TaskButton = new Button
                {
                    Cursor = Cursors.Hand,
                    Height = buttonHeight,
                    Tag = row.Field<long>("task_id").ToString(),
                    Text = row.Field<string>("name"),
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                };
                TaskButton.Click += new System.EventHandler(this.TaskButton_Click);
                flowLayoutPanel1.Controls.Add(TaskButton);
                if (TaskButton.Width > buttonWidth)
                    buttonWidth = TaskButton.Width + 10;
            }
            foreach (Button TaskButton in flowLayoutPanel1.Controls)
            {
                TaskButton.Width = buttonWidth;
            }
        }

        void TaskButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string ID = btn.Tag as string;
            var row = taskTable.Select("task_id = " + ID);
            if (row.Length != 1)
                throw new Exception();
            string name = row[0].Field<string>("name");
            string description = row[0].Field<string>("description");
            DateTime deadline = row[0].Field<DateTime>("deadline");
            DateTime create_date = row[0].Field<DateTime>("create_date");
            DateTime modify_date = row[0].Field<DateTime>("modify_date");
            if (description == "None")
                description = "Нет описания.";
            var examineTaskWindow = new ExamineTaskWindow(name, description, deadline, create_date, modify_date);
            examineTaskWindow.Show();
        }
    }
}
