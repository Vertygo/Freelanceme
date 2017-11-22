using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Freelanceme.Data.Migrations
{
    public partial class Update_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracking_Client_ClientId",
                table: "TimeTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracking_Project_ProjectId",
                table: "TimeTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracking_User_UserId",
                table: "TimeTracking");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "TimeTracking",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "TimeTracking",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "TimeTracking",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracking_Client_ClientId",
                table: "TimeTracking",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracking_Project_ProjectId",
                table: "TimeTracking",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracking_User_UserId",
                table: "TimeTracking",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracking_Client_ClientId",
                table: "TimeTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracking_Project_ProjectId",
                table: "TimeTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracking_User_UserId",
                table: "TimeTracking");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "TimeTracking",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "TimeTracking",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "TimeTracking",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracking_Client_ClientId",
                table: "TimeTracking",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracking_Project_ProjectId",
                table: "TimeTracking",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracking_User_UserId",
                table: "TimeTracking",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
