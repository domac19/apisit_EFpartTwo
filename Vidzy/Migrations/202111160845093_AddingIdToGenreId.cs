namespace Vidzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingIdToGenreId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagVideos", "Video_Id", "dbo.Videos");
            DropIndex("dbo.Videos", new[] { "GenreId" });
            DropColumn("dbo.Videos", "Id");
            RenameColumn(table: "dbo.Videos", name: "GenreId", newName: "id");
            DropPrimaryKey("dbo.Videos");
            AlterColumn("dbo.Videos", "id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Videos", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Videos", "Id");
            CreateIndex("dbo.Videos", "id");
            AddForeignKey("dbo.TagVideos", "Video_Id", "dbo.Videos", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagVideos", "Video_Id", "dbo.Videos");
            DropIndex("dbo.Videos", new[] { "id" });
            DropPrimaryKey("dbo.Videos");
            AlterColumn("dbo.Videos", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Videos", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Videos", "Id");
            RenameColumn(table: "dbo.Videos", name: "id", newName: "GenreId");
            AddColumn("dbo.Videos", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Videos", "GenreId");
            AddForeignKey("dbo.TagVideos", "Video_Id", "dbo.Videos", "Id", cascadeDelete: true);
        }
    }
}
