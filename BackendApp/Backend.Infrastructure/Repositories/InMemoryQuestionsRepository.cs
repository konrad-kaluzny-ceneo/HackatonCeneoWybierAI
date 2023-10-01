using Backend.Controllers;
using Backend.Infrastructure.Services;
using Backend.Model;
using Backend.Model.Interfaces;
using Backend.Model.QuizOutput;

namespace Backend.Infrastructure.Repositories
{
    public class InMemoryQuestionsRepository : IQuestionsRepository
    {
        private readonly List<Question> _questions;
        private const string Yes = "Tak";
        private const string No = "Nie";

        public InMemoryQuestionsRepository(IQuestionsProvider questionsProvider)
        {
            _questions = questionsProvider.GetQuestions().ToList();
        }

        public Task<List<QuestionWithAnswer>> GetQuestionsWithAnswers(QuestionnaireAnswers questionnaire)
        {
            var result = questionnaire.Answers
                .Join(_questions,
                    req => req.QuestionId,
                    mem => mem.Id,
                    (req, mem) => new QuestionWithAnswer
                    {
                        Question = mem.Value,
                        Answer = req.Answer ? Yes : No
                    }).ToList();

            return Task.FromResult(result);
        }
    }
}
