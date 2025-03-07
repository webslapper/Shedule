using Shedule.Data;
using Shedule.Models;
using Microsoft.AspNetCore.Mvc;
using Shedule.Views.Shedule;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Shedule.Controllers;

[Route("/")]
public class SheduleController(IRepository<Lesson> lessonRepository) : Controller
{
	private readonly IRepository<Lesson> _lessonRepository = lessonRepository;
	public IActionResult Index()
	{
		var model = new GroupFormModel();
		InitializeModel(model);
		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> GetSheduleAsync([FromForm] GroupFormModel groupFormModel)
	{
		if (!string.IsNullOrEmpty(groupFormModel.SelectedCourse) &&
			!string.IsNullOrEmpty(groupFormModel.SelectedSpecDesc) &&
			!string.IsNullOrEmpty(groupFormModel.SelectedTrainDir) &&
			!string.IsNullOrEmpty(groupFormModel.SelectedSubGroup))
		{
			groupFormModel.Saved = true;
		}

		var lessons = await _lessonRepository.GetByConditionAsync(l =>
			l.Group.Course == groupFormModel.SelectedCourse &&
			l.Group.SpecDesc == groupFormModel.SelectedSpecDesc &&
			l.Group.SubGroup == groupFormModel.SelectedSubGroup &&
			l.Group.TrainDir == groupFormModel.SelectedTrainDir);

		InitializeModel(groupFormModel);
		return View("Results", lessons);
	}
	private static void InitializeModel(GroupFormModel model)
	{
		model.SpecDescs = [
			new SelectListItem("Информатика", "Информатика"),
			new SelectListItem("Начальное образование","Начальное образование"),
			new SelectListItem("Физкультура","Физкультура")
		];
		model.TrainDirs = [
			new SelectListItem("Программирование", "Программирование"),
			new SelectListItem("Педагогика","Педагогика"),
			new SelectListItem("Молекулярная физика","Молекулярная физика")
		];
		model.SubGroups = [
			new SelectListItem("А", "А"),
			new SelectListItem("Б","Б"),
			new SelectListItem("В","В")
		];
		model.Courses = [
			new SelectListItem("1", "1"),
			new SelectListItem("2","2"),
			new SelectListItem("3","3")
		];
	}
}