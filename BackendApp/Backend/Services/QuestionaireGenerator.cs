using Backend.Model.Interfaces;
using Backend.Model.QuizOutput;
using Backend.OpenAI;

namespace Backend.Services;

public interface IQuestionaireGenerator
{
    Task<Question> GetNextQuestion(int sessionId, string answerFromUser);
}

public class QuestionaireGenerator : IQuestionaireGenerator
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly OpenAIQuestionProvider _questionsProvider;
    private readonly IQuestionHistoryRepository _questionHistoryRepository;

    public QuestionaireGenerator(IQuestionsRepository questionsRepository, OpenAIQuestionProvider questionsProvider, IQuestionHistoryRepository questionHistoryRepository)
    {
        _questionsRepository = questionsRepository;
        _questionsProvider = questionsProvider;
        _questionHistoryRepository = questionHistoryRepository;
    }

    public async Task<Question> GetNextQuestion(int sessionId, string answerFromUser)
    {
        _questionHistoryRepository.FillUpLastElementInHistory(sessionId, answerFromUser);

        var nextQuestion = await _questionsProvider.GetNextQuestion(
            _questionHistoryRepository.GetHistory(sessionId),
        answerFromUser
        );

        var question = new Question { Value = nextQuestion.Question };

        var id = _questionsRepository.Add(question);
        question.Id = id;
        nextQuestion.Id = id;

        _questionHistoryRepository.AddElementToHistory(sessionId, nextQuestion);

        return question;
    }
}
