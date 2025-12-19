using System;
using System.Windows.Forms;
// Добавляем ссылки на ваши новые папки из проекта Core
using MyQuiz.Core.Abstractions;   // Для IQuizService, ITimerService
using MyQuiz.Core.Application;    // Для QuizController, ReportService
using MyQuiz.Core.Infrastructure; // Для QuestionRepository, QuizTimer


namespace MyQuiz.WinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ровно 5 строк информативного текста
            string info = "Добро пожаловать в тест!\n" +
                          $"Всего вопросов: 20\n" +
                          "Время на прохождение: 5 минут\n" +
                          "Будьте внимательны при выборе ответов.\n" +
                          "Желаем удачи!";

            MessageBox.Show(info, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 1. Подготавливаем данные (SRP: Репозиторий сам фильтрует и мешает)
            var repository = new QuestionRepository();
            var questions = repository.GetRandomQuestions(20);

            // 2. Создаем сервисы (DIP: Работаем через интерфейсы)
            IQuizService quizService = new QuizController(questions);
            ITimerService timerService = new QuizTimer();
            ReportService reportService = new ReportService();

            // 3. Внедряем зависимости в форму через конструктор
            Application.Run(new MainForm(quizService, timerService, reportService));
        }
    }
}
