using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafio.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionforMapperInEventHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventHistories_SubscriptionId",
                table: "EventHistories",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventHistories_Subscriptions_SubscriptionId",
                table: "EventHistories",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventHistories_Subscriptions_SubscriptionId",
                table: "EventHistories");

            migrationBuilder.DropIndex(
                name: "IX_EventHistories_SubscriptionId",
                table: "EventHistories");
        }
    }
}
