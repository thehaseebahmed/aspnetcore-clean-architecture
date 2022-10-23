using System.Net;
using Starter.Api.IntegrationTests.Utilities;
using Starter.Application.ViewModels;
using Starter.Tests.Utillities.Builders;

namespace Starter.Api.IntegrationTests
{
    public class CreateWeatherForecastTests : TestBase
    {
        private const string Endpoint = "/api/v1/weather-forecast";
        private const int AnyTemperature = 30;
        private const string AnySummary = "Quo usque tandem abutere, Catilina, patientia nostra?";

        private readonly DateTime AnyDate = DateTime.Parse("2022-10-03T13:00:00.000Z");

        [Fact]
        public async Task CreateWeatherForecast_ReturnsWeatherForecastId()
        {
            // ARRANGE
            var command = new WeatherForecastBuilder()
                .WithDate(AnyDate)
                .WithTemprature(AnyTemperature)
                .WithSummary(AnySummary)
                .BuildCreateCommand();

            // ACT
            var result = await Client.PostJsonAsync<CommandResultViewModel>(Endpoint, command);

            // ASSERT
            Assert.NotNull(result.Id);
        }

        [Fact]
        public async Task CreateWeatherForecastWithMissingTemperature_ReturnsValidationErrors()
        {
            // ARRANGE
            var command = new WeatherForecastBuilder()
                .WithDate(AnyDate)
                .WithSummary(AnySummary)
                .BuildCreateCommand();

            // ACT
            var exception = await Record.ExceptionAsync(
                async () => await Client.PostJsonAsync<ValidationErrorsViewModel>(Endpoint, command)
            ) as HttpException;

            // ASSERT
            Assert.Equal(HttpStatusCode.UnprocessableEntity, exception?.StatusCode);
        }
    }
}