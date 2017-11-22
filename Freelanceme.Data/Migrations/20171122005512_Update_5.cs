using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Freelanceme.Data.Migrations
{
    public partial class Update_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "TimeTracking",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracking_ProjectId",
                table: "TimeTracking",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracking_Project_ProjectId",
                table: "TimeTracking",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracking_Project_ProjectId",
                table: "TimeTracking");

            migrationBuilder.DropIndex(
                name: "IX_TimeTracking_ProjectId",
                table: "TimeTracking");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TimeTracking");
        }
    }
}
