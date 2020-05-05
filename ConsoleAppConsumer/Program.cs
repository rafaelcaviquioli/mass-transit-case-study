using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Configuration;
using ConsoleAppConsumer.consumers;

namespace ConsoleAppConsumer
{
    class Program
    {
        public static async Task Main()
        {
            IConfiguration appSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var hostUri = new Uri(appSettings["rabbitmq:host"]);
                var username = appSettings["rabbitmq:username"];
                var password = appSettings["rabbitmq:password"];

                cfg.Host(hostUri, host =>
                {
                    host.Username(username);
                    host.Password(password);
                });
                
                cfg.ReceiveEndpoint("register-weather-forecast", endpoint =>
                {
                    endpoint.Consumer<RegisterWeatherForecastCommandConsumer>();
                });
            });

            try
            {
                await busControl.StartAsync();
                
                Console.WriteLine("Listening for events, press a key to exit...");
                Console.ReadLine();
            }
            finally
            {
                await busControl.StopAsync();
                
                Console.WriteLine("Stopping...");
            }
        }
    }
}