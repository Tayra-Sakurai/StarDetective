using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouAndIdol.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Bugdet = table.Column<double>(type: "REAL", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    PaymentrMethodId_Expense = table.Column<int>(type: "INTEGER", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "INTEGER", nullable: true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    FromAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    ToAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    FromPaymentMethodId = table.Column<int>(type: "INTEGER", nullable: true),
                    ToPaymentMethodId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.CheckConstraint("CK_EXCLUSIVE", "(([PaymentMethodId] IS NOT NULL) + ([AccountId] IS NOT NULL) = 1) OR (([PaymentMethodId] IS NOT NULL) + ([AccountId] IS NOT NULL) = 0)");
                    table.CheckConstraint("CK_TRANSFER", "((([FromPaymentMethodId] IS NOT NULL) + ([FromAccountId] IS NOT NULL) = 1) AND (([ToPaymentMethodId] IS NOT NULL) + ([ToAccountId] IS NOT NULL) = 1)) OR (([FromPaymentMethodId] IS NOT NULL) + ([FromAccountId] IS NOT NULL) + ([ToPaymentMethodId] IS NOT NULL) + ([ToAccountId] IS NOT NULL) = 0)");
                    table.ForeignKey(
                        name: "FK_Item_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Accounts_FromAccountId",
                        column: x => x.FromAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Accounts_ToAccountId",
                        column: x => x.ToAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_PaymentMethods_FromPaymentMethodId",
                        column: x => x.FromPaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_PaymentMethods_PaymentrMethodId_Expense",
                        column: x => x.PaymentrMethodId_Expense,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_PaymentMethods_ToPaymentMethodId",
                        column: x => x.ToPaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_AccountId",
                table: "Item",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_FromAccountId",
                table: "Item",
                column: "FromAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_FromPaymentMethodId",
                table: "Item",
                column: "FromPaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PaymentMethodId",
                table: "Item",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PaymentrMethodId_Expense",
                table: "Item",
                column: "PaymentrMethodId_Expense");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ToAccountId",
                table: "Item",
                column: "ToAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ToPaymentMethodId",
                table: "Item",
                column: "ToPaymentMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "PaymentMethods");
        }
    }
}
