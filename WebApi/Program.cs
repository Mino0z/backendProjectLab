using AppCore.Repositories;
using Infrastructure.Memory;
using AppCore.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IVehicleRepository, InMemoryVehicleRepository>();
builder.Services.AddSingleton<IParkingSessionRepository, MemoryParkingSessionRepository>();
builder.Services.AddSingleton<IParkingGateRepository, MemoryParkingGateRepository>();
builder.Services.AddSingleton<IParkingUnitOfWork, MemoryParkingUnitOfWork>();
builder.Services.AddSingleton<IParkingGateService, MemoryParkingGateService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Parking API", Version = "v1" });
});
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parking API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.MapGet("/vehicles", async (IVehicleRepository repository) =>
    {
        var vehicles = await repository.FindAllAsync();
        return Results.Ok(vehicles);
    })
    .WithName("GetAllVehicles")
    .WithTags("Vehicles")
    .WithOpenApi();

app.MapGet("/vehicles/{id:guid}", async (IVehicleRepository repository, Guid id) =>
    {
        var vehicle = await repository.FindByIdAsync(id);
        return vehicle is not null ? Results.Ok(vehicle) : Results.NotFound();
    })
    .WithName("GetVehicleById")
    .WithTags("Vehicles")
    .WithOpenApi();

app.MapGet("/vehicles/by-plate/{plateNumber}", async (IVehicleRepository repository, string plateNumber) =>
    {
        var vehicle = await repository.FindByLicensePlateAsync(plateNumber);
        return vehicle is not null ? Results.Ok(vehicle) : Results.NotFound();
    })
    .WithName("GetVehicleByPlateNumber")
    .WithTags("Vehicles")
    .WithOpenApi();

app.Run();
