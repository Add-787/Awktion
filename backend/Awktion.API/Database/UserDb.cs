using Awktion.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

class UserDb : IdentityDbContext<User>
{
    public UserDb(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }

}