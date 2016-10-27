namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverheadsIsDeletedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HbOverheads", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HbOverheads", "IsDeleted");
        }
    }
}
