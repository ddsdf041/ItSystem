using System;
using System.Collections.Generic;

namespace ItSystem.Models.DbModels;

public partial class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string ShortName { get; set; } = null!;

    public Guid? IdPost { get; set; } = null!;

    public DateTime? LastOnline { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime DateCreate { get; set; }

    public bool HasAccess { get; set; }

    public int Role { get; set; }

    public virtual ICollection<Chat> ChatIdUser1Navigations { get; set; } = new List<Chat>();

    public virtual ICollection<Chat> ChatIdUser2Navigations { get; set; } = new List<Chat>();

    public virtual Post? IdPostNavigation { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Task> TaskIdAuthorNavigations { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskIdExecutorNavigations { get; set; } = new List<Task>();

    public virtual ICollection<Channel> IdChannels { get; set; } = new List<Channel>();
}
