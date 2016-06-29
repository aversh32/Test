namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCard : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "card_number");
            DropColumn("dbo.Orders", "expiry_month");
            DropColumn("dbo.Orders", "expiry_year");
            DropColumn("dbo.Orders", "cvv");
            DropColumn("dbo.Orders", "cardholder_name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "cardholder_name", c => c.String());
            AddColumn("dbo.Orders", "cvv", c => c.String());
            AddColumn("dbo.Orders", "expiry_year", c => c.String());
            AddColumn("dbo.Orders", "expiry_month", c => c.String());
            AddColumn("dbo.Orders", "card_number", c => c.String());
        }
    }
}
