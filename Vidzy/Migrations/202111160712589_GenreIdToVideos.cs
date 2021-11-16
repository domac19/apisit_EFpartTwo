namespace Vidzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreIdToVideos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Videos", "GenreId", "dbo.Genres");
            DropIndex("dbo.Videos", new[] { "GenreId" });
            RenameColumn(table: "dbo.Videos", name: "GenreId", newName: "Genre_Id1");
            AddColumn("dbo.Videos", "Genre_Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Videos", "Genre_Id1", c => c.Byte());
            CreateIndex("dbo.Videos", "Genre_Id1");
            AddForeignKey("dbo.Videos", "Genre_Id1", "dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Genre_Id1", "dbo.Genres");
            DropIndex("dbo.Videos", new[] { "Genre_Id1" });
            AlterColumn("dbo.Videos", "Genre_Id1", c => c.Byte(nullable: false));
            DropColumn("dbo.Videos", "Genre_Id");
            RenameColumn(table: "dbo.Videos", name: "Genre_Id1", newName: "GenreId");
            CreateIndex("dbo.Videos", "GenreId");
            AddForeignKey("dbo.Videos", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
    }
}
