using ListLlama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace ListLlama.Accessors
{

   
    public class GroceryListAccessor : IGroceryListAccessor
    {
        private ApplicationDbContext _context;
        public GroceryListAccessor()
        {
            _context = new ApplicationDbContext();
        }

        public void Dispose(bool disposing)
        {
            _context.Dispose();

        }

        /* Author: Anders Long
         * The point of this function is to retrieve a single groceryList from the database
         * using the given unique key, this is used in several places, mostly in the Save, Edit, and Delete
         * methods. 
         */

        public GroceryList Get(int groceryListId)
        {
            return _context.GroceryLists.SingleOrDefault(c => c.GroceryListId == groceryListId);
        }

        /* Author: Anders Long
         * This function, given an instance of a GroceryList,
         * puts it in the DB
         * 
         */
        public void Add(GroceryList groceryList)
        {
            _context.GroceryLists.Add(groceryList);
            _context.SaveChanges();
        }

        /* Author: Anders Long
         * a simple function that retrieves all groceryItems, currently unused. 
         */

        public IEnumerable<GroceryList> GetAll()
        {
            return _context.GroceryLists;
        }

        /* Author: Anders Long
         * This function, given a groceryListId, and a new list, containing all
         * edited properties, applies these properties to the record in the DB.
         * 
         */
        public void Edit(int groceryListId, GroceryList newGroceryList)
        {
            var groceryListsInDb = this.Get(groceryListId);
            groceryListsInDb.Title = newGroceryList.Title;
            _context.SaveChanges();
        }

        /* Author: Anders Long
         * This function removes a GroceryList record from the DB
         * given its unique groceryItemId.
         */
        public void Delete(int groceryListId)
        {
            var groceryList = this.Get(groceryListId);
            var groceryItems = _context.GroceryItems.Where(g => g.GroceryListId == groceryListId);
            foreach (GroceryItem item in groceryItems)
            {
                _context.GroceryItems.Remove(item);
            }
            _context.GroceryLists.Remove(groceryList);
            _context.SaveChanges();
            return;
        }

    }
}