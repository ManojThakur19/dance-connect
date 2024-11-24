using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanceConnect.Server.Migrations
{
    /// <inheritdoc />
    public partial class MessagReplycolumnaddedinContactUsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MessageResponse",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageResponse",
                table: "Contacts");
        }
    }
}
