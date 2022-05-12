namespace Catalog.ConstantValues;

public static class ConfigKeyPaths
{
	// Sections
	public const string ParametersSec = "Parameters";
	public const string BackgroundServiceParametersSec = $"{ParametersSec}:BackgroundServiceParameters";

	//Keys
	public const string MaxFlightsPerDay = $"{BackgroundServiceParametersSec}:MaxFlightsPerDay";
	public const string PeriodMs = $"{BackgroundServiceParametersSec}:PeriodMs";
	public const string AfterErrorDelayMs = $"{BackgroundServiceParametersSec}:AfterErrorDelayMs";
	public const string InitialDelayMs = $"{BackgroundServiceParametersSec}:InitialDelayMs";

	//RabbitMQ
	public static string RabbitMQ => "RabbitMQ";
	public static string RabbitMQHost => $"{RabbitMQ}:Host";
	public static string RabbitMQPort => $"{RabbitMQ}:Port";
	public static string DataManagementPublishQueue => $"{RabbitMQ}:DataManagementPublishQueue";
}
