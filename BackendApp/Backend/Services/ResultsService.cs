using Backend.Model;
using Backend.Model.Interfaces;

namespace Backend.Services;

public interface IResultsService
{
    Task<Model.Results> GetResults(int sessionId);
}

public class ResultsService : IResultsService
{
    private readonly IResultsRepository _resultsRepository;
    private readonly IQuestionHistoryRepository _questionHistoryRepository;
    private readonly IFieldsOfStudiesService _fieldsOfStudiesService;
    private readonly IQuestionaryStatisticsProvider _questionaryStatisticsProvider;

    public ResultsService(IResultsRepository resultsRepository, IQuestionHistoryRepository questionHistoryRepository, IFieldsOfStudiesService fieldsOfStudiesService, IQuestionaryStatisticsProvider questionaryStatisticsProvider)
    {
        _resultsRepository = resultsRepository;
        _questionHistoryRepository = questionHistoryRepository;
        _fieldsOfStudiesService = fieldsOfStudiesService;
        _questionaryStatisticsProvider = questionaryStatisticsProvider;
    }


    public async Task<Model.Results> GetResults(int sessionId)
    {
        var result = _resultsRepository.GetResults(sessionId);

        if (result is not null)
        {
            return result;
        }

        var answers = _questionHistoryRepository.GetHistory(sessionId);
        var fieldOfStudyProposals = await _fieldsOfStudiesService.GetAcademies(answers);
        var expertDescription = await _questionaryStatisticsProvider.GetExpertDescription(fieldOfStudyProposals);
        var matchResult = await _fieldsOfStudiesService.GetMatchRates(answers, fieldOfStudyProposals);

        var newResults = new Model.Results
        {
            FieldOfStudyProposals = fieldOfStudyProposals
                .GroupBy(f => f.Name)
                .Select(field => new MatchResult
                {
                    Name = field.Key,
                    ManagingInstitutions = field.Select(x => x.ManagingInstitution).Distinct().ToList(),
                    PercentageMatch = matchResult.TryGetValue(field.Key.ToLower(), out var val) ? val : 0
                }).ToList(),
            ExpertDescription = expertDescription
        };

        _resultsRepository.SaveResults(newResults);

        return newResults;
    }
}
