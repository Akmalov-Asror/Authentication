using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Teaching.Common.Data.Configurations;
using Teaching.Common.Entities.Users;

namespace Teaching.Common.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider services) 
        : base(options) 
        => this.Services = services;
    public IServiceProvider Services { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Uzdevumi> Uzdevumis { get; set; }
    public DbSet<Monitoring> Monitorings { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.ApplyConfiguration(new RoleConfiguration(Services));
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
    }
}
//versioning , api documentation ,
// dapper , bulk ef core, Stream MemoyStream 