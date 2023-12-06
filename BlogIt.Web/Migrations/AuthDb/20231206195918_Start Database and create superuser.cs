using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogIt.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class StartDatabaseandcreatesuperuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7fb4a9ef-85de-4fbc-bf7b-763dd90e08e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76b1d612-71df-4057-acec-e18e9e09478f", "AQAAAAIAAYagAAAAEL7j6+L7jQfFnmUD+++OGkAWqN9MSiQp0B0Xp+kVeDRT61U7g9QolWEEjFBBvmletw==", "51fd5b1b-032b-4b45-a280-6d9d7417d75e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7fb4a9ef-85de-4fbc-bf7b-763dd90e08e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32af0be9-4a37-4260-acce-02654308c4a2", "AQAAAAIAAYagAAAAEG48UNE8XZY2LmyAGDiKtSwmWvcAzJUq/oUwy07Hpop6AFPZGpAwo1XDi8NKfu+UXg==", "140aa8b4-42f3-4dbb-b75d-2bd418a39f3c" });
        }
    }
}
