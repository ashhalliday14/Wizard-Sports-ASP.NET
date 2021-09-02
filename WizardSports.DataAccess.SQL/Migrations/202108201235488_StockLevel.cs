namespace WizardSports.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BasketItems", "StockLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "StockLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "StockLevel");
            DropColumn("dbo.BasketItems", "StockLevel");
        }
    }
}
