using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class RentalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rental_CustomerId",
                table: "Rental",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_MovieId",
                table: "Rental",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_customers_CustomerId",
                table: "Rental",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_movies_MovieId",
                table: "Rental",
                column: "MovieId",
                principalTable: "movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_customers_CustomerId",
                table: "Rental");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_movies_MovieId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_CustomerId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Rental_MovieId",
                table: "Rental");
        }
    }
}
