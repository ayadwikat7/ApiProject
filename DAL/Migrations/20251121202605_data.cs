using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorTransoulations_Caregories_CaregoryId",
                table: "CategorTransoulations");

            migrationBuilder.DropIndex(
                name: "IX_CategorTransoulations_CaregoryId",
                table: "CategorTransoulations");

            migrationBuilder.DropColumn(
                name: "CaregoryId",
                table: "CategorTransoulations");

            migrationBuilder.CreateIndex(
                name: "IX_CategorTransoulations_CategoryId",
                table: "CategorTransoulations",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorTransoulations_Caregories_CategoryId",
                table: "CategorTransoulations",
                column: "CategoryId",
                principalTable: "Caregories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorTransoulations_Caregories_CategoryId",
                table: "CategorTransoulations");

            migrationBuilder.DropIndex(
                name: "IX_CategorTransoulations_CategoryId",
                table: "CategorTransoulations");

            migrationBuilder.AddColumn<int>(
                name: "CaregoryId",
                table: "CategorTransoulations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CategorTransoulations_CaregoryId",
                table: "CategorTransoulations",
                column: "CaregoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorTransoulations_Caregories_CaregoryId",
                table: "CategorTransoulations",
                column: "CaregoryId",
                principalTable: "Caregories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
