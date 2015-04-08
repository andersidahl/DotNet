namespace DahlDataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        PermissionLevel = c.Int(nullable: false),
                        GivenName = c.String(),
                        SurName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        OfferId = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Response = c.String(),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.OfferId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderStatus = c.String(),
                        CreatedAt = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(),
                        PicturePath = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.TileOffers",
                c => new
                    {
                        TileOfferId = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.TileOfferId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderRefId = c.Int(nullable: false),
                        ProductRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderRefId, t.ProductRefId })
                .ForeignKey("dbo.Orders", t => t.OrderRefId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductRefId, cascadeDelete: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OrderProducts", "ProductRefId", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "OrderRefId", "dbo.Orders");
            DropForeignKey("dbo.TileOffers", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Offers", "Customer_CustomerId", "dbo.Customers");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.TileOffers");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Offers");
            DropTable("dbo.Customers");
        }
    }
}
