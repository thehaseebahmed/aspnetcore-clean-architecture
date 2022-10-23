using AutoMapper;
using Starter.Application.Todo;
using Starter.Application.Todo.Queries.GetTodo;
using Starter.Domain.Entities;
using Starter.Domain.Repositories;
using Starter.Tests.Utillities;
using Starter.Tests.Utillities.Builders;

namespace Starter.Api.UnitTests
{
    public class GetWeatherForecastUnitTests
    {
        private readonly GetTodoHandler _handler;
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;

        private readonly int TemperatureAbove30One = 35;
        private readonly int TemperatureAbove30Two = 40;
        private readonly int TemperatureBelow30 = 25;

        public GetWeatherForecastUnitTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TodoMapper());
            });

            _mapper = mappingConfig.CreateMapper();
            _repository = new FakeTodoRepository();

            _handler = new GetTodoHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handler_GetFilteredWeatherForecastsWithTotalCount_ReturnsValidForecastAndCount()
        {
            // ARRANGE
            const int expectedCount = 2;
            const int expectedMinimumTemperature = 31;

            Create(new WeatherForecastBuilder()
                .WithTemprature(TemperatureBelow30)
                .Build());

            Create(new WeatherForecastBuilder()
                .WithTemprature(TemperatureAbove30One)
                .Build());

            Create(new WeatherForecastBuilder()
                .WithTemprature(TemperatureAbove30Two)
                .Build());

            var query = new GetTodo
            {
                Filter = "temperatureC gt 30",
                Count = true
            };

            // ACT
            var result = await _handler.Handle(query, CancellationToken.None);

            // ASSERT
            Assert.Equal(expectedCount, result.Count);
            Assert.DoesNotContain(result.Data, d => d.TemperatureC < expectedMinimumTemperature);
        }

        [Fact]
        public async Task Handler_GetWeatherForecastsWithCount_ReturnsValidCount()
        {
            // ARRANGE
            const int expectedCount = 1;

            Create(new WeatherForecastBuilder().Build());

            var query = new GetTodo
            {
                Count = true
            };

            // ACT
            var result = await _handler.Handle(query, CancellationToken.None);

            // ASSERT
            Assert.Equal(expectedCount, result.Count);
        }

        private void Create(Todo forecast)
        {
            _repository.CreateAsync(forecast);
        }
    }
}