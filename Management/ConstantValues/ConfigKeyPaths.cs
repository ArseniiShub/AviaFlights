namespace Management.ConstantValues;

public static class ConfigKeyPaths
{
	public static string RabbitMQ => "RabbitMQ";

	public static string ManagementPublishQueue => $"{RabbitMQ}:ManagementPublishQueue";
	public static string RabbitMQHost => $"{RabbitMQ}:Host";
	public static string RabbitMQPort => $"{RabbitMQ}:Port";
}
