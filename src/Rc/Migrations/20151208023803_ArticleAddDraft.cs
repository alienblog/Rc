using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Rc.Migrations
{
    public partial class ArticleAddDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Article",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IsDraft", table: "Article");
        }
    }
}
