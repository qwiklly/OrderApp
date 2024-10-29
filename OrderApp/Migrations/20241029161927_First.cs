using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderApp.Migrations
{
	/// <inheritdoc />
	public partial class First : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Orders",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UnicId = table.Column<int>(type: "int", nullable: false),
					Weight = table.Column<double>(type: "float", nullable: false),
					Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Orders", x => x.Id);
				});

			// Добавление начальных данных
			migrationBuilder.InsertData(
				table: "Orders",
				columns: new[] { "Id", "UnicId", "Weight", "Location", "DateTime" },
				values: new object[,]
				{
					{ 1, 1001, 5.0, "Moscow", new DateTime(2024, 10, 29, 10, 0, 0) }, 
                    { 2, 1002, 10.0, "Moscow", new DateTime(2024, 10, 29, 10, 0, 0) }, 
                    { 3, 1003, 15.0, "Saratov", new DateTime(2024, 10, 29, 11, 0, 0) }, 
                    { 4, 1004, 20.0, "Kursk", new DateTime(2024, 10, 29, 12, 0, 0) }  
                });
		}


		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Orders");
		}
	}
}
