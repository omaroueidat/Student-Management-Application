using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entites.Migrations
{
    /// <inheritdoc />
    public partial class Api_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    CodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code", x => x.CodeId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "CodeValue",
                columns: table => new
                {
                    CodeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeValue", x => x.CodeValueId);
                    table.ForeignKey(
                        name: "FK_CodeValue_Code_CodeId",
                        column: x => x.CodeId,
                        principalTable: "Code",
                        principalColumn: "CodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Region_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Region_Countries_CountryId1",
                        column: x => x.CountryId1,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isPrimary = table.Column<bool>(type: "bit", nullable: false),
                    CodeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contact_CodeValue_CodeValueId",
                        column: x => x.CodeValueId,
                        principalTable: "CodeValue",
                        principalColumn: "CodeValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_Student_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Student",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    isPrimary = table.Column<bool>(type: "bit", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_CodeValue_CodeValueId",
                        column: x => x.CodeValueId,
                        principalTable: "CodeValue",
                        principalColumn: "CodeValueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Student_StudentId1",
                        column: x => x.StudentId1,
                        principalTable: "Student",
                        principalColumn: "StudentId");
                });

            migrationBuilder.InsertData(
                table: "Code",
                columns: new[] { "CodeId", "Name" },
                values: new object[,]
                {
                    { 1, "AddressType" },
                    { 2, "ContactType" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryName" },
                values: new object[,]
                {
                    { new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a52"), "Lebanon" },
                    { new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a53"), "France" },
                    { new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a54"), "Syria" },
                    { new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a55"), "Jordan" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentId", "DateOfBirth", "StudentName", "gender" },
                values: new object[,]
                {
                    { new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f771"), new DateTime(2003, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omar Oueidat", 0 },
                    { new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f772"), new DateTime(2003, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alex Alex", 0 },
                    { new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f773"), new DateTime(2003, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "John John", 0 },
                    { new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f774"), new DateTime(2003, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "June", 1 },
                    { new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f775"), new DateTime(2003, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Query", 1 }
                });

            migrationBuilder.InsertData(
                table: "CodeValue",
                columns: new[] { "CodeValueId", "CodeId", "Value" },
                values: new object[,]
                {
                    { new Guid("86189048-87ba-4d2d-942e-d55db1eddac1"), 1, "Summer" },
                    { new Guid("86189048-87ba-4d2d-942e-d55db1eddac2"), 1, "Home" },
                    { new Guid("86189048-87ba-4d2d-942e-d55db1eddac3"), 1, "Work" },
                    { new Guid("86189048-87ba-4d2d-942e-d55db1eddac5"), 2, "Mobile" },
                    { new Guid("86189048-87ba-4d2d-942e-d55db1eddad1"), 2, "Home" }
                });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "RegionId", "CountryId", "CountryId1", "RegionName" },
                values: new object[,]
                {
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51150"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a52"), null, "Beirut" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51151"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a52"), null, "Hamra" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51152"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a52"), null, "Tripoli" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51153"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a52"), null, "Sayda" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51154"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a53"), null, "Paris" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51155"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a53"), null, "Nouvelle" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51156"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a53"), null, "Auvergne" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51157"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a54"), null, "Hims" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51158"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a54"), null, "Damascus " },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51159"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a55"), null, "Irbid" },
                    { new Guid("2a46c752-0c8b-486e-9674-d86e37c51160"), new Guid("19b427fc-c141-48d4-9d7c-b7dee4074a55"), null, "Mafraq" }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "AddressValue", "CodeValueId", "RegionId", "StudentId", "StudentId1", "isPrimary" },
                values: new object[,]
                {
                    { new Guid("179e627d-26a5-4a4b-b0a2-331518945033"), "Street-1 Region X Buiding Y Floor Z", new Guid("86189048-87ba-4d2d-942e-d55db1eddac2"), new Guid("2a46c752-0c8b-486e-9674-d86e37c51150"), new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f771"), null, true },
                    { new Guid("179e627d-26a5-4a4b-b0a2-331518945034"), "Street-1 Region X Buiding Y Floor Z", new Guid("86189048-87ba-4d2d-942e-d55db1eddac3"), new Guid("2a46c752-0c8b-486e-9674-d86e37c51151"), new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f772"), null, true },
                    { new Guid("179e627d-26a5-4a4b-b0a2-331518945035"), "Street-1 Region X Buiding Y Floor Z", new Guid("86189048-87ba-4d2d-942e-d55db1eddac3"), new Guid("2a46c752-0c8b-486e-9674-d86e37c51152"), new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f773"), null, true },
                    { new Guid("179e627d-26a5-4a4b-b0a2-331518945036"), "Street-1 Region X Buiding Y Floor Z", new Guid("86189048-87ba-4d2d-942e-d55db1eddac1"), new Guid("2a46c752-0c8b-486e-9674-d86e37c51154"), new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f773"), null, false },
                    { new Guid("179e627d-26a5-4a4b-b0a2-331518945037"), "Street-1 Region X Buiding Y Floor Z", new Guid("86189048-87ba-4d2d-942e-d55db1eddac2"), new Guid("2a46c752-0c8b-486e-9674-d86e37c51157"), new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f775"), null, true }
                });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "ContactId", "CodeValueId", "ContactValue", "StudentId", "StudentId1", "isPrimary" },
                values: new object[,]
                {
                    { new Guid("120e7474-5b9a-4b5a-95ed-1e138233f30b"), new Guid("86189048-87ba-4d2d-942e-d55db1eddac5"), "12345678", new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f771"), null, true },
                    { new Guid("120e7474-5b9a-4b5a-95ed-1e138233f30c"), new Guid("86189048-87ba-4d2d-942e-d55db1eddac5"), "12345678", new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f772"), null, true },
                    { new Guid("120e7474-5b9a-4b5a-95ed-1e138233f30d"), new Guid("86189048-87ba-4d2d-942e-d55db1eddac5"), "12345678", new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f773"), null, true },
                    { new Guid("120e7474-5b9a-4b5a-95ed-1e138233f30e"), new Guid("86189048-87ba-4d2d-942e-d55db1eddac5"), "12345678", new Guid("1f41f8fa-a6b6-40f4-8ae1-b0caeec7f771"), null, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CodeValueId",
                table: "Address",
                column: "CodeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_RegionId",
                table: "Address",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StudentId",
                table: "Address",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_StudentId1",
                table: "Address",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_CodeValue_CodeId",
                table: "CodeValue",
                column: "CodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CodeValueId",
                table: "Contact",
                column: "CodeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_StudentId",
                table: "Contact",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_StudentId1",
                table: "Contact",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryId",
                table: "Region",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryId1",
                table: "Region",
                column: "CountryId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "CodeValue");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Code");
        }
    }
}
