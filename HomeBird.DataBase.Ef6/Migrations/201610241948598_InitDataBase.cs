namespace HomeBird.DataBase.Ef6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HbBroods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BroodDate = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        Percent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmptyCount = c.Int(nullable: false),
                        EmptyPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DeadCount = c.Int(nullable: false),
                        DeadPercent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlacePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HbLots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.LotId);
            
            CreateTable(
                "dbo.HbLots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdentifierNumber = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        AvgAdultPrice = c.Decimal(precision: 18, scale: 2),
                        AvgDailyPrice = c.Decimal(precision: 18, scale: 2),
                        SoldCount = c.Int(),
                        Loses = c.Int(),
                        Profit = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HbLayings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationTime = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        EggPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BroodId = c.Int(nullable: false),
                        LotId = c.Int(nullable: false),
                        IncubatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HbLots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.LotId);
            
            CreateTable(
                "dbo.HbOverheads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OverheadDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        LotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HbLots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.LotId);
            
            CreateTable(
                "dbo.HbPurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseDate = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Address = c.String(),
                        LotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HbLots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.LotId);
            
            CreateTable(
                "dbo.HbSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false),
                        Count = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Buyer = c.String(),
                        Type = c.Int(nullable: false),
                        LotId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HbLots", t => t.LotId, cascadeDelete: true)
                .Index(t => t.LotId);
            
            CreateTable(
                "dbo.HbIncubators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HbSales", "LotId", "dbo.HbLots");
            DropForeignKey("dbo.HbPurchases", "LotId", "dbo.HbLots");
            DropForeignKey("dbo.HbOverheads", "LotId", "dbo.HbLots");
            DropForeignKey("dbo.HbLayings", "LotId", "dbo.HbLots");
            DropForeignKey("dbo.HbBroods", "LotId", "dbo.HbLots");
            DropIndex("dbo.HbSales", new[] { "LotId" });
            DropIndex("dbo.HbPurchases", new[] { "LotId" });
            DropIndex("dbo.HbOverheads", new[] { "LotId" });
            DropIndex("dbo.HbLayings", new[] { "LotId" });
            DropIndex("dbo.HbBroods", new[] { "LotId" });
            DropTable("dbo.HbIncubators");
            DropTable("dbo.HbSales");
            DropTable("dbo.HbPurchases");
            DropTable("dbo.HbOverheads");
            DropTable("dbo.HbLayings");
            DropTable("dbo.HbLots");
            DropTable("dbo.HbBroods");
        }
    }
}
