using Catalog.ConstantValues;
using Catalog.Services;

namespace Catalog.BackgroundServices;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly IFlightGenerator _flightGenerator;

	private readonly int _initialDelayMs;
	private readonly int _periodMs;
	private readonly int _afterErrorDelayMs;

	private Timer? _timer;
	private DateOnly _lastSuccessfulGenerationDate;

	public Worker(ILogger<Worker> logger, IFlightGenerator flightGenerator, IConfiguration configuration)
	{
		_logger = logger;
		_flightGenerator = flightGenerator;

		_initialDelayMs = configuration.GetValue<int>(ConfigKeyPaths.InitialDelayMs);
		_periodMs = configuration.GetValue<int>(ConfigKeyPaths.PeriodMs);
		_afterErrorDelayMs = configuration.GetValue<int>(ConfigKeyPaths.AfterErrorDelayMs);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_timer = new Timer(OnTick, null, _initialDelayMs, _periodMs);

		return Task.CompletedTask;
	}

	private void OnTick(object? state)
	{
		var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

		if(_lastSuccessfulGenerationDate == currentDate)
		{
			return;
		}

		try
		{
			//One week starting from tomorrow
			var start = currentDate.AddDays(1);
			var end = currentDate.AddDays(8);

			_flightGenerator.Generate(start, end);

			_lastSuccessfulGenerationDate = currentDate;
			_timer?.Change(_periodMs, _periodMs);
		}
		catch(Exception e)
		{
			_logger.LogError(e,
				"Error Generating Flights, trying again in {AfterErrorDelayMs} milliseconds", _afterErrorDelayMs);
			_timer?.Change(_afterErrorDelayMs, _periodMs);
		}
	}
}
