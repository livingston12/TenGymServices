using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TenGymServices.Api.Plans.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaypalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPaypalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "BillingCycles",
                columns: table => new
                {
                    BillingCycleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    TenureType = table.Column<int>(type: "int", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    TotalCycles = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingCycles", x => x.BillingCycleId);
                    table.ForeignKey(
                        name: "FK_BillingCycles_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPreferences",
                columns: table => new
                {
                    PaymentPreferenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    AutoBillOutstanding = table.Column<bool>(type: "bit", nullable: false),
                    SetupFeeFailureAction = table.Column<int>(type: "int", nullable: false),
                    PaymentFailureThreshold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPreferences", x => x.PaymentPreferenceId);
                    table.ForeignKey(
                        name: "FK_PaymentPreferences_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    TaxId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inclusive = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.TaxId);
                    table.ForeignKey(
                        name: "FK_Taxes_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frequencies",
                columns: table => new
                {
                    FrequencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillingCycleId = table.Column<int>(type: "int", nullable: false),
                    IntervalUnit = table.Column<int>(type: "int", nullable: false),
                    IntervalCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencies", x => x.FrequencyId);
                    table.ForeignKey(
                        name: "FK_Frequencies_BillingCycles_BillingCycleId",
                        column: x => x.BillingCycleId,
                        principalTable: "BillingCycles",
                        principalColumn: "BillingCycleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricingSchemes",
                columns: table => new
                {
                    PricingSchemeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillingCycleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingSchemes", x => x.PricingSchemeId);
                    table.ForeignKey(
                        name: "FK_PricingSchemes_BillingCycles_BillingCycleId",
                        column: x => x.BillingCycleId,
                        principalTable: "BillingCycles",
                        principalColumn: "BillingCycleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetupFees",
                columns: table => new
                {
                    SetupFeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentPreferenceId = table.Column<int>(type: "int", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetupFees", x => x.SetupFeeId);
                    table.ForeignKey(
                        name: "FK_SetupFees_PaymentPreferences_PaymentPreferenceId",
                        column: x => x.PaymentPreferenceId,
                        principalTable: "PaymentPreferences",
                        principalColumn: "PaymentPreferenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FixedPrices",
                columns: table => new
                {
                    FixedPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricingSchemeId = table.Column<int>(type: "int", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedPrices", x => x.FixedPriceId);
                    table.ForeignKey(
                        name: "FK_FixedPrices_PricingSchemes_PricingSchemeId",
                        column: x => x.PricingSchemeId,
                        principalTable: "PricingSchemes",
                        principalColumn: "PricingSchemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingCycles_PlanId",
                table: "BillingCycles",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_FixedPrices_PricingSchemeId",
                table: "FixedPrices",
                column: "PricingSchemeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Frequencies_BillingCycleId",
                table: "Frequencies",
                column: "BillingCycleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPreferences_PlanId",
                table: "PaymentPreferences",
                column: "PlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricingSchemes_BillingCycleId",
                table: "PricingSchemes",
                column: "BillingCycleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SetupFees_PaymentPreferenceId",
                table: "SetupFees",
                column: "PaymentPreferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_PlanId",
                table: "Taxes",
                column: "PlanId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FixedPrices");

            migrationBuilder.DropTable(
                name: "Frequencies");

            migrationBuilder.DropTable(
                name: "SetupFees");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "PricingSchemes");

            migrationBuilder.DropTable(
                name: "PaymentPreferences");

            migrationBuilder.DropTable(
                name: "BillingCycles");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
