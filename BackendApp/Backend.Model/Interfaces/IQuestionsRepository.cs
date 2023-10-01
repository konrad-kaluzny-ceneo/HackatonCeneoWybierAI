using Backend.Model.QuizInput;
using Backend.Model.QuizOutput;

namespace Backend.Model.Interfaces;

public interface IQuestionsRepository
{
    int Add(Question question);
    Task<List<QuestionWithAnswer>> GetQuestionsWithAnswers(QuestionnaireAnswers questionnaire);
}
