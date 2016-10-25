namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedIncubators : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HbIncubators", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HbIncubators", "IsDeleted");
        }
    }
}
