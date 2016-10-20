using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Markup;
using SWEN_344_Bookstore.Models;
using System.Threading.Tasks;

namespace SWEN_344_Bookstore.Database
{
    public class RestAccess
    {
        private static RestAccess singletonInstance;
        //This is the singleton access method. You should never instantiate a RestAccess object, instead just call GetInstance().
        public static RestAccess GetInstance() { if (singletonInstance == null) { singletonInstance = new RestAccess(); } return singletonInstance; }


        private const String URL = "http://vm344e.se.rit.edu/api/";
        private String urlParameters;
        private HttpClient client;

        private RestAccess()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        /* Gets a string from the api and attempts to parse it for a book.
         * If it fails, returns null.
         */ 
        public Book GetBook(int bookId)
        {
            try
            {
                return ParseForBook(GetString("Book.php?action=get_book_by_id&id=" + bookId).Result);
            }catch(Exception ex)
            {
                return null;
            }
        }

        /* Tries to create a book with the information given. If it succeeds, it returns a task with a result of true.
         * If it fails, it returns a task with a result of false.
         */ 
        public async Task<Boolean> CreateBook(String auth, float price, String name)
        {
            HttpResponseMessage response = await client.PostAsync("Book.php?action=create_book&author=" + auth + "&price=" + price + "&name=" + name, null);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        /* Tries to update a book with an ID of bookID with the information given. If it succeeds, it returns a task with a result of true.
         * If it fails, it returns a task with a result of false.
         */ 
        public async Task<Boolean> UpdateBook(int bookID, String auth, float price, String name)
        {
            HttpResponseMessage response = await client.PutAsync("Book.php?action=update_book&id=" + bookID + "&author=" + auth + "&price=" + price + "&name=" + name, null);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        /*Gets a string that includes all book info, then cuts that string up into individual book infos and parses each seperatly
         *to get the list of all books.
         */
        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            String toChop = GetString("Book.php?action=get_all_books").Result;
            while (toChop.IndexOf("}") >= 0)
            {
                books.Add(ParseForBook(toChop.Substring(toChop.IndexOf("{"), toChop.IndexOf("}") - toChop.IndexOf("{") + 1)));
                toChop = toChop.Substring(toChop.IndexOf("}") + 1);
            }
            return books;
        }

        /*This method gets the response string from the api. To actually access the string returned, call GetString(param).Result
         */
        public async Task<String> GetString(String paramater)
        {
            
            HttpResponseMessage response = await client.GetAsync(paramater);
            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return null;
        }

        /*Parses a string for the info for a book object. It has to include a } after all information, and has to have a :
          before the bookID.
         */
        private Book ParseForBook(String s)
        {
            try
            {
                Book b = new Book();
                System.Diagnostics.Debug.WriteLine(s);
                b.BookId = Convert.ToInt32(s.Substring(s.IndexOf(":") + 1, (s.IndexOf(",") - s.IndexOf(":") - 1)));
                s = s.Substring(s.IndexOf(",") + 1);
                b.Author = s.Substring(s.IndexOf(":") + 2, (s.IndexOf(",") - s.IndexOf(":") - 3));
                s = s.Substring(s.IndexOf(",") + 1);
                b.Price = (float) Convert.ToDouble(s.Substring(s.IndexOf(":") + 1, (s.IndexOf(",") - s.IndexOf(":") - 1)));
                s = s.Substring(s.IndexOf(",") + 1);
                b.Name = s.Substring(s.IndexOf(":") + 2, s.IndexOf("}") - 3 - s.IndexOf(":"));
                return b;
            }catch(Exception ex)
            {
                return null;
            }
        }

    }
}