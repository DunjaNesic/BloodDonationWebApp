using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonationApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "PlaceID", "PlaceName" },
                values: new object[,]
                {
                    { 1, "Smederevo" },
                    { 2, "Beograd" },
                    { 3, "Sremska Mitrovica" },
                    { 4, "Cacak" },
                    { 5, "Novi Sad" },
                    { 6, "Nis" },
                    { 7, "Kraljevo" },
                    { 8, "Subotica" },
                    { 9, "Zrenjanin" },
                    { 10, "Pancevo" },
                    { 11, "Vlasotince" },
                    { 12, "Novi Pazar" },
                    { 13, "Kragujevac" }
                });

            migrationBuilder.InsertData(
                table: "TransfusionCoordinators",
                columns: new[] { "CoordinatorID", "CoordinatorCode", "CoordinatorFullName", "Password" },
                values: new object[,]
                {
                    { 1, "DN4213", "Dunja Nesic", "Dunja.Nesic13" },
                    { 2, "PN1107", "Petar Nikodijevic", "PeraSD" },
                    { 3, "SJ3107", "Stefan Jovanovic", "StefanJo3107" }
                });

            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "JMBG", "BloodType", "DonorEmailAddress", "DonorFullName", "IsActive", "LastDonationDate", "PlaceID" },
                values: new object[,]
                {
                    { "0101995700001", 6, "mijailovicmladen5@gmail.com", "Mladen Mijailovic", 1, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { "0202995800002", 4, "vladimir.lazarevic@fonis.rs", "Vladimir Lazarevic", 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { "0303995900003", 5, "sara.jana.djokic@gmail.com", "Sara Djokic", 1, new DateTime(2023, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { "0407945940004", 2, "markovicc26@gmail.com", "Nemanja Markovic", 0, new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { "1104345940234", 1, "djordjemirkovic001@gmail.com", "Djordje Mirkovic", 1, new DateTime(2023, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "TransfusionActions",
                columns: new[] { "ActionID", "ActionDate", "ActionName", "ActionTimeFromTo", "ExactLocation", "PlaceID" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "FON humanitarna akcija", "10:00 - 16:00", "Fakultet organizacionih nauka", 2 },
                    { 2, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sportski centar Smederevo", "08:00 - 18:00", "Sportski centar", 1 },
                    { 3, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dobrovoljno davanje krvi u Vozdovim kapijama", "09:00 - 14:00", "Crveni Krst Vozdovac", 2 },
                    { 4, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Krv za zivot", "11:00 - 17:00", "Opšta bolnica Niš", 6 },
                    { 5, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daj krv, spasi zivot", "10:00 - 15:00", "Dom Zdravlja NP", 12 }
                });

            migrationBuilder.InsertData(
                table: "Volunteers",
                columns: new[] { "VolunteerID", "DateFreeFrom", "DateFreeTo", "DateOfBirth", "PlaceID", "Sex", "VolunteerEmailAddress", "VolunteerFullName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "filip.minja95@gmail.com", "Minja Filip" },
                    { 2, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "nevenadukic4@gmail.com", "Nevena Dukic" },
                    { 3, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "sfilip2022.10215@atssb.edu.rs", "Sofija Filip" },
                    { 4, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "nesicvasilije02@gmail.com", "Vasilije Nesic" },
                    { 5, new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "iva.djokovic@fonis.rs", "Iva Djokovic" },
                    { 6, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, "zippy@gmail.com", "Veljko Nedeljkovic" },
                    { 7, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 0, "predrag.tanaskovic@fonis.rs", "Predrag Tanaskovic" }
                });

            migrationBuilder.InsertData(
                table: "Questionnaires",
                columns: new[] { "ActionID", "JMBG", "QuestionnaireID", "QuestionnaireTitle", "Remark" },
                values: new object[,]
                {
                    { 5, "0101995700001", 1, "Upitnik za akciju Daj krv, spasi zivot u Novom Pazaru", "Odbijen zbog niskog krvnog pritiska" },
                    { 1, "1104345940234", 2, "Upitnik za akciju na FON-u", "/" },
                    { 3, "1104345940234", 3, "Upitnik za akciju u Vozdovim kapijama", "Sve ok" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0202995800002");

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0303995900003");

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0407945940004");

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Questionnaires",
                keyColumns: new[] { "ActionID", "JMBG", "QuestionnaireID" },
                keyValues: new object[] { 5, "0101995700001", 1 });

            migrationBuilder.DeleteData(
                table: "Questionnaires",
                keyColumns: new[] { "ActionID", "JMBG", "QuestionnaireID" },
                keyValues: new object[] { 1, "1104345940234", 2 });

            migrationBuilder.DeleteData(
                table: "Questionnaires",
                keyColumns: new[] { "ActionID", "JMBG", "QuestionnaireID" },
                keyValues: new object[] { 3, "1104345940234", 3 });

            migrationBuilder.DeleteData(
                table: "TransfusionActions",
                keyColumn: "ActionID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransfusionActions",
                keyColumn: "ActionID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TransfusionCoordinators",
                keyColumn: "CoordinatorID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransfusionCoordinators",
                keyColumn: "CoordinatorID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TransfusionCoordinators",
                keyColumn: "CoordinatorID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0101995700001");

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "1104345940234");

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TransfusionActions",
                keyColumn: "ActionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TransfusionActions",
                keyColumn: "ActionID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TransfusionActions",
                keyColumn: "ActionID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "PlaceID",
                keyValue: 12);
        }
    }
}
