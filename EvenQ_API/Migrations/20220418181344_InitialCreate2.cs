using Microsoft.EntityFrameworkCore.Migrations;

namespace EvenQ_API.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refferals_Members_InviteeUID",
                table: "Refferals");

            migrationBuilder.DropForeignKey(
                name: "FK_Refferals_Members_InviterUID",
                table: "Refferals");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Members_MemberUID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MemberUID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Refferals_InviteeUID",
                table: "Refferals");

            migrationBuilder.DropIndex(
                name: "IX_Refferals_InviterUID",
                table: "Refferals");

            migrationBuilder.DropColumn(
                name: "MemberUID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "InviteeUID",
                table: "Refferals");

            migrationBuilder.DropColumn(
                name: "InviterUID",
                table: "Refferals");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "InviterId",
                table: "Refferals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "InviteeId",
                table: "Refferals",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MemberId",
                table: "Tickets",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Refferals_InviteeId",
                table: "Refferals",
                column: "InviteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Refferals_InviterId",
                table: "Refferals",
                column: "InviterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refferals_Members_InviteeId",
                table: "Refferals",
                column: "InviteeId",
                principalTable: "Members",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Refferals_Members_InviterId",
                table: "Refferals",
                column: "InviterId",
                principalTable: "Members",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Members_MemberId",
                table: "Tickets",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refferals_Members_InviteeId",
                table: "Refferals");

            migrationBuilder.DropForeignKey(
                name: "FK_Refferals_Members_InviterId",
                table: "Refferals");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Members_MemberId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MemberId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Refferals_InviteeId",
                table: "Refferals");

            migrationBuilder.DropIndex(
                name: "IX_Refferals_InviterId",
                table: "Refferals");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberUID",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InviterId",
                table: "Refferals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InviteeId",
                table: "Refferals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InviteeUID",
                table: "Refferals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InviterUID",
                table: "Refferals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MemberUID",
                table: "Tickets",
                column: "MemberUID");

            migrationBuilder.CreateIndex(
                name: "IX_Refferals_InviteeUID",
                table: "Refferals",
                column: "InviteeUID");

            migrationBuilder.CreateIndex(
                name: "IX_Refferals_InviterUID",
                table: "Refferals",
                column: "InviterUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Refferals_Members_InviteeUID",
                table: "Refferals",
                column: "InviteeUID",
                principalTable: "Members",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Refferals_Members_InviterUID",
                table: "Refferals",
                column: "InviterUID",
                principalTable: "Members",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Members_MemberUID",
                table: "Tickets",
                column: "MemberUID",
                principalTable: "Members",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
