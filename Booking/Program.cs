using AutoMapper;
using Booking.ConstantValues;
using Booking.Data;
using Booking.Data.Repositories;
using Booking.Dtos;
using Booking.Models;
using Booking.SyncDataServices.Grpc;
using FlightCatalogService.Protos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Service Registration

builder.Services.AddLogging(loggingBuilder =>
{
	loggingBuilder.ClearProviders();
	loggingBuilder.AddSimpleConsole(opt =>
	{
		opt.IncludeScopes = true;
		opt.TimestampFormat = "hh:mm:ss ";
	});
});

builder.Services.AddGrpcClient<FlightService.FlightServiceClient>((_, options) =>
{
	var catalogServiceEndpoint = builder.Configuration[EnvironmentVariablesKeys.CatalogServiceRpcEndpoint];
	options.Address = new Uri(catalogServiceEndpoint);
});

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IFlightDataClient, FlightDataClient>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

#endregion

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("api/b/ticket/{id:int}", (
		[FromServices] ITicketRepository ticketRepository,
		[FromQuery] int id) =>
	ticketRepository.GetTicketById(id));

app.MapPost("api/b/book", (
	[FromServices] IFlightDataClient client,
	[FromServices] IMapper mapper,
	[FromServices] ITicketRepository ticketRepository,
	[FromBody] TicketCreateDto ticketCreateDto) =>
{
	var flight = client.GetFlight(ticketCreateDto.FlightId);
	var ticket = mapper.Map<Ticket>(ticketCreateDto);
	ticketRepository.CreateTicket(ticket);
	ticketRepository.SaveChanges();
});

app.Run();
