using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HowTo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseDtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserUniqueInfoDtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastReadCourseId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUniqueInfoDtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViewDtos",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewDtos", x => new { x.CourseId, x.ArticleId });
                });

            migrationBuilder.CreateTable(
                name: "ContributorEntityDtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CourseDtoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContributorEntityDtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContributorEntityDtos_CourseDtos_CourseDtoId",
                        column: x => x.CourseDtoId,
                        principalTable: "CourseDtos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserViewEntityDtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserUniqueInfoDtoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewEntityDtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserViewEntityDtos_UserUniqueInfoDtos_UserUniqueInfoDtoId",
                        column: x => x.UserUniqueInfoDtoId,
                        principalTable: "UserUniqueInfoDtos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGuid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ViewDtoArticleId = table.Column<int>(type: "INTEGER", nullable: true),
                    ViewDtoCourseId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGuid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGuid_ViewDtos_ViewDtoCourseId_ViewDtoArticleId",
                        columns: x => new { x.ViewDtoCourseId, x.ViewDtoArticleId },
                        principalTable: "ViewDtos",
                        principalColumns: new[] { "CourseId", "ArticleId" });
                });

            migrationBuilder.CreateTable(
                name: "ArticleDtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    CourseDtoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleDtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleDtos_ContributorEntityDtos_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "ContributorEntityDtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleDtos_CourseDtos_CourseDtoId",
                        column: x => x.CourseDtoId,
                        principalTable: "CourseDtos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleDtos_AuthorId",
                table: "ArticleDtos",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleDtos_CourseDtoId",
                table: "ArticleDtos",
                column: "CourseDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorEntityDtos_CourseDtoId",
                table: "ContributorEntityDtos",
                column: "CourseDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGuid_ViewDtoCourseId_ViewDtoArticleId",
                table: "UserGuid",
                columns: new[] { "ViewDtoCourseId", "ViewDtoArticleId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserViewEntityDtos_UserUniqueInfoDtoId",
                table: "UserViewEntityDtos",
                column: "UserUniqueInfoDtoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleDtos");

            migrationBuilder.DropTable(
                name: "UserGuid");

            migrationBuilder.DropTable(
                name: "UserViewEntityDtos");

            migrationBuilder.DropTable(
                name: "ContributorEntityDtos");

            migrationBuilder.DropTable(
                name: "ViewDtos");

            migrationBuilder.DropTable(
                name: "UserUniqueInfoDtos");

            migrationBuilder.DropTable(
                name: "CourseDtos");
        }
    }
}
