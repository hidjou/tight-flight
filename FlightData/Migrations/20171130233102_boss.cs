using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FlightData.Migrations
{
    public partial class boss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceEmployees_MaintenanceTypes_MaintenanceTypeId",
                table: "MaintenanceEmployees");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceEmployees_MaintenanceTypeId",
                table: "MaintenanceEmployees");

            migrationBuilder.DropColumn(
                name: "MaintenanceTypeId",
                table: "MaintenanceEmployees");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceId",
                table: "MaintenanceEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceEmployees_MaintenanceId",
                table: "MaintenanceEmployees",
                column: "MaintenanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceEmployees_Maintenances_MaintenanceId",
                table: "MaintenanceEmployees",
                column: "MaintenanceId",
                principalTable: "Maintenances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceEmployees_Maintenances_MaintenanceId",
                table: "MaintenanceEmployees");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceEmployees_MaintenanceId",
                table: "MaintenanceEmployees");

            migrationBuilder.DropColumn(
                name: "MaintenanceId",
                table: "MaintenanceEmployees");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceTypeId",
                table: "MaintenanceEmployees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceEmployees_MaintenanceTypeId",
                table: "MaintenanceEmployees",
                column: "MaintenanceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceEmployees_MaintenanceTypes_MaintenanceTypeId",
                table: "MaintenanceEmployees",
                column: "MaintenanceTypeId",
                principalTable: "MaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
