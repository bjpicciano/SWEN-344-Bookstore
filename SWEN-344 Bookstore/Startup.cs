using Microsoft.Owin;
using Owin;
using SWEN_344_Bookstore.Models;
using SWEN_344_Bookstore.Database;
using System.Collections.Generic;

[assembly: OwinStartupAttribute(typeof(SWEN_344_Bookstore.Startup))]
namespace SWEN_344_Bookstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            RestAccess ra = RestAccess.GetInstance();
            Database_Test.SQLite_Database db = Database_Test.SQLite_Database.GetInstance();
        }
    }
}
