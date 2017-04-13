using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace test3.Migrations
{
    public partial class eee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    OptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImagePath = table.Column<string>(type: "nchar(10)", nullable: true),
                    Name = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.OptionID);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    RateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RateLevel = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.RateID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Admin = table.Column<bool>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "nchar(50)", nullable: true),
                    EmailNormalized = table.Column<string>(type: "nchar(50)", nullable: true),
                    HashSalt = table.Column<string>(type: "nchar(128)", nullable: true),
                    Name = table.Column<string>(type: "nchar(30)", nullable: true),
                    Password = table.Column<string>(type: "nchar(128)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "char(9)", nullable: true),
                    Surname = table.Column<string>(type: "nchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    ApartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adress = table.Column<string>(type: "nchar(100)", nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(type: "nchar(100)", nullable: false),
                    OwnerID = table.Column<int>(nullable: false),
                    PriceBasic = table.Column<decimal>(type: "money", nullable: false),
                    RoomSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.ApartmentID);
                    table.ForeignKey(
                        name: "FK_Apartment_User",
                        column: x => x.OwnerID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApartImage",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApartmentID = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartImage", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_ApartImage_Apartment",
                        column: x => x.ApartmentID,
                        principalTable: "Apartment",
                        principalColumn: "ApartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApartOption",
                columns: table => new
                {
                    ApartOptionsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApartmentID = table.Column<int>(nullable: false),
                    OptionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartOption", x => x.ApartOptionsID);
                    table.ForeignKey(
                        name: "FK_ApartOption_Apartment",
                        column: x => x.ApartmentID,
                        principalTable: "Apartment",
                        principalColumn: "ApartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApartOption_Option",
                        column: x => x.OptionID,
                        principalTable: "Option",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApartmentID = table.Column<int>(nullable: true),
                    ClientID = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    OwnerReply = table.Column<string>(nullable: false),
                    RateID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(type: "nchar(30)", nullable: true),
                    ToDate = table.Column<DateTime>(type: "date", nullable: true),
                    TotalCost = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservation_Apartment",
                        column: x => x.ApartmentID,
                        principalTable: "Apartment",
                        principalColumn: "ApartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_User",
                        column: x => x.ClientID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_Rate",
                        column: x => x.RateID,
                        principalTable: "Rate",
                        principalColumn: "RateID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApartImage_ApartmentID",
                table: "ApartImage",
                column: "ApartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_OwnerID",
                table: "Apartment",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_ApartOption_ApartmentID",
                table: "ApartOption",
                column: "ApartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ApartOption_OptionID",
                table: "ApartOption",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ApartmentID",
                table: "Reservation",
                column: "ApartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClientID",
                table: "Reservation",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RateID",
                table: "Reservation",
                column: "RateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartImage");

            migrationBuilder.DropTable(
                name: "ApartOption");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
