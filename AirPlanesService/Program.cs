global using AirplanesService.Models;
global using AirplanesService.Dtos;
global using AirplanesService.Data.Repositories;
global using AirplanesService.Data;
using System.Text.Json.Serialization;
using AirplanesService.AsyncDataServices;
using AirplanesService.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Logging

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var loggerFactory = LoggerFactory.Create(configure =>
{
	configure.ClearProviders();
	configure.AddConsole();
});
var logger = loggerFactory.CreateLogger<Program>();

#endregion

#region Services Registration

if(builder.Environment.IsDevelopment())
{
	logger.LogInformation("Using InMemory Database");
	builder.Services.AddDbContext<AirplanesDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
}
else
{
	logger.LogInformation("Using SqlServer Database");
	var connStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"))
	{
		Password = Environment.GetEnvironmentVariable(Constants.MsSqlSAPasswordKey)
	};

	builder.Services.AddDbContext<AirplanesDbContext>(options => options.UseSqlServer(connStrBuilder.ConnectionString));
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IMessageBusClient, RabbitMQMessageBusClient>();

builder.Services.AddScoped<IAirplaneRepository, AirplaneRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

#endregion

builder.Services.AddControllers()
	.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

if(app.Environment.IsProduction())
{
	using var context = app.Services.GetService<AirplanesDbContext>();
	if(context == null)
	{
		throw new InvalidOperationException($"Could not get {nameof(AirplanesDbContext)} service");
	}

	context.Database.Migrate();
}

app.Run();
