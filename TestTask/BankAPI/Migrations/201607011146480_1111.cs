namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1111 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cards", "expiry_month", c => c.String());
            AlterColumn("dbo.Cards", "expiry_year", c => c.String());
            AlterColumn("dbo.Cards", "cvv", c => c.String());
            AlterColumn("dbo.Cards", "cardholder_name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cards", "cardholder_name", c => c.String(nullable: false));
            AlterColumn("dbo.Cards", "cvv", c => c.String(nullable: false));
            AlterColumn("dbo.Cards", "expiry_year", c => c.String(nullable: false));
            AlterColumn("dbo.Cards", "expiry_month", c => c.String(nullable: false));
        }
    }
}
