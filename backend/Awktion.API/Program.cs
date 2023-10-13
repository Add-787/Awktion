using Awktion.API.Hubs;
using Awktion.API.Middleware;
using Awktion.API.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RoomDb>(options => options.UseInMemoryDatabase("rooms"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());

});

var app = builder.Build();

GlobalHost.HubPipeline.AddModule(new HubLogging());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Awktion API V1");
    });
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/rooms", async (RoomDb db) => { 
    var rooms = await db.Rooms.ToListAsync();
    return rooms;
    // return rooms;
});

app.MapPost("/room", async (RoomDb db, Room room, IHubContext<RoomHub,IRoomClient> hubContext) =>
{
    await db.Rooms.AddAsync(room);
    await db.SaveChangesAsync();
    await hubContext.Clients.All.GetRooms();
    return Results.Created($"/room/{room.Id}", room);

});

app.MapHub<RoomHub>("/room");
app.MapHub<NotificationHub>("/notify");

app.UseCors("NewPolicy");



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
