namespace Teaching.V1.Auth.Models.AuthModels;

public class RemoveRoleFromUserModel
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
