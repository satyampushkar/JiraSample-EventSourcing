using JiraSample.Query.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraSample.Query.Infrastructure.Persistance;

public class JiraDatabaseContext : DbContext
{
    public JiraDatabaseContext(DbContextOptions<JiraDatabaseContext> options) 
        : base(options)
    {   
    }

    public DbSet<JiraItemEntity> JiraItems { get; set; }
    public DbSet<JiraItemHistoryEntity> JiraItemsHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<JiraItemEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<JiraItemEntity>().Property(x => x.Name).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<JiraItemEntity>().Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
        modelBuilder.Entity<JiraItemEntity>().Property(x => x.Author).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<JiraItemEntity>().Property(x => x.Asignee).IsRequired(false).HasMaxLength(50);
        modelBuilder.Entity<JiraItemEntity>().Property(x => x.ParentId).IsRequired(false);
        modelBuilder.Entity<JiraItemEntity>().Property(x => x.ItemStatus).HasConversion<int>().IsRequired();
        modelBuilder.Entity<JiraItemEntity>().Property(x => x.ItemType).HasConversion<int>().IsRequired();
        

        modelBuilder.Entity<JiraItemHistoryEntity>().HasKey(x => x.Id);
        modelBuilder.Entity<JiraItemHistoryEntity>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<JiraItemHistoryEntity>().Property(x => x.JiraItemId).IsRequired();

    }
}
