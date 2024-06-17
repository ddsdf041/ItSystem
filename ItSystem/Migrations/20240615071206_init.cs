using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItSystem.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UserCount = table.Column<int>(type: "int", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdPost = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastOnline = table.Column<DateTime>(type: "datetime", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    HasAccess = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Post",
                        column: x => x.IdPost,
                        principalTable: "Post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Board",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IdProject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortName = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Board_Project",
                        column: x => x.IdProject,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Channel/User",
                columns: table => new
                {
                    IdChannel = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel/User", x => new { x.IdChannel, x.IdUser });
                    table.ForeignKey(
                        name: "FK_Channel/User_Channel",
                        column: x => x.IdChannel,
                        principalTable: "Channel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Channel/User_User",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_User",
                        column: x => x.IdUser1,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chat_User1",
                        column: x => x.IdUser2,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateChange = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdExecutor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAuthor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBoard = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Board",
                        column: x => x.IdBoard,
                        principalTable: "Board",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_User2",
                        column: x => x.IdExecutor,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task_User3",
                        column: x => x.IdAuthor,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    IdChannel = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    HasBranch = table.Column<bool>(type: "bit", nullable: false),
                    HasTask = table.Column<bool>(type: "bit", nullable: false),
                    HasProject = table.Column<bool>(type: "bit", nullable: false),
                    IdBranchMessage = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdTask = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Chat",
                        column: x => x.IdChat,
                        principalTable: "Chat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Message",
                        column: x => x.IdBranchMessage,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Task",
                        column: x => x.IdTask,
                        principalTable: "Task",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_User1",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Task/File",
                columns: table => new
                {
                    IdTask = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFile = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task/File", x => new { x.IdTask, x.IdFile });
                    table.ForeignKey(
                        name: "FK_Task/File_File",
                        column: x => x.IdFile,
                        principalTable: "File",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Task/File_Task",
                        column: x => x.IdTask,
                        principalTable: "Task",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message/File",
                columns: table => new
                {
                    IdMessage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFile = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message/File", x => new { x.IdMessage, x.IdFile });
                    table.ForeignKey(
                        name: "FK_Message/File_File",
                        column: x => x.IdFile,
                        principalTable: "File",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message/File_Message",
                        column: x => x.IdMessage,
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Board_IdProject",
                table: "Board",
                column: "IdProject");

            migrationBuilder.CreateIndex(
                name: "IX_Channel/User_IdUser",
                table: "Channel/User",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_IdUser1",
                table: "Chat",
                column: "IdUser1");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_IdUser2",
                table: "Chat",
                column: "IdUser2");

            migrationBuilder.CreateIndex(
                name: "IX_Message",
                table: "Message",
                column: "IdBranchMessage",
                unique: true,
                filter: "[IdBranchMessage] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Message_1",
                table: "Message",
                column: "IdChannel",
                unique: true,
                filter: "[IdChannel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Message_2",
                table: "Message",
                column: "IdChat",
                unique: true,
                filter: "[IdChat] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Message_IdTask",
                table: "Message",
                column: "IdTask");

            migrationBuilder.CreateIndex(
                name: "IX_Message_IdUser",
                table: "Message",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Message/File_IdFile",
                table: "Message/File",
                column: "IdFile");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdAuthor",
                table: "Task",
                column: "IdAuthor");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdBoard",
                table: "Task",
                column: "IdBoard");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdExecutor",
                table: "Task",
                column: "IdExecutor");

            migrationBuilder.CreateIndex(
                name: "IX_Task/File_IdFile",
                table: "Task/File",
                column: "IdFile");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdPost",
                table: "User",
                column: "IdPost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channel/User");

            migrationBuilder.DropTable(
                name: "Message/File");

            migrationBuilder.DropTable(
                name: "Task/File");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
