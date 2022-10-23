using Starter.Application.Todo.Commands.CreateTodo;
using Starter.Domain.Entities;

namespace Starter.Tests.Utillities.Builders
{
    public class WeatherForecastBuilder
    {
        private Guid _id;
        private DateTime _date;
        private int _temperatureC;
        private string _summary;

        public WeatherForecastBuilder()
        {
            _id = Guid.NewGuid();
            _date = DateTime.UtcNow;
            _temperatureC = 0;
            _summary = "Lorem ipsum dolor sit amet, consectetur adipisici elit.";
        }

        public Todo Build()
        {
            return new Todo
            {
                Id = _id,
                Date = _date,
                TemperatureC = _temperatureC,
                Summary = _summary
            };
        }

        public CreateTodo BuildCreateCommand()
        {
            return new CreateTodo
            {
                Date = _date,
                TemperatureC = _temperatureC,
                Summary = _summary
            };
        }

        public WeatherForecastBuilder WithDate(DateTime date)
        {
            _date = date;
            return this;
        }

        public WeatherForecastBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public WeatherForecastBuilder WithTemprature(int tempratureC)
        {
            _temperatureC = tempratureC;
            return this;
        }

        public WeatherForecastBuilder WithSummary(string summary)
        {
            _summary = summary;
            return this;
        }
    }
}
