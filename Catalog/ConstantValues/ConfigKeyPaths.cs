namespace Catalog.ConstantValues;

public static class ConfigKeyPaths
{
	// Sections
	public const string ParametersSec = "Parameters";
	public const string FlightGenerationWorkerParametersSec = $"{ParametersSec}:FlightGenerationWorkerParameters";

	//Keys
	public const string MaxFlightsPerDay = $"{FlightGenerationWorkerParametersSec}:MaxFlightsPerDay";
	public const string PeriodMs = $"{FlightGenerationWorkerParametersSec}:PeriodMs";
	public const string AfterErrorDelayMs = $"{FlightGenerationWorkerParametersSec}:AfterErrorDelayMs";
	public const string InitialDelayMs = $"{FlightGenerationWorkerParametersSec}:InitialDelayMs";

	//RabbitMQ
	public static string RabbitMQ => "RabbitMQ";
	public static string RabbitMQHost => $"{RabbitMQ}:Host";
	public static string RabbitMQPort => $"{RabbitMQ}:Port";
	public static string ManagementPublishQueue => $"{RabbitMQ}:ManagementPublishQueue";
}
