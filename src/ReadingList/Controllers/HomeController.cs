using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Models;

namespace ReadingList.Controllers
{
    public class HomeController : Controller
    {
        //private readonly BookManager bookManager;
        private HttpHelper httpHelper;

        public HomeController()
        {
            //this.bookManager = bookManager;
            httpHelper = new HttpHelper("https://localhost:44390/");
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public async Task<IActionResult> Index()
        {
            List<Book> myShelf = await httpHelper.GetAsync<List<Book>>("api/books/shelvedBooks");
            //List<Book> myShelf = bookManager.GetShelvedBooks();

            ViewData["ShelvedBooks"] = myShelf;
            ViewData["Title"] = "MyShelf";

            return View(myShelf);
        }

        [Route("Home/Book")]
        public async Task<IActionResult> BookAsync(long id)
        {
            //Book shelvedBook = bookManager.GetShelvedBook(id);
            Book shelvedBook = await httpHelper.GetAsync<Book>($"api/books/shelvedBook/{id}");

            // if book has been read, get string stating when it was last finished
            if (shelvedBook.TimesRead > 0)
            {
                // DEMO 3: ReturnValue, Step Into Specific, Format Specifier dropdown
                ViewBag.TimeFinished = await httpHelper.GetAsStringAsync($"api/books/finishedToString/{Person.ToString("Leslie")}/{id}");
            }

            ViewData["ShelvedBook"] = shelvedBook;
            ViewData["Title"] = "My Shelved Book";

            return View(shelvedBook);
        }

        [Route("Home/RateBooks")]
        public async Task<IActionResult> RateBooks()
        {
            Book neutralBook = null;
            bool neutralIsEmpty = await httpHelper.GetAsync<bool>("api/books/neutralIsEmpty");
            if(!neutralIsEmpty)
            {
                //neutralBook = bookManager.GetNeutralBook();
                neutralBook = await httpHelper.GetAsync<Book>("api/books/neutralBook");

                ViewData["NeutralBook"] = neutralBook;
                ViewData["Title"] = "RateBooks";

                return View(neutralBook);
            }

            ViewData["NeutralBook"] = neutralBook;
            ViewData["Title"] = "RateBooks";

            return View();
        }

        [Route("Home/StoreBook/{currentBookId}/{isSaved}")]
        public async Task<IActionResult> StoreBookAsync(long currentBookId, bool isSaved)
        {
            if (isSaved)
            {
                //bookManager.AddShelvedBook(currentBookId);
                await httpHelper.PutAsync($"api/books/addShelved/{currentBookId}", currentBookId);
            }
            else
            {
                //bookManager.AddRejectedBook(currentBookId);
                await httpHelper.PutAsync($"api/books/addRejected/{currentBookId}", currentBookId);
            }
            //bookManager.RemoveNeutralBook(currentBookId);
            await httpHelper.PutAsync($"api/books/removeNeutral/{currentBookId}", currentBookId);

            return Redirect("/Home/RateBooks/");
        }

        [Route("Home/StoreAllBooks/{areSaved}")]
        public async Task<IActionResult> StoreAllBooksAsync(bool areSaved)
        {
            if (areSaved)
            {
                //bookManager.AddAllToShelf();
                await httpHelper.PutAsync("api/books/addAll", 1);
                return Redirect("/Home/Index");
            }
            else
            {
                //bookManager.AddAllToRejected();
                await httpHelper.PutAsync("api/books/rejectAll", 1);             
            }
            return Redirect("/Home/RateBooks/");
        }

        [Route("Home/RemoveBookFromShelf/{id}")]
        public async Task<IActionResult> RemoveBookFromShelfAsync(long id)
        {
            //bookManager.RemoveShelvedBook(id);
            await httpHelper.PutAsync($"api/books/removeShelved/{id}", id);
            return Redirect("/Home/Index");
        }

        // Remove all books from shelf and place all books back under neutral books
        [Route("Home/ResetAllBooks")]
        public async Task<IActionResult> ResetAllBooksAsync()
        {
            //bookManager.ResetAllBooks();
            await httpHelper.PutAsync("api/books/resetAll", 1);
            return Redirect("/Home/RateBooks/");
        }

        [Route("Home/FinishedBook/{id}")]
        public async Task<IActionResult> FinishedBookAsync(int id)
        {
            //bookManager.FinishedBook(id);
            await httpHelper.PutAsync($"api/books/finishedBook/{id}", id);
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
