using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model.Interfaces;
using Backend.OpenAI.QuestionProviderStructures;

namespace Backend.Infrastructure.Repositories
{
    public class InMemoryQuestionHistoryRepository : IQuestionHistoryRepository
    {
        public Dictionary<int, List<QuestionAndAnswer>> History { get; set; } = new();

        public List<QuestionAndAnswer> GetHistory(int sessionId)
        {
            if (!History.ContainsKey(sessionId))
            {
                return new List<QuestionAndAnswer>();
            }

            return History[sessionId];
        }

        public void AddElementToHistory(int sessionId, QuestionAndAnswer element)
        {
            if (!History.ContainsKey(sessionId))
            {
                History.Add(sessionId, new List<QuestionAndAnswer>());
            }

            History[sessionId].Add(element);
        }

        public void FillUpLastElementInHistory(int sessionId, string userAnswer)
        {
            if (History.ContainsKey(sessionId) && History[sessionId].Count > 0)
            {
                History[sessionId].Last().Answer = userAnswer;
            }
        }
    }
}
