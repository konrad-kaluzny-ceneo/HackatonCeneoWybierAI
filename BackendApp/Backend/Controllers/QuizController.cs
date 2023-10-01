using Backend.Infrastructure.Services;
using Backend.Model;
using Backend.Model.Interfaces;
using Backend.Model.QuizOutput;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IResultsRepository _resultsRepository;
        private readonly IQuestionsProvider _questionsProvider;
        private readonly IFieldsOfStudiesService _fieldsOfStudiesService;
        private readonly IQuestionaryStatisticsProvider _questionaryStatisticsProvider;

        public QuizController(IResultsRepository resultsRepository, IQuestionsProvider questionsProvider, IFieldsOfStudiesService fieldsOfStudiesService, IQuestionaryStatisticsProvider questionaryStatisticsProvider)
        {
            _questionsProvider = questionsProvider;
            _resultsRepository = resultsRepository;
            _fieldsOfStudiesService = fieldsOfStudiesService;
            _questionaryStatisticsProvider = questionaryStatisticsProvider;
        }

        [HttpGet("GetQuizQuestions")]
        public ActionResult<Questionnaire> GetQuizQuestions()
        {
            return new Questionnaire
            {
                Questions = _questionsProvider.GetQuestions().ToList()
            };
        }

        [HttpPost("PostQuizResults")]
        public async Task<ActionResult<int>> PostQuizResults(QuestionnaireAnswers questionnaireAnswers)
        {
            var fieldOfStudyProposals = await _fieldsOfStudiesService.GetAcademies(questionnaireAnswers);
            var expertDescription = await _questionaryStatisticsProvider.GetExpertDescription(fieldOfStudyProposals);
            var matchResult = await _fieldsOfStudiesService.GetMatchRates(questionnaireAnswers, fieldOfStudyProposals);

            var sessionId = _resultsRepository.SaveResults(new Model.Results
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
            });

            return Ok(sessionId);
        }

    }
}
