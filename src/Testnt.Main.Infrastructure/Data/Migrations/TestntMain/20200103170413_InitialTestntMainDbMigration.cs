using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Testnt.Main.Infrastructure.Data.Migrations.TestntMain
{
    public partial class InitialTestntMainDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestOutlines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TestProjectId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TestExecutionResult = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    TestScenarioId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestOutlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestOutlines_Projects_TestProjectId",
                        column: x => x.TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestOutlines_TestOutlines_TestScenarioId",
                        column: x => x.TestScenarioId,
                        principalTable: "TestOutlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestOutlines_Projects_TestProjectId1",
                        column: x => x.TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TestProjectId = table.Column<Guid>(nullable: false),
                    Started = table.Column<DateTimeOffset>(nullable: false),
                    Finished = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSession_Projects_TestProjectId",
                        column: x => x.TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false),
                    TestProjectId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestTags_Projects_TestProjectId",
                        column: x => x.TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestStep",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestCaseId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStep", x => new { x.TestCaseId, x.Id });
                    table.ForeignKey(
                        name: "FK_TestStep_TestOutlines_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestOutlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestScenarioSnapshot",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestScenarioId = table.Column<Guid>(nullable: false),
                    TestScenarioName = table.Column<string>(nullable: true),
                    TestExecutionResult = table.Column<int>(nullable: false),
                    TestSessionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestScenarioSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestScenarioSnapshot_TestSession_TestSessionId",
                        column: x => x.TestSessionId,
                        principalTable: "TestSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestTag",
                columns: table => new
                {
                    TestOutlineId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTag", x => new { x.TestOutlineId, x.TagId });
                    table.ForeignKey(
                        name: "FK_TestTag_TestOutlines_TagId",
                        column: x => x.TagId,
                        principalTable: "TestOutlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestTag_TestTags_TestOutlineId",
                        column: x => x.TestOutlineId,
                        principalTable: "TestTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCaseSnapshot",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestCaseId = table.Column<Guid>(nullable: false),
                    TestCaseName = table.Column<string>(nullable: true),
                    TestScenarioSnapshotId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCaseSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCaseSnapshot_TestScenarioSnapshot_TestScenarioSnapshotId",
                        column: x => x.TestScenarioSnapshotId,
                        principalTable: "TestScenarioSnapshot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestStepSnapshot",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TestCaseSnapshotId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStepSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestStepSnapshot_TestCaseSnapshot_TestCaseSnapshotId",
                        column: x => x.TestCaseSnapshotId,
                        principalTable: "TestCaseSnapshot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseSnapshot_TestScenarioSnapshotId",
                table: "TestCaseSnapshot",
                column: "TestScenarioSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOutlines_TestProjectId",
                table: "TestOutlines",
                column: "TestProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOutlines_TestScenarioId",
                table: "TestOutlines",
                column: "TestScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOutlines_TestProjectId1",
                table: "TestOutlines",
                column: "TestProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestScenarioSnapshot_TestSessionId",
                table: "TestScenarioSnapshot",
                column: "TestSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSession_TestProjectId",
                table: "TestSession",
                column: "TestProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStepSnapshot_TestCaseSnapshotId",
                table: "TestStepSnapshot",
                column: "TestCaseSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTag_TagId",
                table: "TestTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTags_TestProjectId",
                table: "TestTags",
                column: "TestProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestStep");

            migrationBuilder.DropTable(
                name: "TestStepSnapshot");

            migrationBuilder.DropTable(
                name: "TestTag");

            migrationBuilder.DropTable(
                name: "TestCaseSnapshot");

            migrationBuilder.DropTable(
                name: "TestOutlines");

            migrationBuilder.DropTable(
                name: "TestTags");

            migrationBuilder.DropTable(
                name: "TestScenarioSnapshot");

            migrationBuilder.DropTable(
                name: "TestSession");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
