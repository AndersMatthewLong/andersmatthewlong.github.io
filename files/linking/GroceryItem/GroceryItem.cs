using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/* AUTHORS:
 * 
 * the main goal of this class is to represent
 * and item in the grocery class
 * 
 * 
 * NOTE: (SHOULD BE COMPLETE)
 */

namespace ListLlama.Models
{
    public class GroceryItem
    {
        [Key]
        public int GroceryItemId { get; set; }
        public int GroceryListId { get; set; }
        [ForeignKey("GroceryListId")]
        public GroceryList GroceryList { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }
    }
}