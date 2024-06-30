using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Flag = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                });

            migrationBuilder.CreateTable(
                name: "Donors",
                columns: table => new
                {
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonorFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonorEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastDonationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceID = table.Column<int>(type: "int", nullable: false)
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
                name: "RedCross",
                columns: table => new
                {
                    RedCrossID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedCross", x => x.RedCrossID);
                    table.ForeignKey(
                        name: "FK_RedCross_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Officials",
                columns: table => new
                {
                    OfficialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficialFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthcareOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RedCrossID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Officials", x => x.OfficialID);
                    table.ForeignKey(
                        name: "FK_Officials_RedCross_RedCrossID",
                        column: x => x.RedCrossID,
                        principalTable: "RedCross",
                        principalColumn: "RedCrossID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    VolunteerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VolunteerEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    DateFreeFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFreeTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RedCrossID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.VolunteerID);
                    table.ForeignKey(
                        name: "FK_Volunteers_RedCross_RedCrossID",
                        column: x => x.RedCrossID,
                        principalTable: "RedCross",
                        principalColumn: "RedCrossID",
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
                    ActionTimeFromTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExactLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceID = table.Column<int>(type: "int", nullable: false),
                    OfficialID = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransfusionActions", x => x.ActionID);
                    table.ForeignKey(
                        name: "FK_TransfusionActions_Officials_OfficialID",
                        column: x => x.OfficialID,
                        principalTable: "Officials",
                        principalColumn: "OfficialID");
                    table.ForeignKey(
                        name: "FK_TransfusionActions_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedOfficial",
                columns: table => new
                {
                    OfficialID = table.Column<int>(type: "int", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedOfficial", x => new { x.OfficialID, x.ActionID });
                    table.ForeignKey(
                        name: "FK_AssignedOfficial_Officials_OfficialID",
                        column: x => x.OfficialID,
                        principalTable: "Officials",
                        principalColumn: "OfficialID");
                    table.ForeignKey(
                        name: "FK_AssignedOfficial_TransfusionActions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "TransfusionActions",
                        principalColumn: "ActionID");
                });

            migrationBuilder.CreateTable(
                name: "CallsToDonate",
                columns: table => new
                {
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    AcceptedTheCall = table.Column<bool>(type: "bit", nullable: false),
                    ShowedUp = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallsToDonate", x => new { x.JMBG, x.ActionID });
                    table.ForeignKey(
                        name: "FK_CallsToDonate_Donors_JMBG",
                        column: x => x.JMBG,
                        principalTable: "Donors",
                        principalColumn: "JMBG",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CallsToDonate_TransfusionActions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "TransfusionActions",
                        principalColumn: "ActionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CallsToVolunteer",
                columns: table => new
                {
                    VolunteerID = table.Column<int>(type: "int", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    AcceptedTheCall = table.Column<bool>(type: "bit", nullable: false),
                    ShowedUp = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallsToVolunteer", x => new { x.VolunteerID, x.ActionID });
                    table.ForeignKey(
                        name: "FK_CallsToVolunteer_TransfusionActions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "TransfusionActions",
                        principalColumn: "ActionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CallsToVolunteer_Volunteers_VolunteerID",
                        column: x => x.VolunteerID,
                        principalTable: "Volunteers",
                        principalColumn: "VolunteerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    QuestionnaireTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfMaking = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => new { x.JMBG, x.ActionID });
                    table.ForeignKey(
                        name: "FK_Questionnaires_Donors_JMBG",
                        column: x => x.JMBG,
                        principalTable: "Donors",
                        principalColumn: "JMBG",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questionnaires_TransfusionActions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "TransfusionActions",
                        principalColumn: "ActionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireQuestions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    QuestionnaireJMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionnaireActionID = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireQuestions", x => new { x.QuestionID, x.QuestionnaireJMBG, x.QuestionnaireActionID });
                    table.ForeignKey(
                        name: "FK_QuestionnaireQuestions_Questionnaires_QuestionnaireJMBG_QuestionnaireActionID",
                        columns: x => new { x.QuestionnaireJMBG, x.QuestionnaireActionID },
                        principalTable: "Questionnaires",
                        principalColumns: new[] { "JMBG", "ActionID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireQuestions_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Officials",
                columns: new[] { "OfficialID", "Discriminator", "OfficialFullName", "Password", "Username" },
                values: new object[] { 1, "Official", "Dunja Nesic", "123", "dule42" });

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
                table: "Questions",
                columns: new[] { "QuestionID", "Flag", "QuestionText" },
                values: new object[,]
                {
                    { 1, 0, "Da li redovno (svakodnevno) uzimate bilo kakve lekove?" },
                    { 2, 0, "Da li ste poslednja 2-3 dana uzimali bilo kakve lekove (npr. Brufen, Kafetin, Analgin...)?" },
                    { 3, 0, "Da li stalno uzimate Aspirin (Cardiopirin)? Da li ste ga uzimali u poslednjih 5 dana?" },
                    { 4, 0, "Da li ste do sada ispitivani ili lečeni u bolnici ili ste trenutno na ispitivanju ili bolovanju?" },
                    { 5, 0, "Da li ste vadili zub u proteklih 7 dana?" },
                    { 6, 0, "Da li ste u poslednjih 7 do 10 dana imali temperaturu preko 38 C, kijavicu, prehladu ili uzimali antibiotike?" },
                    { 7, 0, "Da li ste u poslednjih 6 meseci naglo izgubili na težini?" },
                    { 8, 0, "Da li ste imali ubode krpelja u proteklih 12 meseci i da li ste se zbog toga javljali lekaru?" },
                    { 9, 0, "Da li ste ikada lečeni od epilepsije (padavice), šećerne bolesti, astme, tuberkuloze, infarkta, moždanog udara, malignih oboljenja, mentalnih bolesti ili malarije?" },
                    { 10, 0, "Da li bolujete od neke druge hronične bolesti: srca, pluća, bubrega, jetre, želuca i creva, kostiju i zglobova, nervnog sistema, krvi i krvnih sudova?" },
                    { 11, 0, "Da li ste u proteklih 6 meseci putovali ili živeli u inostranstvu?" },
                    { 12, 0, "Da li ste ikada imali problema sa štitastom žlezdom, hipofizom i/ili primali hormone?" },
                    { 13, 0, "Da li imate neke promene na koži ili bolujete od alergije?" },
                    { 14, 0, "Da li dugo krvavite posle povrede ili spontano dobijate modrice?" },
                    { 15, 0, "Da li ste bolovali ili bolujete od hepatitisa (žutice) A, B ili C?" },
                    { 16, 0, "Da li ste u proteklih 6 meseci imali akupunkturu, piercing ili tetovažu?" },
                    { 17, 0, "Da li mislite da je postojala mogućnost da se zarazite HIV-om?" },
                    { 18, 0, "Da li ste ikada koristili bilo koju vrstu droge?" },
                    { 19, 0, "Da li ste ikada koristili preparate koji se zvanično ne izdaju na recept i/ili preparate za bodi bilding (steroide)?" },
                    { 20, 0, "Da li znate na koje sve načine ste mogli izložiti sebe riziku od zaraznih, krvlju prenosivih bolesti?" },
                    { 21, 0, "Da li ste u proteklih 6 meseci imali neku operaciju ili primili krv?" }
                });

            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "JMBG", "BloodType", "DonorEmailAddress", "DonorFullName", "IsActive", "LastDonationDate", "Password", "PlaceID" },
                values: new object[,]
                {
                    { "0101995700001", 6, "mijailovicmladen5@gmail.com", "Mladen Mijailovic", true, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "comi", 12 },
                    { "0202995800002", 4, "vladimir.lazarevic@fonis.rs", "Vladimir Lazarevic", true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "vlada", 4 },
                    { "0303995900003", 5, "sara.jana.djokic@gmail.com", "Sara Djokic", true, new DateTime(2023, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "srdjkc", 1 },
                    { "0407945940004", 2, "markovicc26@gmail.com", "Nemanja Markovic", false, new DateTime(2023, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "nmnj", 2 },
                    { "1104345940234", 1, "djordjemirkovic001@gmail.com", "Djordje Mirkovic", true, new DateTime(2023, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "djole", 2 }
                });

            migrationBuilder.InsertData(
                table: "RedCross",
                columns: new[] { "RedCrossID", "InstitutionName", "PlaceID" },
                values: new object[,]
                {
                    { 1, "CKSmederevo", 1 },
                    { 2, "CK_Vozdovac", 1 },
                    { 3, "Crveni krst NBG", 3 },
                    { 4, "CK Sremska Mitrovica", 4 }
                });

            migrationBuilder.InsertData(
                table: "TransfusionActions",
                columns: new[] { "ActionID", "ActionDate", "ActionName", "ActionTimeFromTo", "ExactLocation", "OfficialID", "PlaceID", "RowVersion" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "FON humanitarna akcija", "10:00 - 16:00", "Fakultet organizacionih nauka", 1, 2, new byte[0] },
                    { 2, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sportski centar Smederevo", "08:00 - 18:00", "Sportski centar", 1, 1, new byte[0] },
                    { 3, new DateTime(2024, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dobrovoljno davanje krvi u Vozdovim kapijama", "09:00 - 14:00", "Crveni Krst Vozdovac", 1, 2, new byte[0] },
                    { 4, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Krv za zivot", "11:00 - 17:00", "Opšta bolnica Niš", 1, 6, new byte[0] },
                    { 5, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daj krv, spasi zivot", "10:00 - 15:00", "Dom Zdravlja NP", 1, 12, new byte[0] }
                });

            migrationBuilder.InsertData(
                table: "Questionnaires",
                columns: new[] { "ActionID", "JMBG", "Approved", "QRCode", "QuestionnaireTitle", "Remark" },
                values: new object[,]
                {
                    { 5, "0101995700001", false, null, "Upitnik za akciju Daj krv, spasi zivot u Novom Pazaru", "Odbijen zbog niskog krvnog pritiska" },
                    { 1, "1104345940234", true, null, "Upitnik za akciju na FON-u", "/" },
                    { 3, "1104345940234", true, null, "Upitnik za akciju u Vozdovim kapijama", "Sve ok" }
                });

            migrationBuilder.InsertData(
                table: "Volunteers",
                columns: new[] { "VolunteerID", "DateFreeFrom", "DateFreeTo", "DateOfBirth", "Password", "RedCrossID", "Sex", "Username", "VolunteerEmailAddress", "VolunteerFullName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "leptirica", 1, 1, "minja", "filip.minja95@gmail.com", "Minja Filip" },
                    { 2, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "nensi", 2, 1, "nensi", "nevenadukic4@gmail.com", "Nevena Dukic" },
                    { 3, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "sof", 1, 1, "sofija", "sfilip2022.10215@atssb.edu.rs", "Sofija Filip" },
                    { 4, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2002, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "nesic", 1, 1, "vasa", "nesicvasilije02@gmail.com", "Vasilije Nesic" },
                    { 5, new DateTime(2024, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "poz", 2, 1, "pozitiva", "iva.djokovic@fonis.rs", "Iva Djokovic" },
                    { 6, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "zephe", 1, 0, "zippy", "zippy@gmail.com", "Veljko Nedeljkovic" },
                    { 7, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "pedja", 4, 0, "djpedja", "predrag.tanaskovic@fonis.rs", "Predrag Tanaskovic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedOfficial_ActionID",
                table: "AssignedOfficial",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_CallsToDonate_ActionID",
                table: "CallsToDonate",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_CallsToVolunteer_ActionID",
                table: "CallsToVolunteer",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_Donors_PlaceID",
                table: "Donors",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Officials_RedCrossID",
                table: "Officials",
                column: "RedCrossID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireQuestions_QuestionnaireJMBG_QuestionnaireActionID",
                table: "QuestionnaireQuestions",
                columns: new[] { "QuestionnaireJMBG", "QuestionnaireActionID" });

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaires_ActionID",
                table: "Questionnaires",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_RedCross_PlaceID",
                table: "RedCross",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_TransfusionActions_OfficialID",
                table: "TransfusionActions",
                column: "OfficialID");

            migrationBuilder.CreateIndex(
                name: "IX_TransfusionActions_PlaceID",
                table: "TransfusionActions",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_RedCrossID",
                table: "Volunteers",
                column: "RedCrossID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedOfficial");

            migrationBuilder.DropTable(
                name: "CallsToDonate");

            migrationBuilder.DropTable(
                name: "CallsToVolunteer");

            migrationBuilder.DropTable(
                name: "QuestionnaireQuestions");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Donors");

            migrationBuilder.DropTable(
                name: "TransfusionActions");

            migrationBuilder.DropTable(
                name: "Officials");

            migrationBuilder.DropTable(
                name: "RedCross");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
