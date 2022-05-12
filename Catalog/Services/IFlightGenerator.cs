namespace Catalog.Services;

public interface IFlightGenerator
{
	void Generate(DateOnly start, DateOnly end);
}
