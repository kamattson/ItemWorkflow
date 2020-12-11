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
    }
    
}
