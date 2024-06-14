using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class Channel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int UserCount { get; set; }

    public DateTime DateCreate { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}
