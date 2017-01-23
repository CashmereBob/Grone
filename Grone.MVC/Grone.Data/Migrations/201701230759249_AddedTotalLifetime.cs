namespace Grone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTotalLifetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostEntityModels", "TotalLifetime", c => c.Int(nullable: false));
            DropColumn("dbo.PostEntityModels", "TimeAdded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostEntityModels", "TimeAdded", c => c.Int(nullable: false));
            DropColumn("dbo.PostEntityModels", "TotalLifetime");
        }
    }
}
