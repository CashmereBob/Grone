namespace Grone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDemoField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostEntityModels", "TotalTimeAdded", c => c.Int());
        }

        public override void Down()
        {
            DropColumn("dbo.PostEntityModels", "TotalLifeTime");
        }
    }
}
