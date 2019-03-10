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
        public IActionResult Index()
        {
            List<ReadingList.Models.Book> shelvedBooks = BookManager.GetShelvedBooks();

            ViewData["ShelvedBooks"] = shelvedBooks;
            ViewData["Title"] = "MyShelf";

            return View(shelvedBooks);
        }

        public IActionResult Book(long id)
        {
            Book shelvedBook = BookManager.GetShelvedBook(id);

            ViewData["ShelvedBook"] = shelvedBook;
            ViewData["Title"] = "My Shelved Book";

            return View(shelvedBook);
        }

        public IActionResult RateBooks()
        {
            Book neutralBook = BookManager.GetNeutralBook();

            ViewData["NeutralBook"] = neutralBook;
            ViewData["Title"] = "RateBooks";

            return View(neutralBook);
        }

        public IActionResult StoreBook(long currentBookId, bool isSaved)
        {
            if (isSaved)
            {
                BookManager.AddShelvedBook(currentBookId);
            }
            else
            {
                BookManager.AddRejectedBook(currentBookId);
            }
            BookManager.RemoveNeutralBook(currentBookId);
            return Redirect("/Home/RateBooks/");
        }

        public IActionResult RemoveBookFromShelf(long id)
        {
            BookManager.RemoveShelvedBook(id);
            return Redirect("/Home/Index");
        }

        // Remove all books from shelf and place all books back under neutral books
        public IActionResult ResetAllBooks()
        {
            BookManager.ResetAllBooks();
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
