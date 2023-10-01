using Backend.Controllers;

namespace Backend.Model.Interfaces
{
    public interface IQuestionsRepository
    {
        Task<List<QuestionWithAnswer>> GetQuestionsWithAnswers(QuestionnaireAnswers questionnaire);
    }
}
