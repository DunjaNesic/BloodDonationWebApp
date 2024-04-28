using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceID);
                });

            migrationBuilder.CreateTable(
                name: "TransfusionCoordinators",
                columns: table => new
                {
                    CoordinatorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoordinatorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinatorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransfusionCoordinators", x => x.CoordinatorID);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DonorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonorEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    LastDonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceID = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donors", x => x.JMBG);
                    table.ForeignKey(
                        name: "FK_Donors_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransfusionActions",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionTimeFromTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExactLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransfusionActions", x => x.ActionID);
                    table.ForeignKey(
                        name: "FK_TransfusionActions_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    VolunteerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    DateFreeFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFreeTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceID = table.Column<int>(type: "int", nullable: false),
                    VolunteerEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.VolunteerID);
                    table.ForeignKey(
                        name: "FK_Volunteers_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CallToDonate",
                columns: table => new
                {
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallToDonate", x => new { x.JMBG, x.ActionID });
                    table.ForeignKey(
                        name: "FK_CallToDonate_Donors_JMBG",
                        column: x => x.JMBG,
                        principalTable: "Donors",
                        principalColumn: "JMBG",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallToDonate_TransfusionActions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "TransfusionActions",
                        principalColumn: "ActionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    QuestionnaireID = table.Column<int>(type: "int", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    QuestionnaireTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => new { x.JMBG, x.QuestionnaireID, x.ActionID });
                    table.ForeignKey(
                        name: "FK_Questionnaires_Donors_JMBG",
                        column: x => x.JMBG,
                        principalTable: "Donors",
                        principalColumn: "JMBG",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questionnaires_TransfusionActions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "TransfusionActions",
                        principalColumn: "ActionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CallToVolunteer",
                columns: table => new
                {
                    VolunteerID = table.Column<int>(type: "int", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallToVolunteer", x => new { x.VolunteerID, x.ActionID });
                    table.ForeignKey(
                        name: "FK_CallToVolunteer_TransfusionActions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "TransfusionActions",
                        principalColumn: "ActionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallToVolunteer_Volunteers_VolunteerID",
                        column: x => x.VolunteerID,
                        principalTable: "Volunteers",
                        principalColumn: "VolunteerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    QuestionnaireID = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => new { x.JMBG, x.QuestionnaireID, x.ActionID, x.QuestionID });
                    table.ForeignKey(
                        name: "FK_Questions_Questionnaires_JMBG_QuestionnaireID_ActionID",
                        columns: x => new { x.JMBG, x.QuestionnaireID, x.ActionID },
                        principalTable: "Questionnaires",
                        principalColumns: new[] { "JMBG", "QuestionnaireID", "ActionID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CallToDonate_ActionID",
                table: "CallToDonate",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_CallToVolunteer_ActionID",
                table: "CallToVolunteer",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_PlaceID",
                table: "Donors",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_ActionID",
                table: "Questionnaires",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransfusionActions_PlaceID",
                table: "TransfusionActions",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_PlaceID",
                table: "Volunteers",
                column: "PlaceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallToDonate");

            migrationBuilder.DropTable(
                name: "CallToVolunteer");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "TransfusionCoordinators");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "TransfusionActions");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
