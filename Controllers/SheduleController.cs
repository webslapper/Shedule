using Shedule.Data;
using Shedule.Models;
using Microsoft.AspNetCore.Mvc;
namespace Shedule.Controllers;

[ApiController]
[Route("api/controller")]
public class SheduleController(IRepository<Lesson> lessonRepository) : Controller
{
	private readonly IRepository<Lesson> _lessonRepository = lessonRepository;
	public IActionResult Index() => View(new Group());

	[HttpPost]
	public async Task<IActionResult> GetSheduleAsync(Group group)
	{
		var lessons = await _lessonRepository.GetByConditionAsync(l => l.Group == group);
		return View("Results", lessons);
	}
}