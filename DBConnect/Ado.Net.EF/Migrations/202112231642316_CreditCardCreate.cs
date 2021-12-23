namespace Ado.Net.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditCardCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNumber = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(),
                        CardHolder = c.String(),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCards", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.CreditCards", new[] { "EmployeeId" });
            DropTable("dbo.CreditCards");
        }
    }
}
