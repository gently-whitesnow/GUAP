using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HowTo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddInteractive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleDtos_ContributorEntityDtos_AuthorId",
                table: "ArticleDtos");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleDtos_CourseDtos_CourseDtoId",
                table: "ArticleDtos");

            migrationBuilder.DropForeignKey(
                name: "FK_ContributorEntityDtos_CourseDtos_CourseDtoId",
                table: "ContributorEntityDtos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGuid_ViewDtos_ViewDtoCourseId_ViewDtoArticleId",
                table: "UserGuid");

            migrationBuilder.DropForeignKey(
                name: "FK_UserViewEntityDtos_UserUniqueInfoDtos_UserUniqueInfoDtoId",
                table: "UserViewEntityDtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ViewDtos",
                table: "ViewDtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserViewEntityDtos",
                table: "UserViewEntityDtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUniqueInfoDtos",
                table: "UserUniqueInfoDtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDtos",
                table: "CourseDtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContributorEntityDtos",
                table: "ContributorEntityDtos");

            migrationBuilder.DropIndex(
                name: "IX_ContributorEntityDtos_CourseDtoId",
                table: "ContributorEntityDtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleDtos",
                table: "ArticleDtos");

            migrationBuilder.DropColumn(
                name: "CourseDtoId",
                table: "ContributorEntityDtos");

            migrationBuilder.RenameTable(
                name: "ViewDtos",
                newName: "ViewContext");

            migrationBuilder.RenameTable(
                name: "UserViewEntityDtos",
                newName: "UserViewEntityContext");

            migrationBuilder.RenameTable(
                name: "UserUniqueInfoDtos",
                newName: "UserUniqueInfoContext");

            migrationBuilder.RenameTable(
                name: "CourseDtos",
                newName: "CourseContext");

            migrationBuilder.RenameTable(
                name: "ContributorEntityDtos",
                newName: "ContributorEntityContext");

            migrationBuilder.RenameTable(
                name: "ArticleDtos",
                newName: "ArticleContext");

            migrationBuilder.RenameIndex(
                name: "IX_UserViewEntityDtos_UserUniqueInfoDtoId",
                table: "UserViewEntityContext",
                newName: "IX_UserViewEntityContext_UserUniqueInfoDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleDtos_CourseDtoId",
                table: "ArticleContext",
                newName: "IX_ArticleContext_CourseDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleDtos_AuthorId",
                table: "ArticleContext",
                newName: "IX_ArticleContext_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ViewContext",
                table: "ViewContext",
                columns: new[] { "CourseId", "ArticleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserViewEntityContext",
                table: "UserViewEntityContext",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUniqueInfoContext",
                table: "UserUniqueInfoContext",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseContext",
                table: "CourseContext",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContributorEntityContext",
                table: "ContributorEntityContext",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleContext",
                table: "ArticleContext",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InteractiveContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteractiveContext", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LastCheckListContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckedClausesJsonBoolArray = table.Column<bool>(type: "INTEGER", nullable: false),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserUniqueInfoDtoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastCheckListContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LastCheckListContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                        column: x => x.UserUniqueInfoDtoId,
                        principalTable: "UserUniqueInfoContext",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LastChoiceOfAnswerContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnswersJsonBoolArray = table.Column<bool>(type: "INTEGER", nullable: false),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserUniqueInfoDtoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastChoiceOfAnswerContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LastChoiceOfAnswerContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                        column: x => x.UserUniqueInfoDtoId,
                        principalTable: "UserUniqueInfoContext",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LastProgramWritingContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserUniqueInfoDtoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastProgramWritingContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LastProgramWritingContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                        column: x => x.UserUniqueInfoDtoId,
                        principalTable: "UserUniqueInfoContext",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LastWritingOfAnswerContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArticleId = table.Column<int>(type: "INTEGER", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserUniqueInfoDtoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastWritingOfAnswerContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LastWritingOfAnswerContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                        column: x => x.UserUniqueInfoDtoId,
                        principalTable: "UserUniqueInfoContext",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogChoiceOfAnswerContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnswersJsonBoolArray = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    LogDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogChoiceOfAnswerContext", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogProgramWritingContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    LogDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogProgramWritingContext", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogWritingOfAnswerContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Success = table.Column<bool>(type: "INTEGER", nullable: false),
                    LogDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogWritingOfAnswerContext", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckListContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClausesJsonStringArray = table.Column<string>(type: "TEXT", nullable: false),
                    InteractiveId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckListContext_InteractiveContext_InteractiveId",
                        column: x => x.InteractiveId,
                        principalTable: "InteractiveContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoiceOfAnswerContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionsJsonStringArray = table.Column<string>(type: "TEXT", nullable: false),
                    AnswersJsonBoolArray = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoiceOfAnswerContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoiceOfAnswerContext_InteractiveContext_InteractiveId",
                        column: x => x.InteractiveId,
                        principalTable: "InteractiveContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramWritingContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    CodeExample = table.Column<string>(type: "TEXT", nullable: false),
                    Output = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramWritingContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramWritingContext_InteractiveContext_InteractiveId",
                        column: x => x.InteractiveId,
                        principalTable: "InteractiveContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WritingOfAnswerContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WritingOfAnswerContext", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WritingOfAnswerContext_InteractiveContext_InteractiveId",
                        column: x => x.InteractiveId,
                        principalTable: "InteractiveContext",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListContext_InteractiveId",
                table: "CheckListContext",
                column: "InteractiveId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceOfAnswerContext_InteractiveId",
                table: "ChoiceOfAnswerContext",
                column: "InteractiveId");

            migrationBuilder.CreateIndex(
                name: "IX_LastCheckListContext_UserUniqueInfoDtoId",
                table: "LastCheckListContext",
                column: "UserUniqueInfoDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_LastChoiceOfAnswerContext_UserUniqueInfoDtoId",
                table: "LastChoiceOfAnswerContext",
                column: "UserUniqueInfoDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_LastProgramWritingContext_UserUniqueInfoDtoId",
                table: "LastProgramWritingContext",
                column: "UserUniqueInfoDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_LastWritingOfAnswerContext_UserUniqueInfoDtoId",
                table: "LastWritingOfAnswerContext",
                column: "UserUniqueInfoDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramWritingContext_InteractiveId",
                table: "ProgramWritingContext",
                column: "InteractiveId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingOfAnswerContext_InteractiveId",
                table: "WritingOfAnswerContext",
                column: "InteractiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleContext_ContributorEntityContext_AuthorId",
                table: "ArticleContext",
                column: "AuthorId",
                principalTable: "ContributorEntityContext",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleContext_CourseContext_CourseDtoId",
                table: "ArticleContext",
                column: "CourseDtoId",
                principalTable: "CourseContext",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGuid_ViewContext_ViewDtoCourseId_ViewDtoArticleId",
                table: "UserGuid",
                columns: new[] { "ViewDtoCourseId", "ViewDtoArticleId" },
                principalTable: "ViewContext",
                principalColumns: new[] { "CourseId", "ArticleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserViewEntityContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "UserViewEntityContext",
                column: "UserUniqueInfoDtoId",
                principalTable: "UserUniqueInfoContext",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleContext_ContributorEntityContext_AuthorId",
                table: "ArticleContext");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleContext_CourseContext_CourseDtoId",
                table: "ArticleContext");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGuid_ViewContext_ViewDtoCourseId_ViewDtoArticleId",
                table: "UserGuid");

            migrationBuilder.DropForeignKey(
                name: "FK_UserViewEntityContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "UserViewEntityContext");

            migrationBuilder.DropTable(
                name: "CheckListContext");

            migrationBuilder.DropTable(
                name: "ChoiceOfAnswerContext");

            migrationBuilder.DropTable(
                name: "LastCheckListContext");

            migrationBuilder.DropTable(
                name: "LastChoiceOfAnswerContext");

            migrationBuilder.DropTable(
                name: "LastProgramWritingContext");

            migrationBuilder.DropTable(
                name: "LastWritingOfAnswerContext");

            migrationBuilder.DropTable(
                name: "LogChoiceOfAnswerContext");

            migrationBuilder.DropTable(
                name: "LogProgramWritingContext");

            migrationBuilder.DropTable(
                name: "LogWritingOfAnswerContext");

            migrationBuilder.DropTable(
                name: "ProgramWritingContext");

            migrationBuilder.DropTable(
                name: "WritingOfAnswerContext");

            migrationBuilder.DropTable(
                name: "InteractiveContext");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ViewContext",
                table: "ViewContext");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserViewEntityContext",
                table: "UserViewEntityContext");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUniqueInfoContext",
                table: "UserUniqueInfoContext");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseContext",
                table: "CourseContext");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContributorEntityContext",
                table: "ContributorEntityContext");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleContext",
                table: "ArticleContext");

            migrationBuilder.RenameTable(
                name: "ViewContext",
                newName: "ViewDtos");

            migrationBuilder.RenameTable(
                name: "UserViewEntityContext",
                newName: "UserViewEntityDtos");

            migrationBuilder.RenameTable(
                name: "UserUniqueInfoContext",
                newName: "UserUniqueInfoDtos");

            migrationBuilder.RenameTable(
                name: "CourseContext",
                newName: "CourseDtos");

            migrationBuilder.RenameTable(
                name: "ContributorEntityContext",
                newName: "ContributorEntityDtos");

            migrationBuilder.RenameTable(
                name: "ArticleContext",
                newName: "ArticleDtos");

            migrationBuilder.RenameIndex(
                name: "IX_UserViewEntityContext_UserUniqueInfoDtoId",
                table: "UserViewEntityDtos",
                newName: "IX_UserViewEntityDtos_UserUniqueInfoDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleContext_CourseDtoId",
                table: "ArticleDtos",
                newName: "IX_ArticleDtos_CourseDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleContext_AuthorId",
                table: "ArticleDtos",
                newName: "IX_ArticleDtos_AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "CourseDtoId",
                table: "ContributorEntityDtos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ViewDtos",
                table: "ViewDtos",
                columns: new[] { "CourseId", "ArticleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserViewEntityDtos",
                table: "UserViewEntityDtos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUniqueInfoDtos",
                table: "UserUniqueInfoDtos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDtos",
                table: "CourseDtos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContributorEntityDtos",
                table: "ContributorEntityDtos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleDtos",
                table: "ArticleDtos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContributorEntityDtos_CourseDtoId",
                table: "ContributorEntityDtos",
                column: "CourseDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleDtos_ContributorEntityDtos_AuthorId",
                table: "ArticleDtos",
                column: "AuthorId",
                principalTable: "ContributorEntityDtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleDtos_CourseDtos_CourseDtoId",
                table: "ArticleDtos",
                column: "CourseDtoId",
                principalTable: "CourseDtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContributorEntityDtos_CourseDtos_CourseDtoId",
                table: "ContributorEntityDtos",
                column: "CourseDtoId",
                principalTable: "CourseDtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGuid_ViewDtos_ViewDtoCourseId_ViewDtoArticleId",
                table: "UserGuid",
                columns: new[] { "ViewDtoCourseId", "ViewDtoArticleId" },
                principalTable: "ViewDtos",
                principalColumns: new[] { "CourseId", "ArticleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserViewEntityDtos_UserUniqueInfoDtos_UserUniqueInfoDtoId",
                table: "UserViewEntityDtos",
                column: "UserUniqueInfoDtoId",
                principalTable: "UserUniqueInfoDtos",
                principalColumn: "Id");
        }
    }
}
