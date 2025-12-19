namespace MyQuiz.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Оставляем только нужные элементы
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.ProgressBar pbTimer;
        private System.Windows.Forms.Label lblTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblQuestion = new System.Windows.Forms.Label();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.pbTimer = new System.Windows.Forms.ProgressBar();
            this.lblTimer = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 1. pbProgress (Общий прогресс вопросов - самая верхняя полоска)
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbProgress.Height = 10;
            this.pbProgress.Name = "pbProgress";

            // 2. pbTimer (Полоска времени - чуть ниже)
            this.pbTimer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbTimer.Height = 4;
            this.pbTimer.Name = "pbTimer";
            this.pbTimer.ForeColor = System.Drawing.Color.Tomato; // Обозначим время красным/оранжевым

            // 3. lblTimer (Цифровой отсчет рядом с кнопкой)
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTimer.Location = new System.Drawing.Point(280, 283); // Слева от кнопки "Далее"
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(40, 15);
            this.lblTimer.Text = "05:00";

            // 4. lblQuestion
            this.lblQuestion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuestion.Location = new System.Drawing.Point(12, 40);
            this.lblQuestion.Size = new System.Drawing.Size(460, 50);
            this.lblQuestion.Text = "Текст вопроса";

            // 5. gbOptions
            this.gbOptions.Location = new System.Drawing.Point(12, 100);
            this.gbOptions.Size = new System.Drawing.Size(460, 160);
            this.gbOptions.Text = "Варианты ответа";

            // 6. btnNext
            this.btnNext.Location = new System.Drawing.Point(352, 270);
            this.btnNext.Size = new System.Drawing.Size(120, 40);
            this.btnNext.Text = "Далее";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);

            // MainForm
            this.ClientSize = new System.Drawing.Size(484, 320);
            this.Controls.Add(this.lblTimer);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.pbTimer);
            this.Controls.Add(this.pbProgress);

            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Interview Quiz";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

