using Microsoft.EntityFrameworkCore.Migrations;

namespace NotificationService.Infrastructure.Migrations
{
    public partial class AddPropertyIsSent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_sent",
                table: "telegram_messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_sent",
                table: "sms_messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_sent",
                table: "email_messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_sent",
                table: "telegram_messages");

            migrationBuilder.DropColumn(
                name: "is_sent",
                table: "sms_messages");

            migrationBuilder.DropColumn(
                name: "is_sent",
                table: "email_messages");
        }
    }
}
