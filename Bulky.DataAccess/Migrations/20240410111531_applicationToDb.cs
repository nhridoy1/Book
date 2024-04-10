using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookSell.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class applicationToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DisplayOrder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name", "price" },
                values: new object[,]
                {
                    { 1, "1", "Action", 0 },
                    { 2, "2", "SciFi", 0 },
                    { 3, "3", "History", 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, " Dave Eggers", "A Heartbreaking Work of Staggering Genius is the moving memoir of a college senior who, in the space of five weeks, loses both of his parents to cancer and inherits his eight-year-old brother. This exhilarating debut that manages to be simultaneously hilarious and wildly inventive as well as a deeply heartfelt story of the love that holds a family together.", "0375725784", 99.0, 90.0, 80.0, 85.0, "A Heartbreaking Work of Staggering Genius: Pulitzer Prize Finalist " },
                    { 2, "Ishmael Beah", "This is how wars are fought now: by children, hopped-up on drugs and wielding AK-47s. Children have become soldiers of choice. In the more than fifty conflicts going on worldwide, it is estimated that there are some 300,000 child soldiers. Ishmael Beah used to be one of them.", "9780374531263", 99.0, 90.0, 80.0, 85.0, "Long Way Gone" },
                    { 3, " Ernest Hemingway", "Published posthumously in 1964, A Moveable Feast remains one of Ernest Hemingway’s most enduring works. Since Hemingway’s personal papers were released in 1979, scholars have examined the changes made to the text before publication. Now, this special restored edition presents the original manuscript as the author prepared it to be published.", "143918271X", 99.0, 90.0, 80.0, 85.0, "A Moveable Feast: The Restored Edition" },
                    { 4, "William Manchester", "Inspiring, outrageous... A thundering paradox of a man. Douglas MacArthur, one of only five men in history to have achieved the rank of General of the United States Army. He served in World Wars I, II, and the Korean War, and is famous for stating that \"in war, there is no substitute for victory.\"\r\n", "0316024740", 99.0, 90.0, 80.0, 85.0, "American Caesar: Douglas MacArthur 1880 - 1964" },
                    { 5, "Kai Bird, Martin J. Sherwin", "THE INSPIRATION FOR THE ACADEMY AWARD®-WINNING MAJOR MOTION PICTURE OPPENHEIMER • \"A riveting account of one of history’s most essential and paradoxical figures.”—Christopher Nolan", "0375726268", 99.0, 90.0, 80.0, 85.0, "American Prometheus: The Inspiration for the Major Motion Picture OPPENHEIMER " },
                    { 6, "Patti Smith", "“Reading rocker Smith’s account of her relationship with photographer Robert Mapplethorpe, it’s hard not to believe in fate. How else to explain the chance encounter that threw them together, allowing both to blossom? Quirky and spellbinding.” -- People", "0060936228", 99.0, 90.0, 80.0, 85.0, "Just Kids: A National Book Award Winner" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
