using ListLlama.Accessors;
using ListLlama.Models;
using ListLlama.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/* Authors: Anders Long
 * The purpose of this class is to handle the user experience with Grocery Items
 * including the listing of all items in a list, the creation of new, deletion of old,
 * and editing of existing item records in the program 
 * 
 * TODO
 * -implement deletion functionality
 * -add check off functionality.
 */

namespace ListLlama.Controllers
{
    public class GroceryItemsController : Controller
    {
        // FIELDS

        private GroceryItemAccessor groceryItemAccessor;

        //Gives the controller access to the database
        public GroceryItemsController()
        {
            groceryItemAccessor = new GroceryItemAccessor();
        }
    

        /*
         * This is the place to view all the items associated with a list
         * The user is directed to this page from an Action link in the GroceryList index page
         * Which feeds the list id so that we can only grab the items associated with that list
         * (pretty nifty huh?)
         */

        public ViewResult Index(int? id)
        {
            var GroceryItemIndexViewModel = new GroceryItemIndexViewModel
            {
                GroceryItems = groceryItemAccessor.GetOnlyItemsForList((int)id),
                GroceryListId = (int)id
            };
            
            return View(GroceryItemIndexViewModel);
        }

        /*
         * This is where new GrocertItem records are created (shocker)
         * The user is directed here if they click an actionlink in the index page for GroceryItems
         * the actionlink feeds this function the GroceryListId, so that the new groceryItem is given a
         * foreign key associating it with the appropriate list
         * 
         * 
         * This function redirects the user to the GroceryItemForm view, feeding it 
         * a GroceryItemFormViewModel instance, which contains the listId, and will later
         * contain the GroceryItem.
         */

        public ActionResult New(int GroceryListId)
        {
            GroceryItemFormViewModel m = new GroceryItemFormViewModel
            {
                GroceryListId = GroceryListId
            };
            return View("GroceryItemForm", m);
        }

        /* This function is the end point of either the new item process or the edit item process
         * In the GroceryItemForm view, the submit button redirects the user to this controller,
         * which takes the modified model, now with an edited groceryItem model. If the model is new
         * it is added to the DB, otherwise, it is modified and saved
         * 
         * This page ends with redirecting the user back to the index page, which displays the contents
         * of the list the user is on.
         */

        [HttpPost]
        public ActionResult Save(GroceryItem groceryItem)
        {
            if (groceryItem.Title == null || groceryItem.Quantity == 0)
            {
                GroceryItemFormViewModel m = new GroceryItemFormViewModel
                {
                    GroceryItem = groceryItem,
                    GroceryListId = groceryItem.GroceryListId
                };
                return View("GroceryItemForm", m);
            }
            if (groceryItem.GroceryItemId == 0)
            {
                groceryItemAccessor.Add(groceryItem);
            }
            else
            {
                var groceryItemsInDb = groceryItemAccessor.Get(groceryItem.GroceryItemId);
                groceryItemAccessor.Edit(groceryItem.GroceryItemId, groceryItem);
            }
        
            return RedirectToAction("Index", "GroceryItems", new { id = groceryItem.GroceryListId });
        }

        //See (New)'s documentation, similar except that this edits an existing record
         
        public ActionResult Edit(int id)
        {
            var groceryItem = groceryItemAccessor.Get(id);

            if (groceryItem == null)
                return RedirectToAction("Error", "Home");
            var viewModel = new GroceryItemFormViewModel
            {
                GroceryItem = groceryItem,
                GroceryListId = groceryItem.GroceryListId
            };
            return View("GroceryItemForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            var groceryItem = groceryItemAccessor.Get(id);
            groceryItemAccessor.Delete(id);
            return RedirectToAction("Index", "GroceryItems", new { id = groceryItem.GroceryListId });
        }


    }
}