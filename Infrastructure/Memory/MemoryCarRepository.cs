using AppCore.Models;
using AppCore.ValueObject;
using AppCore.Repositories;

namespace Infrastructure.Memory;

public class MemoryCarRepository: ICarRepository
{
    public Task<T> GetByIdAsync<T>(int id) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync<T>() where T : class
    {
        throw new NotImplementedException();
    }

    public Task AddAsync<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<Car?> GetByPlateNumberAsync(string plateNumber)
    {
        if (plateNumber == "KR 12345")
        {
            return Task.FromResult<Car?>(new Car()
            {
                id = 1,
                PlateNumber = PlateNumber.Of("KR 12345"),
                entryDate = DateTime.Now,
                exitDate = null
            });
        }

        return Task.FromResult<Car?>(null);
    }
}