using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/**
 * We may want to extract these to their own files,
 * but im unsure where the database comes in
 **/
namespace SWEN_344_Bookstore.Models {
    public class User {
        private int uid { get; set; }

        public int getUid () {
            return this.uid;
        }
    }

    public class ActiveUser {
        private List<Book> receipts;

        public void addReceipt (Book book) {
            this.receipts.Add(book);
        }
    }

    public class Student : ActiveUser {
    }

    public class Instructer : ActiveUser {
    }

    public class Admin : User {
    }
}