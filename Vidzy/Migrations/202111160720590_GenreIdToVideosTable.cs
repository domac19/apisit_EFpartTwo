namespace Vidzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreIdToVideosTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Videos", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.Videos", new[] { "Genre_Id" });
            AddColumn("dbo.Videos", "Genre_Id1", c => c.Byte());
            AlterColumn("dbo.Videos", "Genre_Id", c => c.Byte(nullable: false));
            CreateIndex("dbo.Videos", "Genre_Id1");
            AddForeignKey("dbo.Videos", "Genre_Id1", "dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Genre_Id1", "dbo.Genres");
            DropIndex("dbo.Videos", new[] { "Genre_Id1" });
            AlterColumn("dbo.Videos", "Genre_Id", c => c.Byte());
            DropColumn("dbo.Videos", "Genre_Id1");
            CreateIndex("dbo.Videos", "Genre_Id");
            AddForeignKey("dbo.Videos", "Genre_Id", "dbo.Genres", "Id");
        }
    }
}
