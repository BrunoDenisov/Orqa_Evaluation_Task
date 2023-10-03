using System;
using System.Configuration;
using System.Data.Entity;
using OrqaDB.Domain.Models;
using MySql.Data.EntityFramework;

[DbConfigurationType(typeof(MySqlEFConfiguration))]
public class OrqaDbContext : DbContext
{
    public OrqaDbContext() : base("OrqaDbConnectionString")
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<WorkPosition> WorkPositions { get; set; }
    public DbSet<UserWorkPosition> UserWorkPositions { get; set; }

   
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {

    }
}
