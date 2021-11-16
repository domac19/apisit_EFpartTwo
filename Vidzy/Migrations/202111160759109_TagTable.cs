namespace Vidzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagVideos",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Video_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Video_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Videos", t => t.Video_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Video_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagVideos", "Video_Id", "dbo.Videos");
            DropForeignKey("dbo.TagVideos", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagVideos", new[] { "Video_Id" });
            DropIndex("dbo.TagVideos", new[] { "Tag_Id" });
            DropTable("dbo.TagVideos");
            DropTable("dbo.Tags");
        }
    }
}
