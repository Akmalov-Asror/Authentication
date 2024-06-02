namespace Teaching.Common.Entities.Users;

public class Auditable
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}
