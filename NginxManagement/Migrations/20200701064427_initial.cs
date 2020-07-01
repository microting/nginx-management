using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace NginxManagement.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PluginConfigurationValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginConfigurationValues", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PluginConfigurationValues",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Name", "UpdatedAt", "UpdatedByUserId", "Value", "Version", "WorkflowState" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 7, 1, 9, 44, 27, 123, DateTimeKind.Local).AddTicks(1754), -1, "BaseSettings:ServiceAccessToken", null, 0, "", 1, "created" },
                    { 2, new DateTime(2020, 7, 1, 9, 44, 27, 148, DateTimeKind.Local).AddTicks(5498), -1, "BaseSettings:Host", null, 0, "microting.com", 1, "created" },
                    { 3, new DateTime(2020, 7, 1, 9, 44, 27, 148, DateTimeKind.Local).AddTicks(5581), -1, "BaseSettings:AccessLogTemplate", null, 0, "/var/log/nginx/nginx.{0}.access.log", 1, "created" },
                    { 4, new DateTime(2020, 7, 1, 9, 44, 27, 148, DateTimeKind.Local).AddTicks(5594), -1, "BaseSettings:ErrorLogTemplate", null, 0, "/var/log/nginx/nginx.{0}.error.log", 1, "created" },
                    { 5, new DateTime(2020, 7, 1, 9, 44, 27, 148, DateTimeKind.Local).AddTicks(5605), -1, "BaseSettings:PostExecuteCommand", null, 0, "/etc/init.d/nginx reload", 1, "created" },
                    { 6, new DateTime(2020, 7, 1, 9, 44, 27, 148, DateTimeKind.Local).AddTicks(5616), -1, "BaseSettings:DestinationPath", null, 0, "/etc/nginx/sites-enabled/", 1, "created" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hosts");

            migrationBuilder.DropTable(
                name: "PluginConfigurationValues");
        }
    }
}
