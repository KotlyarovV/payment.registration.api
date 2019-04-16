using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Registration.Ef.Migrations
{
    public partial class Start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ApplicantId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentForms_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPosition",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Sum = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    PaymentFormId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPosition_PaymentForms_PaymentFormId",
                        column: x => x.PaymentFormId,
                        principalTable: "PaymentForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    WayToFile = table.Column<string>(nullable: true),
                    PaymentPositionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_PaymentPosition_PaymentPositionId",
                        column: x => x.PaymentPositionId,
                        principalTable: "PaymentPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_PaymentPositionId",
                table: "File",
                column: "PaymentPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentForms_ApplicantId",
                table: "PaymentForms",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPosition_PaymentFormId",
                table: "PaymentPosition",
                column: "PaymentFormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "PaymentPosition");

            migrationBuilder.DropTable(
                name: "PaymentForms");

            migrationBuilder.DropTable(
                name: "Applicant");
        }
    }
}
