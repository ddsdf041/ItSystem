﻿// <auto-generated />
using System;
using ItSystem.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ItSystem.Migrations
{
    [DbContext(typeof(ItSystemContext))]
    [Migration("20240615071206_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChannelUser", b =>
                {
                    b.Property<Guid>("IdChannel")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdChannel", "IdUser");

                    b.HasIndex("IdUser");

                    b.ToTable("Channel/User", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("IdProject")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("IdProject");

                    b.ToTable("Board", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Channel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Channel", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser2")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdUser1");

                    b.HasIndex("IdUser2");

                    b.ToTable("Chat", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.File", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("File1")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("File");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("File", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime");

                    b.Property<bool>("HasBranch")
                        .HasColumnType("bit");

                    b.Property<bool>("HasProject")
                        .HasColumnType("bit");

                    b.Property<bool>("HasTask")
                        .HasColumnType("bit");

                    b.Property<Guid?>("IdBranchMessage")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdChannel")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdChat")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdTask")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdTask");

                    b.HasIndex("IdUser");

                    b.HasIndex(new[] { "IdBranchMessage" }, "IX_Message")
                        .IsUnique()
                        .HasFilter("[IdBranchMessage] IS NOT NULL");

                    b.HasIndex(new[] { "IdChannel" }, "IX_Message_1")
                        .IsUnique()
                        .HasFilter("[IdChannel] IS NOT NULL");

                    b.HasIndex(new[] { "IdChat" }, "IX_Message_2")
                        .IsUnique()
                        .HasFilter("[IdChat] IS NOT NULL");

                    b.ToTable("Message", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Post", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateChange")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("IdAuthor")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdBoard")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdExecutor")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IsDelete")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAuthor");

                    b.HasIndex("IdBoard");

                    b.HasIndex("IdExecutor");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("HasAccess")
                        .HasColumnType("bit");

                    b.Property<Guid?>("IdPost")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LastOnline")
                        .HasColumnType("datetime");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("Role")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_User_1");

                    b.HasIndex("IdPost");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("MessageFile", b =>
                {
                    b.Property<Guid>("IdMessage")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdFile")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdMessage", "IdFile");

                    b.HasIndex("IdFile");

                    b.ToTable("Message/File", (string)null);
                });

            modelBuilder.Entity("TaskFile", b =>
                {
                    b.Property<Guid>("IdTask")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdFile")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdTask", "IdFile");

                    b.HasIndex("IdFile");

                    b.ToTable("Task/File", (string)null);
                });

            modelBuilder.Entity("ChannelUser", b =>
                {
                    b.HasOne("ItSystem.Models.DbModels.Channel", null)
                        .WithMany()
                        .HasForeignKey("IdChannel")
                        .IsRequired()
                        .HasConstraintName("FK_Channel/User_Channel");

                    b.HasOne("ItSystem.Models.DbModels.User", null)
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .IsRequired()
                        .HasConstraintName("FK_Channel/User_User");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Board", b =>
                {
                    b.HasOne("ItSystem.Models.DbModels.Project", "IdProjectNavigation")
                        .WithMany("Boards")
                        .HasForeignKey("IdProject")
                        .IsRequired()
                        .HasConstraintName("FK_Board_Project");

                    b.Navigation("IdProjectNavigation");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Chat", b =>
                {
                    b.HasOne("ItSystem.Models.DbModels.User", "IdUser1Navigation")
                        .WithMany("ChatIdUser1Navigations")
                        .HasForeignKey("IdUser1")
                        .IsRequired()
                        .HasConstraintName("FK_Chat_User");

                    b.HasOne("ItSystem.Models.DbModels.User", "IdUser2Navigation")
                        .WithMany("ChatIdUser2Navigations")
                        .HasForeignKey("IdUser2")
                        .IsRequired()
                        .HasConstraintName("FK_Chat_User1");

                    b.Navigation("IdUser1Navigation");

                    b.Navigation("IdUser2Navigation");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Message", b =>
                {
                    b.HasOne("ItSystem.Models.DbModels.Message", "IdBranchMessageNavigation")
                        .WithOne("InverseIdBranchMessageNavigation")
                        .HasForeignKey("ItSystem.Models.DbModels.Message", "IdBranchMessage")
                        .HasConstraintName("FK_Message_Message");

                    b.HasOne("ItSystem.Models.DbModels.Chat", "IdChatNavigation")
                        .WithOne("Message")
                        .HasForeignKey("ItSystem.Models.DbModels.Message", "IdChat")
                        .HasConstraintName("FK_Message_Chat");

                    b.HasOne("ItSystem.Models.DbModels.Task", "IdTaskNavigation")
                        .WithMany("Messages")
                        .HasForeignKey("IdTask")
                        .HasConstraintName("FK_Message_Task");

                    b.HasOne("ItSystem.Models.DbModels.User", "IdUserNavigation")
                        .WithMany("Messages")
                        .HasForeignKey("IdUser")
                        .IsRequired()
                        .HasConstraintName("FK_Message_User1");

                    b.Navigation("IdBranchMessageNavigation");

                    b.Navigation("IdChatNavigation");

                    b.Navigation("IdTaskNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Task", b =>
                {
                    b.HasOne("ItSystem.Models.DbModels.User", "IdAuthorNavigation")
                        .WithMany("TaskIdAuthorNavigations")
                        .HasForeignKey("IdAuthor")
                        .IsRequired()
                        .HasConstraintName("FK_Task_User3");

                    b.HasOne("ItSystem.Models.DbModels.Board", "IdBoardNavigation")
                        .WithMany("Tasks")
                        .HasForeignKey("IdBoard")
                        .IsRequired()
                        .HasConstraintName("FK_Task_Board");

                    b.HasOne("ItSystem.Models.DbModels.User", "IdExecutorNavigation")
                        .WithMany("TaskIdExecutorNavigations")
                        .HasForeignKey("IdExecutor")
                        .IsRequired()
                        .HasConstraintName("FK_Task_User2");

                    b.Navigation("IdAuthorNavigation");

                    b.Navigation("IdBoardNavigation");

                    b.Navigation("IdExecutorNavigation");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.User", b =>
                {
                    b.HasOne("ItSystem.Models.DbModels.Post", "IdPostNavigation")
                        .WithMany("Users")
                        .HasForeignKey("IdPost")
                        .HasConstraintName("FK_User_Post");

                    b.Navigation("IdPostNavigation");
                });

            modelBuilder.Entity("MessageFile", b =>
                {
                    b.HasOne("ItSystem.Models.DbModels.File", null)
                        .WithMany()
                        .HasForeignKey("IdFile")
                        .IsRequired()
                        .HasConstraintName("FK_Message/File_File");

                    b.HasOne("ItSystem.Models.DbModels.Message", null)
                        .WithMany()
                        .HasForeignKey("IdMessage")
                        .IsRequired()
                        .HasConstraintName("FK_Message/File_Message");
                });

            modelBuilder.Entity("TaskFile", b =>
                {
                    b.HasOne("ItSystem.Models.DbModels.File", null)
                        .WithMany()
                        .HasForeignKey("IdFile")
                        .IsRequired()
                        .HasConstraintName("FK_Task/File_File");

                    b.HasOne("ItSystem.Models.DbModels.Task", null)
                        .WithMany()
                        .HasForeignKey("IdTask")
                        .IsRequired()
                        .HasConstraintName("FK_Task/File_Task");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Board", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Chat", b =>
                {
                    b.Navigation("Message");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Message", b =>
                {
                    b.Navigation("InverseIdBranchMessageNavigation");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Post", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Project", b =>
                {
                    b.Navigation("Boards");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.Task", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("ItSystem.Models.DbModels.User", b =>
                {
                    b.Navigation("ChatIdUser1Navigations");

                    b.Navigation("ChatIdUser2Navigations");

                    b.Navigation("Messages");

                    b.Navigation("TaskIdAuthorNavigations");

                    b.Navigation("TaskIdExecutorNavigations");
                });
#pragma warning restore 612, 618
        }
    }
}