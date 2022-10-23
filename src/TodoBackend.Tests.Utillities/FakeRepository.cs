using Starter.Domain.Extensions;
using Starter.Domain.Interfaces;

namespace Starter.Tests.Utillities
{
    public abstract class FakeRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly IList<TEntity> _forecasts = new List<TEntity>();

        public Task<TEntity> CreateAsync(TEntity model, CancellationToken cancellatonToken = default)
        {
            _forecasts.Add(model);
            return Task.FromResult(model);
        }

        public Task<TEntity> DeleteAsync(Guid id, CancellationToken cancellatonToken = default)
        {
            var item = _forecasts.FirstOrDefault(x => x.Id == id);
            if (item == null) { return null; }

            _forecasts.Remove(item);
            return Task.FromResult(item);
        }

        public IQueryable<TEntity> Read()
        {
            return _forecasts.AsQueryable();
        }

        public (IEnumerable<TEntity> data, int count) Read(
            string filter = null,
            bool shouldCount = false,
            int limit = 0,
            int skip = 0,
            string order = null
            )
        {
            var query = _forecasts.AsQueryable();

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

        public Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellatonToken = default)
        {
            var item = _forecasts.First(x => x.Id == model.Id);
            _forecasts.Remove(item);

            _forecasts.Add(model);
            return Task.FromResult(model);
        }
    }
}
