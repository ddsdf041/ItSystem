using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNullableTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Message_1",
                table: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Message_2",
                table: "Message",
                newName: "IX_Message_IdChat");

            migrationBuilder.RenameIndex(
                name: "IX_Message",
                table: "Message",
                newName: "IX_Message_IdBranchMessage");

            //migrationBuilder.AlterColumn<bool>(
            //    name: "IsDelete",
            //    table: "Task",
            //    type: "bit",
            //    nullable: false,
            //    oldClrType: typeof(Guid),
            //    oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Message",
                table: "Message",
                column: "IdBranchMessage");

            migrationBuilder.CreateIndex(
                name: "IX_Message_1",
                table: "Message",
                column: "IdChannel");

            migrationBuilder.CreateIndex(
                name: "IX_Message_2",
                table: "Message",
                column: "IdChat");

            migrationBuilder.DropColumn(
               name: "IsDelete",
               table: "Task");

            migrationBuilder.AddColumn<bool>(
               name: "IsDelete",
               table: "Task",
               type: "bit",
               nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Message",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_1",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_2",
                table: "Message");

            migrationBuilder.RenameIndex(
                name: "IX_Message_IdChat",
                table: "Message",
                newName: "IX_Message_2");

            migrationBuilder.RenameIndex(
                name: "IX_Message_IdBranchMessage",
                table: "Message",
                newName: "IX_Message");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "IsDelete",
            //    table: "Task",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    oldClrType: typeof(bool),
            //    oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Message_1",
                table: "Message",
                column: "IdChannel",
                unique: true,
                filter: "[IdChannel] IS NOT NULL");

            migrationBuilder.DropColumn(
               name: "IsDelete",
               table: "Task");

            migrationBuilder.AddColumn<Guid>(
               name: "IsDelete",
               table: "Task",
               type: "uniqueidentifier",
               nullable: false);
        }
    }
}


