using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Management.AsyncDataServices;
using Management.Controllers;
using Management.Data.Repositories;
using Management.Dtos;
using Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Management.Tests;

public class AirplanesControllerTests
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void GetAllAirplanes_Returns_All_Airplanes()
	{
		//Arrange

		var airplaneRepository = Substitute.For<IAirplaneRepository>();
		var airplaneVariantRepository = Substitute.For<IAirplaneVariantRepository>();
		var messageBusClient = Substitute.For<IMessageBusClient>();
		var mapper = Substitute.For<IMapper>();
		var logger = Substitute.For<ILogger<AirplanesController>>();

		var airplanesCollection = new[] { new Airplane(), new Airplane(), new Airplane() };
		var airplaneReadDtosCollection = new[] { new AirplaneReadDto(), new AirplaneReadDto(), new AirplaneReadDto() };

		airplaneRepository.GetAllAirplanes().Returns(airplanesCollection);
		mapper.Map<IEnumerable<AirplaneReadDto>>(Arg.Any<IEnumerable<Airplane>>()).Returns(airplaneReadDtosCollection);

		var controller = new AirplanesController(airplaneRepository, messageBusClient, airplaneVariantRepository,
			mapper, logger);

		//Act

		var actionResult = controller.GetAllAirplanes();
		var result = actionResult.Result as OkObjectResult;
		var airplanes = result?.Value as IEnumerable<AirplaneReadDto>;

		//Assert
		Assert.NotNull(actionResult.Result, "actionResult.Result != null");
		Assert.NotNull(result, "result != null");
		Assert.NotNull(result?.Value, "result?.Value != null");
		Assert.NotNull(actionResult, "actionResult != null");
		Assert.AreEqual(airplanesCollection.Length, airplanes?.Count());
	}
}
