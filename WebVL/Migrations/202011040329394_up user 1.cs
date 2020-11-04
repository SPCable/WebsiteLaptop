namespace WebVL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upuser1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Cargo");
            DropColumn("dbo.Orders", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Count", c => c.String());
            AddColumn("dbo.Orders", "Cargo", c => c.String());
        }
    }
}
