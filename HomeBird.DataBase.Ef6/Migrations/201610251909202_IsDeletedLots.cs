namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedLots : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HbLots", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HbLots", "IsDeleted");
        }
    }
}
