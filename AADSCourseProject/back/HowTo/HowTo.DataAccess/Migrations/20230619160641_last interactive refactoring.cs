using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HowTo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class lastinteractiverefactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LastCheckListContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "LastCheckListContext");

            migrationBuilder.DropForeignKey(
                name: "FK_LastChoiceOfAnswerContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "LastChoiceOfAnswerContext");

            migrationBuilder.DropForeignKey(
                name: "FK_LastProgramWritingContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "LastProgramWritingContext");

            migrationBuilder.DropForeignKey(
                name: "FK_LastWritingOfAnswerContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "LastWritingOfAnswerContext");

            migrationBuilder.DropTable(
                name: "CheckListContext");

            migrationBuilder.DropTable(
                name: "ChoiceOfAnswerContext");

            migrationBuilder.DropTable(
                name: "ProgramWritingContext");

            migrationBuilder.DropTable(
                name: "WritingOfAnswerContext");

            migrationBuilder.DropIndex(
                name: "IX_LastWritingOfAnswerContext_UserUniqueInfoDtoId",
                table: "LastWritingOfAnswerContext");

            migrationBuilder.DropIndex(
                name: "IX_LastProgramWritingContext_UserUniqueInfoDtoId",
                table: "LastProgramWritingContext");

            migrationBuilder.DropIndex(
                name: "IX_LastChoiceOfAnswerContext_UserUniqueInfoDtoId",
                table: "LastChoiceOfAnswerContext");

            migrationBuilder.DropIndex(
                name: "IX_LastCheckListContext_UserUniqueInfoDtoId",
                table: "LastCheckListContext");

            migrationBuilder.DropColumn(
                name: "UserUniqueInfoDtoId",
                table: "LastWritingOfAnswerContext");

            migrationBuilder.DropColumn(
                name: "UserUniqueInfoDtoId",
                table: "LastProgramWritingContext");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "LastChoiceOfAnswerContext");

            migrationBuilder.DropColumn(
                name: "UserUniqueInfoDtoId",
                table: "LastChoiceOfAnswerContext");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "LastCheckListContext");

            migrationBuilder.DropColumn(
                name: "UserUniqueInfoDtoId",
                table: "LastCheckListContext");

            migrationBuilder.RenameColumn(
                name: "Success",
                table: "LogChoiceOfAnswerContext",
                newName: "InteractiveId");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "InteractiveContext",
                newName: "Discriminator");

            migrationBuilder.AddColumn<int>(
                name: "InteractiveId",
                table: "LogWritingOfAnswerContext",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InteractiveId",
                table: "LogProgramWritingContext",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SuccessAnswersJsonBoolArray",
                table: "LogChoiceOfAnswerContext",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "LastWritingOfAnswerContext",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "LastProgramWritingContext",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "AnswersJsonBoolArray",
                table: "LastChoiceOfAnswerContext",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "SuccessAnswersJsonBoolArray",
                table: "LastChoiceOfAnswerContext",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "LastChoiceOfAnswerContext",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "CheckedClausesJsonBoolArray",
                table: "LastCheckListContext",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "LastCheckListContext",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "InteractiveContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswersJsonBoolArray",
                table: "InteractiveContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClausesJsonStringArray",
                table: "InteractiveContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "InteractiveContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "InteractiveContext",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Output",
                table: "InteractiveContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionsJsonStringArray",
                table: "InteractiveContext",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InteractiveId",
                table: "LogWritingOfAnswerContext");

            migrationBuilder.DropColumn(
                name: "InteractiveId",
                table: "LogProgramWritingContext");

            migrationBuilder.DropColumn(
                name: "SuccessAnswersJsonBoolArray",
                table: "LogChoiceOfAnswerContext");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LastWritingOfAnswerContext");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LastProgramWritingContext");

            migrationBuilder.DropColumn(
                name: "SuccessAnswersJsonBoolArray",
                table: "LastChoiceOfAnswerContext");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LastChoiceOfAnswerContext");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LastCheckListContext");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "InteractiveContext");

            migrationBuilder.DropColumn(
                name: "AnswersJsonBoolArray",
                table: "InteractiveContext");

            migrationBuilder.DropColumn(
                name: "ClausesJsonStringArray",
                table: "InteractiveContext");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "InteractiveContext");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "InteractiveContext");

            migrationBuilder.DropColumn(
                name: "Output",
                table: "InteractiveContext");

            migrationBuilder.DropColumn(
                name: "QuestionsJsonStringArray",
                table: "InteractiveContext");

            migrationBuilder.RenameColumn(
                name: "InteractiveId",
                table: "LogChoiceOfAnswerContext",
                newName: "Success");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "InteractiveContext",
                newName: "Question");

            migrationBuilder.AddColumn<Guid>(
                name: "UserUniqueInfoDtoId",
                table: "LastWritingOfAnswerContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserUniqueInfoDtoId",
                table: "LastProgramWritingContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AnswersJsonBoolArray",
                table: "LastChoiceOfAnswerContext",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "LastChoiceOfAnswerContext",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserUniqueInfoDtoId",
                table: "LastChoiceOfAnswerContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CheckedClausesJsonBoolArray",
                table: "LastCheckListContext",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "LastCheckListContext",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserUniqueInfoDtoId",
                table: "LastCheckListContext",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckListContext",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InteractiveId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClausesJsonStringArray = table.Column<string>(type: "TEXT", nullable: false)
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
                    AnswersJsonBoolArray = table.Column<string>(type: "TEXT", nullable: false),
                    QuestionsJsonStringArray = table.Column<string>(type: "TEXT", nullable: false)
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
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false)
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
                name: "IX_LastWritingOfAnswerContext_UserUniqueInfoDtoId",
                table: "LastWritingOfAnswerContext",
                column: "UserUniqueInfoDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_LastProgramWritingContext_UserUniqueInfoDtoId",
                table: "LastProgramWritingContext",
                column: "UserUniqueInfoDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_LastChoiceOfAnswerContext_UserUniqueInfoDtoId",
                table: "LastChoiceOfAnswerContext",
                column: "UserUniqueInfoDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_LastCheckListContext_UserUniqueInfoDtoId",
                table: "LastCheckListContext",
                column: "UserUniqueInfoDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListContext_InteractiveId",
                table: "CheckListContext",
                column: "InteractiveId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoiceOfAnswerContext_InteractiveId",
                table: "ChoiceOfAnswerContext",
                column: "InteractiveId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramWritingContext_InteractiveId",
                table: "ProgramWritingContext",
                column: "InteractiveId");

            migrationBuilder.CreateIndex(
                name: "IX_WritingOfAnswerContext_InteractiveId",
                table: "WritingOfAnswerContext",
                column: "InteractiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_LastCheckListContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "LastCheckListContext",
                column: "UserUniqueInfoDtoId",
                principalTable: "UserUniqueInfoContext",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LastChoiceOfAnswerContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "LastChoiceOfAnswerContext",
                column: "UserUniqueInfoDtoId",
                principalTable: "UserUniqueInfoContext",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LastProgramWritingContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "LastProgramWritingContext",
                column: "UserUniqueInfoDtoId",
                principalTable: "UserUniqueInfoContext",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LastWritingOfAnswerContext_UserUniqueInfoContext_UserUniqueInfoDtoId",
                table: "LastWritingOfAnswerContext",
                column: "UserUniqueInfoDtoId",
                principalTable: "UserUniqueInfoContext",
                principalColumn: "Id");
        }
    }
}
