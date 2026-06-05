using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasPessoais.Infra.Data.Migrations
{
    public partial class RemovingEntitySubcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsPayable_Subcategories_SubcategoryId",
                table: "AccountsPayable");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReleases_Subcategories_SubcategoryId",
                table: "FinancialReleases");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInInstallments_Subcategories_SubcategoryId",
                table: "PurchaseInInstallments");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "PurchaseInInstallments",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseInInstallments_SubcategoryId",
                table: "PurchaseInInstallments",
                newName: "IX_PurchaseInInstallments_CategoryId");

            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "FinancialReleases",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialReleases_SubcategoryId",
                table: "FinancialReleases",
                newName: "IX_FinancialReleases_CategoryId");

            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "AccountsPayable",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountsPayable_SubcategoryId",
                table: "AccountsPayable",
                newName: "IX_AccountsPayable_CategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("5a7dd7d1-0fc2-4606-8ed2-5a32311e321e"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 22, 33, 41, 266, DateTimeKind.Local).AddTicks(9184));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("a9d03220-ddea-45a0-bf2b-be3075b3c7c0"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 22, 33, 41, 268, DateTimeKind.Local).AddTicks(4274));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 22, 33, 41, 271, DateTimeKind.Local).AddTicks(4807));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 22, 33, 41, 271, DateTimeKind.Local).AddTicks(7514));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 22, 33, 41, 271, DateTimeKind.Local).AddTicks(7543));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 22, 33, 41, 271, DateTimeKind.Local).AddTicks(7555));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 22, 33, 41, 271, DateTimeKind.Local).AddTicks(7562));

            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: new Guid("cbaba53b-a642-4904-ad7e-542ef52c2ec0"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 22, 33, 41, 275, DateTimeKind.Local).AddTicks(6535));

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsPayable_Categories_CategoryId",
                table: "AccountsPayable",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReleases_Categories_CategoryId",
                table: "FinancialReleases",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInInstallments_Categories_CategoryId",
                table: "PurchaseInInstallments",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsPayable_Categories_CategoryId",
                table: "AccountsPayable");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialReleases_Categories_CategoryId",
                table: "FinancialReleases");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInInstallments_Categories_CategoryId",
                table: "PurchaseInInstallments");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "PurchaseInInstallments",
                newName: "SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseInInstallments_CategoryId",
                table: "PurchaseInInstallments",
                newName: "IX_PurchaseInInstallments_SubcategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "FinancialReleases",
                newName: "SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialReleases_CategoryId",
                table: "FinancialReleases",
                newName: "IX_FinancialReleases_SubcategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "AccountsPayable",
                newName: "SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountsPayable_CategoryId",
                table: "AccountsPayable",
                newName: "IX_AccountsPayable_SubcategoryId");

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("5a7dd7d1-0fc2-4606-8ed2-5a32311e321e"),
                column: "CreationDate",
                value: new DateTime(2026, 3, 1, 22, 2, 42, 577, DateTimeKind.Local).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("a9d03220-ddea-45a0-bf2b-be3075b3c7c0"),
                column: "CreationDate",
                value: new DateTime(2026, 3, 1, 22, 2, 42, 579, DateTimeKind.Local).AddTicks(9950));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"),
                column: "CreationDate",
                value: new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(7752));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"),
                column: "CreationDate",
                value: new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(3320));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"),
                column: "CreationDate",
                value: new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(7706));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"),
                column: "CreationDate",
                value: new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(7735));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"),
                column: "CreationDate",
                value: new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(7744));

            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: new Guid("cbaba53b-a642-4904-ad7e-542ef52c2ec0"),
                column: "CreationDate",
                value: new DateTime(2026, 3, 1, 22, 2, 42, 592, DateTimeKind.Local).AddTicks(8404));

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Code", "CreationDate", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("7d6ab60c-fa60-4482-8b2d-2bba4aa7cb54"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1003", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5168), "Padaria", "Padaria" },
                    { new Guid("e5673067-7281-4858-987b-64121feae47c"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1002", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5159), "Feira: frutas e verduras", "Feira: frutas e verduras" },
                    { new Guid("e5110ae7-3280-4d31-b528-261d963aee25"), new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"), "R4002", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5139), "Transferência entre contas", "Transferência entre contas" },
                    { new Guid("fbacb127-fb7e-4a58-997a-2aa5d2c689e1"), new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"), "R4001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5130), "Proventos de investimentos", "Proventos de investimentos" },
                    { new Guid("44a8faf1-d11e-45be-89a7-755e1adcbb39"), new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"), "R3001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5121), "Empréstimo - Recebimento", "Empréstimo - Recebimento" },
                    { new Guid("4c6f2c59-e03b-4ed3-8bea-96a7b9067cb6"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2003", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5108), "Revenda de peças", "Revenda de peças" },
                    { new Guid("075a3fbd-199e-4753-8866-0909cc033e46"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2002", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5034), "Venda de peças", "Venda de peças" },
                    { new Guid("28419dcf-3fb8-446c-83f3-04333185f3e5"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5024), "Mão-de-obra", "Mão-de-obra" },
                    { new Guid("ad4a83ea-5f19-4b99-a51e-87e3e4d57531"), new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"), "R1002", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(4998), "13º salário", "13º salário" },
                    { new Guid("f9be2968-b99c-4962-8521-39a3daef380e"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5150), "Compra em supermercado", "Compra em supermercado" },
                    { new Guid("2b89b6ef-8751-4b37-8610-ca2f05354199"), new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"), "R1001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(1782), "Salário", "Salário" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsPayable_Subcategories_SubcategoryId",
                table: "AccountsPayable",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialReleases_Subcategories_SubcategoryId",
                table: "FinancialReleases",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInInstallments_Subcategories_SubcategoryId",
                table: "PurchaseInInstallments",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
