namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestDelete : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Tests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        User = c.String(nullable: false, maxLength: 3),
                        Name = c.String(nullable: false, maxLength: 10),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.User);
            
        }
    }
}
