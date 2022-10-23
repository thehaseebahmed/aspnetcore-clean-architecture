using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using Starter.Application.Todo;
using Starter.Application.Todo.Commands.CreateTodo;
using Starter.Domain.Repositories;
using Starter.Tests.Utillities;
using Starter.Tests.Utillities.Builders;

namespace Starter.Api.UnitTests
{
    public class CreateWeatherForecastUnitTests
    {
        private readonly CreateTodoHandler _handler;
        private readonly IMapper _mapper;
        private readonly ITodoRepository _repository;

        private readonly DateTime AnyDate = DateTime.Parse("2022-10-03T13:00:00.000Z");
        private readonly int AnyTemperature = 30;
        private readonly string AnySummary = "Quo usque tandem abutere, Catilina, patientia nostra?";

        public CreateWeatherForecastUnitTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TodoMapper());
            });

            _mapper = mappingConfig.CreateMapper();
            _repository = new FakeTodoRepository();

            _handler = new CreateTodoHandler(_mapper, _repository);
        }

        [Fact]
        public async Task Handler_NewWeatherForecast_ReturnsWeatherForecastId()
        {
            // ARRANGE
            var command = new WeatherForecastBuilder()
                .WithDate(AnyDate)
                .WithTemprature(AnyTemperature)
                .WithSummary(AnySummary)
                .BuildCreateCommand();

            // ACT
            var result = await _handler.Handle(command, CancellationToken.None);

            // ASSERT
            Assert.NotNull(result.Id);
        }
    }
}