namespace DataManagementService.ConstantValues;

public static class ConfigKeyPaths
{
	public static string RabbitMQ => "RabbitMQ";

	public static string DataManagementPublishQueue => $"{RabbitMQ}:DataManagementPublishQueue";
	public static string RabbitMQHost => $"{RabbitMQ}:Host";
	public static string RabbitMQPort => $"{RabbitMQ}:Port";
}
