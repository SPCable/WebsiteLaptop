namespace WebVL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrdersDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.String(),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Orders", "Count", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Count");
            DropTable("dbo.OrdersDetails");
        }
    }
}
