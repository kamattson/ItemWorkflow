using System;
using System.Collections.Generic;
using item_workflow.Model;
using Microsoft.EntityFrameworkCore;

namespace item_workflow.Database
{
    public class ItemRepository : IItemRepository
    {
        private ItemDbContext _context;
        public ItemRepository(ItemDbContext itemContext)
        {
            this._context = itemContext;
        }
       
        public void InsertItem(Item item)
        {
            _context.Item.Add(item);
        }
        public void DeleteItem(int itemID)
        {
            Item item = _context.Item.Find(itemID);
            _context.Item.Remove(item);
        }
        public void UpdateItem(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
