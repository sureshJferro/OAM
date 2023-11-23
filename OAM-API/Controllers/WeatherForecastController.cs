using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OAM.Core.Entities;
using System.Security.Cryptography;

namespace OAM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly OamDevContext _context;


        public WeatherForecastController(ILogger<WeatherForecastController> logger,OamDevContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            User user = new User();
            user.UserName = "suresh";
            user.Email = "suresh@gmail.com";
            using (var hmac = new HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Suresh"));
            }
            user.RoleId = 1;
            _context.Users.Add(user);
            _context.SaveChanges();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}