using AppCore.Repositories;
using Infrastructure.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IVehicleRepository, InMemoryVehicleRepository>();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapGet("/vehicles/{id:guid}", async (IVehicleRepository repository, Guid id) =>
    {
        var vehicle = await repository.FindByIdAsync(id);
        return vehicle is not null ? Results.Ok(vehicle) : Results.NotFound();
    })
    .WithName("GetVehicleById")
    .WithOpenApi();

app.MapGet("/vehicles/by-plate/{plateNumber}", async (IVehicleRepository repository, string plateNumber) =>
    {
        var vehicle = await repository.FindByLicensePlateAsync(plateNumber);
        return vehicle is not null ? Results.Ok(vehicle) : Results.NotFound();
    })
    .WithName("GetVehicleByPlateNumber")
    .WithOpenApi();

app.Run();

