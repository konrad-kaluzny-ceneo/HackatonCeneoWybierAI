namespace Backend.Model.Interfaces;

public interface IQuestionHistoryRepository
{
    public List<QuestionAndAnswer> GetHistory(int sessionId);
    void FillUpLastElementInHistory(int sessionId, string userAnswer);
    void AddElementToHistory(int sessionId, QuestionAndAnswer element);
}
