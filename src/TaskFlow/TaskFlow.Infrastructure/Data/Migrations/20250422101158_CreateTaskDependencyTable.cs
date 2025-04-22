using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTaskDependencyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskDependencies",
                columns: table => new
                {
                    TaskItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrerequisiteTaskId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDependencies", x => new { x.TaskItemId, x.PrerequisiteTaskId });
                    table.ForeignKey(
                        name: "FK_TaskDependencies_TaskItem_PrerequisiteTaskId",
                        column: x => x.PrerequisiteTaskId,
                        principalTable: "TaskItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskDependencies_TaskItem_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDependencies_PrerequisiteTaskId",
                table: "TaskDependencies",
                column: "PrerequisiteTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskDependencies");
        }
    }
}
