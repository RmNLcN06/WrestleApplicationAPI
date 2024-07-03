using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WrestleApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrationCountryCityRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    IdCity = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCity = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.IdCity);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "IdCity", "CountryId", "NameCity" },
                values: new object[,]
                {
                    { 1, 1, "Paris" },
                    { 2, 1, "Marseille" },
                    { 3, 2, "Milan" },
                    { 4, 2, "Rome" },
                    { 5, 3, "Wuhan" },
                    { 6, 3, "Jinan" },
                    { 7, 4, "Tokyo" },
                    { 8, 4, "Osaka" },
                    { 9, 5, "Alger" },
                    { 10, 5, "Annaba" },
                    { 11, 6, "Tzaneen" },
                    { 12, 6, "Brits" },
                    { 13, 7, "Washington" },
                    { 14, 7, "Los Angeles" },
                    { 15, 8, "Toronto" },
                    { 16, 8, "Quebec" },
                    { 17, 9, "Rio de Janeiro" },
                    { 18, 9, "Sao Paulo" },
                    { 19, 10, "Buenos Aires" },
                    { 20, 10, "Santa Fe" },
                    { 21, 11, "Sydney" },
                    { 22, 11, "Darwin" },
                    { 23, 12, "Wellington" },
                    { 24, 12, "Auckland" },
                    { 25, 13, "Antarctica" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
