using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Testnt.Main.Infrastructure.Data.Migrations
{
    public partial class addtenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TestTags",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TestStepSnapshot",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TestStep",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TestSessions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TestScenarioSnapshot",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TestOutlines",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TestFeatures",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "TestCaseSnapshot",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Projects",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TestTags");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TestStepSnapshot");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TestStep");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TestSessions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TestScenarioSnapshot");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TestOutlines");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TestFeatures");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TestCaseSnapshot");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Projects");
        }
    }
}
