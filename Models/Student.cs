namespace Shedule.Models;

public class Student
{
    public Guid Id { get; set; } = new Guid();

    public virtual Group? Group { get; set; }
    public string Name { get; set; }
}