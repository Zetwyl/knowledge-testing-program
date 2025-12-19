using MyQuiz.Core.Abstractions;
using System;

namespace MyQuiz.Core.Domain
{
    public class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; } = Array.Empty<string>();
        public int[] CorrectIndices { get; set; }
        public IAnswerStrategy Strategy { get; set; }
    }
}
