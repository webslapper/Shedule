using Shedule.Data;
using Shedule.Models;
using Microsoft.AspNetCore.Mvc;
namespace Shedule.Controllers;

[Route("/")]
public class SheduleController(IRepository<Lesson> lessonRepository) : Controller
{
	private readonly IRepository<Lesson> _lessonRepository = lessonRepository;
	public IActionResult Index() => View(new Group());

	[HttpPost]
	public async Task<IActionResult> GetSheduleAsync(Group group)
	{
		var lessons = await _lessonRepository.GetByConditionAsync(l => 
			l.Group.Course == group.Course &&
			l.Group.SpecDesc == group.SpecDesc &&
			l.Group.SubGroup == group.SubGroup &&
			l.Group.TrainDir == group.TrainDir);
		return View("Results", lessons);
		
	}
}