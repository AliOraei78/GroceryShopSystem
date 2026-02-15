using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryShopSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_product",
                table: "product");

            migrationBuilder.RenameTable(
                name: "product",
                newName: "user");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user",
                table: "user",
                column: "id");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "product");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product",
                table: "product",
                column: "id");
        }
    }
}
