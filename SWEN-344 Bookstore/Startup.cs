using Microsoft.Owin;
using Owin;
using SWEN_344_Bookstore.Models;
using SWEN_344_Bookstore.Database;

[assembly: OwinStartupAttribute(typeof(SWEN_344_Bookstore.Startup))]
namespace SWEN_344_Bookstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //Database_Test.SQLite_Database fug = Database_Test.SQLite_Database.getInstance();
            //fug.InsertInventoryBook(20, true, 2);
            //InventoryBook book = fug.GetInventoryBook(2);
            RestAccess ra = RestAccess.GetInstance();
            //System.Diagnostics.Debug.WriteLine(ra.GetBooks().ToArray()[0].Author);
            //ra.CreateBook("rxh4133", 200.37f, "How to access web apis", "fug");
            Database_Test.SQLite_Database db = Database_Test.SQLite_Database.GetInstance();
            for(int i = 6; i >=0; i--)
            {
                db.InsertReview(7, 1, "fug" + i);
            }
            
        }
    }
}
