using System.Linq;
using MyQuiz.Core.Abstractions;

namespace MyQuiz.Core.Strategies
{
    public class MultipleChoiceStrategy : IAnswerStrategy
    {
        public bool IsCorrect(int[] userIndices, int[] correctIndices)
        {
            if (userIndices == null || correctIndices == null)
            {
                return false;
            }

            return userIndices.Distinct().OrderBy(i => i)
                  .SequenceEqual(correctIndices.Distinct().OrderBy(i => i));
        }
    }
}
