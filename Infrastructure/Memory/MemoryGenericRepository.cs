using AppCore.Dto;
using AppCore.Entities;
using AppCore.Repositories;

namespace Infrastructure.Memory;

public class MemoryGenericRepository<T> : IGenericRepositoryAsync<T> where T : EntityBase
{
    protected readonly Dictionary<Guid, T> _data = new();

    public Task<T?> FindByIdAsync(Guid id)
    {
        var result = _data.TryGetValue(id, out var value) ? value : null;
        return Task.FromResult(result);
    }

    public Task<IEnumerable<T>> FindAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_data.Values.ToList());
    }

    public Task<PagedResult<T>> FindPagedAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;
        
        var totalCount = _data.Count;
        var items = _data.Values
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            
        return Task.FromResult(new PagedResult<T>(items, totalCount, page, pageSize));
    }

    public Task<T> AddAsync(T entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        
        _data[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public Task<T> UpdateAsync(T entity)
    {
        if (!_data.ContainsKey(entity.Id))
        {
            throw new InvalidOperationException($"Entity of type {typeof(T).Name} with ID {entity.Id} not found.");
        }
        
        _data[entity.Id] = entity;
        return Task.FromResult(entity);
    }

    public Task RemoveByIdAsync(Guid id)
    {
        if (!_data.Remove(id))
        {
            throw new InvalidOperationException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }
        return Task.CompletedTask;
    }
}
