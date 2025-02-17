using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStream.Data.Migrations
{
    /// <inheritdoc />
    public partial class OttPlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OttPlatforms",
                columns: table => new
                {
                    OttPlatformId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatformName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OttPlatforms", x => x.OttPlatformId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OttPlatforms");
        }
    }
}
