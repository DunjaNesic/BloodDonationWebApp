using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PleaseWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0407945940004",
                columns: new[] { "DonorFullName", "Sex" },
                values: new object[] { "Jelena Subotic", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$tvyxd5iJc7m/2mdEH6p1c.AL.8e2xxoEX3Dwq7hcJ3HHtOwUxLtwe", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$PL0NnIsO.JDBFBN/QOJ3q.YAvkJBtWYwrzhLo0.XdlTzJazATsLbe", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$B0RhE/MtHE52eWoK/hJgvOccblMTcZM8UfO3R1XMbte8622QfNC/i", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "Email", "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "jelena.subotic@gmail.com", "$2a$11$YSx2MFHZtDyzbxZVVstFS.bkK83rJV9c6Z64JDeXqg0f8peoAxEZW", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$xw.7xkBox1uPv9Qdj92nx.gPts1sRLtO/Dsb3R6mZHfb7TllSu..C", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$3NIHlOI2NI30ZZFyBXfWzOHXysWqaPqPqibDEcFw7dWQmMe1/9/9C", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$x86XTnwWkHQJXy.ZfXGNAenhhCKXPnvgTwRqSK4MLYCM6XZ2w2yJq", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 8,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$kqPJ242lCefhA55ZQYCxBeTiFh0eAkb9G/AidWwhTchIyWsdpJ6.m", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 9,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$DnhCfiuQrpKRWjDmSm1B5eBKFz0aQwccyckqYSbbGH/fLgKnkJD6m", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 10,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$g/r6EOGyylRkDWQwX4rgXu5FMmZEDdlKpXzP2befTvwvLmQdl9vCS", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 11,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$B2d4GnhJRUa6b/YqmD/c4.izEt3B.Yf/0GJF.7wwC5.P9BT9pvpZK", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 12,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$BLDfxdZtFBHfV33bgK6V1.fgCV5lHrAWPblopx41tqiTjt6vM4Ire", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 13,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$sFTVkHFAPnF6bk/kkMUvVOmjuTnOsJanFjTrI5gn/8NpeCzLHyKyy", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 14,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$j0xOll1Xgpl3cURI3bNWi.0IfHerUZjYzGcRBT/FTaWMmmPvIAw9a", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 15,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$ouuqUJpzEdMTC0WuAoUoAexVK8Y6rcmkIaCDEFF4aQWC.5LMyOVsW", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 16,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$P6cT4XgewekGilSJsS/Pg.kYzAFCobMnWoQIgkYzS3zUAzHPad8SS", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 17,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$UTbBdH4jE.YRVKlcvhv.nOl2uYDP1urU5Y0o78D1/VxZK/zPLyXzO", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 18,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$ZdOjumlMUbsu19g/2SQKtuubrbsKLc0NsFSeDtigwPz74i/V84w.i", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 19,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$hNKsMbVmPoS2RgjLFljDBONZEosXRasvrHxtmw5J3wD7SCgd7BCMq", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 20,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$KZKDM52vHdrgLMWAu7iEo.MN7IhA3JVGJnfXJlsVjAM1dmIJsvZ1a", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 21,
                columns: new[] { "Password", "RefreshToken", "RefreshTokenExpiryTime" },
                values: new object[] { "$2a$11$8ZJr9JCmrtp0ZZGtJr/bt.9caqEmSQA0b6ddHAOoxiXw58DS35q4G", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Donors",
                keyColumn: "JMBG",
                keyValue: "0407945940004",
                columns: new[] { "DonorFullName", "Sex" },
                values: new object[] { "Nemanja Markovic", 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$YXWfiQbwUkNIfWBngbEF0.ObkG0YIOPHhf51bL/SxfdTrtSHMWnOG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$w5pkJ2avFOnRzr974jmxcOOEcM9um6iw/hrd.iCKu4AP2DHLxskca");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$m.u2OWBZzVJOSxfZv0.7u.rTgfjTiyBsSf9f9G/eUUjwg3UgfkOlq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4,
                columns: new[] { "Email", "Password" },
                values: new object[] { "nemanja.markovic@gmail.com", "$2a$11$C7nZQrsriG7U2LlRM5tjde/LCT9aQiOsfYhG0hH8P.RfN2EVLjCfy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5,
                column: "Password",
                value: "$2a$11$DF68.5gFIz/5UIvfR4aUpeKRUPvDMhssFB2km9bU0JKglNmlc9CE6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 6,
                column: "Password",
                value: "$2a$11$CA8LY5pbFyTdHfygczRrY.JHef7m9pqo8QC4Qx7bk9lE2gKT3ZJFe");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 7,
                column: "Password",
                value: "$2a$11$q4k.42a.othLaSFpGqDE5eBA4avZTopH2ci5ZkuXTZEsFMuERO41S");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 8,
                column: "Password",
                value: "$2a$11$H/iRvurucqB7r34iT/dkhuEKcDcRPATTfe3tlGwllN4nCUIMabahy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 9,
                column: "Password",
                value: "$2a$11$89OJHSDKAUPMhPnviN5rqee46r5eVsNTEJxxzOWpXSNuactNsX2UG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 10,
                column: "Password",
                value: "$2a$11$2fkgpOBFFxj4CUZIDEk7HOJDpKRk7kOcH/EcFft0ReNn31VSPoA82");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 11,
                column: "Password",
                value: "$2a$11$rklEmEk.kcpWhp1Pkrdry.qiusspLOmOJBLRryvDeAQUi/rQFzyK6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 12,
                column: "Password",
                value: "$2a$11$HOWDneaw0n0uog7mFhWt.uuN1O75sQzuDi8NTFR9HQ3/WhP16m1f6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 13,
                column: "Password",
                value: "$2a$11$R.EhCbON4CQyTodCXI7LNeLWY4jWTPbJxPUNbAA/sPFrnJGgHMNpK");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 14,
                column: "Password",
                value: "$2a$11$Ha2WR4l7FQWOsiWEJ3y0NePcDz4kikR3B95XmfBabZ/qVncUdekRG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 15,
                column: "Password",
                value: "$2a$11$5CMei/a5zyJXKO3E0fa4Oe9g3V8tT4wXdfI/kofch7GblkQF7wCZm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 16,
                column: "Password",
                value: "$2a$11$xUh2f86BxYjfdMH8U.HOeeBNwK33SrJBQXlDOHEyUKWPV/b7KwN6i");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 17,
                column: "Password",
                value: "$2a$11$RRxZ8KjFaNioLokTKPVOu.ACwd0Z0N9acS.KC/zWuw1yTrAHNEAtO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 18,
                column: "Password",
                value: "$2a$11$P078LI76mfrd38LMAq/xOeNWSiMd1xZ6V7J3nYnvZ3dJrbupKWPky");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 19,
                column: "Password",
                value: "$2a$11$qop8Sqon7QCvUdkgmlilrOk4MUPj69bHYf83PyeXtkyBrpBpFuf2C");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 20,
                column: "Password",
                value: "$2a$11$JxJgmzBqh3arvxSMAIMiPOC4aDn5vHLcnXOZJTIsQ4RfqaHKdMqJ2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 21,
                column: "Password",
                value: "$2a$11$oGUtg.IHoZU3aLGWm2/C5O.aTailywnkmzzVzScPoDTfyRGEpbY0q");
        }
    }
}
