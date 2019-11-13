using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieShop.Data.Migrations
{
    public partial class createagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_MovieGenre_MovieGenreMovieId_MovieGenreGenreId",
                table: "MovieGenre");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenre_MovieGenreMovieId_MovieGenreGenreId",
                table: "MovieGenre");

            migrationBuilder.DropColumn(
                name: "MovieGenreGenreId",
                table: "MovieGenre");

            migrationBuilder.DropColumn(
                name: "MovieGenreMovieId",
                table: "MovieGenre");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieGenreGenreId",
                table: "MovieGenre",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieGenreMovieId",
                table: "MovieGenre",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_MovieGenreMovieId_MovieGenreGenreId",
                table: "MovieGenre",
                columns: new[] { "MovieGenreMovieId", "MovieGenreGenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_MovieGenre_MovieGenreMovieId_MovieGenreGenreId",
                table: "MovieGenre",
                columns: new[] { "MovieGenreMovieId", "MovieGenreGenreId" },
                principalTable: "MovieGenre",
                principalColumns: new[] { "MovieId", "GenreId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
