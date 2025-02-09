using Shedule.Data;
using Shedule.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRepository<Lesson>, Repository<Lesson>>();
builder.Services.AddTransient<IRepository<Group>, Repository<Group>>();
builder.Services.AddTransient<IRepository<Teacher>, Repository<Teacher>>();
builder.Services.AddTransient<IRepository<Student>, Repository<Student>>();
builder.Services.AddTransient<IRepository<DisciplineType>, Repository<DisciplineType>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
