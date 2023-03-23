using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guacactings.Migrations
{
    /// <inheritdoc />
    public partial class NewSiteAndEnterpriseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enterprise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Siret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enterprise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_enterprise_address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "address",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    EnterpriseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_site_address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_site_enterprise_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "enterprise",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_enterprise_AddressId",
                table: "enterprise",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_site_AddressId",
                table: "site",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_site_EnterpriseId",
                table: "site",
                column: "EnterpriseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "site");

            migrationBuilder.DropTable(
                name: "enterprise");
        }
    }
}
