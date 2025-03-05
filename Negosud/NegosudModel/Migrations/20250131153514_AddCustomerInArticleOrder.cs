using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegosudModel.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerInArticleOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "ArticleOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOrders_PersonId",
                table: "ArticleOrders",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleOrders_Persons_PersonId",
                table: "ArticleOrders",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleOrders_Persons_PersonId",
                table: "ArticleOrders");

            migrationBuilder.DropIndex(
                name: "IX_ArticleOrders_PersonId",
                table: "ArticleOrders");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ArticleOrders");
        }
    }
}
