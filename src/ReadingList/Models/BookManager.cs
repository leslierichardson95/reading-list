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
        private Dictionary<long, Book> neutralBooks = new Dictionary<long, Book>();
        private Dictionary<long, Book> shelvedBooks = new Dictionary<long, Book>();
        private Dictionary<long, Book> rejectedBooks = new Dictionary<long, Book>();

        private static string relativePath = "App_Data/books.json";

        // Use when book info isn't available
        private static string noImagePath = "App_Data/imageNotFound.png";

        // DEMO: Snapshot debugger - Only use this for demo!
        private static string absolutePath = "C:/Users/lerich/Documents/ReadingList/ReadingList/App_Data/books.json";

        private static int totalBooks = 100;

        private Random random = new Random();

        // store keys for each dictionary
        private static List<long> neutralKeysEnumerator = new List<long>();
        private static List<long> shelvedKeysEnumerator = new List<long>();
        private static List<long> rejectedKeysEnumerator = new List<long>();

        public BookManager()
        {
            // extract book data from JSON file and store in books dictionary
            List<Book> books;
            using (StreamReader r = new StreamReader(relativePath))
            {
                string json = r.ReadToEnd();
                books = JsonConvert.DeserializeObject<List<Book>>(json);
            }

            for (int i = 0; i < books.Count; i++)
            {
                books[i].Cover = GetBase64StringForImage(books[i].Cover);
                books[i].TimesRead = 0;
            }

            // Make 5000 placeholder books to mess up perf when creating a new book
            for (int i = 0; i < 5000; i++)
            {
                Book book = new Book();
                book.Id = ++totalBooks;
                book.Title = "Book" + i;
                book.Author = "Unknown";
                book.TimesRead = 0;

                books.Add(book);
            }

            // convert list of books to dictionary
            neutralBooks = books.ToDictionary(x => x.Id, x => x);

            neutralKeysEnumerator = neutralBooks.Keys.ToList();
        }

        public List<Book> GetNeutralBooks()
        {
            if (neutralBooks == null) return null;
            return neutralBooks.Values.ToList();
        }

        public List<Book> GetShelvedBooks()
        {
            if (shelvedBooks == null) return null;
            return shelvedBooks.Values.ToList();
        }
        
        public List<Book> GetRejectedBooks()
        {
            if (rejectedBooks == null) return null;
            return rejectedBooks.Values.ToList();
        }

        // retrieve a random neutral book
        public Book GetNeutralBook()
        {
            int count = neutralBooks.Count;
            if (count <= 0)
            {
                return null;
            }

            int bookIndex = random.Next(count);
            return neutralBooks[neutralKeysEnumerator[bookIndex]];
        }

        public Book GetShelvedBook(long id)
        {
            Book shelvedBook = shelvedBooks[id];
            // DEMO 2: Data breakpoints - add an extra TimesRead incrementation here
            //shelvedBook.TimesRead++;
            return shelvedBook;
        }

        public void AddShelvedBook(long id)
        {
            shelvedBooks.Add(id, neutralBooks[id]);
            shelvedKeysEnumerator.Add(id);
        }

        public void AddAllToShelf()
        {
            // DEMO 1: Tracepoints & Conditional BPs - replace neutralKeysEnumerator.Count with 
            // neutralBooks.Count and uncomment neutralBooks.Remove(). To fix, make neutralBooks.Countloop 
            // comparison, comment neutralBooks.Remove() and uncomment neutralBooks.Clear()
            for (int i = 0; i < neutralBooks.Count; i++)
            {
                AddShelvedBook(neutralKeysEnumerator[i]);
                //neutralBooks.Remove(neutralKeysEnumerator[i]);
            }

            neutralKeysEnumerator.Clear();
            neutralBooks.Clear();
        }

        public void AddRejectedBook(long id)
        {
            // DEMO: Data Bps - change rejectedBooks to shelvedBooks instead to see where shelvedBooks is changing
            rejectedBooks.Add(id, neutralBooks[id]);
            rejectedKeysEnumerator.Add(id);
        }

        public void AddAllToRejected()
        {
            for (int i = 0; i < neutralKeysEnumerator.Count; i++)
            {
                AddRejectedBook(neutralKeysEnumerator[i]);
            }

            neutralKeysEnumerator.Clear();
            neutralBooks.Clear();
        }

        public void AddNeutralBook(Book book)
        {
            neutralBooks.Add(book.Id, book);
            neutralKeysEnumerator.Add(book.Id);
        }

        public void RemoveNeutralBook(long id)
        {
            neutralBooks.Remove(id);
            neutralKeysEnumerator.Remove(id);
        }

        public void RemoveShelvedBook(long id)
        {
            Book book = GetShelvedBook(id);
            rejectedBooks.Add(id, book);
            rejectedKeysEnumerator.Add(id);

            shelvedBooks.Remove(id);
            shelvedKeysEnumerator.Remove(id);

        }

        public void RemoveRejectedBook(long id)
        {
            rejectedBooks.Remove(id);
            rejectedKeysEnumerator.Remove(id);
        }

        public void ResetAllBooks()
        {
            for (int i = 0; i < shelvedBooks.Count; i++)
            {
                Book book = shelvedBooks[shelvedKeysEnumerator[i]];
                AddNeutralBook(book);
                //shelvedBooks.Remove(book.Id);
                //shelvedKeysEnumerator.Remove(book.Id);
            }
            shelvedBooks.Clear();
            shelvedKeysEnumerator.Clear();

            for (int i = 0; i < rejectedBooks.Count; i++)
            {
                Book book = rejectedBooks[rejectedKeysEnumerator[i]];
                AddNeutralBook(book);
                //rejectedBooks.Remove(book.Id);
            }
            rejectedBooks.Clear();
            rejectedKeysEnumerator.Clear();
        }
        
        // Add new book to neutral books list for Rate Books page
        public bool CreateBook(Book book)
        {
            if (ValidateBook(book) == true)
            {
                // Set default book properties and add book to neutralbooks list
                book.TimesRead = 0;
                book.Id = ++totalBooks;

                AddNeutralBook(book);

                return true;
            }
            return false;
        }

        // Check that the newly created book does not already exist
        public bool ValidateBook(Book book)
        {
            // Check if book already exists

            // DEMO: Profiling - Using unnecessary for loop over Dictionary perks to examine
            // high CPU usage. Improve perf by searching for duplicates via 
            // neutralBooks.ContainsValue()
            for (int i = 1; i <= neutralBooks.Count; i++)
            {
                for (int j = 1; j <= neutralBooks.Count; j++)
                {
                    if (neutralBooks[j].Author.Equals(book.Author))
                    {
                        return false;
                    }
                }
                if (neutralBooks[i].Title.Equals(book.Title))
                {
                    return false;
                }
            }
            for (int i = 1; i <= shelvedBooks.Count; i++)
            {
                if (shelvedBooks[i].Title.Equals(book.Title))
                {
                    return false;
                }
            }
            for (int i = 1; i <= rejectedBooks.Count; i++)
            {
                if (rejectedBooks[i].Title.Equals(book.Title))
                {
                    return false;
                }
            }
            return true;
        }

        public void FinishedBook(int id)
        {
            Book book = GetShelvedBook(id);
            book.TimesRead++;
            book.LastReadDate = DateTime.Now;
        }

        public bool NeutralIsEmpty()
        {
            if (neutralBooks.Count == 0) return true;
            else return false;
        }

        public string FinishedToString(string person, Book book)
        {
            return person + " " + book.FinishedBookString();
        }

        // convert book cover images into base 64
        public string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes;
            try
            {
                imageBytes = System.IO.File.ReadAllBytes(imgPath);
            }
            catch(Exception)
            {
                imageBytes = System.IO.File.ReadAllBytes(noImagePath);

            }
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
    }
}
