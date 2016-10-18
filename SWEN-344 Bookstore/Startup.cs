using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SWEN_344_Bookstore.Startup))]
namespace SWEN_344_Bookstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            System.Diagnostics.Debug.WriteLine("fug");
            ConfigureAuth(app);
            Database_Test.SQLite_Database fug = Database_Test.SQLite_Database.getInstance();
            System.Object[] book = fug.GetInventoryBook(7);
            
  
        }
    }
}
