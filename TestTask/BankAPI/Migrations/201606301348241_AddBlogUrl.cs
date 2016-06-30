namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlogUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "card_number", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "card_number");
        }
    }
}
