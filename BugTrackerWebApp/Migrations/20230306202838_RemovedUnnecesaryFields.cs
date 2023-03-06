using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerWebApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnnecesaryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trackables_AspNetUsers_AppUserId",
                table: "Trackables");

            migrationBuilder.DropIndex(
                name: "IX_Trackables_AppUserId",
                table: "Trackables");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Trackables");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Trackables",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trackables_AppUserId",
                table: "Trackables",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trackables_AspNetUsers_AppUserId",
                table: "Trackables",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
