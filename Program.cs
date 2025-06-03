using DevHabitTracker.Database;
using DevHabitTracker.Extensions;
using DevHabitTracker.Middleware;
using DevHabitTracker.Services;
using DevHabitTracker.Services.Interfaces;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAuthenticationServices();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    })
    .AddNewtonsoftJson()
    .AddXmlSerializerFormatters();


builder.Services.AddProblemDetails();


builder.Services.AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddApiVersioning().AddMvc();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IHabitService, HabitService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();
