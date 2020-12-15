using System;
using item_workflow.Model;
using Microsoft.EntityFrameworkCore;

namespace item_workflow.Database
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }
        public DbSet<Approval> Approval { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Item>().HasOne<Approval>().WithMany().HasForeignKey(a => a.WorkflowId);


        //    modelBuilder.Entity<Approval>()
        //    .HasNoKey()
        //    .HasOne<Item>()
        //    .WithMany()
        //    .HasForeignKey(a => a.WorkflowId);


        //}
    }

}
