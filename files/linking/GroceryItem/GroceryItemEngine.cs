using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListLlama.Accessors;
using ListLlama.Models;

namespace ListLlama.Engines
{
    public class GroceryItemEngine : IGroceryItemEngine
    {
        private readonly IGroceryItemAccessor _groceryItemAccessor;

        public GroceryItemEngine()
        {
            _groceryItemAccessor = new GroceryItemAccessor();
        }

        public IEnumerable<GroceryItem> GetItemsOnList(int ListId)
        {
            return _groceryItemAccessor.GetOnlyItemsForList(ListId);
        }

        public IEnumerable<GroceryItem> GetGroceryItems()
        {
            return _groceryItemAccessor.GetAll().ToList();
        }

        public GroceryItem GetGroceryItem(int id)
        {
            return _groceryItemAccessor.Get(id);
        }

        public GroceryItem InsertGroceryItem(GroceryItem newGroceryitem)
        {
            _groceryItemAccessor.Add(newGroceryitem);
            return newGroceryitem;
        }

        public GroceryItem UpdateGroceryItem(int id, GroceryItem updated)
        {
            _groceryItemAccessor.Edit(id, updated);
            return updated;
        }

        public GroceryItem DeleteGroceryItem(int id)
        {
            var groceryItem = _groceryItemAccessor.Get(id);
            if (groceryItem != null)
            {
                _groceryItemAccessor.Delete(groceryItem.GroceryItemId);
            }
            return groceryItem;
        }
    }
}