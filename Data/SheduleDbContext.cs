using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shedule.Models;

namespace Shedule.Data;
public class SheduleDbContext : DbContext
{
	public DbSet<DisciplineType> DisciplineTypes { get; set; }
	public DbSet<Teacher> Teachers { get; set; }
	public DbSet<Group> Groups { get; set; }
	public DbSet<Student> Students { get; set; }
	public DbSet<Lesson> Lessons { get; set; }

	public SheduleDbContext(DbContextOptions<SheduleDbContext> options) : base(options) { }

	public async Task SetDataAsync(string jsonFilePath)
	{
		if (!File.Exists(jsonFilePath))
		{
			throw new FileNotFoundException($"JSON file not found: {jsonFilePath}");
		}

		var jsonData = await File.ReadAllTextAsync(jsonFilePath);
		var dataDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);

		if (dataDict == null) return;

		foreach (var entry in dataDict)
		{
			var entityType = GetType().GetProperties()
				.FirstOrDefault(p => p.Name == entry.Key && p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))?
				.PropertyType.GetGenericArguments()[0];

			if (entityType == null) continue;

			var dbSet = GetType().GetProperty(entry.Key)?.GetValue(this);
			var method = typeof(JsonConvert)
				.GetMethod("DeserializeObject", new[] { typeof(string) })?
				.MakeGenericMethod(typeof(List<>).MakeGenericType(entityType));

			var entityList = method?.Invoke(null, new object[] { entry.Value.ToString()! }) as System.Collections.IEnumerable;

			if (dbSet != null && entityList != null && !(dbSet as IQueryable<object>)!.Any())
			{
				var addRangeMethod = dbSet.GetType().GetMethod("AddRange");
				addRangeMethod?.Invoke(dbSet, new object[] { entityList });
			}
		}

		await SaveChangesAsync();
	}
}