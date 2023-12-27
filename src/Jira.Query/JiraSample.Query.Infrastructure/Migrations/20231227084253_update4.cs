using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiraSample.Query.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "JiraItems",
                newName: "ItemStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemStatus",
                table: "JiraItems",
                newName: "Status");
        }
    }
}
