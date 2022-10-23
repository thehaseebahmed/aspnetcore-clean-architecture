using System;
using Starter.Domain.Extensions;
using Starter.Domain.Interfaces;
using Starter.Infra.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Starter.Infra.Repositories;

public abstract class GenericRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly ApiDbContext _db;

    protected GenericRepository(
        ApiDbContext db
    )
    {
        _db = db;
    }

    public (IEnumerable<TEntity> data, int count) Read(
        string filter = null,
        bool shouldCount = false,
        int limit = 0,
        int skip = 0,
        string order = null
    )
    {
        var query = _db.Set<TEntity>().AsQueryable();

        if (filter.HasValue())
        {
            query = query.Filter(filter);
        }

        if (order.HasValue())
        {
            query = query.Order(order);
        }

        if (skip > 0)
        {
            query = query.Skip(skip);
        }

        if (limit > 0)
        {
            query = query.Take(limit);
        }

        int count = 0;
        if (shouldCount)
        {
            count = query.Count();
        }

        return (data: query.AsEnumerable(), count);
    }

    public async Task<TEntity> ReadByIdAsync(
        Guid id
    )
    {
        var entity = await _db.Set<TEntity>()
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
            
        return entity;
    }
}