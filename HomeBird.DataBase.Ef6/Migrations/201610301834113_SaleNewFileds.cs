namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaleNewFileds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HbSales", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.HbSales", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HbSales", "Comment");
            DropColumn("dbo.HbSales", "IsDeleted");
        }
    }
}
