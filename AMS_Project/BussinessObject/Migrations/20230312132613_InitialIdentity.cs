using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    public partial class InitialIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //create table ConcurrencyStamp
            migrationBuilder.AddColumn<string>(
       name: "Name",
       table: "User",
       nullable: true);

          

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "User",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
