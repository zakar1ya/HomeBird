namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LayingIsDeletedFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HbLayings", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HbLayings", "IsDeleted");
        }
    }
}
