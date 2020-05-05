using System;
using System.Threading.Tasks;
using CrossCutting.Messages.Commands;
using MassTransit;

namespace ConsoleAppConsumer.consumers
{
    public class RegisterWeatherForecastCommandConsumer: IConsumer<RegisterWeatherForecastCommand>
    {
        public async Task Consume(ConsumeContext<RegisterWeatherForecastCommand> context)
        {
            await Console.Out.WriteLineAsync($"Event received: {context.Message.Summary}");
        }
    }
}