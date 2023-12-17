using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MILS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImprovementProcess = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceCc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracking",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Line",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Line_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incident",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incident", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incident_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Incident_Tracking_TrackingId",
                        column: x => x.TrackingId,
                        principalSchema: "dbo",
                        principalTable: "Tracking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncidentLine",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidentId = table.Column<int>(type: "int", nullable: false),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    Yes = table.Column<bool>(type: "bit", nullable: false),
                    No = table.Column<bool>(type: "bit", nullable: false),
                    ToEvalute = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncidentLine_Incident_IncidentId",
                        column: x => x.IncidentId,
                        principalSchema: "dbo",
                        principalTable: "Incident",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_IncidentLine_Line_LineId",
                        column: x => x.LineId,
                        principalSchema: "dbo",
                        principalTable: "Line",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Category",
                columns: new[] { "Id", "ImprovementProcess", "LongLabel", "ReferenceCc", "ShortLabel" },
                values: new object[,]
                {
                    { 1, "Q-4-06-XX-00", "CHECK LIST Evaluation des impacts CC", null, "Management" },
                    { 2, "Q-4-06-XX-00", "CHECK LIST Evaluation des impacts CC CONCEPTION", null, "Conception" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Line",
                columns: new[] { "Id", "CategoryId", "Label" },
                values: new object[,]
                {
                    { 1, 1, "Modification administrative (nom, entité, adresse)" },
                    { 2, 1, "Modification des certificats (ajout/retrait d'une activité, modification, libellé)" },
                    { 3, 1, "Déménagement" },
                    { 4, 1, "Ajout de nouveaux sites" },
                    { 5, 1, "Transfert/internalisation d'activités" },
                    { 6, 1, "Internalisation d'une activité" },
                    { 7, 1, "Changement de responsabilités au niveau des rôles règlementaires ou de la direction générale" },
                    { 8, 1, "Modification des infrastructures" },
                    { 9, 1, "Formation" },
                    { 10, 2, "Destination / Utilisation du produit" },
                    { 11, 2, "Caractéristiques techniques du dispositif" },
                    { 12, 2, "Performance du produit" },
                    { 13, 2, "Plan de contrôle" },
                    { 14, 2, "Plaque de marque" },
                    { 15, 2, "Etiquetage" },
                    { 16, 2, "Gestion des alarmes" },
                    { 17, 2, "Modification de composant" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incident_CategoryId",
                schema: "dbo",
                table: "Incident",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incident_TrackingId",
                schema: "dbo",
                table: "Incident",
                column: "TrackingId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentLine_IncidentId",
                schema: "dbo",
                table: "IncidentLine",
                column: "IncidentId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidentLine_LineId",
                schema: "dbo",
                table: "IncidentLine",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_Line_CategoryId",
                schema: "dbo",
                table: "Line",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncidentLine",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Incident",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Line",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tracking",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "dbo");
        }
    }
}
