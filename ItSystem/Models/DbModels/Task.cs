using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class Task
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public DateTime? DateChange { get; set; }

    public Guid IdExecutor { get; set; }

    public Guid IdAuthor { get; set; }

    public Guid IsDelete { get; set; }

    public Guid IdBoard { get; set; }

    public string ShortName { get; set; } = null!;

    public int Status { get; set; }

    public int Priority { get; set; }

    public virtual User IdAuthorNavigation { get; set; } = null!;

    public virtual Board IdBoardNavigation { get; set; } = null!;

    public virtual User IdExecutorNavigation { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<File> IdFiles { get; set; } = new List<File>();
}
