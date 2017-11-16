using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Freelanceme.Data.Migrations
{
    public partial class Update_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Client");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Project",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Project",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Project",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Client",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompany",
                table: "Client",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientId",
                table: "Project",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Client_ClientId",
                table: "Project",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Client_ClientId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ClientId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "IsCompany",
                table: "Client");

            migrationBuilder.AddColumn<bool>(
                name: "Company",
                table: "Client",
                nullable: false,
                defaultValue: false);
        }
    }
}
