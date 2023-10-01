using Backend.Model;
using Backend.Model.Interfaces;
using Backend.Model.QuizOutput;
using Backend.OpenAI;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsGeneratorController : Controller
    {
        private readonly IQuestionaireGenerator _questionaireGenerator;
        private readonly IResultsService _resultsService;

        public QuestionsGeneratorController(
            IQuestionaireGenerator questionaireGenerator,
            IResultsService resultsService)
        {
            _questionaireGenerator = questionaireGenerator;
            _resultsService = resultsService;
        }

        [HttpGet("NextQuestion")]
        public async Task<ActionResult<Question>> NextQuestionAsync(int sessionid, string answerFromUser = "")
        {
            return await _questionaireGenerator.GetNextQuestion(sessionid, answerFromUser);
        }

        [HttpGet("GetQuizResults/{sessionId}")]
        public ActionResult<Model.Results> GenerateResults(int sessionId)
        {
            var r = _resultsService.GetResults(sessionId);
            return Ok(r);
        }
    }
}
