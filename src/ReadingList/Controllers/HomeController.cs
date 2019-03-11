using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Models;

namespace ReadingList.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookManager bookManager;

        public HomeController(BookManager bookManager)
        {
            this.bookManager = bookManager;
        }

        public IActionResult Index()
        {
            List<Book> shelvedBooks = bookManager.GetShelvedBooks();

            ViewData["ShelvedBooks"] = shelvedBooks;
            ViewData["Title"] = "MyShelf";

            return View(shelvedBooks);
        }

        public IActionResult Book(long id)
        {
            Book shelvedBook = bookManager.GetShelvedBook(id);

            ViewData["ShelvedBook"] = shelvedBook;
            ViewData["Title"] = "My Shelved Book";

            return View(shelvedBook);
        }

        public IActionResult RateBooks()
        {
            Book neutralBook = bookManager.GetNeutralBook();

            ViewData["NeutralBook"] = neutralBook;
            ViewData["Title"] = "RateBooks";

            return View(neutralBook);
        }

        public IActionResult StoreBook(long currentBookId, bool isSaved)
        {
            if (isSaved)
            {
                bookManager.AddShelvedBook(currentBookId);
            }
            else
            {
                bookManager.AddRejectedBook(currentBookId);
            }
            bookManager.RemoveNeutralBook(currentBookId);
            return Redirect("/Home/RateBooks/");
        }

        public IActionResult RemoveBookFromShelf(long id)
        {
            bookManager.RemoveShelvedBook(id);
            return Redirect("/Home/Index");
        }

        // Remove all books from shelf and place all books back under neutral books
        public IActionResult ResetAllBooks()
        {
            bookManager.ResetAllBooks();
            return Redirect("/Home/RateBooks/");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
