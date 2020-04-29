using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniProjet_Core.Migrations
{
    public partial class fin008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Students_IdSeance",
                table: "Presences");

            migrationBuilder.CreateIndex(
                name: "IX_Presences_IdStudent",
                table: "Presences",
                column: "IdStudent");

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Students_IdStudent",
                table: "Presences",
                column: "IdStudent",
                principalTable: "Students",
                principalColumn: "IdStudent",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presences_Students_IdStudent",
                table: "Presences");

            migrationBuilder.DropIndex(
                name: "IX_Presences_IdStudent",
                table: "Presences");

            migrationBuilder.AddForeignKey(
                name: "FK_Presences_Students_IdSeance",
                table: "Presences",
                column: "IdSeance",
                principalTable: "Students",
                principalColumn: "IdStudent",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
