using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonationApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Officials");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Officials");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Donors");

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionID", "Flag", "QuestionText" },
                values: new object[,]
                {
                    { 22, 1, "Da li je davalac u dobrom opštem zdravstvenom stanju?" },
                    { 23, 1, "Da li davalac ima normalne vitalne znakove (krvni pritisak, puls, temperatura)?" },
                    { 24, 1, "Da li su nalazi krvne slike davaoca u granicama normale?" },
                    { 25, 1, "Da li je koža davaoca bez osipa, rana ili infekcija?" },
                    { 26, 1, "Da li je srčani ritam davaoca regularan bez znakova aritmije?" },
                    { 27, 1, "Da li su pluća davaoca čista i bez znakova infekcije ili zagušenja?" },
                    { 28, 1, "Da li davalac ima normalan nivo hemoglobina?" },
                    { 29, 1, "Da li davalac pokazuje znake anemije ili drugih krvnih poremećaja?" },
                    { 30, 1, "Da li je rezultat testa za HIV, hepatitis B, hepatitis C i sifilis negativan?" },
                    { 31, 1, "Da li davalac ima adekvatan nivo hidratacije i nije dehidriran?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionID",
                keyValue: 31);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Volunteers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Officials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Officials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0101995700001",
                column: "Password",
                value: "comi");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0303995900003",
                column: "Password",
                value: "srdjkc");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0407945940004",
                column: "Password",
                value: "nmnj");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "1104001765020",
                column: "Password",
                value: "sakisan");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "1104345940234",
                column: "Password",
                value: "vlada");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "1107001543432",
                column: "Password",
                value: "pera");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "1505001498898",
                column: "Password",
                value: "kotlaja");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "1604345940234",
                column: "Password",
                value: "djole");

            migrationBuilder.UpdateData(
                table: "Officials",
                keyColumn: "OfficialID",
                keyValue: 1,
                columns: new[] { "Password", "Username" },
                values: new object[] { "123", "dule42" });

            migrationBuilder.UpdateData(
                table: "Officials",
                keyColumn: "OfficialID",
                keyValue: 2,
                columns: new[] { "Password", "Username" },
                values: new object[] { "456", "stefanJov3107" });

            migrationBuilder.UpdateData(
                table: "Officials",
                keyColumn: "OfficialID",
                keyValue: 3,
                columns: new[] { "Password", "Username" },
                values: new object[] { "789", "gasa" });

            migrationBuilder.UpdateData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 1,
                columns: new[] { "Password", "Username" },
                values: new object[] { "poz", "pozitiva" });

            migrationBuilder.UpdateData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 2,
                columns: new[] { "Password", "Username" },
                values: new object[] { "nensi", "nensi" });

            migrationBuilder.UpdateData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 3,
                columns: new[] { "Password", "Username" },
                values: new object[] { "pedja", "djpedja" });

            migrationBuilder.UpdateData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 4,
                columns: new[] { "Password", "Username" },
                values: new object[] { "zephe", "zippy" });

            migrationBuilder.UpdateData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 5,
                columns: new[] { "Password", "Username" },
                values: new object[] { "nesic", "vasa" });

            migrationBuilder.UpdateData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 6,
                columns: new[] { "Password", "Username" },
                values: new object[] { "leptirica", "minja" });

            migrationBuilder.UpdateData(
                table: "Volunteers",
                keyColumn: "VolunteerID",
                keyValue: 7,
                columns: new[] { "Password", "Username" },
                values: new object[] { "sof", "sofija" });
        }
    }
}
