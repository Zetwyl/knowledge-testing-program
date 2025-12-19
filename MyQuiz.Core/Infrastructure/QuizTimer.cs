using System;
using System.Timers;
using MyQuiz.Core.Abstractions;

namespace MyQuiz.Core.Infrastructure
{
    public class QuizTimer : ITimerService
    {
        private Timer _timer;
        private int _secondsLeft;

        public event Action<int> OnTick;
        public event Action OnElapsed;

        public void Start(int seconds)
        {
            _secondsLeft = seconds;
            Stop(); // Останавливаем старый, если был

            _timer = new Timer(1000);
            _timer.Elapsed += (s, e) =>
            {
                _secondsLeft--;
                OnTick?.Invoke(_secondsLeft);

                if (_secondsLeft <= 0)
                {
                    Stop();
                    OnElapsed?.Invoke();
                }
            };
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}
