using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;

namespace Shedule.Views.Shedule;

public class GroupFormModel : PageModel{
    [BindProperty]
    public string SelectedSpecDesc {get; set;} = string.Empty;
    [BindProperty]
    public string SelectedTrainDir {get; set;} = string.Empty;
    [BindProperty]
    public string SelectedSubGroup {get; set;} = string.Empty;
    [BindProperty]
    public string SelectedCourse {get; set;} = string.Empty;

    public List<SelectListItem> SpecDescs {get; set;} = [];
    public List<SelectListItem> TrainDirs {get; set;} = [];
    public List<SelectListItem> SubGroups {get; set;} = [];
    public List<SelectListItem> Courses {get; set;} = [];

    public bool Saved { get; set; } = false;

    public GroupFormModel() => OnGet();
    public void OnGet(){
        SpecDescs = [
            new SelectListItem("Информатика", "Информатика"), 
            new SelectListItem("Начальное образование","Начальное образование"),
            new SelectListItem("Физкультура","Физкультура")
        ];
        TrainDirs = [
            new SelectListItem("Программирование", "Программирование"), 
            new SelectListItem("Педагогика","Педагогика"),
            new SelectListItem("Молекуряная физика","Молекуряная физика")
        ];
        SubGroups = [
            new SelectListItem("А", "А"), 
            new SelectListItem("Б","Б"),
            new SelectListItem("В","В")
        ];
        Courses = [
            new SelectListItem("1", "1"), 
            new SelectListItem("2","2"),
            new SelectListItem("3","3")
        ];
    }

    public IActionResult OnPost(){
        if (!string.IsNullOrEmpty(SelectedCourse) &&
            !string.IsNullOrEmpty(SelectedSpecDesc) &&
            !string.IsNullOrEmpty(SelectedTrainDir) &&
            !string.IsNullOrEmpty(SelectedSubGroup))
            {
                Saved = true;
            }
        OnGet();
        return Page();
    }
}