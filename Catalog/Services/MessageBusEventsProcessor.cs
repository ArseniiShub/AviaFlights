using System.Text.Json;
using AutoMapper;
using Catalog.Data.Repositories;
using Catalog.Dtos;
using Catalog.Models;

namespace Catalog.Services;

public class MessageBusEventsProcessor : IMessageBusEventsProcessor
{
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ILogger<MessageBusEventsProcessor> _logger;
	private readonly IMapper _mapper;

	public MessageBusEventsProcessor(IServiceScopeFactory serviceScopeFactory, ILogger<MessageBusEventsProcessor> logger, IMapper mapper)
	{
		_serviceScopeFactory = serviceScopeFactory;
		_logger = logger;
		_mapper = mapper;
	}

	public void ProcessEvent(string jsonNotification)
	{
		ArgumentNullException.ThrowIfNull(jsonNotification);

		_logger.LogInformation("Processing MessageBus event");

		var type = DetermineEventType(jsonNotification);

		switch(type)
		{
			case EventType.Undefined:
				_logger.LogWarning($"Skipping {nameof(EventType.Undefined)} event");
				break;
			case EventType.AirplanePublished:
				ProcessAirplanePublishedEvent(jsonNotification);
				_logger.LogInformation($"Event of type {nameof(EventType.AirplanePublished)} processed");
				break;
			default:
				throw new NotSupportedException($"{type} event type is not supported");
		}
	}

	private void ProcessAirplanePublishedEvent(string jsonNotification)
	{
		var airplanePublishDto = JsonSerializer.Deserialize<AirplanePublishDto>(jsonNotification);
		var airplane = _mapper.Map<Airplane>(airplanePublishDto);

		_logger.LogInformation($"\nTest:\n\tId: {airplane.Id} ExtId: {airplane.ExternalId}");

		using var scope = _serviceScopeFactory.CreateScope();
		var airplaneRepository = scope.ServiceProvider.GetRequiredService<IAirplaneRepository>();

		airplaneRepository.CreateAirplane(airplane);
		airplaneRepository.SaveChanges();
	}

	private EventType DetermineEventType(string jsonNotification)
	{
		var genericEventDto = JsonSerializer.Deserialize<GenericEventDto>(jsonNotification);
		if(genericEventDto == null)
		{
			throw new InvalidOperationException("Could not determine notification event type");
		}

		return genericEventDto.EventType;
	}
}
