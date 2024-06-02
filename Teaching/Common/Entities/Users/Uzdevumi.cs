namespace Teaching.Common.Entities.Users;

public class Uzdevumi : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string DisplayName { get; set; }
    public string ImagePath { get; set; }
}
