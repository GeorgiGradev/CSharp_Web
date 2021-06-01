using Microsoft.EntityFrameworkCore.Migrations;

namespace Suls.Migrations
{
    public partial class InitialCreateCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_UserTrips_ProblemId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTrips_Users_UserId",
                table: "UserTrips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTrips",
                table: "UserTrips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.RenameTable(
                name: "UserTrips",
                newName: "Problems");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "Submissions");

            migrationBuilder.RenameIndex(
                name: "IX_UserTrips_UserId",
                table: "Problems",
                newName: "IX_Problems_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_UserId",
                table: "Submissions",
                newName: "IX_Submissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_ProblemId",
                table: "Submissions",
                newName: "IX_Submissions_ProblemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Problems",
                table: "Problems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_Users_UserId",
                table: "Problems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Problems_ProblemId",
                table: "Submissions",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Users_UserId",
                table: "Submissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_Users_UserId",
                table: "Problems");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Problems_ProblemId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Users_UserId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Problems",
                table: "Problems");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Trips");

            migrationBuilder.RenameTable(
                name: "Problems",
                newName: "UserTrips");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_UserId",
                table: "Trips",
                newName: "IX_Trips_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_ProblemId",
                table: "Trips",
                newName: "IX_Trips_ProblemId");

            migrationBuilder.RenameIndex(
                name: "IX_Problems_UserId",
                table: "UserTrips",
                newName: "IX_UserTrips_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTrips",
                table: "UserTrips",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_UserTrips_ProblemId",
                table: "Trips",
                column: "ProblemId",
                principalTable: "UserTrips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTrips_Users_UserId",
                table: "UserTrips",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
