using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WrestleApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationWithContinentCountryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continents",
                columns: table => new
                {
                    IdContinent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameContinent = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continents", x => x.IdContinent);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullNameCountry = table.Column<string>(type: "nvarchar(170)", maxLength: 170, nullable: false),
                    ShortNameCountry = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    UrlFlagCountry = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContinentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.IdCountry);
                    table.ForeignKey(
                        name: "FK_Countries_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "IdContinent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Continents",
                columns: new[] { "IdContinent", "NameContinent" },
                values: new object[,]
                {
                    { 1, "Europe" },
                    { 2, "Asia" },
                    { 3, "Africa" },
                    { 4, "North America" },
                    { 5, "South America" },
                    { 6, "Oceania" },
                    { 7, "Antarctica" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "IdCountry", "ContinentId", "FullNameCountry", "ShortNameCountry", "UrlFlagCountry" },
                values: new object[,]
                {
                    { 1, 1, "France", "FRA", "https://upload.wikimedia.org/wikipedia/commons/c/c3/Flag_of_France.svg?uselang=fr" },
                    { 2, 1, "Italy", "ITA", "https://upload.wikimedia.org/wikipedia/commons/0/03/Flag_of_Italy.svg?uselang=fr" },
                    { 3, 2, "China", "CHN", "https://upload.wikimedia.org/wikipedia/commons/f/fa/Flag_of_the_People%27s_Republic_of_China.svg?uselang=fr" },
                    { 4, 2, "Japan", "JPN", "https://upload.wikimedia.org/wikipedia/commons/9/9e/Flag_of_Japan.svg?uselang=fr" },
                    { 5, 3, "Algeria", "DZA", "https://upload.wikimedia.org/wikipedia/commons/7/77/Flag_of_Algeria.svg?uselang=fr" },
                    { 6, 3, "South Africa", "ZAF", "https://upload.wikimedia.org/wikipedia/commons/a/af/Flag_of_South_Africa.svg?uselang=fr" },
                    { 7, 4, "United States", "USA", "https://upload.wikimedia.org/wikipedia/commons/a/a4/Flag_of_the_United_States.svg?uselang=fr" },
                    { 8, 4, "Canada", "CAN", "https://upload.wikimedia.org/wikipedia/commons/c/cf/Flag_of_Canada.svg?uselang=fr" },
                    { 9, 5, "Brazil", "BRA", "https://upload.wikimedia.org/wikipedia/commons/0/05/Flag_of_Brazil.svg?uselang=fr" },
                    { 10, 5, "Argentina", "ARG", "https://upload.wikimedia.org/wikipedia/commons/1/1a/Flag_of_Argentina.svg?uselang=fr" },
                    { 11, 6, "Australia", "AUS", "https://upload.wikimedia.org/wikipedia/commons/b/b9/Flag_of_Australia.svg?uselang=fr" },
                    { 12, 6, "New Zealand", "NZL", "https://upload.wikimedia.org/wikipedia/commons/3/3e/Flag_of_New_Zealand.svg?uselang=fr" },
                    { 13, 7, "Antarctica", "ATA", "https://upload.wikimedia.org/wikipedia/commons/b/bb/Proposed_flag_of_Antarctica_%28Graham_Bartram%29.svg?uselang=fr" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_ContinentId",
                table: "Countries",
                column: "ContinentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Continents");
        }
    }
}
