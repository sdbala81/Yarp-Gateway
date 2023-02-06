using Microsoft.AspNetCore.Mvc;

namespace StudentService.Controllers;

[ApiController]
[Route("[controller]s")]
public class StudentController : ControllerBase
{
    private static readonly string[] StudentNames = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetStudentNames")]
    public IEnumerable<Student> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Student
            {
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                FirstName = StudentNames[Random.Shared.Next(StudentNames.Length)],
                LastName = StudentNames[Random.Shared.Next(StudentNames.Length)]
            })
            .ToArray();
    }
}