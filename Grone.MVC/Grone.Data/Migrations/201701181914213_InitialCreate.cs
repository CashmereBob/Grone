namespace Grone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentEntityModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Uploaded = c.DateTime(nullable: false),
                        ImgSrc = c.String(),
                        CommentId = c.Guid(nullable: false),
                        MemberId = c.String(),
                        PostEntityModelId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostEntityModels", t => t.PostEntityModelId, cascadeDelete: true)
                .Index(t => t.PostEntityModelId);
            
            CreateTable(
                "dbo.PostEntityModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Uploaded = c.DateTime(nullable: false),
                        ImgSrc = c.String(),
                        TimeLeft = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        TimeAdded = c.Int(nullable: false),
                        MemberId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentEntityModels", "PostEntityModelId", "dbo.PostEntityModels");
            DropIndex("dbo.CommentEntityModels", new[] { "PostEntityModelId" });
            DropTable("dbo.PostEntityModels");
            DropTable("dbo.CommentEntityModels");
        }
    }
}
