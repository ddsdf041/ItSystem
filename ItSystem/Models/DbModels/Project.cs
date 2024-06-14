using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class Project
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
}
