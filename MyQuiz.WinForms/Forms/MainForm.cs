using MyQuiz.Core.Abstractions;
using MyQuiz.Core.Application;

using MyQuiz.Core.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyQuiz.WinForms.Extensions;

namespace MyQuiz.WinForms
{
    public partial class MainForm : Form
    {
        // Зависимости (DIP - Dependency Inversion)
        private readonly IQuizService _quiz;
        private readonly ITimerService _timer;
        private readonly ReportService _report;

        // Список контролов для текущих ответов
        private List<ButtonBase> _optionControls = new List<ButtonBase>();

        public MainForm(IQuizService quiz, ITimerService timer, ReportService report)
        {
            InitializeComponent();

            _quiz = quiz;
            _timer = timer;
            _report = report;

            SubscribeToEvents();
            SetupInitialUi();

            // Начинаем тест
            _timer.Start(300); // 300 секунд
            DisplayQuestion();
        }

        private void SubscribeToEvents()
        {
            // Подписка на прогресс (через интерфейс)
            _quiz.OnProgressChanged += (curr, total) => {
                this.SafeInvoke(() => pbProgress.SetValueInstant(curr));
            };

            // Подписка на таймер
            _timer.OnTick += (seconds) => {
                this.SafeInvoke(() => {
                    lblTimer.Text = TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
                    pbTimer.Value = Math.Max(0, seconds);
                });
            };

            _timer.OnElapsed += () => {
                this.SafeInvoke(() => {
                    MessageBox.Show("Время вышло!", "Стоп", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowFinalResults();
                });
            };
        }

        private void SetupInitialUi()
        {
            // Настраиваем прогресс вопросов
            pbProgress.Maximum = _quiz.TotalQuestions;
            pbProgress.Value = 0; // На старте 0 ответов

            // Настраиваем таймер (300 секунд)
            int initialSeconds = 300;
            pbTimer.Maximum = initialSeconds;

            // Мгновенно ставим полоску в максимум (используем наш метод из Extensions)
            pbTimer.SetValueInstant(initialSeconds);

            // Сразу пишем текст 05:00
            lblTimer.Text = TimeSpan.FromSeconds(initialSeconds).ToString(@"mm\:ss");
        }

        private void DisplayQuestion()
        {
            if (_quiz.IsFinished)
            {
                ShowFinalResults();
                return;
            }

            var current = _quiz.GetCurrentQuestion();
            lblQuestion.Text = current.Text;

            // Очищаем старые ответы
            gbOptions.Controls.Clear();
            _optionControls.Clear();

            // Создаем новые контролы (Логика OCP: форма знает о базовых типах стратегий)
            bool isMultiple = current.Strategy is MultipleChoiceStrategy;

            for (int i = 0; i < current.Options.Length; i++)
            {
                ButtonBase control = isMultiple ? (ButtonBase)new CheckBox() : new RadioButton();
                control.Text = current.Options[i];
                control.Location = new System.Drawing.Point(20, 35 + (i * 30));
                control.AutoSize = true;

                // ВАЖНО: Привязываем индекс ответа к контролу
                control.Tag = i;

                gbOptions.Controls.Add(control);
                _optionControls.Add(control);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Собираем индексы выбранных ответов
            var selectedIndices = _optionControls
                .Where(c => (c is CheckBox cb && cb.Checked) || (c is RadioButton rb && rb.Checked))
                .Select(c => (int)c.Tag)
                .ToArray();

            if (selectedIndices.Length == 0)
            {
                MessageBox.Show("Выберите хотя бы один вариант!");
                return;
            }

            _quiz.Answer(selectedIndices);
            DisplayQuestion();
        }

        private void ShowFinalResults()
        {
            _timer.Stop();

            // SRP: Делегируем создание текста отчета специальному сервису
            string finalReport = _report.GenerateSummary(_quiz);

            MessageBox.Show(finalReport, "Результаты теста");
            Application.Exit();
        }
    }
}