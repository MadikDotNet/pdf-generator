using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfGenerator.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PdfConversions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    OriginFileName = table.Column<string>(type: "text", nullable: false),
                    OriginFilePath = table.Column<string>(type: "text", nullable: false),
                    ResultPath = table.Column<string>(type: "text", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdfConversions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PdfConversions_OriginFilePath",
                table: "PdfConversions",
                column: "OriginFilePath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PdfConversions_ResultPath",
                table: "PdfConversions",
                column: "ResultPath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PdfConversions_Status",
                table: "PdfConversions",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PdfConversions");
        }
    }
}
