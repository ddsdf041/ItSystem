using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class Message
{
    public Guid Id { get; set; }

    public DateTime DateCreate { get; set; }

    public string Text { get; set; } = null!;

    public Guid? IdChannel { get; set; }

    public Guid IdUser { get; set; }

    public bool IsDelete { get; set; }

    public bool HasBranch { get; set; }

    public bool HasTask { get; set; }

    public bool HasProject { get; set; }

    public Guid? IdBranchMessage { get; set; }

    public Guid? IdTask { get; set; }

    public Guid? IdChat { get; set; }

    public virtual Message? IdBranchMessageNavigation { get; set; }

    public virtual Chat? IdChatNavigation { get; set; }

    public virtual Task? IdTaskNavigation { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual Message? InverseIdBranchMessageNavigation { get; set; }

    public virtual ICollection<File> IdFiles { get; set; } = new List<File>();
}
