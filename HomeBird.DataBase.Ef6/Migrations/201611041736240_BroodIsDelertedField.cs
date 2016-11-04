namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BroodIsDelertedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HbBroods", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HbBroods", "IsDeleted");
        }
    }
}
