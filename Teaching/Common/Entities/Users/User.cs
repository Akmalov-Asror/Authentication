using Microsoft.AspNetCore.Identity;

namespace Teaching.Common.Entities.Users;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
