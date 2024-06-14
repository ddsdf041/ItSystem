using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class Board
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public string Description { get; set; } = null!;

    public Guid IdProject { get; set; }

    public string ShortName { get; set; } = null!;

    public virtual Project IdProjectNavigation { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
