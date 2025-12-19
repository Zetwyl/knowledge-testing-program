using System;
using System.Collections.Generic;
using MyQuiz.Core.Domain;

namespace MyQuiz.Core.Abstractions
{
    public interface IQuizService
    {
        // Событие для обновления прогресс-бара
        event Action<int, int> OnProgressChanged;

        // Методы управления
        void Answer(int[] selectedIndices);
        Question GetCurrentQuestion();

        // Свойства состояния
        bool IsFinished { get; }
        int Score { get; }
        int TotalQuestions { get; }

        // Список ошибок для отчета (IReadOnlyList защищает данные от изменений извне)
        IReadOnlyList<(Question Question, int[] UserAnswers)> Mistakes { get; }
    }
}