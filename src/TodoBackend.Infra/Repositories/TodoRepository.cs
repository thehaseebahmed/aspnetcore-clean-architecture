using Starter.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Starter.Domain.Repositories;
using Starter.Domain.Entities;
using System;
using System.Linq;
using Starter.Infra.Data;

namespace Starter.Infra.Repositories;

public class TodoRepository : GenericRepository<Todo>, ITodoRepository
{
    private readonly ApiDbContext _context;

    public TodoRepository(
        ApiDbContext db
    ) : base(db)
    {
        _context = db;
    }


    public async Task<Todo> CreateAsync(Todo entity, CancellationToken cancellationToken = default)
    {
        if (entity is IAuditable)
        {
            (entity as IAuditable).CreatedOn = DateTimeOffset.UtcNow;
        }

        _context.Set<Todo>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        _context.Todos.RemoveRange(_context.Todos);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Todo> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Todo>().FindAsync(id);
        if (entity == null) { return null; }

        if (entity is IAuditable)
        {
            (entity as IAuditable).UpdatedOn = DateTimeOffset.UtcNow;
        }

        if (entity is ISoftDeletable)
        {
            (entity as ISoftDeletable).IsActive = false;
        }
        else
        {
            _context.Set<Todo>().Remove(entity);
        }

        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Todo> UpdateAsync(Todo model, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<Todo>().FindAsync(model.Id);
        if (entity == null) { return model; }

        if (entity is IAuditable)
        {
            (entity as IAuditable).UpdatedOn = DateTimeOffset.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}