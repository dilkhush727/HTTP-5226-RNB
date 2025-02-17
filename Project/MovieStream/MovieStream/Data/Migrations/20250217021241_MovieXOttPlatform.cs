using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStream.Data.Migrations
{
    /// <inheritdoc />
    public partial class MovieXOttPlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieXOttPlatforms",
                columns: table => new
                {
                    MovieXOttPlatformId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    OttPlatformId = table.Column<int>(type: "int", nullable: false),
                    AvailabilityDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieXOttPlatforms", x => x.MovieXOttPlatformId);
                    table.ForeignKey(
                        name: "FK_MovieXOttPlatforms_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieXOttPlatforms_OttPlatforms_OttPlatformId",
                        column: x => x.OttPlatformId,
                        principalTable: "OttPlatforms",
                        principalColumn: "OttPlatformId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieXOttPlatforms_MovieId",
                table: "MovieXOttPlatforms",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieXOttPlatforms_OttPlatformId",
                table: "MovieXOttPlatforms",
                column: "OttPlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieXOttPlatforms");
        }
    }
}
