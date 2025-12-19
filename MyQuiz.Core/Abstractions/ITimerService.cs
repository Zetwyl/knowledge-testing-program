using System;

namespace MyQuiz.Core.Abstractions
{
    public interface ITimerService
    {
        event Action<int> OnTick;
        event Action OnElapsed;

        void Start(int seconds);
        void Stop();
    }
}