using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VOM_HIVE.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    id_company = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_company = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    cnpj = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_register = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.id_company);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id_product = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_product = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    category_product = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id_product);
                });

            migrationBuilder.CreateTable(
                name: "Profile_user",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_user = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    pass_user = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_user = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    permission_user = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    status_user = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_company = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile_user", x => x.id_user);
                    table.ForeignKey(
                        name: "FK_Profile_user_Company_id_company",
                        column: x => x.id_company,
                        principalTable: "Company",
                        principalColumn: "id_company",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    id_campaign = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_campaing = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    target = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    dt_register = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    details = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    id_product = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_company = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.id_campaign);
                    table.ForeignKey(
                        name: "FK_Campaign_Company_id_company",
                        column: x => x.id_company,
                        principalTable: "Company",
                        principalColumn: "id_company",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Campaign_Product_id_product",
                        column: x => x.id_product,
                        principalTable: "Product",
                        principalColumn: "id_product",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_id_company",
                table: "Campaign",
                column: "id_company");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_id_product",
                table: "Campaign",
                column: "id_product");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_user_id_company",
                table: "Profile_user",
                column: "id_company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Profile_user");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
