namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
                        amount_kop = c.Int(nullable: false),
                        card_number = c.String(),
                        expiry_month = c.String(),
                        expiry_year = c.String(),
                        cvv = c.String(),
                        cardholder_name = c.String(),
                    })
                .PrimaryKey(t => t.order_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
