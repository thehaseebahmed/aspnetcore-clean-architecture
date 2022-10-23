using Microsoft.Extensions.DependencyInjection;
using Starter.Api.IntegrationTests.Utilities;
using Starter.Application.Todo.Queries.GetTodo;
using Starter.Application.ViewModels;
using Starter.Domain.Entities;
using Starter.Domain.Repositories;
using Starter.Tests.Utillities.Builders;

namespace Starter.Api.IntegrationTests
{
    public class GetWeatherForecastTests : TestBase
    {
        private const string Endpoint = "/api/v1/weather-forecast";
        private const int TemperatureAbove30One = 35;
        private const int TemperatureAbove30Two = 40;
        private const int TemperatureBelow30 = 25;

        private readonly ITodoRepository _repository;

        public GetWeatherForecastTests()
        {
            _repository =
                (ITodoRepository)TestScope.ServiceProvider
                    .GetRequiredService(typeof(ITodoRepository));
        }

        [Fact]
        public async Task GetFilteredWeatherForecastsWithTotalCount_ReturnsValidForecastAndCount()
        {
            // ARRANGE
            const int expectedCount = 2;
            const int expectedMinimumTemperature = 31;

            await Create(new[]
            {
                new WeatherForecastBuilder()
                    .WithTemprature(TemperatureBelow30)
                    .Build(),
                new WeatherForecastBuilder()
                    .WithTemprature(TemperatureAbove30One)
                    .Build(),
                new WeatherForecastBuilder()
                    .WithTemprature(TemperatureAbove30Two)
                    .Build()
            });

            // ACT
            const string filter = "temperatureC gt 30";
            const bool count = true;

            var result =
                await Client.GetAsync<PagedResultViewModel<GetTodoViewModel>>(
                    $"{Endpoint}?filter={filter}&count={count}");

            // ASSERT
            Assert.Equal(expectedCount, result.Count);
            Assert.DoesNotContain(result.Data, d => d.TemperatureC < expectedMinimumTemperature);
        }

        [Fact]
        public async Task GetWeatherForecastsWithCount_ReturnsValidCount()
        {
            // ARRANGE
            const int greaterThanCount = 0;

            await Create(new WeatherForecastBuilder().Build());

            // ACT
            const bool count = true;

            var result =
                await Client.GetAsync<PagedResultViewModel<GetTodoViewModel>>(
                    $"{Endpoint}?count={count}");

            // ASSERT
            Assert.True(result.Count > greaterThanCount);
        }

        private Task Create(Todo forecast)
        {
            return Create(new[] { forecast });
        }

        private async Task Create(IEnumerable<Todo> forecasts)
        {
            foreach (var forecast in forecasts)
            {
                await _repository.CreateAsync(forecast);
            }
        }
    }
}