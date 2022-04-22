global using DataManagementService.Data;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataManagementService.AsyncDataServices;
using DataManagementService.ConstantValues;
using DataManagementService.Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Logging

builder.Logging.ClearProviders();
builder.Logging.AddSystemdConsole(opt =>
{
	opt.IncludeScopes = true;
	opt.TimestampFormat = "hh:mm:ss ";
});

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

if(builder.Environment.IsDevelopment())
{
	logger.LogInformation("Using UnMemory Database");
	builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));

	builder.Services.AddSingleton<IMessageBusClient, TestMessageBusClient>();
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

	builder.Services.AddSingleton<IMessageBusClient, RabbitMQMessageBusClient>();
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAirplaneRepository, AirplaneRepository>();
builder.Services.AddScoped<IAirplaneVariantRepository, AirplaneVariantRepository>();
builder.Services.AddScoped<IAirportRepository, AirportRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IFlightRouteRepository, FlightRouteRepository>();
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

	using var scope = app.Services.CreateScope();
	using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

	new TestDataCreator(context).FillData();
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
}

app.UseAuthorization();

app.MapControllers();

app.Run();
