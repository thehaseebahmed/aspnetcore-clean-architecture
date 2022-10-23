using Microsoft.EntityFrameworkCore;
using Starter.Domain.Entities;

namespace Starter.Infra.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(
        DbContextOptions<ApiDbContext> options
    ) : base(options) { }

    public DbSet<Todo> Todos { get; set; }
}