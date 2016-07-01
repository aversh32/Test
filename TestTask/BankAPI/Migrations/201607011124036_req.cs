namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class req : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cards", "expiry_month", c => c.String(nullable: false));
            AlterColumn("dbo.Cards", "expiry_year", c => c.String(nullable: false));
            AlterColumn("dbo.Cards", "cvv", c => c.String(nullable: false));
            AlterColumn("dbo.Cards", "cardholder_name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cards", "cardholder_name", c => c.String());
            AlterColumn("dbo.Cards", "cvv", c => c.String());
            AlterColumn("dbo.Cards", "expiry_year", c => c.String());
            AlterColumn("dbo.Cards", "expiry_month", c => c.String());
        }
    }
}
