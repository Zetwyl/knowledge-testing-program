using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MyQuiz.Core.Domain;
using MyQuiz.Core.Application;
using MyQuiz.Core.Strategies;

namespace MyQuiz.Tests
{
    [TestClass]
    public class QuizControllerTests
    {
        // Вспомогательный метод для создания тестовых данных
        private List<Question> GetMockData() => new List<Question>
        {
            new Question { Text = "Q1", CorrectIndices = new[]{0}, Strategy = new SingleChoiceStrategy() },
            new Question { Text = "Q2", CorrectIndices = new[]{1}, Strategy = new SingleChoiceStrategy() }
        };

        [TestMethod]
        public void Quiz_InitialState_ShouldBeReady()
        {
            var controller = new QuizController(GetMockData());

            Assert.AreEqual(0, controller.Score, "Начальный счет должен быть 0");
            Assert.IsFalse(controller.IsFinished, "Тест не должен быть завершен в начале");
            Assert.AreEqual("Q1", controller.GetCurrentQuestion().Text);
        }

        [TestMethod]
        public void Answer_CorrectAndWrong_ShouldUpdateState()
        {
            var questions = GetMockData();
            var controller = new QuizController(questions);
            bool progressFired = false;
            controller.OnProgressChanged += (c, t) => progressFired = true;

            // 1. Верный ответ
            controller.Answer(new[] { 0 });
            Assert.AreEqual(1, controller.Score);
            Assert.IsTrue(progressFired, "Событие прогресса должно сработать");
            Assert.AreEqual("Q2", controller.GetCurrentQuestion().Text, "Должен произойти переход к Q2");

            // 2. Неверный ответ
            controller.Answer(new[] { 0 }); // На Q2 верный индекс - 1
            Assert.AreEqual(1, controller.Score, "Счет не должен увеличиться");
            Assert.AreEqual(1, controller.Mistakes.Count, "Должна записаться 1 ошибка");
            Assert.AreSame(questions[1], controller.Mistakes[0].Question);
        }

        [TestMethod]
        public void Answer_InvalidInput_ShouldBeCountedAsMistake()
        {
            var controller = new QuizController(GetMockData());

            // Имитируем нажатие "Далее" без выбора или системный сбой (индекс 99)
            controller.Answer(new int[0]);
            controller.Answer(new[] { 99 });

            Assert.AreEqual(0, controller.Score, "Баллы за некорректный ввод не даются");
            Assert.AreEqual(2, controller.Mistakes.Count, "Пустой ввод и неверный индекс — это ошибки");
            Assert.IsTrue(controller.IsFinished, "После 2 ответов тест должен завершиться");
        }

        [TestMethod]
        public void Quiz_Completion_ShouldReturnNullAndStopScore()
        {
            var data = GetMockData();
            var controller = new QuizController(data);

            // Проходим тест до конца
            controller.Answer(new[] { 0 });
            controller.Answer(new[] { 1 });

            // Проверка состояния финиша
            Assert.IsTrue(controller.IsFinished);
            Assert.IsNull(controller.GetCurrentQuestion(), "После финиша вопрос должен быть null");

            // Проверка защиты от повторных ответов (Score не должен расти выше максимума)
            controller.Answer(new[] { 1 });
            Assert.AreEqual(data.Count, controller.Score, "Счет не может превысить количество вопросов");
        }
    }
}