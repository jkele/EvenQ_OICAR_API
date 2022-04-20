using Microsoft.EntityFrameworkCore.Migrations;

namespace EvenQ_API.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipQRCodeInfo",
                table: "Members");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "MembershipQRCodeInfo",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
