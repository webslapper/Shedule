namespace Shedule.Models;

public class Student
{
    public Guid Id { get; set; }

    public Group Group { get; set; }
    public string Name { get; set; }
}