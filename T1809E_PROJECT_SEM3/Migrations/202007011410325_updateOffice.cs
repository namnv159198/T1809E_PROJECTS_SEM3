namespace T1809E_PROJECT_SEM3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOffice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Offices", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Offices", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
