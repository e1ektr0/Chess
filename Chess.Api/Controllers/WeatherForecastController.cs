using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<WeatherForecast> Get()
    {
        return new WeatherForecast
        {
            Summary = "Fuck you"
        };
    }
}