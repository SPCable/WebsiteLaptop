namespace WebVL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteM : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Cargo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Cargo", c => c.String());
        }
    }
}
