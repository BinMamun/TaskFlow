using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTaskDependencyTableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskDependencies",
                table: "TaskDependencies");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId",
                table: "TaskDependencies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskDependencies",
                table: "TaskDependencies",
                columns: new[] { "TaskItemId", "PrerequisiteTaskId" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDependencies_Id",
                table: "TaskDependencies",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskDependencies",
                table: "TaskDependencies");

            migrationBuilder.DropIndex(
                name: "IX_TaskDependencies_Id",
                table: "TaskDependencies");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "TaskDependencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskDependencies",
                table: "TaskDependencies",
                columns: new[] { "Id", "PrerequisiteTaskId" });
        }
    }
}
