using System;
using item_workflow.Model;

namespace item_workflow.Database
{
    public interface IItemRepository : IDisposable
    {
        void InsertItem(Item item);
        void UpdateItem(Item item);
        void Save();
    }
}
