using Backend.Model.QuizOutput;

namespace Backend.Infrastructure.Services
{
    public interface IQuestionsProvider
    {
        Question[] GetQuestions();
    }
}