using Microsoft.EntityFrameworkCore;
using Shedule.Models;

namespace Shedule.Data;

public class SheduleDbContext: DbContext
{
    public DbSet<DisciplineType> DisciplineTypes { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lesson> Lessons { get; set; }

    public SheduleDbContext(DbContextOptions<SheduleDbContext> options): base (options) { }

    public async Task SetDataAsync()
    {
        if (!DisciplineTypes.Any())
        {
            var disciplineTypesData = await File.ReadAllTextAsync("DisciplineTypes.json");
            var disciplineTypes = JsonConvert.DeserializeObject<List<DisciplineType>>(disciplineTypesData);
            if(disciplineTypes != null)
            {
                await DisciplineTypes.AddRangeAsync(disciplineTypes);
            }
        }
        if (!Groups.Any())
        {
            var groupsData = await File.ReadAllTextAsync("Groups.json");
            var groups = JsonConvert.DeserializeObject<List<Group>>(groupsData);
            if (groups != null)
            {
                await Groups.AddRangeAsync(groups);
            }
        }
        if (!Lessons.Any())
        {
            var lessonsData = await File.ReadAllTextAsync("Lessons.json");
            var lessons = JsonConvert.DeserializeObject<List<Lesson>>(lessonsData);
            if (lessons != null)
            {
                await Lessons.AddRangeAsync(lessons);
            }
        }
        if (!Students.Any())
        {
            var studentsData = await File.ReadAllTextAsync("Students.json");
            var students = JsonConvert.DeserializeObject<List<Student>>(studentsData);
            if (students != null)
            {
                await Students.AddRangeAsync(students);
            }
        }
        if (!Teachers.Any())
        {
            var teachersData = await File.ReadAllTextAsync("Teachers.json");
            var teachers = JsonConvert.DeserializeObject<List<Teacher>>(teachersData);
            if (teachers != null)
            {
                await Teachers.AddRangeAsync(teachers);
            }
        }
        await SaveChangesAsync();
    }
}