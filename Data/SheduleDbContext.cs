using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shedule.Models;

namespace Shedule.Data;
public class SheduleDbContext(DbContextOptions<SheduleDbContext> options) : DbContext(options)
{
	public DbSet<DisciplineType> DisciplineTypes { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<Lesson> Lessons { get; set; }
	public async Task SetDataAsync()
	{
		var jsonFilePath = "StartData.json";
		var jsonData = await File.ReadAllTextAsync(jsonFilePath);

		var data = JsonConvert.DeserializeObject<StartData>(jsonData);

		if (data != null)
		{
			foreach (var disciplineType in data.DisciplineTypes!)
			{
				if (!await DisciplineTypes.AnyAsync(x => x.Id == disciplineType.Id))
				{
					DisciplineTypes.Add(disciplineType);
				}
			}

			foreach (var teacher in data.Teachers!)
			{
				if (!await Teachers.AnyAsync(x => x.Id == teacher.Id))
				{
					Teachers.Add(teacher);
				}
			}

			foreach (var group in data.Groups!)
			{
				var existingGroup = await Groups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == group.Id);
				if (existingGroup == null)
				{
					Groups.Add(group);
				}
			}

			foreach (var student in data.Students!)
			{
				var group = await Groups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == student.Group.Id);
				if (group != null)
				{
					var existingStudent = await Students.AsNoTracking().FirstOrDefaultAsync(x => x.Name == student.Name && x.Group.Id == student.Group.Id);
					if (existingStudent == null)
					{
						student.Group = group;
						Students.Add(student);
					}
				}
			}

			foreach (var lesson in data.Lessons!)
			{
				var disciplineType = await DisciplineTypes.FirstOrDefaultAsync(x => x.Id == lesson.DisciplineType.Id);
				var group = await Groups.FirstOrDefaultAsync(x => x.Id == lesson.Group.Id);
				var teacher = await Teachers.FirstOrDefaultAsync(x => x.Id == lesson.Teacher.Id);

				if (disciplineType != null && group != null && teacher != null)
				{
					var lessonEntity = new Lesson
					{
						StartTime = lesson.StartTime,
						EndTime = lesson.EndTime,
						Discipline = lesson.Discipline,
						DisciplineType = disciplineType,
						Group = group,
						Teacher = teacher,
						Date = lesson.Date
					};

					Lessons.Add(lessonEntity);
				}
			}

			await SaveChangesAsync();
		}
	}
}
public class StartData
{
	public List<DisciplineType>? DisciplineTypes { get; set; }
	public List<Group>? Groups { get; set; }
	public List<Lesson>? Lessons { get; set; }
	public List<Student>? Students { get; set; }
	public List<Teacher>? Teachers { get; set; }
}