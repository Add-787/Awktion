using Awktion.API.Models;
using Microsoft.EntityFrameworkCore;

class RoomDb : DbContext 
{
    public RoomDb(DbContextOptions<RoomDb> options) : base(options) { }
    public DbSet<Room> Rooms { get; set; }
}