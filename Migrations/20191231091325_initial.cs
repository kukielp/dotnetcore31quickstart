using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace pgapp.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    DateOfPost = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Author = table.Column<string>(nullable: true),
                    CommentText = table.Column<string>(nullable: true),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.Sql(@"INSERT INTO public.""Posts""(""Name"", ""Count"", ""DateOfPost"")
                                 VALUES('Initial Post: 1', 1, current_timestamp - interval '10' day);");

            migrationBuilder.Sql(@"INSERT INTO public.""Posts""(""Name"", ""Count"", ""DateOfPost"")
                                 VALUES('Initial Post: 1', 2, current_timestamp - interval '10' day);");

            migrationBuilder.Sql(@"INSERT INTO public.""Posts""(""Name"", ""Count"", ""DateOfPost"")
                                 VALUES('Initial Post: 3', 3, current_timestamp);");

            migrationBuilder.Sql(@"INSERT INTO ""Comments"" (""PostId"", ""CommentText"", ""Author"")
                                    VALUES(1, 'Comment for Post 1', 'Paul K')");

            migrationBuilder.Sql(@"INSERT INTO ""Comments"" (""PostId"", ""CommentText"", ""Author"")
                                    VALUES(1, 'Another Comment for Post 1', 'Adam K')");

            migrationBuilder.Sql(@"INSERT INTO ""Comments"" (""PostId"", ""CommentText"", ""Author"")
                                    VALUES(1, 'Another Comment for Post 1', 'Kevin K')");

            migrationBuilder.Sql(@"INSERT INTO ""Comments"" (""PostId"", ""CommentText"", ""Author"")
                                    VALUES(3, 'Comment for Post 3', 'Paul K')");

            migrationBuilder.Sql(@"INSERT INTO ""Comments"" (""PostId"", ""CommentText"", ""Author"")
                                    VALUES(3, 'Another Comment for Post 3', 'Paul K')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}