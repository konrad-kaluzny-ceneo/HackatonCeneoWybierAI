using Backend.Infrastructure.Services;
using Backend.Model;
using Backend.Model.Interfaces;
using Backend.Model.QuizInput;
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

        public int Add(Question question)
        {
            var id = _questions.Max(x => x.Id) + 1;
            question.Id = id;
            _questions.Add(question);
            return id;
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
