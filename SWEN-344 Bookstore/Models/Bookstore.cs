using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_344_Bookstore.Models {
    public class Bookstore {
        private List<InventoryBook> inventory { get; set; }

        public List<InventoryBook> getInventory () {
            return this.inventory;
        }

        public void addToInventory (Book book) {
            InventoryBook inventoryBook = new InventoryBook(book);

            // if inventory !contains inventoryBook
                this.inventory.Add(inventoryBook);
            // else increment stock + 1
        }
    }

    public class ShoppingCart
    {
        public string BookName;
        public string BookAuthor;
        public string BookPrice;

    }
}