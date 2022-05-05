using BookingService.Dtos;

namespace BookingService.SyncDataServices.Grpc;

public interface IFlightDataClient
{
	FlightDto GetFlight(int id);
}
