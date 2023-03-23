using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guacactings.Migrations
{
    /// <inheritdoc />
    public partial class JobAndJobOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job_offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoneyPerHour = table.Column<int>(type: "int", nullable: false),
                    HoursPerWeek = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job_offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_job_offer_site_SiteId",
                        column: x => x.SiteId,
                        principalTable: "site",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "job",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobOfferId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job", x => x.Id);
                    table.ForeignKey(
                        name: "FK_job_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_job_job_offer_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "job_offer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_job_EmployeeId",
                table: "job",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_job_JobOfferId",
                table: "job",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_job_offer_SiteId",
                table: "job_offer",
                column: "SiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job");

            migrationBuilder.DropTable(
                name: "job_offer");
        }
    }
}
