using System;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Dto;
using AppCore.Entities;
using AppCore.Repositories;
using AppCore.Services;

namespace Infrastructure.Memory;

public class MemoryParkingGateService(IParkingUnitOfWork unit) : IParkingGateService
{
    public async Task<PagedResult<ParkingGateDto>> GetAllPaged(int page, int size)
    {
        var result = await unit.Gates.FindPagedAsync(page, size);
        return new PagedResult<ParkingGateDto>(
            result.Items.Select(e => (ParkingGateDto)e).ToList(),
            result.TotalCount,
            result.Page,
            result.PageSize
        );
    }

    public async Task<ParkingGateDto?> GetById(Guid id)
    {
        var entity = await unit.Gates.FindByIdAsync(id);
        if (entity is null)
        {
            return null;
        }
        return entity; 
    }

    public async Task<ParkingGateDto?> GetByName(string name)
    {
        var entity = await unit.Gates.FindByNameAsync(name);
        return entity is not null ? (ParkingGateDto)entity : null;
    }

    public async Task<ParkingGateDto> Create(CreateGateDto dto)
    {
        var entity = dto.ToEntity();
        await unit.Gates.AddAsync(entity);
        await unit.SaveChangesAsync();
        return entity;
    }

    public async Task<ParkingGateDto?> ChangeOperationalStatus(Guid id, bool status)
    {
        var entity = await unit.Gates.FindByIdAsync(id);
        if (entity is null)
        {
            return null;
        }

        entity.IsOperational = status;
        await unit.Gates.UpdateAsync(entity);
        await unit.SaveChangesAsync();
        return entity;
    }
}

