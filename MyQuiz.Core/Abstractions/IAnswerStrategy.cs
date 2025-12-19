namespace MyQuiz.Core.Abstractions
{
    public interface IAnswerStrategy
    {
        bool IsCorrect(int[] selectedIndices, int[] correctIndices);
    }
}
