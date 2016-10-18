using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace SWEN_344_Bookstore.Models
{
    public class Bookstore
    {
        private List<InventoryBook> Inventory { get; set; }

        public List<InventoryBook> GetInventory()
        {
            return this.Inventory;
        }

        public void AddToInventory(Book book)
        {
            var inventoryBook = new InventoryBook(book);

            // if Inventory contains inventoryBook, increment its stock by 1
            Boolean exists = false;
            foreach (InventoryBook ib in Inventory)
            {
                if (ib.GetBook() == book)
                {
                    ib.incStock();
                    exists = true;
                }
            }
            // else add it to Inventory
            if (exists == false)
            {
                this.Inventory.Add(inventoryBook);
            }
        }
    }
}


    //need to refactor Book

 /*   public class ShoppingCart
    {


        public List<Book> Items { get; private set; }

        // Readonly properties can only be set in initialization or in a constructor
        public static readonly ShoppingCart Instance;

        // The static constructor is called as soon as the class is loaded into memory
        static ShoppingCart()
        {
            // If the cart is not in the session, create one and put it there
            // Otherwise, get it from the session
        }

        // A protected constructor ensures that an object can't be created from outside
        //protected ShoppingCart() { }




        /*
         * AddItem() - Adds an item to the shopping 
         */
        /*     public void AddItem(int productId)
             {
                 // Create a new item to add to the cart
                 Book newItem = new Book(productId);

                 // If this item already exists in our list of items, increase the quantity
                 // Otherwise, add the new item to the list
                 if (Items.Contains(newItem))
                 {
                     foreach (Book item in Items)
                     {
                         if (item.Equals(newItem))
                         {
                             item.Quantity++;
                             return;
                         }
                     }
                 }
                 else
                 {
                     newItem.Quantity = 1;
                     Items.Add(newItem);
                 }
             }
     */

        /*
         * SetItemQuantity() - Changes the quantity of an item in the cart
         
        public void SetItemQuantity(int productId, int quantity)
        {
            // If we are setting the quantity to 0, remove the item entirely
            if (quantity == 0)
            {
                RemoveItem(productId);
                return;
            }

            // Find the item and update the quantity
            Book updatedItem = new Book(productId);

            foreach (Book item in Items)
            {
                if (item.Equals(updatedItem))
                {
                    item.Quantity = quantity;
                    return;
                }
            }
        }
        */
        /*
         * RemoveItem() - Removes an item from the shopping cart
         
        public void RemoveItem(int productId)
        {
            Book removedItem = new Book(productId);
            Items.Remove(removedItem);
        }


        /*
         * GetSubTotal() - returns the total price of all of the items
         *                 before tax, shipping, etc.
         */
     /*   public decimal GetSubTotal()
        {
            decimal subTotal = 0;
            foreach (Book item in Items)
                subTotal += item.TotalPrice;

            return subTotal;
        }

    }
}
*/