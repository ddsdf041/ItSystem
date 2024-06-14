using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ItSystem.Models.DbModels;

public partial class ItSystemContext : DbContext
{
    public ItSystemContext()
    {
    }

    public ItSystemContext(DbContextOptions<ItSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SAVANNA\\SQLEXPRESS;Database=ItSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Board>(entity =>
        {
            entity.ToTable("Board");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ShortName)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Boards)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Board_Project");
        });

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.ToTable("Channel");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasMany(d => d.IdUsers).WithMany(p => p.IdChannels)
                .UsingEntity<Dictionary<string, object>>(
                    "ChannelUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Channel/User_User"),
                    l => l.HasOne<Channel>().WithMany()
                        .HasForeignKey("IdChannel")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Channel/User_Channel"),
                    j =>
                    {
                        j.HasKey("IdChannel", "IdUser");
                        j.ToTable("Channel/User");
                    });
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.ToTable("Chat");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdUser1Navigation).WithMany(p => p.ChatIdUser1Navigations)
                .HasForeignKey(d => d.IdUser1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chat_User");

            entity.HasOne(d => d.IdUser2Navigation).WithMany(p => p.ChatIdUser2Navigations)
                .HasForeignKey(d => d.IdUser2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chat_User1");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.ToTable("File");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.File1).HasColumnName("File");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.HasIndex(e => e.IdBranchMessage, "IX_Message").IsUnique();

            entity.HasIndex(e => e.IdChannel, "IX_Message_1").IsUnique();

            entity.HasIndex(e => e.IdChat, "IX_Message_2").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Text).HasColumnType("text");

            entity.HasOne(d => d.IdBranchMessageNavigation).WithOne(p => p.InverseIdBranchMessageNavigation)
                .HasForeignKey<Message>(d => d.IdBranchMessage)
                .HasConstraintName("FK_Message_Message");

            entity.HasOne(d => d.IdChatNavigation).WithOne(p => p.Message)
                .HasForeignKey<Message>(d => d.IdChat)
                .HasConstraintName("FK_Message_Chat");

            entity.HasOne(d => d.IdTaskNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdTask)
                .HasConstraintName("FK_Message_Task");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_User1");

            entity.HasMany(d => d.IdFiles).WithMany(p => p.IdMessages)
                .UsingEntity<Dictionary<string, object>>(
                    "MessageFile",
                    r => r.HasOne<File>().WithMany()
                        .HasForeignKey("IdFile")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Message/File_File"),
                    l => l.HasOne<Message>().WithMany()
                        .HasForeignKey("IdMessage")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Message/File_Message"),
                    j =>
                    {
                        j.HasKey("IdMessage", "IdFile");
                        j.ToTable("Message/File");
                    });
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("Task");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateChange).HasColumnType("datetime");
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(50);

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.TaskIdAuthorNavigations)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_User3");

            entity.HasOne(d => d.IdBoardNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdBoard)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Board");

            entity.HasOne(d => d.IdExecutorNavigation).WithMany(p => p.TaskIdExecutorNavigations)
                .HasForeignKey(d => d.IdExecutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_User2");

            entity.HasMany(d => d.IdFiles).WithMany(p => p.IdTasks)
                .UsingEntity<Dictionary<string, object>>(
                    "TaskFile",
                    r => r.HasOne<File>().WithMany()
                        .HasForeignKey("IdFile")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Task/File_File"),
                    l => l.HasOne<Task>().WithMany()
                        .HasForeignKey("IdTask")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Task/File_Task"),
                    j =>
                    {
                        j.HasKey("IdTask", "IdFile");
                        j.ToTable("Task/File");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User_1");

            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LastOnline).HasColumnType("datetime");
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.ShortName).HasMaxLength(50);

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPost)
                .HasConstraintName("FK_User_Post");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
