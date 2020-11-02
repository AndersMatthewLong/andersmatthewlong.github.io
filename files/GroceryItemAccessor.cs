using ListLlama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace ListLlama.Accessors
{

   
    public class GroceryItemAccessor
    {
        private ApplicationDbContext _context;
        public GroceryItemAccessor()
        {
            _context = new ApplicationDbContext();
        }

        protected void Dispose(bool disposing)
        {
            _context.Dispose();
            return;

        }

        /* Author: Anders Long
         * The point of this function is to retrieve a single groceryItem from the database
         * using the given unique key, this is used in several places, mostly in the Save, Edit, and Delete
         * methods. 
         */

        public GroceryItem Get(int groceryItemId)
        {
            return _context.GroceryItems.SingleOrDefault(c => c.GroceryItemId == groceryItemId); 
        }

        /* Author: Anders Long
         * Important function for maintaining users only see their items, and that, when
         * working with a list, only the items linked to the list are displayed for editing,
         * deletion, etc. 
         */

        public IEnumerable<GroceryItem> GetOnlyItemsForList(int listId)
        {
            IEnumerable<GroceryItem> groceryItems = _context.GroceryItems.Where(g => g.GroceryListId == listId);
            return groceryItems;
        }

        /* Author: Anders Long
         * This function, given an instance of a GroceryItem,
         * puts it in the DB
         * 
         */

        public void Add(GroceryItem groceryItem)
        {
            _context.GroceryItems.Add(groceryItem);
            _context.SaveChanges();
            return;
        }

        /* Author: Anders Long
         * a simple function that retrieves all groceryItems, currently unused. 
         */

        public IEnumerable<GroceryItem> GetAll()
        {
            return _context.GroceryItems;
        }

        /* Author: Anders Long
         * This function, given a groceryItemId, and a new item, containing all
         * edited properties, applies these properties to the record in the DB.
         * 
         */

        public void Edit(int groceryItemId, GroceryItem newGroceryItem)
        {
            var groceryItemsInDb = this.Get(groceryItemId);
            groceryItemsInDb.Title = newGroceryItem.Title;
            groceryItemsInDb.Quantity = newGroceryItem.Quantity;
            groceryItemsInDb.IsChecked = newGroceryItem.IsChecked;
            _context.SaveChanges();
            return;
        }

        /* Author: Anders Long
         * This function removes a GroceryItem record from the DB
         * given its unique groceryItemId.
         */

        public void Delete(int groceryItemId)
        {
            var groceryItem = this.Get(groceryItemId);
            _context.GroceryItems.Remove(groceryItem);
            _context.SaveChanges();
            return;
        }

    }
}