namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modval2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tests");
            AddColumn("dbo.Tests", "User", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Tests", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Tests", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.Cards", "cvv", c => c.String(nullable: false, maxLength: 3));
            AddPrimaryKey("dbo.Tests", "User");
            DropColumn("dbo.Tests", "id");
            DropColumn("dbo.Tests", "value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tests", "value", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Tests");
            AlterColumn("dbo.Cards", "cvv", c => c.String());
            DropColumn("dbo.Tests", "Age");
            DropColumn("dbo.Tests", "Name");
            DropColumn("dbo.Tests", "User");
            AddPrimaryKey("dbo.Tests", "id");
        }
    }
}
