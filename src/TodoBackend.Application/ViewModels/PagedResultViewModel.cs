using AutoMapper;

namespace Starter.Application.ViewModels;

public class PagedResultViewModel<T>
{
    public int Count { get; set; }
    public IEnumerable<T> Data { get; set; }
}

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap(typeof(PagedResultViewModel<>), typeof(PagedResultViewModel<>));
    }
}