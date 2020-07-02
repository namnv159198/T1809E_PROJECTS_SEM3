namespace T1809E_PROJECT_SEM3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Email");
        }
    }
}
