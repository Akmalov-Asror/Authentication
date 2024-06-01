using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Teaching.Common.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        var userRole = new IdentityUserRole<Guid>
        {
            RoleId = Guid.Parse("066ffda9-706f-44c1-8e63-0de63801376d"),
            UserId = Guid.Parse("8EEDBE4F-DEF8-449C-AB43-08DC731C72EC"),

        };
        var userRol = new IdentityUserRole<Guid>
        {
            RoleId = Guid.Parse("066ffda9-706f-44c1-8e63-0de63801376d"),
            UserId = Guid.Parse("cde79a12-0364-4df7-ac73-9b9fb0a41745"),

        };
        builder.HasData(userRol);
    }
}
