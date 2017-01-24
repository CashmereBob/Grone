namespace Grone.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserEntityThatIsAdmin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserEntityModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        eMail = c.String(),
                        Fullname = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserEntityModels");
        }
    }
}
