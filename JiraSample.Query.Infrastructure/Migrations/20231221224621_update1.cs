using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiraSample.Query.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Children",
                table: "JiraItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Children",
                table: "JiraItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
