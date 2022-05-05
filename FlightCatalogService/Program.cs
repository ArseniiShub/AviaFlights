global using FlightCatalogService.Enums;
using FlightCatalogService.AsyncDataServices;
using FlightCatalogService.BackgroundServices;
using FlightCatalogService.ConstantValues;
using FlightCatalogService.Data;
using FlightCatalogService.Data.Repositories;
using FlightCatalogService.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Logging

var loggerFactory = LoggerFactory.Create(configure =>
{
	configure.ClearProviders();
	configure.AddSimpleConsole(opt =>
	{
		opt.IncludeScopes = true;
		opt.TimestampFormat = "hh:mm:ss ";
	});
});
var logger = loggerFactory.CreateLogger<Program>();

#endregion

#region Services Registration

builder.Services.AddLogging(loggingBuilder =>
{
	loggingBuilder.ClearProviders();
	loggingBuilder.AddSimpleConsole(opt =>
	{
		opt.IncludeScopes = true;
		opt.TimestampFormat = "hh:mm:ss ";
	});
});
builder.Services.AddHostedService<Worker>();

if(builder.Environment.IsDevelopment())
{
	logger.LogInformation("Using UnMemory Database");
	builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
}
else
{
	logger.LogInformation("Using SqlServer Database");
	var connStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"))
	{
		DataSource = Environment.GetEnvironmentVariable(EnvironmentVariablesKeys.SqlServerUrl) ?? "",
		Password = Environment.GetEnvironmentVariable(EnvironmentVariablesKeys.SqlServerSAPasswordKey) ?? ""
	};
	builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connStrBuilder.ConnectionString));
	builder.Services.AddHostedService<RabbitMQMessageBusSubscriber>();
}

builder.Services.AddGrpc();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IFlightGenerator, FlightGenerator>();
builder.Services.AddSingleton<IMessageBusEventsProcessor, MessageBusEventsProcessor>();

builder.Services.AddScoped<IAirplaneRepository, AirplaneRepository>();
builder.Services.AddScoped<IFlightRouteRepository, FlightRouteRepository>();
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<TestDataCreator>();

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

	using var scope = app.Services.CreateScope();
	using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	scope.ServiceProvider.GetRequiredService<TestDataCreator>().FillData();
}
else
{
	using var scope = app.Services.CreateScope();
	using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	if(context.Database.GetPendingMigrations().Any())
	{
		context.Database.Migrate();
		logger.LogInformation("Migrations applied");
	}
	scope.ServiceProvider.GetRequiredService<TestDataCreator>().FillData();
}

app.MapGrpcService<FlightService>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
