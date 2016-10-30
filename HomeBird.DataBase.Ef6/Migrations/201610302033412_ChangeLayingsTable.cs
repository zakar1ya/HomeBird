namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLayingsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HbLayings", "BroodId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HbLayings", "BroodId", c => c.Int(nullable: false));
        }
    }
}
