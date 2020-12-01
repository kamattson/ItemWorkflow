using System;
using item_workflow.Model;
using Microsoft.EntityFrameworkCore;

namespace item_workflow.Database
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }
    }
    
}
