using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Shedule.Data;
using Shedule.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace Shedule.Controllers;

[ApiController]
[Route("api/controller")]

public class SheduleController(IRepository<Lesson> lessonRepository,
    IRepository<Group> groupRepository,
    IRepository<DisciplineType> disciplineTypeRepository,
    IRepository<Student> studentRepository,
    IRepository<Teacher> teacherRepository) : ControllerBase
{
    private IRepository<DisciplineType> _disciplineTypeRepository = disciplineTypeRepository;
    private IRepository<Group> _groupRepository = groupRepository;
    private IRepository<Lesson> _lessonRepository = lessonRepository;
    private IRepository<Student> _studentRepository = studentRepository;
    private IRepository<Teacher> _teacherRepository = teacherRepository;
    //public SheduleController(lessonRepository) 
    //{
    //    _lessonRepository = lessonRepository;
    //}
    public IActionResult Index() => View();

    [HttpPost]
    Task<IActionResult> GetSheduleAsync(Group group)
    {
        var lessons = _lessonRepository._dbset
            .Where(l => l.Contains(group))
            .ToList();
        return View("Results", lessons);
    }
}