namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableCards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        card_number = c.String(nullable: false, maxLength: 128),
                        limit = c.Double(nullable: false),
                        expiry_month = c.String(),
                        expiry_year = c.String(),
                        cvv = c.String(),
                        cardholder_name = c.String(),
                    })
                .PrimaryKey(t => t.card_number);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cards");
        }
    }
}
