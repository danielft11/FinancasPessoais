using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancasPessoais.Infra.Data.Migrations
{
    public partial class IncreasingSizeOfCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Categories",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("5a7dd7d1-0fc2-4606-8ed2-5a32311e321e"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 21, 41, 233, DateTimeKind.Local).AddTicks(6347));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("a9d03220-ddea-45a0-bf2b-be3075b3c7c0"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 21, 41, 235, DateTimeKind.Local).AddTicks(2906));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 21, 41, 238, DateTimeKind.Local).AddTicks(5868));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 21, 41, 238, DateTimeKind.Local).AddTicks(9242));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 21, 41, 238, DateTimeKind.Local).AddTicks(9278));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 21, 41, 238, DateTimeKind.Local).AddTicks(9288));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 21, 41, 238, DateTimeKind.Local).AddTicks(9342));

            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: new Guid("cbaba53b-a642-4904-ad7e-542ef52c2ec0"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 21, 41, 243, DateTimeKind.Local).AddTicks(5469));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Categories",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("5a7dd7d1-0fc2-4606-8ed2-5a32311e321e"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 17, 20, 704, DateTimeKind.Local).AddTicks(5044));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("a9d03220-ddea-45a0-bf2b-be3075b3c7c0"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 17, 20, 706, DateTimeKind.Local).AddTicks(1812));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e3d619d-9e30-4de1-a43a-82aca124e259"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 17, 20, 709, DateTimeKind.Local).AddTicks(1192));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1e1430da-a3a8-4c0a-a9d6-bc7057ec52f7"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 17, 20, 709, DateTimeKind.Local).AddTicks(3878));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a6611ff8-9866-486d-a13c-3b816e3a5d19"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 17, 20, 709, DateTimeKind.Local).AddTicks(3905));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("af5c3c93-9724-49ed-97cb-e8a385eedaae"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 17, 20, 709, DateTimeKind.Local).AddTicks(3916));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("effe2b1f-b5fc-4ce3-8306-084e7759d20c"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 17, 20, 709, DateTimeKind.Local).AddTicks(3932));

            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "Id",
                keyValue: new Guid("cbaba53b-a642-4904-ad7e-542ef52c2ec0"),
                column: "CreationDate",
                value: new DateTime(2026, 6, 4, 23, 17, 20, 713, DateTimeKind.Local).AddTicks(2303));
        }
    }
}
