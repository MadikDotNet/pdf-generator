﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfGenerator.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAtColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                table: "PdfConversions");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PdfConversions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PdfConversions");

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                table: "PdfConversions",
                type: "text",
                nullable: true);
        }
    }
}
