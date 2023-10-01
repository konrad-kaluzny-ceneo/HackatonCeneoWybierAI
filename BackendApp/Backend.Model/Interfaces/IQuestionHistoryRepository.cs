using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.OpenAI;
using Backend.OpenAI.QuestionProviderStructures;

namespace Backend.Model.Interfaces
{
    public interface IQuestionHistoryRepository
    {
        public List<QuestionAndAnswer> GetHistory(int sessionId);
        void FillUpLastElementInHistory(int sessionId, string userAnswer);
        void AddElementToHistory(int sessionId, QuestionAndAnswer element);
    }
}
