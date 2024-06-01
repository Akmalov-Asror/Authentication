using Teaching.Common.Entities.Users;

namespace Teaching.V1.Auth.Models.AuthModels;

public class UsersModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public virtual UsersModel MapFromEntity(User entity)
    {
        Id = entity.Id;
        Username = entity.UserName;
        Email = entity.Email;
        Roles = new List<string>();
        return this;
    }
    public virtual UsersModel MapFromEntities(User model, IList<string> roles)
    {
        Id = model.Id;
        Username = model.UserName;
        Email = model.Email;
        Roles = roles;
        FirstName = model.FirstName;
        LastName = model.LastName;
        return this;
    }
}
