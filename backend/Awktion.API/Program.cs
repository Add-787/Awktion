using Awktion.API.Hubs;
using Awktion.API.Middleware;
using Awktion.API.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("PostgreSQL");

// Add services to the container.
builder.Services.AddDbContext<RoomDb>(optionsBuilder => optionsBuilder.UseNpgsql(connection));
builder.Services.AddDbContext<UserDb>(optionsBuilder => optionsBuilder.UseNpgsql(connection));
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<UserDb>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy", builder => builder
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
});

app.MapPost("/room", async (RoomDb db, Room room, IHubContext<RoomHub,IRoomClient> hubContext) =>
{
    await db.Rooms.AddAsync(room);
    await db.SaveChangesAsync();
    await hubContext.Clients.All.GetRooms();
    return Results.Created($"/room/{room.ID}", room);

});

app.MapHub<RoomHub>("/room");
app.MapHub<NotificationHub>("/notify");

app.UseCors("DevPolicy");



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
