namespace BankAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hz1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Models", newName: "Orders");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Orders", newName: "Models");
        }
    }
}
