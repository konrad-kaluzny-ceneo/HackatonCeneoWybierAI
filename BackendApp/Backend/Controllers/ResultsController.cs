using Backend.Model.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsRepository resultsRepository;

        public ResultsController(IResultsRepository resultsRepository)
        {
            this.resultsRepository = resultsRepository;
        }

        [HttpGet("GetResults/{sessionId}")]
        public ActionResult<Model.Results> GetResults(int sessionId)
        {
            return resultsRepository.GetResults(sessionId);
        }
    }
}
