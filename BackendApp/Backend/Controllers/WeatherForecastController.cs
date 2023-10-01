using Backend.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IAcademyRepository _academyRepository;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IAcademyRepository academyRepository)
    {
        _logger = logger;
        _academyRepository = academyRepository;
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    //public async Task<IEnumerable<WeatherForecast>> Get()
    //{
    //    //var test = await _academyRepository.GetAcademies();
    //    //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //    //{
    //    //    Date = DateTime.Now.AddDays(index),
    //    //    TemperatureC = Random.Shared.Next(-20, 55),
    //    //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    //})
    //    //.ToArray();
    //}
}