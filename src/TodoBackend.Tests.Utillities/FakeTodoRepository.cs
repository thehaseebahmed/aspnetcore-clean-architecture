using Starter.Domain.Entities;
using Starter.Domain.Repositories;

namespace Starter.Tests.Utillities
{
    public class FakeTodoRepository : FakeRepository<Todo>, ITodoRepository
    {
    }
}
