using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTestBot.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Activity = table.Column<string>(nullable: true),
                    ActivityRu = table.Column<string>(nullable: true),
                    Accessibility = table.Column<double>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Participants = table.Column<int>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
