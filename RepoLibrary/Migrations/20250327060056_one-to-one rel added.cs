using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepoLibrary.Migrations
{
    /// <inheritdoc />
    public partial class onetoonereladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFamilyDetails",
                columns: table => new
                {
                    UserFamilyDetailsid = table.Column<int>(type: "int", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFamilyDetails", x => x.UserFamilyDetailsid);
                    table.ForeignKey(
                        name: "FK_UserFamilyDetails_Users_UserFamilyDetailsid",
                        column: x => x.UserFamilyDetailsid,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFamilyDetails");
        }
    }
}
