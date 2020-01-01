using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyEventManagement.Persistence.Migrations
{
    public partial class Update002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "EmailAddress", "EventId", "Name", "Position" },
                values: new object[] { 1, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dan@org.com", null, "Daniel Agg", "Developer" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "EmailAddress", "EventId", "Name", "Position" },
                values: new object[] { 2, new DateTime(2019, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "pete@org.com", null, "Peter Smith", "Manager" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "EmailAddress", "EventId", "Name", "Position" },
                values: new object[] { 3, new DateTime(2019, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "jackie@org.com", null, "Jackie Taylor", "SysAdmin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
