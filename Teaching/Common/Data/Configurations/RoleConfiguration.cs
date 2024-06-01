using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Teaching.Common.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public RoleConfiguration(IServiceProvider services) => this.Services = services;
    public IServiceProvider Services { get; set; }

    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.Property(r => r.Name).IsRequired();
        builder.Property(r => r.NormalizedName).IsRequired();

        var roles = new List<IdentityRole<Guid>>()
        {
            new IdentityRole<Guid>{
                Id = Guid.Parse("066ffda9-706f-44c1-8e63-0de68301376d"),
                Name = "Freelancer", NormalizedName = "Freelancer".ToUpper()
            },
            new IdentityRole<Guid>{
                Id = Guid.Parse("a2764599-ece5-4f15-b221-a5a77e87eb76"),
                Name = "Admin", NormalizedName = "Admin".ToUpper()
            },
            new IdentityRole<Guid>{
                Id = Guid.Parse("066ffda9-706f-44c1-8e63-0de63801376d"),
                Name = "SuperAdmin", NormalizedName = "SuperAdmin".ToUpper()
            }

        };

        builder.HasData(roles);
    }
}
