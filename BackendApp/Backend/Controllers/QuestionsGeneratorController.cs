using Backend.Model;
using Backend.Model.Interfaces;
using Backend.OpenAI;
using Backend.OpenAI.QuestionProviderStructures;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class QuestionsGeneratorController : Controller
    {
        private OpenAIQuestionProvider _questionsProvider;
        private IQuestionHistoryRepository _questionHistoryRepository;

        public QuestionsGeneratorController(
            OpenAIQuestionProvider questionProvider,
            IQuestionHistoryRepository questionHistoryRepository
            )
        {
            _questionsProvider = questionProvider;
            _questionHistoryRepository = questionHistoryRepository;
        }

        [HttpGet("NextQuestion")]
        public async Task<ActionResult<QuestionAndAnswer>> NextQuestionAsync(int sessionid, string answerFromUser = "")
        {
            _questionHistoryRepository.FillUpLastElementInHistory(sessionid, answerFromUser);

            var nextQuestion = await _questionsProvider.GetNextQuestion(
                _questionHistoryRepository.GetHistory(sessionid),
                answerFromUser
            );

            _questionHistoryRepository.AddElementToHistory(sessionid, nextQuestion);

            return nextQuestion;
        }
    }
}
