using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class Chat
{
    public Guid Id { get; set; }

    public Guid IdUser1 { get; set; }

    public Guid IdUser2 { get; set; }

    public virtual User IdUser1Navigation { get; set; } = null!;

    public virtual User IdUser2Navigation { get; set; } = null!;

    public virtual Message? Message { get; set; }
}
