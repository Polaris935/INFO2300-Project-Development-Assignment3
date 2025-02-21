using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare_And_Wellness.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    jobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.jobID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "applicants",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emailUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jobID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicants", x => x.userID);
                    table.ForeignKey(
                        name: "FK_applicants_jobs_jobID",
                        column: x => x.jobID,
                        principalTable: "jobs",
                        principalColumn: "jobID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "ConfirmPassword", "ContentType", "DateOfBirth", "Name", "Password", "ProfilePic", "Role", "Username" },
                values: new object[] { 1, 30, "Admin@1234", null, "1994-01-01", "Administrator", "Admin@1234", null, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "ConfirmPassword", "ContentType", "DateOfBirth", "Name", "Password", "ProfilePic", "Role", "Username" },
                values: new object[] { 2, 21, "Lovepreet@1234", null, "2003-10-26", "Lovepreet Singh", "Lovepreet@1234", null, "Member", "member" });

            migrationBuilder.InsertData(
                table: "jobs",
                columns: new[] { "jobID", "description", "jobName", "statusJob" },
                values: new object[] { 1, "The responsibility of the Instructor Therapist is to deliver direct ABA (Applied Behaviour Analysis) interventions. This includes providing input into the development and implementation of Behaviour Support Plans (BSPs), collecting baseline data, maintaining progress notes (i.e., case notes), the recording and graphing of relevant data, parent coaching, individual and group services, as well as the preparation of teaching materials and also working with, and mentoring volunteers.", "Instructor Therapist", "Apply" });

            migrationBuilder.CreateIndex(
                name: "IX_applicants_jobID",
                table: "applicants",
                column: "jobID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "applicants");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "jobs");
        }
    }
}
