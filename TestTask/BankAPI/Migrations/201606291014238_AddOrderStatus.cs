namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "status");
        }
    }
}
