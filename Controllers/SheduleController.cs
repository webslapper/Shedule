using Shedule.Data;
using Shedule.Models;
using Microsoft.AspNetCore.Mvc;
using Shedule.Views.Shedule;
namespace Shedule.Controllers;

[Route("/")]
public class SheduleController(IRepository<Lesson> lessonRepository) : Controller
{
	private readonly IRepository<Lesson> _lessonRepository = lessonRepository;
	public IActionResult Index() => View(new GroupFormModel());

	[HttpPost]
	public async Task<IActionResult> GetSheduleAsync(GroupFormModel groupFormModel)
	{
		var lessons = await _lessonRepository.GetByConditionAsync(l => 
			l.Group.Course == groupFormModel.SelectedCourse &&
			l.Group.SpecDesc == groupFormModel.SelectedSpecDesc &&
			l.Group.SubGroup == groupFormModel.SelectedSubGroup &&
			l.Group.TrainDir == groupFormModel.SelectedTrainDir);
		return View("Results", lessons);
		
	}
}