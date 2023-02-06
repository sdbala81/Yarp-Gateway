using Microsoft.AspNetCore.Mvc;

namespace CoursesService.Controllers;

[ApiController]
[Route("[controller]s")]
public class CourseController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Social Studies", "Science", "Maths", "Chemistry", "Physics", "Music", "Dance", "Art", "Tamil", "Religion"
    };

    private readonly ILogger<CourseController> _logger;

    public CourseController(ILogger<CourseController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<Course> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Course
            {
                PublishedDate = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                CourseName = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}