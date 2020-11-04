using ListLlama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListLlama.ViewModels
{
    public class GroceryItemIndexViewModel
    {
        public IEnumerable<GroceryItem> GroceryItems { get; set; }

        public int GroceryListId { get; set; }
        public bool IsChecked { get; set; }
    }
}