using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListLlama.Models

    /**
     * GroceryList class
     * Authors: Anders Long and Natalie Ruckman
     * 
     * Main purpose to contain the data associated with a grocery list
     * 
     * NOTE: (NOT COMPLETE AS IT NEEDS TO BE LINKED TO THE USER)
     */
{
    public class GroceryList
    {
        [Key]
        public int GroceryListId { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<GroceryItem> GroceryItems { get; set; }

        public string UserName { get; set; }
        [ForeignKey("UserName")]
        public ApplicationUser ApplicationUser { get; set; }

    }
}