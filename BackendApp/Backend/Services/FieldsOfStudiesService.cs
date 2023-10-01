using Backend.Model;
using Backend.Model.Interfaces;
using Backend.Model.QuizInput;

namespace Backend.Services
{
    public interface IFieldsOfStudiesService
    {
        Task<List<Academy>> GetAcademies(QuestionnaireAnswers questionnaireAnswers);
        Task<List<Academy>> GetAcademies(List<QuestionAndAnswer> questions);
        Task<Dictionary<string, decimal>> GetMatchRates(QuestionnaireAnswers questionnaireAnswers, List<Academy> fieldsOfStudies);
        Task<Dictionary<string, decimal>> GetMatchRates(List<QuestionAndAnswer> questions, List<Academy> fieldsOfStudies);
    }

    public class FieldsOfStudiesService : IFieldsOfStudiesService
    {
        private readonly ISearchParamsGenerator _searchParamsGenerator;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IAcademyRepository _academyRepository;
        private readonly IQuestionaryStatisticsProvider _questionaryStatisticsProvider;

        public FieldsOfStudiesService(ISearchParamsGenerator searchParamsGenerator, IQuestionsRepository questionsRepository, IAcademyRepository academyRepository, IQuestionaryStatisticsProvider questionaryStatisticsProvider)
        {
            _searchParamsGenerator = searchParamsGenerator;
            _questionsRepository = questionsRepository;
            _academyRepository = academyRepository;
            _questionaryStatisticsProvider = questionaryStatisticsProvider;
        }

        public async Task<List<Academy>> GetAcademies(QuestionnaireAnswers questionnaireAnswers)
        {
            var questionsWithAnswers = await _questionsRepository.GetQuestionsWithAnswers(questionnaireAnswers);
            var searchParameters = await _searchParamsGenerator.Generate(questionsWithAnswers);
            return await _academyRepository.GetAcademies(searchParameters);
        }

        public async Task<List<Academy>> GetAcademies(List<QuestionAndAnswer> questions)
        {
            var searchParameters = await _searchParamsGenerator.Generate(questions);
            return await _academyRepository.GetAcademies(searchParameters);
        }

        public async Task<Dictionary<string, decimal>> GetMatchRates(QuestionnaireAnswers questionnaireAnswers, List<Academy> fieldsOfStudies)
        {
            var questionsWithAnswers = await _questionsRepository.GetQuestionsWithAnswers(questionnaireAnswers);
            return await _questionaryStatisticsProvider.GetMatchRates(questionsWithAnswers, fieldsOfStudies.Select(f => f.Name.ToLower()).ToList());
        }

        public async Task<Dictionary<string, decimal>> GetMatchRates(List<QuestionAndAnswer> questions, List<Academy> fieldsOfStudies)
        {
            return await _questionaryStatisticsProvider.GetMatchRates(questions, fieldsOfStudies.Select(f => f.Name.ToLower()).ToList());
        }
    }
}
