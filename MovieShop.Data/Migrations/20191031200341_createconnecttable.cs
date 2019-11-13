using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieShop.Data.Migrations
{
    public partial class createconnecttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieGenre",
                columns: table => new
                {
                    MovieId = table.Column<int>(nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    MovieGenreGenreId = table.Column<int>(nullable: true),
                    MovieGenreMovieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenre", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenre_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenre_MovieGenre_MovieGenreMovieId_MovieGenreGenreId",
                        columns: x => new { x.MovieGenreMovieId, x.MovieGenreGenreId },
                        principalTable: "MovieGenre",
                        principalColumns: new[] { "MovieId", "GenreId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_GenreId",
                table: "MovieGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_MovieGenreMovieId_MovieGenreGenreId",
                table: "MovieGenre",
                columns: new[] { "MovieGenreMovieId", "MovieGenreGenreId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenre");
        }
    }
}
