using System.Collections.Immutable;
using Awktion.API.Hubs;
using Awktion.API.Middleware;
using Awktion.API.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;
using MiniValidation;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("PostgreSQL");

// Add services to the container.
builder.Services.AddDbContext<RoomDb>(optionsBuilder => optionsBuilder.UseNpgsql(connection));
builder.Services.AddDbContext<UserDb>(optionsBuilder => optionsBuilder.UseNpgsql(connection));
builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<UserDb>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:5265"
    };

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());

});

var app = builder.Build();

// Middleware for hub requests
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
})
.WithName("GetRooms")
.WithOpenApi();

app.MapPost("/room", async (RoomDb db, Room room, IHubContext<RoomHub,IRoomClient> hubContext) =>
{
    await db.Rooms.AddAsync(room);
    await db.SaveChangesAsync();
    await hubContext.Clients.All.GetRooms();
    return Results.Created($"/room/{room.ID}", room);
})
.WithName("GetRoom")
.WithOpenApi();

app.MapPost("/login", () =>
{

});

app.MapPost("/register", async ([FromBody] RegisterForm form, UserManager<User> userManager,IMapper mapper) => {

    if(!MiniValidator.TryValidate(form, out var errors))
    {
        return Results.ValidationProblem(errors);
    }

    var user = mapper.Map<User>(form);

    var res = await userManager.CreateAsync(user, form.Password!);
    if(!res.Succeeded)
    {
        var errorDescs = res.Errors.Select(e => e.Description);
        return Results.BadRequest(new RegisterResponse
        {
            IsSuccessful = false,
            Errors = errorDescs
        });
    }

    return Results.Ok(new RegisterResponse {
        IsSuccessful = true
    });

});

app.MapGet("/logout", () => {
    // Results.SignOut(authenticationSchemes: new List<string>() { "cookie" });
});

app.MapHub<RoomHub>("/room");
app.MapHub<NotificationHub>("/notify");

app.UseCors("DevPolicy");



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
