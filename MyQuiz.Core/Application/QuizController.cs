using System;
using System.Collections.Generic;
using MyQuiz.Core.Abstractions;
using MyQuiz.Core.Domain;

namespace MyQuiz.Core.Application
{
    public class QuizController : IQuizService
    {
        private readonly List<Question> _questions;
        private int _currentIndex;
        public int Score { get; private set; }

        private List<(Question Question, int[] UserAnswers)> _mistakes = new List<(Question Question, int[] UserAnswers)>();
        public IReadOnlyList<(Question Question, int[] UserAnswers)> Mistakes => _mistakes;

        public event Action<int, int> OnProgressChanged;

        public QuizController(List<Question> questions)
        {
            _questions = questions;
            _currentIndex = 0;
        }

        public void Answer(int[] selectedIndices)
        {
            if (IsFinished)
            {
                return;
            }

            var current = GetCurrentQuestion();
            bool isCorrect = current.Strategy.IsCorrect(selectedIndices, current.CorrectIndices);

            if (isCorrect)
            {
                Score++;
            }
            else
            {
                _mistakes.Add((current, selectedIndices));
            }

            _currentIndex++;
            OnProgressChanged?.Invoke(_currentIndex, _questions.Count);
        }

        public Question GetCurrentQuestion()
        {
            if (_currentIndex < 0 || _currentIndex >= _questions.Count)
            {
                return null;
            }
            return _questions[_currentIndex];
        }
        public bool IsFinished => _currentIndex >= _questions.Count;
        public int TotalQuestions => _questions.Count;
    }
}