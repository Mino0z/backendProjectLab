using AppCore.Repositories;
using Infrastructure.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICarRepository, MemoryCarRepository>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/cars/{number}", async (ICarRepository repository, string number) =>
    {
       return await repository.GetByPlateNumberAsync(number);
    })
    .WithName("GetCarByPlateNumber")
    .WithOpenApi();

app.Run();

