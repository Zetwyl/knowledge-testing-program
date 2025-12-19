using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MyQuiz.Core.Domain;
using MyQuiz.Core.Application;
using MyQuiz.Core.Strategies;

namespace MyQuiz.Tests
{
    [TestClass]
    public class ReportServiceTests
    {
        private Question CreateTestQuestion(string text, bool isCorrectHandled)
        {
            return new Question
            {
                Text = text,
                CorrectIndices = new[] { 0 },
                Options = new[] { "Ответ 1", "Ответ 2" }, // Создаем один раз здесь
                Strategy = new SingleChoiceStrategy()
            };
        }

        // 1. Проверяем основную работу: цифры и проценты
        [TestMethod]
        public void GenerateSummary_ShouldCalculateScoreAndPercent()
        {
            var questions = new List<Question>
            {
                new Question
                {
                    Text = "Q", CorrectIndices = new[]{0}, Strategy = new SingleChoiceStrategy()
                }
            };
            var controller = new QuizController(questions);
            controller.Answer(new[] { 0 }); // 1 из 1

            var report = new ReportService().GenerateSummary(controller);

            // Проверяем наличие ключевых данных в строке
            Assert.IsTrue(report.Contains("1 из 1"));
            Assert.IsTrue(report.Contains("100"));
        }

        // 2. Проверяем фильтрацию ошибок: в отчет идут только промахи
        [TestMethod]
        public void GenerateSummary_ShouldListOnlyMistakes()
        {
            var q1 = CreateTestQuestion("ОшибкаТут", false);
            var q2 = CreateTestQuestion("ТутВерно", true);

            var controller = new QuizController(new List<Question> { q1, q2 });

            controller.Answer(new[] { 1 }); // Ошибка для q1
            controller.Answer(new[] { 0 }); // Верно для q2

            var report = new ReportService().GenerateSummary(controller);

            Assert.IsTrue(report.Contains("ОшибкаТут"));
            Assert.IsFalse(report.Contains("ТутВерно"));
        }

        [TestMethod]
        public void GenerateSummary_ZeroQuestions_ShouldReturnErrorMessage()
        {
            // Arrange: создаем контроллер с пустым списком вопросов
            var controller = new QuizController(new List<Question>());
            var service = new ReportService();

            // Act: вызываем генерацию отчета
            string report = service.GenerateSummary(controller);

            // Assert: проверяем, что вернулась строка с ошибкой, а не NaN или Exception
            Assert.IsTrue(report.Contains("Ошибка"), "Для пустого теста должно возвращаться сообщение об ошибке");
            Assert.IsTrue(report.Contains("не было вопросов"));
        }
    }
}