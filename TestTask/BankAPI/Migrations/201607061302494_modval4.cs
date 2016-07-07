namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modval4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tests");
            AlterColumn("dbo.Tests", "User", c => c.String(nullable: false, maxLength: 3));
            AddPrimaryKey("dbo.Tests", "User");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Tests");
            AlterColumn("dbo.Tests", "User", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Tests", "User");
        }
    }
}
