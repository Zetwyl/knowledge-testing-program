using MyQuiz.Core.Abstractions;

namespace MyQuiz.Core.Strategies
{
    public class SingleChoiceStrategy : IAnswerStrategy
    {
        public bool IsCorrect(int[] userIndices, int[] correctIndices)
        {
            if (userIndices == null || correctIndices == null)
            {
                return false;
            }

            if (userIndices.Length == 0)
            {
                return false;
            }

            return userIndices[0] == correctIndices[0];
        }
    }
}
