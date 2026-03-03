using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasPessoais.Infra.Data.Migrations
{
    public partial class InitialMigration : Migration
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        name: "FK_FinancialReleases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("5a7dd7d1-0fc2-4606-8ed2-5a32311e321e"), "278499", "5611", new DateTime(2026, 3, 1, 22, 2, 42, 577, DateTimeKind.Local).AddTicks(4342), "Itaú" },
                    { new Guid("a9d03220-ddea-45a0-bf2b-be3075b3c7c0"), "000007181", "0081", new DateTime(2026, 3, 1, 22, 2, 42, 579, DateTimeKind.Local).AddTicks(9950), "Caixa" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Code", "CreationDate", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"), "R1", new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(3320), "Salário", "Salário", 0 },
                    { new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2", new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(7706), "Serviços", "Serviços", 0 },
                    { new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"), "R3", new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(7735), "Empréstimo - Recebimento", "Empréstimo - Recebimento", 0 },
                    { new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"), "R4", new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(7744), "Outras Receitas", "Outras Receitas", 0 },
                    { new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1", new DateTime(2026, 3, 1, 22, 2, 42, 582, DateTimeKind.Local).AddTicks(7752), "Alimentação", "Alimentação", 1 }
                });

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "Id", "CardLimit", "CardName", "CardNumber", "CreationDate", "InvoiceClosingDate", "InvoiceDueDate" },
                values: new object[] { new Guid("cbaba53b-a642-4904-ad7e-542ef52c2ec0"), 15000m, "Itaucard Click Final 9289", "5316805324229289", new DateTime(2026, 3, 1, 22, 2, 42, 592, DateTimeKind.Local).AddTicks(8404), 2, 9 });

            migrationBuilder.InsertData(
                table: "Subcategories",
                columns: new[] { "Id", "CategoryId", "Code", "CreationDate", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2b89b6ef-8751-4b37-8610-ca2f05354199"), new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"), "R1001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(1782), "Salário", "Salário" },
                    { new Guid("ad4a83ea-5f19-4b99-a51e-87e3e4d57531"), new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"), "R1002", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(4998), "13º salário", "13º salário" },
                    { new Guid("28419dcf-3fb8-446c-83f3-04333185f3e5"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5024), "Mão-de-obra", "Mão-de-obra" },
                    { new Guid("075a3fbd-199e-4753-8866-0909cc033e46"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2002", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5034), "Venda de peças", "Venda de peças" },
                    { new Guid("4c6f2c59-e03b-4ed3-8bea-96a7b9067cb6"), new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"), "R2003", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5108), "Revenda de peças", "Revenda de peças" },
                    { new Guid("44a8faf1-d11e-45be-89a7-755e1adcbb39"), new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"), "R3001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5121), "Empréstimo - Recebimento", "Empréstimo - Recebimento" },
                    { new Guid("fbacb127-fb7e-4a58-997a-2aa5d2c689e1"), new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"), "R4001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5130), "Proventos de investimentos", "Proventos de investimentos" },
                    { new Guid("e5110ae7-3280-4d31-b528-261d963aee25"), new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"), "R4002", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5139), "Transferência entre contas", "Transferência entre contas" },
                    { new Guid("f9be2968-b99c-4962-8521-39a3daef380e"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1001", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5150), "Compra em supermercado", "Compra em supermercado" },
                    { new Guid("e5673067-7281-4858-987b-64121feae47c"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1002", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5159), "Feira: frutas e verduras", "Feira: frutas e verduras" },
                    { new Guid("7d6ab60c-fa60-4482-8b2d-2bba4aa7cb54"), new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"), "A1003", new DateTime(2026, 3, 1, 22, 2, 42, 586, DateTimeKind.Local).AddTicks(5168), "Padaria", "Padaria" }
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
                name: "IX_FinancialReleases_UserId",
                table: "FinancialReleases",
                column: "UserId");

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
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
