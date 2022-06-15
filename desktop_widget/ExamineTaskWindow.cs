using System;
using System.Windows.Forms;

namespace desktop_widget
{
    public partial class ExamineTaskWindow : Form
    {
        /// <summary>
        /// Инициализация окна с информацией о задаче.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="deadline"></param>
        /// <param name="create_date"></param>
        /// <param name="modify_date"></param>
        public ExamineTaskWindow(string name, string description, DateTime deadline, DateTime create_date, DateTime modify_date)
        {
            InitializeComponent();
            this.Text = name;
            taskDescriptionBox.Text = description;
            if (deadline != new DateTime(1970, 1, 1))
            {
                deadlineCalendar.Visible = true;
                deadlineCalendar.AddBoldedDate(deadline);
                deadlineCalendar.SelectionStart = deadline;
                deadlineCalendar.SelectionEnd = deadline;
                deadlineLabel.Visible = false;
                //deadlineLabel.Text = deadline.ToString();
            }
            else
            {
                deadlineCalendar.Visible = false;
                deadlineLabel.Visible = true;
                deadlineLabel.Text = "не назначен";
            }
            createDateLabel.Text = create_date.ToString();
            modifyDateLabel.Text = modify_date.ToString();
        }
    }
}
