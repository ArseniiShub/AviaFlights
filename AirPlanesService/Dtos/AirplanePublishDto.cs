namespace AirplanesService.Dtos;

public class AirplanePublishDto
{
	public int Id { get; set; }
	public int ManufacturerId { get; set; }
	public string Model { get; set; } = "";
	public Status Status { get; set; }
}
