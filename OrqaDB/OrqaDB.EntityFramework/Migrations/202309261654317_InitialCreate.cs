using System;
using System.Data.Entity.Migrations;

public partial class InitialCreate : DbMigration
{
    public override void Up()
    {
        CreateTable(
            "dbo.Roles",
            c => new
                {
                    Id = c.Guid(nullable: false),
                    RoleName = c.String(),
                    RoleDescription = c.String(),
                })
            .PrimaryKey(t => t.Id);
        
        CreateTable(
            "dbo.Users",
            c => new
                {
                    Id = c.Guid(nullable: false),
                    FirstName = c.String(),
                    LastName = c.String(),
                    Username = c.String(),
                    Password = c.String(),
                    RoleId = c.Guid(nullable: false),
                })
            .PrimaryKey(t => t.Id);
        
        CreateTable(
            "dbo.UserWorkPositions",
            c => new
                {
                    Id = c.Guid(nullable: false),
                    User = c.String(),
                    WorkPosition = c.String(),
                    ProductName = c.String(),
                    Date = c.DateTime(nullable: false),
                    UserId = c.Guid(nullable: false),
                    WorkPositionId = c.Guid(nullable: false),
                })
            .PrimaryKey(t => t.Id);
        
        CreateTable(
            "dbo.WorkPositions",
            c => new
                {
                    Id = c.Guid(nullable: false),
                    PositionName = c.String(),
                    PositionDescription = c.String(),
                })
            .PrimaryKey(t => t.Id);
        
    }
    
    public override void Down()
    {
        DropTable("dbo.WorkPositions");
        DropTable("dbo.UserWorkPositions");
        DropTable("dbo.Users");
        DropTable("dbo.Roles");
    }
}
