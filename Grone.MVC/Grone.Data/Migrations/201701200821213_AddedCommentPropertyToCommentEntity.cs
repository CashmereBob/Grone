namespace Grone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentPropertyToCommentEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentEntityModels", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentEntityModels", "Comment");
        }
    }
}
