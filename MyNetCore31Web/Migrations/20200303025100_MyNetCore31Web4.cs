using Microsoft.EntityFrameworkCore.Migrations;

namespace MyNetCore31Web.Migrations
{
    public partial class MyNetCore31Web4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                 name: "RoleNameIndex",
                 table: "AspNetRoles",
                 column: "NormalizedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
               name: "RoleNameIndex",
               table: "AspNetRoles",
               column: "NormalizedName",
               unique: true,
               filter: "[NormalizedName] IS NOT NULL");
           
        }
    }
}
