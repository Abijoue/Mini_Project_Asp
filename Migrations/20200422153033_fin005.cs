using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniProjet_Core.Migrations
{
    public partial class fin005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nbrPre",
                table: "Seances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    IdAbs = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<string>(nullable: false),
                    IdSeance = table.Column<int>(nullable: false),
                    NumSalle = table.Column<int>(nullable: false),
                    IdStudent = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.IdAbs);
                    table.ForeignKey(
                        name: "FK_Absences_Seances_IdSeance",
                        column: x => x.IdSeance,
                        principalTable: "Seances",
                        principalColumn: "IdSeance",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Absences_Students_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Students",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Absences_Salles_NumSalle",
                        column: x => x.NumSalle,
                        principalTable: "Salles",
                        principalColumn: "NumSalle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absences_IdSeance",
                table: "Absences",
                column: "IdSeance");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_IdStudent",
                table: "Absences",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_NumSalle",
                table: "Absences",
                column: "NumSalle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropColumn(
                name: "nbrPre",
                table: "Seances");
        }
    }
}
