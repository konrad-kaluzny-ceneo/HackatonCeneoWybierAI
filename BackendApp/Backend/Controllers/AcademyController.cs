using Backend.Model;
using Backend.Model.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademyController : ControllerBase
    {
        private readonly IAcademyRepository _academyRepository;

        public AcademyController(IAcademyRepository academyRepository)
        {
            _academyRepository = academyRepository;
        }

        [HttpPost("GetAcademiesFiltered")]
        public async Task<IActionResult> GetAcademiesFiltered(GetAcademySearchParams searchParameters)
        {
            var result = await _academyRepository.GetAcademies(searchParameters);
            return Ok(result);
        }
    }
}
