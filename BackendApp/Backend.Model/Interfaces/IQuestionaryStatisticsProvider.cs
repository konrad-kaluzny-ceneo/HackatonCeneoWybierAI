namespace Backend.Model.Interfaces
{
    public interface IQuestionaryStatisticsProvider
    {
        Task<string> GetExpertDescription(List<Academy> academies);
        Task<Dictionary<string, decimal>> GetMatchRates(List<QuestionWithAnswer> questionnaireHistory, List<string> fieldsOfStudies);
        Task<Dictionary<string, decimal>> GetMatchRates(List<QuestionAndAnswer> questions, List<string> fieldsOfStudies);
    }
}
