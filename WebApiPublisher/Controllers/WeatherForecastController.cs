using System;
using System.Threading.Tasks;
using CrossCutting.Messages.Commands;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApiPublisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task Get()
        {
            var uri = new Uri($"{_configuration.GetValue<string>("rabbitmq:host")}register-weather-forecast");
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(uri);
            var random = new Random();

            var command = new RegisterWeatherForecastCommand()
            {
                CommandId = Guid.NewGuid(),
                Date = DateTime.Now,
                TemperatureC = random.Next(5, 10),
                Summary = "Cool"
            };
            await endpoint.Send(command);
        }
    }
}