using AppCore.Entities;
using AppCore.Repositories;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T> : IGenericRepositoryAsync<T>
    where T : EntityBase
{
    protected readonly Dictionary<Guid, T> _data = new();

    public virtual Task<T?> FindByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

    public virtual Task<IEnumerable<T>> FindAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_data.Values.ToList());
    }

    public virtual Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
    {
        if (page <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");
        }

        var allItems = _data.Values.ToList();
        var totalCount = allItems.Count;
        var items = allItems
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PagedResult<T>(items, totalCount, page, pageSize);
        return Task.FromResult(result);
    }

    public virtual Task<T> AddAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        if (_data.ContainsKey(entity.Id))
        {
            throw new InvalidOperationException($"Entity with id '{entity.Id}' already exists.");
        }

        _data[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public virtual Task<T> UpdateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity.Id == Guid.Empty)
        {
            throw new ArgumentException("Entity id cannot be empty for update.", nameof(entity));
        }

        if (!_data.ContainsKey(entity.Id))
        {
            throw new KeyNotFoundException($"Entity with id '{entity.Id}' was not found.");
        }

        _data[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public virtual Task RemoveByIdAsync(Guid id)
    {
        if (!_data.Remove(id))
        {
            throw new KeyNotFoundException($"Entity with id '{id}' was not found.");
        }

        return Task.CompletedTask;
    }
}

