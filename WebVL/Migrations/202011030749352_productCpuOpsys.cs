namespace WebVL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productCpuOpsys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Cpu", c => c.String());
            AddColumn("dbo.Products", "OpSys", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "OpSys");
            DropColumn("dbo.Products", "Cpu");
        }
    }
}
