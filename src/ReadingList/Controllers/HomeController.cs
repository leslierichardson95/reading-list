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

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            List<Book> myShelf = bookManager.GetShelvedBooks();

            ViewData["ShelvedBooks"] = myShelf;
            ViewData["Title"] = "MyShelf";

            return View(myShelf);
        }

        [Route("Home/Book")]
        public IActionResult Book(long id)
        {
            Book shelvedBook = bookManager.GetShelvedBook(id);

            // if book has been read, get string stating when it was last finished
            if (shelvedBook.TimesRead > 0)
            {
                // DEMO 3: ReturnValue, Step Into Specific, Format Specifier dropdown
                ViewBag.TimeFinished = bookManager.FinishedToString(Person.ToString("Leslie"), shelvedBook);
            }

            ViewData["ShelvedBook"] = shelvedBook;
            ViewData["Title"] = "My Shelved Book";

            return View(shelvedBook);
        }

        [Route("Home/RateBooks")]
        public IActionResult RateBooks()
        {
            Book neutralBook = null;
            if(!bookManager.NeutralIsEmpty())
            {
                neutralBook = bookManager.GetNeutralBook();

                ViewData["NeutralBook"] = neutralBook;
                ViewData["Title"] = "RateBooks";

                return View(neutralBook);
            }

            ViewData["NeutralBook"] = neutralBook;
            ViewData["Title"] = "RateBooks";

            return View();
        }

        [Route("Home/StoreBook/{currentBookId}/{isSaved}")]
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

        [Route("Home/StoreAllBooks/{areSaved}")]
        public IActionResult StoreAllBooks(bool areSaved)
        {
            if (areSaved)
            {
                bookManager.AddAllToShelf();
                return Redirect("/Home/Index");
            }
            else
            {
                bookManager.AddAllToRejected();
            }
            return Redirect("/Home/RateBooks/");
        }

        [Route("Home/RemoveBookFromShelf/{id}")]
        public IActionResult RemoveBookFromShelf(long id)
        {
            bookManager.RemoveShelvedBook(id);
            return Redirect("/Home/Index");
        }

        // Remove all books from shelf and place all books back under neutral books
        [Route("Home/ResetAllBooks")]
        public IActionResult ResetAllBooks()
        {
            bookManager.ResetAllBooks();
            return Redirect("/Home/RateBooks/");
        }

        [Route("Home/FinishedBook/{id}")]
        public IActionResult FinishedBook(int id)
        {
            bookManager.FinishedBook(id);
            return Redirect("/Home/Index");
        }

        [Route("Home/Privacy")]
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
