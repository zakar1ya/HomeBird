namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeletedPurchasesField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HbPurchases", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HbPurchases", "IsDeleted");
        }
    }
}
