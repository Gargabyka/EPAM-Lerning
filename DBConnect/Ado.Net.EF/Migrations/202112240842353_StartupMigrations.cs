namespace Ado.Net.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartupMigrations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "SupplierID", c => c.Int());
            AlterColumn("dbo.Products", "CategoryID", c => c.Int());
            AlterColumn("dbo.Order Details", "OrderID", c => c.Int(nullable: false));
            AlterColumn("dbo.Order Details", "ProductID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "CustomerID", c => c.String(maxLength: 5, fixedLength: true));
            AlterColumn("dbo.Orders", "EmployeeID", c => c.Int());
            AlterColumn("dbo.Orders", "ShipVia", c => c.Int());
            AlterColumn("dbo.Employees", "ReportsTo", c => c.Int());
            AlterColumn("dbo.Territories", "RegionID", c => c.Int(nullable: false));
            AlterColumn("dbo.CreditCards", "EmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.EmployeeTerritories", "EmployeeID", c => c.Int(nullable: false));
            AlterColumn("dbo.EmployeeTerritories", "TerritoryID", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmployeeTerritories", "TerritoryID", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.EmployeeTerritories", "EmployeeID", c => c.Int(nullable: false));
            AlterColumn("dbo.CreditCards", "EmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Territories", "RegionID", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "ReportsTo", c => c.Int());
            AlterColumn("dbo.Orders", "ShipVia", c => c.Int());
            AlterColumn("dbo.Orders", "EmployeeID", c => c.Int());
            AlterColumn("dbo.Orders", "CustomerID", c => c.String(maxLength: 5, fixedLength: true));
            AlterColumn("dbo.Order Details", "ProductID", c => c.Int(nullable: false));
            AlterColumn("dbo.Order Details", "OrderID", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "CategoryID", c => c.Int());
            AlterColumn("dbo.Products", "SupplierID", c => c.Int());
        }
    }
}
