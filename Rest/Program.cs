using Application.Common.Mapping;
using Application.Interfaces;
using Application.Services;
using Domain.Repositories.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MyCleanArchitecture;
using MyCleanArchitecture.Filtres;
using MyCleanArchitecture.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = AppDomain.CurrentDomain.Load("Application");

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ApplicationDbContext).Assembly));
});


builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IToDoListService,ToDoListService>();
builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();

builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

builder.Services.AddSingleton<AcceptTypeMiddleware>();
builder.Services.AddTransient<ExceptionFilter>();


builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseInMemoryDatabase("Memory");
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();


app.UseMiddleware<AcceptTypeMiddleware>();

app.MapControllers();

app.PrepareDatabase();

app.Run();
