using System.Collections.Generic;

namespace Domain;

public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? ParentId { get; set; }
    public virtual User? Parent { get; set; }
    public virtual ICollection<User>? Users { get; set; }
}
