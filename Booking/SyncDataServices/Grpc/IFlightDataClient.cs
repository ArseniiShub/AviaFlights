using Booking.Dtos;

namespace Booking.SyncDataServices.Grpc;

public interface IFlightDataClient
{
	FlightDto GetFlight(int id);
}
