using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWEN_344_Bookstore.Models {
    public class Bookstore {
        private List<InventoryBook> Inventory { get; set; }

        public List<InventoryBook> GetInventory () {
            return this.Inventory;
        }

        public void AddToInventory (Book book) {
            var inventoryBook = new InventoryBook(book);

            // if Inventory !contains inventoryBook
                this.Inventory.Add(inventoryBook);
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