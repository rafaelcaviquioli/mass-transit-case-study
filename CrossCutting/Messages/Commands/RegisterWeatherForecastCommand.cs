using System;

namespace CrossCutting.Messages.Commands
{
    public class RegisterWeatherForecastCommand
    {
        public Guid CommandId { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}