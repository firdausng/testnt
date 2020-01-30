using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Testnt.Main.Infrastructure.Data.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false),
                    TestProjectId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Projects_TestProjectId",
                        column: x => x.TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TestProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestFeatures_Projects_TestProjectId",
                        column: x => x.TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TestProjectId = table.Column<Guid>(nullable: false),
                    Started = table.Column<DateTimeOffset>(nullable: false),
                    Finished = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSessions_Projects_TestProjectId",
                        column: x => x.TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    UserProfileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.ProjectId, x.UserProfileId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_UserProfiles_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestOutlines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TestExecutionResult = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    TestProjectId = table.Column<Guid>(nullable: true),
                    TestScenarioId = table.Column<Guid>(nullable: true),
                    TestScenario_TestProjectId = table.Column<Guid>(nullable: true),
                    TestFeatureId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestOutlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestOutlines_Projects_TestProjectId",
                        column: x => x.TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestOutlines_TestOutlines_TestScenarioId",
                        column: x => x.TestScenarioId,
                        principalTable: "TestOutlines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestOutlines_TestFeatures_TestFeatureId",
                        column: x => x.TestFeatureId,
                        principalTable: "TestFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestOutlines_Projects_TestScenario_TestProjectId",
                        column: x => x.TestScenario_TestProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestScenarioSnapshot",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    TestScenarioId = table.Column<Guid>(nullable: false),
                    TestScenarioName = table.Column<string>(nullable: true),
                    TestExecutionResult = table.Column<int>(nullable: false),
                    TestSessionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestScenarioSnapshot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestScenarioSnapshot_TestSessions_TestSessionId",
                        column: x => x.TestSessionId,
                        principalTable: "TestSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestStep",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestCaseId = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
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
                        name: "FK_TestTag_Tags_TestOutlineId",
                        column: x => x.TestOutlineId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCaseSnapshot",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
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
                    TenantId = table.Column<Guid>(nullable: false),
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
                name: "IX_ProjectUser_UserProfileId",
                table: "ProjectUser",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TestProjectId",
                table: "Tags",
                column: "TestProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCaseSnapshot_TestScenarioSnapshotId",
                table: "TestCaseSnapshot",
                column: "TestScenarioSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_TestFeatures_TestProjectId",
                table: "TestFeatures",
                column: "TestProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOutlines_TestProjectId",
                table: "TestOutlines",
                column: "TestProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOutlines_TestScenarioId",
                table: "TestOutlines",
                column: "TestScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOutlines_TestFeatureId",
                table: "TestOutlines",
                column: "TestFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TestOutlines_TestScenario_TestProjectId",
                table: "TestOutlines",
                column: "TestScenario_TestProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestScenarioSnapshot_TestSessionId",
                table: "TestScenarioSnapshot",
                column: "TestSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSessions_TestProjectId",
                table: "TestSessions",
                column: "TestProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStepSnapshot_TestCaseSnapshotId",
                table: "TestStepSnapshot",
                column: "TestCaseSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTag_TagId",
                table: "TestTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.DropTable(
                name: "TestStep");

            migrationBuilder.DropTable(
                name: "TestStepSnapshot");

            migrationBuilder.DropTable(
                name: "TestTag");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "TestCaseSnapshot");

            migrationBuilder.DropTable(
                name: "TestOutlines");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TestScenarioSnapshot");

            migrationBuilder.DropTable(
                name: "TestFeatures");

            migrationBuilder.DropTable(
                name: "TestSessions");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
