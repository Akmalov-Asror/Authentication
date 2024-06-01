using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Teaching.Common.Entities.Users;

namespace Teaching.Common.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{

    public UserConfiguration() {}

    public void Configure(EntityTypeBuilder<User> builder)
    {
        var user = new User
        {
            Id = Guid.Parse("cde79a12-0364-4df7-ac73-9b9fb0a41745"),
            UserName = "Admin",
            NormalizedUserName = "Admin".ToUpper(),
            Email = "admin@gmail.com",
            NormalizedEmail = "admin@gmail.com".ToUpper(),
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEFO6ftiV/u05Xiv1Lorpej0W6LEmFqXnGUKSe6VaVecjWdxevE/+3Rn0o/QwxZOXfQ==",
            ConcurrencyStamp = "4d7805a0-d8d8-4faa-aac5-1dcea562fa21",
            FirstName = "Admin",
            LastName = "Admin"
        };
        builder.HasData(user);
    }
}
