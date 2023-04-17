using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guacactings.Migrations
{
    /// <inheritdoc />
    public partial class DefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "administrator",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 17, 11, 54, 42, 361, DateTimeKind.Local).AddTicks(580), "$2a$11$ehqIFO73f8XVceFjdCcoN.DzyhlPWmI9tUs1xVaij6lKFr5r898ZC", new DateTime(2023, 4, 17, 11, 54, 42, 361, DateTimeKind.Local).AddTicks(670) });

            migrationBuilder.UpdateData(
                table: "employee",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "CreatedAt", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2023, 4, 17, 11, 54, 42, 217, DateTimeKind.Local).AddTicks(2900), new DateTime(2023, 4, 17, 11, 54, 42, 217, DateTimeKind.Local).AddTicks(2970), new DateTime(2023, 4, 17, 11, 54, 42, 217, DateTimeKind.Local).AddTicks(2980), "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "administrator",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2023, 4, 17, 11, 52, 34, 260, DateTimeKind.Local).AddTicks(4540), "$2a$11$61IUUgVcj490o8c6bPe.xO8DnRu47N3qhDfzojOyLyDAoB9V.nQym", new DateTime(2023, 4, 17, 11, 52, 34, 260, DateTimeKind.Local).AddTicks(4620) });

            migrationBuilder.UpdateData(
                table: "employee",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "CreatedAt", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2023, 4, 17, 11, 52, 34, 115, DateTimeKind.Local).AddTicks(7920), new DateTime(2023, 4, 17, 11, 52, 34, 115, DateTimeKind.Local).AddTicks(8010), new DateTime(2023, 4, 17, 11, 52, 34, 115, DateTimeKind.Local).AddTicks(8010), null });
        }
    }
}
