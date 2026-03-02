using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasPessoais.Infra.Data.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BankBranch = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Type = table.Column<int>(type: "int", maxLength: 7, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CardLimit = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    InvoiceClosingDate = table.Column<int>(type: "int", nullable: false),
                    InvoiceDueDate = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AccountsPayable",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    SubcategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Emails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsPayable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsPayable_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInInstallments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PurchaseValue = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    NumberOfInstallments = table.Column<int>(type: "int", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubcategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInInstallments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseInInstallments_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseInInstallments_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialReleases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", maxLength: 7, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SubcategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseInInstallmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialReleases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialReleases_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialReleases_CreditCards_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialReleases_PurchaseInInstallments_PurchaseInInstallmentsId",
                        column: x => x.PurchaseInInstallmentsId,
                        principalTable: "PurchaseInInstallments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialReleases_Subcategories_SubcategoryId",
                        column: x => x.SubcategoryId,
                        principalTable: "Subcategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "BankBranch", "CreationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("5a7dd7d1-0fc2-4606-8ed2-5a32311e321e"), "278499", "5611", new DateTime(2026, 3, 1, 21, 13, 46, 588, DateTimeKind.Local).AddTicks(9308), "Itaú" },
                    { new Guid("a9d03220-ddea-45a0-bf2b-be3075b3c7c0"), "000007181", "0081", new DateTime(2026, 3, 1, 21, 13, 46, 592, DateTimeKind.Local).AddTicks(8580), "Caixa" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "CreationDate", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"), "R1", new DateTime(2026, 3, 1, 21, 13, 46, 595, DateTimeKind.Local).AddTicks(2711), "Salário", "Salário", 0 },
                    { new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2", new DateTime(2026, 3, 1, 21, 13, 46, 595, DateTimeKind.Local).AddTicks(6508), "Serviços", "Serviços", 0 },
                    { new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"), "R3", new DateTime(2026, 3, 1, 21, 13, 46, 595, DateTimeKind.Local).AddTicks(6536), "Empréstimo - Recebimento", "Empréstimo - Recebimento", 0 },
                    { new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"), "R4", new DateTime(2026, 3, 1, 21, 13, 46, 595, DateTimeKind.Local).AddTicks(6546), "Outras Receitas", "Outras Receitas", 0 },
                    { new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1", new DateTime(2026, 3, 1, 21, 13, 46, 595, DateTimeKind.Local).AddTicks(6554), "Alimentação", "Alimentação", 1 }
                });

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "Id", "CardLimit", "CardName", "CardNumber", "CreationDate", "InvoiceClosingDate", "InvoiceDueDate" },
                values: new object[] { new Guid("cbaba53b-a642-4904-ad7e-542ef52c2ec0"), 15000m, "Itaucard Click Final 9289", "5316805324229289", new DateTime(2026, 3, 1, 21, 13, 46, 604, DateTimeKind.Local).AddTicks(4426), 2, 9 });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Code", "CreationDate", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("63ad1c50-68b8-4edd-9e38-5da54ce746bf"), new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"), "R1001", new DateTime(2026, 3, 1, 21, 13, 46, 598, DateTimeKind.Local).AddTicks(7096), "Salário", "Salário" },
                    { new Guid("656b9e6e-ccb2-4ec4-aec7-260e12e77091"), new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"), "R1002", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(297), "13º salário", "13º salário" },
                    { new Guid("89862987-61fb-404c-83df-c345250260bb"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2001", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(323), "Mão-de-obra", "Mão-de-obra" },
                    { new Guid("c294b7d4-16fa-4f48-8acb-07f4ffb228b2"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2002", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(333), "Venda de peças", "Venda de peças" },
                    { new Guid("e098c0de-4220-4dc6-b9e6-1817a7e17323"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2003", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(347), "Revenda de peças", "Revenda de peças" },
                    { new Guid("b83d0254-2036-4f58-b337-24975934ac18"), new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"), "R3001", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(438), "Empréstimo - Recebimento", "Empréstimo - Recebimento" },
                    { new Guid("1ce0f441-1361-467f-ad6a-d30a524b5aca"), new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"), "R4001", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(447), "Proventos de investimentos", "Proventos de investimentos" },
                    { new Guid("c92fd019-17dc-4a7d-8d63-58f7f9fee606"), new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"), "R4002", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(456), "Transferência entre contas", "Transferência entre contas" },
                    { new Guid("c9c571d3-7d03-44f8-aa86-c9d064edc056"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1001", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(468), "Compra em supermercado", "Compra em supermercado" },
                    { new Guid("900611ec-7d08-403a-8b52-637a6149cb76"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1002", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(477), "Feira: frutas e verduras", "Feira: frutas e verduras" },
                    { new Guid("ccc4f160-37fb-41bc-9147-191793c0a03e"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1003", new DateTime(2026, 3, 1, 21, 13, 46, 599, DateTimeKind.Local).AddTicks(486), "Padaria", "Padaria" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountsPayable_SubcategoryId",
                table: "AccountsPayable",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReleases_AccountId",
                table: "FinancialReleases",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReleases_CreditCardId",
                table: "FinancialReleases",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReleases_PurchaseInInstallmentsId",
                table: "FinancialReleases",
                column: "PurchaseInInstallmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialReleases_SubcategoryId",
                table: "FinancialReleases",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInInstallments_CreditCardId",
                table: "PurchaseInInstallments",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInInstallments_SubcategoryId",
                table: "PurchaseInInstallments",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsPayable");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FinancialReleases");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "PurchaseInInstallments");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "Subcategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
