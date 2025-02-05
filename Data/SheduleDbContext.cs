using Microsoft.EntityFrameworkCore;
using Shedule.Models;

namespace Shedule.Data;

public class SheduleDbContext: DbContext
{
    public SheduleDbContext(DbContextOptions<SheduleDbContext> options): base (options) { }

    public DbSet<DisciplineType> DisciplineTypes { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
}