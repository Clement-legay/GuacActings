using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guacactings.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "administrator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administrator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "document_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_type_administrator_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_document_type_administrator_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_service_administrator_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_service_administrator_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "administrator",
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
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_site_administrator_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_site_administrator_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_administrator_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_administrator_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_employee_site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_administrator_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_document_administrator_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "administrator",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_document_document_type_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "document_type",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_document_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_CreatedBy",
                table: "address",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_address_UpdatedBy",
                table: "address",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_administrator_EmployeeId",
                table: "administrator",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_document_CreatedBy",
                table: "document",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_document_DocumentTypeId",
                table: "document",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_document_EmployeeId",
                table: "document",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_document_UpdatedBy",
                table: "document",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_document_type_CreatedBy",
                table: "document_type",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_document_type_UpdatedBy",
                table: "document_type",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_employee_AddressId",
                table: "employee",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_CreatedBy",
                table: "employee",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_employee_ServiceId",
                table: "employee",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_SiteId",
                table: "employee",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_UpdatedBy",
                table: "employee",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_service_CreatedBy",
                table: "service",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_service_UpdatedBy",
                table: "service",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_site_AddressId",
                table: "site",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_site_CreatedBy",
                table: "site",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_site_UpdatedBy",
                table: "site",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_address_administrator_CreatedBy",
                table: "address",
                column: "CreatedBy",
                principalTable: "administrator",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_address_administrator_UpdatedBy",
                table: "address",
                column: "UpdatedBy",
                principalTable: "administrator",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_administrator_employee_EmployeeId",
                table: "administrator",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_address_administrator_CreatedBy",
                table: "address");

            migrationBuilder.DropForeignKey(
                name: "FK_address_administrator_UpdatedBy",
                table: "address");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_administrator_CreatedBy",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_administrator_UpdatedBy",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_service_administrator_CreatedBy",
                table: "service");

            migrationBuilder.DropForeignKey(
                name: "FK_service_administrator_UpdatedBy",
                table: "service");

            migrationBuilder.DropForeignKey(
                name: "FK_site_administrator_CreatedBy",
                table: "site");

            migrationBuilder.DropForeignKey(
                name: "FK_site_administrator_UpdatedBy",
                table: "site");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "document_type");

            migrationBuilder.DropTable(
                name: "administrator");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "service");

            migrationBuilder.DropTable(
                name: "site");

            migrationBuilder.DropTable(
                name: "address");
        }
    }
}
