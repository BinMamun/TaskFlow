using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTaskDependencyColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_TaskItem_Id",
                table: "TaskDependencies");

            migrationBuilder.DropIndex(
                name: "IX_TaskDependencies_Id",
                table: "TaskDependencies");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_TaskItem_TaskItemId",
                table: "TaskDependencies",
                column: "TaskItemId",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDependencies_TaskItem_TaskItemId",
                table: "TaskDependencies");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDependencies_Id",
                table: "TaskDependencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDependencies_TaskItem_Id",
                table: "TaskDependencies",
                column: "Id",
                principalTable: "TaskItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
