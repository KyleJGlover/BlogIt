using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogIt.Web.Migrations
{
    /// <inheritdoc />
    public partial class MigrateAddTagstoBlogPostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Tags_TagId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_TagId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "BlogPosts");

            migrationBuilder.CreateTable(
                name: "BlogPostTag",
                columns: table => new
                {
                    BlogPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostTag", x => new { x.BlogPostsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BlogPostTag_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostTag_TagsId",
                table: "BlogPostTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostTag");

            migrationBuilder.AddColumn<Guid>(
                name: "TagId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_TagId",
                table: "BlogPosts",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Tags_TagId",
                table: "BlogPosts",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
