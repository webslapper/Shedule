using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace Shedule.Views.Shedule;

public class GroupFormModel
{
	[BindProperty]
	public string SelectedSpecDesc { get; set; } = string.Empty;
	[BindProperty]
	public string SelectedTrainDir { get; set; } = string.Empty;
	[BindProperty]
	public string SelectedSubGroup { get; set; } = string.Empty;
	[BindProperty]
	public string SelectedCourse { get; set; } = string.Empty;

	public List<SelectListItem> SpecDescs { get; set; } = [];
	public List<SelectListItem> TrainDirs { get; set; } = [];
	public List<SelectListItem> SubGroups { get; set; } = [];
	public List<SelectListItem> Courses { get; set; } = [];

	public bool Saved { get; set; } = false;
}