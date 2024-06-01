using Teaching.Common.Entities.Users;

namespace Teaching.V1.Auth.Models.AuthModels;

public class UserModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual UserModel MapFromEntity(User entity)
    {
        Id = entity.Id;
        Username = entity.UserName;
        Email = entity.Email;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        return this;
    }
}
