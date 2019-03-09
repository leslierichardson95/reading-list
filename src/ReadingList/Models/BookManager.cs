using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ReadingList.Models
{
    public class BookManager
    {
        // stores list of all books by their id
        private static readonly Dictionary<long, Book> neutralBooks = new Dictionary<long, Book>();
        private static readonly Dictionary<long, Book> shelvedBooks = new Dictionary<long, Book>();
        private static readonly Dictionary<long, Book> rejectedBooks = new Dictionary<long, Book>();

        private static string filePath = "App_Data/books.json";
        private static Random random = new Random();
        
        static BookManager()
        {
            // extract book data from JSON file and store in books dictionary
            List<Book> books;
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                books = JsonConvert.DeserializeObject<List<Book>>(json);
            }
            // convert list of books to dictionary
            neutralBooks = books.ToDictionary(x => x.Id, x => x);
        }

        public static List<Book> GetNeutralBooks()
        {
            if (neutralBooks == null) return null;
            return neutralBooks.Values.ToList();
        }

        public static List<Book> GetShelvedBooks()
        {
            if (shelvedBooks == null) return null;
            return shelvedBooks.Values.ToList();
        }
        
        public static List<Book> GetRejectedBooks()
        {
            if (rejectedBooks == null) return null;
            return rejectedBooks.Values.ToList();
        }

        // retrieve a random neutral book
        public static Book GetNeutralBook()
        {
            int bookIndex = random.Next(neutralBooks.Count);
            return neutralBooks[bookIndex];
        }

        public static void RemoveNeutralBook(long id)
        {
            neutralBooks.Remove(id);
        }

        public static void AddShelvedBook(long id)
        {
            shelvedBooks.Add(id, neutralBooks[id]);
        }

        public static void AddRejectedBook(long id)
        {
            rejectedBooks.Add(id, neutralBooks[id]);
        }
    }
}
