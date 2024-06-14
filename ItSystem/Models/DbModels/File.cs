using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class File
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public byte[] File1 { get; set; } = null!;

    public virtual ICollection<Message> IdMessages { get; set; } = new List<Message>();

    public virtual ICollection<Task> IdTasks { get; set; } = new List<Task>();
}
