using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Reflection;
using TestTask1.Data;
using TestTask1.Repositories;
using TestTask1.Services;

var builder = WebApplication.CreateBuilder(args);

//Добавил Serilog
Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Debug()
	.WriteTo.Console()
	.WriteTo.Debug()
	.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

// Serilog
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
	});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IApplicationRepo, ApplicationRepo>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
