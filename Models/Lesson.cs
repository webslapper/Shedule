namespace Shedule.Models;

public class Lesson
{
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string Discipline { get; set; }
    public DisciplineType DisciplineType { get; set; }
    public Group Group { get; set; }
    public Teacher Teacher { get; set; }
    public string Date { get; set; }
}