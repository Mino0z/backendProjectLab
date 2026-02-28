using AppCore.Models;
namespace AppCore.Repositories;

public interface ICarRepository : IGenericRepository<Car>
{
    Task<Car?> GetByPlateNumberAsync(string plateNumber);
}