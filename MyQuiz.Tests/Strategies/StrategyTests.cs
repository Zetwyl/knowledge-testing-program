using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyQuiz.Core.Strategies;
using MyQuiz.Core.Abstractions;

namespace MyQuiz.Tests
{
    [TestClass]
    public class StrategyTests
    {
        private readonly IAnswerStrategy _single = new SingleChoiceStrategy();
        private readonly IAnswerStrategy _multiple = new MultipleChoiceStrategy();

        [TestMethod]
        [DataRow(new[] { 0 }, new[] { 0 }, true, DisplayName = "Верный одиночный выбор")]
        [DataRow(new[] { 1 }, new[] { 0 }, false, DisplayName = "Неверный одиночный выбор")]
        [DataRow(new int[0], new[] { 0 }, false, DisplayName = "Пустой ввод в одиночном выборе")]
        public void SingleChoice_Tests(int[] user, int[] correct, bool expected)
        {
            var result = _single.IsCorrect(user, correct);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(new[] { 0, 1 }, new[] { 0, 1 }, true, DisplayName = "Идеальное совпадение")]
        [DataRow(new[] { 1, 0 }, new[] { 0, 1 }, true, DisplayName = "Верно (порядок не важен)")]
        [DataRow(new[] { 0, 1, 2 }, new[] { 0, 1 }, false, DisplayName = "Лишний вариант (перебор)")]
        [DataRow(new[] { 0 }, new[] { 0, 1 }, false, DisplayName = "Неполный выбор (недобор)")]
        [DataRow(new[] { 2, 3 }, new[] { 0, 1 }, false, DisplayName = "Совершенно неверные индексы")]
        [DataRow(new int[0], new[] { 0, 1 }, false, DisplayName = "Пустой ввод в множ. выборе")]
        public void MultipleChoice_Tests(int[] user, int[] correct, bool expected)
        {
            var result = _multiple.IsCorrect(user, correct);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Strategies_ShouldHandleNullInput()
        {
            // Проверка на null — критически важная защита от падения приложения
            Assert.IsFalse(_single.IsCorrect(null, new[] { 0 }), "SingleChoice упал при null");
            Assert.IsFalse(_multiple.IsCorrect(null, new[] { 0, 1 }), "MultipleChoice упал при null");
        }
    }
}