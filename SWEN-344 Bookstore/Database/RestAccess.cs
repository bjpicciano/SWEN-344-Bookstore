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
        private const String currencyURL = "http://api.fixer.io/latest?base=USD";
        public Dictionary<String,int> CurrRates = new Dictionary<string, int>();
        private HttpClient client;

        private RestAccess()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(currencyURL);
            client.DefaultRequestHeaders.Accept.Clear();
            //populateCurrRates();
            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
        }

        private void populateCurrRates()
        {
            
            //dynamic json = System.Web.Helpers.Json.Decode(@"{ ""Name"": ""Jon Smith"", ""Address"": { ""City"": ""New York"", ""State"": ""NY"" }, ""Age"": 42 }");
            dynamic json = System.Web.Helpers.Json.Decode(GetString(""));
            CurrRates["CAN"] = json.rates.CAN;
            CurrRates["AUD"] = json.rates.AUD;
            CurrRates["GBP"] = json.rates.GBP;
            CurrRates["JPY"] = json.rates.JPY;
            CurrRates["EUR"] = json.rates.EUR;
            CurrRates["NZD"] = json.rates.NZD;
            CurrRates["RUB"] = json.rates.RUB;
        } 

        public User GetUserByEmail(String email)
        {
            User toReturn = null;
            try
            {
                String[] fields = GetFieldsFromJSON(GetString("User.php?action=get_user_by_email&email=" + email)).ToArray();
                toReturn = new Models.User(Convert.ToInt32(fields[0]), fields[1], fields[3], fields[4]);
            }
            catch (Exception ex)
            {
            }
            return toReturn;
        }

        public User GetUserByID(int userId)
        {
            User toReturn = null;
            try
            {
                String[] fields = GetFieldsFromJSON(GetString("User.php?action=get_user_by_id&id=" + userId)).ToArray();
                toReturn = new Models.User(Convert.ToInt32(fields[0]), fields[1], fields[3], fields[4]);
            }
            catch (Exception ex)
            {
            }
            return toReturn;
        }


        public Boolean AuthenticateUser(String email, String authtoken)
        {
            try
            {
                String[] fields = GetFieldsFromJSON(GetString("User.php?action=get_user_by_email&email=" + email)).ToArray();
                return authtoken.Equals(fields[5]);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /* Gets a string from the api and attempts to parse it for a book.
         * If it fails, returns null.
         */ 
        public Book GetBook(int bookId)
        {
            try
            {
                String s = GetString("Book.php?action=get_book_by_id&id=" + bookId);
                return ParseForBook(s);
            }catch(Exception ex)
            {
                return null;
            }
        }

        /* Tries to create a book with the information given. If it succeeds, it returns a task with a result of true.
         * If it fails, it returns a task with a result of false.
         */ 
        public int CreateBook(String auth, float price, String name, String desc)
        {
            HttpResponseMessage response = client.PostAsync("Book.php?action=create_book&author=" + FormatSpaces(auth) + "&price=" + price + "&name=" + FormatSpaces(name) + "&description=" + FormatSpaces(desc), null).Result;
            if (response.IsSuccessStatusCode)
            {
                return Convert.ToInt32(GetFieldsFromJSON((response.Content.ReadAsStringAsync().Result)).ToArray()[0]);
            }
            return -1;
        }

        public String FormatSpaces(String str)
        {
            String s = str;
            String newStr = "";
            while(s.IndexOf(" ") >= 0)
            {
                newStr += s.Substring(0, s.IndexOf(" "));
                newStr += "%20";
                s = s.Substring(s.IndexOf(" ") + 1);
            }
            newStr += s;
            return newStr;
        }

        /* Tries to update a book with an ID of bookID with the information given. If it succeeds, it returns a task with a result of true.
         * If it fails, it returns a task with a result of false.
         */ 
        public Boolean UpdateBook(int bookID, String auth, float price, String name, String desc)
        {
            Book[] books = GetBooks().ToArray();
            Boolean isThere = false;
            for(int i = 0; i < books.Length; i++)
            {
                if (books[i].BookId == bookID)
                {
                    isThere = true;
                }
            }
            if (!isThere)
            {
                return false;
            }
            HttpResponseMessage response = client.PostAsync("Book.php?action=update_book&id=" + bookID + "&author=" + FormatSpaces(auth) + "&price=" + price + "&name=" + FormatSpaces(name) + "&description=" + FormatSpaces(desc), null).Result;
            return true;
        }

        public void DeleteLastBook()
        {
            HttpResponseMessage response = client.PostAsync("Book.php?action=delete_last_book", null).Result;
            //return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }

        public void DeleteAllBooks()
        {
            HttpResponseMessage response = client.PostAsync("Book.php?action=delete_all_books", null).Result;
            //return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }

        /*Gets a string that includes all book info, then cuts that string up into individual book infos and parses each seperatly
         *to get the list of all books.
         */
        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            String toChop = GetString("Book.php?action=get_all_books");
            while (toChop.IndexOf("}") >= 0)
            {
                books.Add(ParseForBook(toChop.Substring(toChop.IndexOf("{"), toChop.IndexOf("}") - toChop.IndexOf("{") + 1)));
                toChop = toChop.Substring(toChop.IndexOf("}") + 1);
            }
            return books;
        }

        /*This method gets the response string from the api.
         */
        public String GetString(String paramater)
        {
            
            HttpResponseMessage response = client.GetAsync(paramater).Result;
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
                //System.Diagnostics.Debug.WriteLine(s);
                String[] fields = GetFieldsFromJSON(s).ToArray();
                b.BookId = Convert.ToInt32(fields[0]);
                b.Author = fields[1];
                b.Price = (float) Convert.ToDouble(fields[2]);
                b.Name = fields[3];
                b.desc = fields[4];
                return b;
            }catch(Exception ex)
            {
                return null;
            }
        }

        public List<String> GetFieldsFromJSON(String s)
        {
            String jString = s;
            List<String> toReturn = new List<String>();
            String field;
            while(jString.IndexOf(",") >= 0)
            {
                field = jString.Substring(jString.IndexOf(":") + 1, (jString.IndexOf(",") - jString.IndexOf(":") - 1));
                field = field.Trim('"');
                toReturn.Add(field);
                jString = jString.Substring(jString.IndexOf(",") + 1);
            }
            field = jString.Substring(jString.IndexOf(":") + 1, jString.IndexOf("}") - 1 - jString.IndexOf(":"));
            field = field.Trim('"');
            toReturn.Add(field);
            return toReturn;
        }

    }
}