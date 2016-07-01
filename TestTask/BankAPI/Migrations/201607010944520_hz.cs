namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hz : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Orders", newName: "Models");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Models", newName: "Orders");
        }
    }
}
