namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hz2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tests");
        }
    }
}
