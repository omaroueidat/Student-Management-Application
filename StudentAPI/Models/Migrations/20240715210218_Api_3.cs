using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entites.Migrations
{
    /// <inheritdoc />
    public partial class Api_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Student_StudentId1",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Student_StudentId1",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Region_Countries_CountryId1",
                table: "Region");

            migrationBuilder.DropIndex(
                name: "IX_Region_CountryId1",
                table: "Region");

            migrationBuilder.DropIndex(
                name: "IX_Contact_StudentId1",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Address_StudentId1",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CountryId1",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Address");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountryId1",
                table: "Region",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId1",
                table: "Address",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: new Guid("179e627d-26a5-4a4b-b0a2-331518945033"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: new Guid("179e627d-26a5-4a4b-b0a2-331518945034"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: new Guid("179e627d-26a5-4a4b-b0a2-331518945035"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: new Guid("179e627d-26a5-4a4b-b0a2-331518945036"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Address",
                keyColumn: "AddressId",
                keyValue: new Guid("179e627d-26a5-4a4b-b0a2-331518945037"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contact",
                keyColumn: "ContactId",
                keyValue: new Guid("120e7474-5b9a-4b5a-95ed-1e138233f30b"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contact",
                keyColumn: "ContactId",
                keyValue: new Guid("120e7474-5b9a-4b5a-95ed-1e138233f30c"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contact",
                keyColumn: "ContactId",
                keyValue: new Guid("120e7474-5b9a-4b5a-95ed-1e138233f30d"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Contact",
                keyColumn: "ContactId",
                keyValue: new Guid("120e7474-5b9a-4b5a-95ed-1e138233f30e"),
                column: "StudentId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51150"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51151"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51152"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51153"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51154"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51155"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51156"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51157"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51158"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51159"),
                column: "CountryId1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Region",
                keyColumn: "RegionId",
                keyValue: new Guid("2a46c752-0c8b-486e-9674-d86e37c51160"),
                column: "CountryId1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryId1",
                table: "Region",
                column: "CountryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_StudentId1",
                table: "Contact",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StudentId1",
                table: "Address",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Student_StudentId1",
                table: "Address",
                column: "StudentId1",
                principalTable: "Student",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Student_StudentId1",
                table: "Contact",
                column: "StudentId1",
                principalTable: "Student",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Region_Countries_CountryId1",
                table: "Region",
                column: "CountryId1",
                principalTable: "Countries",
                principalColumn: "CountryId");
        }
    }
}
