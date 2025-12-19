using System;
using System.Linq;
using System.Text;
using MyQuiz.Core.Abstractions;

namespace MyQuiz.Core.Application
{
    public class ReportService
    {
        public string GenerateSummary(IQuizService quiz)
        {
            if (quiz.TotalQuestions == 0)
            {
                return "=== ИТОГИ ИНТЕРВЬЮ ===\nОшибка: В тесте не было вопросов.";
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("=== ИТОГИ ИНТЕРВЬЮ ===");
            sb.AppendLine($"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}");
            sb.AppendLine($"Результат: {quiz.Score} из {quiz.TotalQuestions}");

            double percentage = (double)quiz.Score / quiz.TotalQuestions * 100;

            sb.AppendLine($"Процент правильных ответов: {percentage:F1}%");
            sb.AppendLine(GetPerformanceMessage(percentage));
            sb.AppendLine(new string('-', 30));

            if (quiz.Mistakes.Any())
            {
                sb.AppendLine("\nДЕТАЛИЗАЦИЯ ОШИБОК:");
                sb.AppendLine();

                foreach (var m in quiz.Mistakes)
                {
                    sb.AppendLine($"   Вопрос: {m.Question.Text}");

                    // Используем .Select(i => ...) для получения текста ответа по индексу
                    var userAnswers = string.Join(", ", m.UserAnswers.Select(i => m.Question.Options[i]));
                    sb.AppendLine($"   Ваш ответ: {userAnswers}");

                    var correctAnswers = string.Join(", ", m.Question.CorrectIndices.Select(i => m.Question.Options[i]));
                    sb.AppendLine($"   Правильно: {correctAnswers}");

                    sb.AppendLine();
                }
            }
            else
            {
                sb.AppendLine("\nИдеально! Ошибок нет. Вы готовы к Senior-позиции! 🚀");
            }

            return sb.ToString();
        }

        private string GetPerformanceMessage(double percentage)
        {
            if (percentage >= 90) return "Отличный результат! Глубокие знания C#.";
            if (percentage >= 70) return "Хороший уровень. Есть небольшие пробелы.";
            if (percentage >= 50) return "Удовлетворительно. Стоит подтянуть теорию.";
            return "Рекомендуется повторить основы платформы .NET.";
        }
    }
}