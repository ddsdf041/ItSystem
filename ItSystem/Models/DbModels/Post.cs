using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class Post
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
