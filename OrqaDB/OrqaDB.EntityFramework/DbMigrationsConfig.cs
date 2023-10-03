using System.Data.Entity.Migrations;

internal sealed class Configuration : DbMigrationsConfiguration<OrqaDbContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = false; 
        ContextKey = "OrqaDB.EntityFramework.OrqaDbContext"; 
    }

    protected override void Seed(OrqaDbContext context)
    {

    }
}
