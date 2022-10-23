using Starter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Starter.Domain.Repositories;

public interface ITodoRepository
{
    Task<Todo> CreateAsync(Todo model, CancellationToken cancellationToken = default);
    Task DeleteAllAsync(CancellationToken cancellationToken);
    Task<Todo> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    (IEnumerable<Todo> data, int count) Read(
        string filter = null,
        bool shouldCount = false,
        int limit = 0,
        int skip = 0,
        string order = null
    );
    Task<Todo> ReadByIdAsync(Guid id);
    Task<Todo> UpdateAsync(Todo model, CancellationToken cancellationToken = default);
}